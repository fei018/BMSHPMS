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

            var donationproject = DC.Set<Opt_DonationProject>().AsNoTracking().CheckID(CreateVMEntity.SumSelectDonationID, x => x.ID).SingleOrDefault();
            if (donationproject == null)
            {
                MSD.AddModelError("Opt_DonationProject", $"{nameof(Opt_DonationProject)}.ID:{CreateVMEntity.SumSelectDonationID} is null in database.");
                return;
            }

            int nextNumber = donationproject.UsedNumber + 1;
            donationproject.UsedNumber = nextNumber;

            var serial = donationproject.SerialCode + nextNumber.ToString().PadLeft(4, '0');

            var newEntity = new Info_Longevity
            {
                Name = CreateVMEntity.Name,
                Sum = donationproject.Sum,
                SerialCode = serial,
                DSRemark = CreateVMEntity.DSRemark,
                DonationProjectId = donationproject.ID,
                DProjectSerial = donationproject.SerialCode,
                DProjectSerialNumber = nextNumber,

                ReceiptID = receipt.ID,
                CreateBy = LoginUserInfo.Name,
                CreateTime = DateTime.Now,
                UpdateBy = LoginUserInfo.Name,
                UpdateTime = DateTime.Now
            };

            using var dcTrans = DC.BeginTransaction();
            try
            {
                lock (DbTableLocker.T_Opt_DonationProject)
                {
                    DC.UpdateProperty(donationproject, x => x.UsedNumber);
                    DC.SaveChanges();
                }

                lock (DbTableLocker.T_Longevity)
                {
                    DC.Set<Info_Longevity>().Add(newEntity);
                    DC.SaveChanges();
                }

                dcTrans.Commit();
                CreateVMEntity.SerialCode = serial;
            }
            catch (Exception ex)
            {
                dcTrans.Rollback();
                MSD.AddModelError("BeginTransaction", "BeginTransaction: " + ex.Message);
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
        [Required(ErrorMessage = "收據號碼必填")]
        public string ReceiptNumber { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "姓名必填")]
        public string Name { get; set; }

        [Display(Name = "金額")]
        [Required(ErrorMessage = "金額必填")]
        public Guid? SumSelectDonationID { get; set; }

        [Display(Name = "延生編號")]
        public string SerialCode { get; set; }

        [Display(Name = "備註")]
        public string DSRemark { get; set; }

        /// <summary>
        /// 選擇法會
        /// </summary>
        [Display(Name = "選擇法會")]
        public string DharmaService { get; set; }


        public List<ComboSelectListItem> DharmaServiceSelectList { get; set; }


        protected override void InitVM()
        {
            DharmaServiceSelectList = DC.Set<Opt_DharmaService>().OrderBy(x => x.SerialCode).GetSelectListItems(Wtm, x => x.ServiceName, y => y.ID);
        }

        public dynamic GetDonationByDharmaServiceID(string id)
        {
            var list = DC.Set<Opt_DonationProject>().AsNoTracking()
                                                    .CheckID(id, x => x.DharmaServiceID)
                                                    .CheckEqual(DonationProjectOptions.Category.延生位, x => x.DonationCategory)
                                                    .OrderBy(x => x.SerialCode)
                                                    .Select(x => new { Text = x.Sum.ToString(), Value = x.ID })
                                                    .ToList();
            return list;
        }
    }
    #endregion
}
