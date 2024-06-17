using BMSHPMS.Helper;
using BMSHPMS.Models.DharmaService;
using BMSHPMS.Models.DharmaServiceExtention;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;


namespace BMSHPMS.DSManage.ViewModels.Info_LongevityVMs
{
    public partial class Info_LongevityVM : BaseCRUDVM<Info_Longevity>
    {

        public InfoLongevityCreateVM CreateVMEntity { get; set; }

        public Info_LongevityVM()
        {
            SetInclude(x => x.Receipt);

        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {
            CreateVMEntity.ReceiptNumber?.Trim();
            CreateVMEntity.SerialCode?.Trim();

            var receipt = DC.Set<Info_Receipt>().Include(x => x.Info_Longevitys).AsNoTracking().Where(x => x.ReceiptNumber.ToLower() == CreateVMEntity.ReceiptNumber.ToLower()).FirstOrDefault();

            if (receipt == null)
            {
                MSD.AddModelError("收據號碼不存在", "收據號碼不存在");
                return;
            }

            var donationproject = new Opt_DonationProject();
            if (receipt.DharmaServiceId != null)
            {
                donationproject = DC.Set<Opt_DonationProject>().AsNoTracking().CheckID(receipt.DharmaServiceId, x => x.DharmaServiceID).SingleOrDefault();
            }
            else
            {
                var dservice = DC.Set<Opt_DharmaService>().AsNoTracking().CheckEqual(receipt.DharmaServiceName, x => x.ServiceName).SingleOrDefault();
                if (dservice != null)
                {
                    donationproject = DC.Set<Opt_DonationProject>().AsNoTracking()
                                        .CheckID(dservice.ID, x => x.DharmaServiceID)
                                        .CheckEqual(DonationProjectOptions.Category.延生位, x => x.DonationCategory)
                                        .CheckEqual(CreateVMEntity.Sum, x => x.Sum)
                                        .FirstOrDefault();
                }
            }

            var newEntity = new Info_Longevity
            {
                Name = CreateVMEntity.Name,
                Sum = CreateVMEntity.Sum,
                SerialCode = CreateVMEntity.SerialCode,
                DSRemark = CreateVMEntity.DSRemark,
                DonationProjectId = donationproject?.ID,
                DProjectSerial = donationproject?.SerialCode,
                DProjectSerialNumber = donationproject?.UsedNumber,

                ReceiptID = receipt.ID,
                CreateBy = LoginUserInfo.Name,
                CreateTime = DateTime.Now,
                UpdateBy = LoginUserInfo.Name,
                UpdateTime = DateTime.Now
            };

            lock (DbTableLocker.T_Longevity)
            {
                DC.Set<Info_Longevity>().Add(newEntity);
                DC.SaveChanges();
            }
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            var old = DC.Set<Info_Longevity>().Find(Entity.ID);
            if (old != null)
            {

                old.Name = Entity.Name;
                old.DSRemark = Entity.DSRemark;
                if (Entity.Sum.HasValue) old.Sum = Entity.Sum.Value;

                old.UpdateBy = LoginUserInfo.Name;
                old.UpdateTime = DateTime.Now;
                DC.UpdateEntity(old);
                DC.SaveChanges();
            }
        }

        public override void DoDelete()
        {
            DC.Set<Info_Longevity>().Remove(Entity);
            DC.SaveChanges();
        }
    }

    #region create new class       
    public class InfoLongevityCreateVM : BaseVM
    {
        [Display(Name = "收據號碼")]
        [Required(ErrorMessage = "{0}必填")]
        public string ReceiptNumber { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "金額")]
        [Required(ErrorMessage = "{0}必填")]
        public int? Sum { get; set; }

        [Display(Name = "延生編號")]
        [Required(ErrorMessage = "{0}必填")]
        public string SerialCode { get; set; }

        [Display(Name = "備註")]
        public string DSRemark { get; set; }

    }
    #endregion
}
