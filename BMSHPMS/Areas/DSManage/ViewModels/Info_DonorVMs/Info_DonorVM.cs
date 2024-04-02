using BMSHPMS.Helper;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WalkingTec.Mvvm.Core;


namespace BMSHPMS.DSManage.ViewModels.Info_DonorVMs
{
    public partial class Info_DonorVM : BaseCRUDVM<Info_Donor>
    {

        public InfoDonorCreateVM CreateVMEntity { get; set; }

        public Info_DonorVM()
        {
            SetInclude(x => x.Receipt);
        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {
            CreateVMEntity.TrimAsString();

            var receipt = DC.Set<Info_Receipt>().Include(x => x.Info_Donors).AsNoTracking().Where(x => x.ReceiptNumber.ToLower() == CreateVMEntity.ReceiptNumber.ToLower()).FirstOrDefault();

            if (receipt == null)
            {
                MSD.AddModelError("收據號碼不存在", "收據號碼不存在");
                return;
            }

            // 收據的 功德主集合 里的 功德主編號已存在
            if (receipt.Info_Donors != null)
            {
                if (receipt.Info_Donors.Any(x => x.SerialCode.ToLower() == CreateVMEntity.SerialCode.ToLower()))
                {
                    MSD.AddModelError("功德主編號已存在", "功德主編號已存在");
                    return;
                }
            }

            var newEntity = new Info_Donor
            {
                BenefactorName = CreateVMEntity.BenefactorName,
                DeceasedName_1 = CreateVMEntity.DeceasedName_1,
                DeceasedName_2 = CreateVMEntity.DeceasedName_2,
                DeceasedName_3 = CreateVMEntity.DeceasedName_3,
                LongevityName = CreateVMEntity.LongevityName,
                DSRemark = CreateVMEntity.DSRemark,
                Sum = CreateVMEntity.Sum,
                SerialCode = CreateVMEntity.SerialCode,

                ReceiptID = receipt.ID,
                CreateBy = LoginUserInfo.Name,
                CreateTime = DateTime.Now,
                UpdateBy = LoginUserInfo.Name,
                UpdateTime = DateTime.Now
            };

            lock (DbTableLocker.T_Donor)
            {
                DC.Set<Info_Donor>().Add(newEntity);
                DC.SaveChanges();
            }
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            var old = DC.Set<Info_Donor>().Find(Entity.ID);
            if (old != null)
            {
                old.BenefactorName = Entity.BenefactorName;
                old.DeceasedName_1 = Entity.DeceasedName_1;
                old.DeceasedName_2 = Entity.DeceasedName_2;
                old.DeceasedName_3 = Entity.DeceasedName_3;
                old.LongevityName = Entity.LongevityName;
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
            DC.Set<Info_Donor>().Remove(Entity);
            DC.SaveChanges();
        }
    }

    #region InfoDonorCreateVM
    public class InfoDonorCreateVM : BaseVM
    {
        [Display(Name = "收據號碼")]
        [Required(ErrorMessage = "收據號碼必填")]
        public string ReceiptNumber { get; set; }

        [Display(Name = "延生位姓名")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string LongevityName { get; set; }

        [Display(Name = "附薦名稱_1")]
        public string DeceasedName_1 { get; set; }

        [Display(Name = "附薦名稱_2")]
        public string DeceasedName_2 { get; set; }

        [Display(Name = "附薦名稱_3")]
        public string DeceasedName_3 { get; set; }

        [Display(Name = "陽居姓名")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string BenefactorName { get; set; }

        [Display(Name = "金額")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? Sum { get; set; }

        [Display(Name = "功德主編號")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string SerialCode { get; set; }

        [Display(Name = "備註")]
        public string DSRemark { get; set; }
    }
    #endregion
}
