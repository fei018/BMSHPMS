using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using BMSHPMS.DSManage.ViewModels.Info_LongevityVMs;
using BMSHPMS.Helper;


namespace BMSHPMS.DSManage.ViewModels.Info_MemorialVMs
{
    public partial class Info_MemorialVM : BaseCRUDVM<Info_Memorial>
    {
        public InfoMemorialCreateVM CreateVMEntity { get; set; }


        public Info_MemorialVM()
        {
            SetInclude(x => x.Receipt);
        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {
            CreateVMEntity.TrimAsString();

            var receipt = DC.Set<Info_Receipt>().Include(x => x.Info_Memorials).AsNoTracking().Where(x => x.ReceiptNumber.ToLower() == CreateVMEntity.ReceiptNumber.ToLower()).FirstOrDefault();

            if (receipt == null)
            {
                MSD.AddModelError("收據號碼不存在", "收據號碼不存在");
                return;
            }

            // 收據的 延生集合 里的 附薦編號已存在
            if (receipt.Info_Memorials != null)
            {
                if (receipt.Info_Memorials.Any(x => x.SerialCode.ToLower() == CreateVMEntity.SerialCode.ToLower()))
                {
                    MSD.AddModelError("附薦編號已存在", "附薦編號已存在");
                    return;
                }
            }

            var newEntity = new Info_Memorial
            {
                BenefactorName = CreateVMEntity.BenefactorName,
                DeceasedName_1 = CreateVMEntity.DeceasedName_1,
                DeceasedName_2 = CreateVMEntity.DeceasedName_2,
                DeceasedName_3 = CreateVMEntity.DeceasedName_3,
                DSRemark = CreateVMEntity.DSRemark,
                Sum = CreateVMEntity.Sum,
                SerialCode = CreateVMEntity.SerialCode,

                ReceiptID = receipt.ID,
                CreateBy = LoginUserInfo.Name,
                CreateTime = DateTime.Now,
                UpdateBy = LoginUserInfo.Name,
                UpdateTime = DateTime.Now
            };

            lock (DbTableLocker.T_Memorial)
            {
                DC.Set<Info_Memorial>().Add(newEntity);
                DC.SaveChanges();
            }
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            var old = DC.Set<Info_Memorial>().Find(Entity.ID);
            if (old != null)
            {
                old.BenefactorName = Entity.BenefactorName;
                old.DeceasedName_1 = Entity.DeceasedName_1;
                old.DeceasedName_2 = Entity.DeceasedName_2;
                old.DeceasedName_3 = Entity.DeceasedName_3;
                old.DSRemark = Entity.DSRemark;
                if(Entity.Sum.HasValue) old.Sum = Entity.Sum.Value;

                old.UpdateBy = LoginUserInfo.Name;
                old.UpdateTime = DateTime.Now;
                DC.UpdateEntity(old);
                DC.SaveChanges();
            }
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }

    #region InfoMemorialCreateVM
    public class InfoMemorialCreateVM : BaseVM
    {
        [Display(Name = "收據號碼")]
        [Required(ErrorMessage = "收據號碼必填")]
        public string ReceiptNumber { get; set; }

        [Display(Name = "附薦編號")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string SerialCode { get; set; }

        [Display(Name = "陽居姓名")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string BenefactorName { get; set; }

        [Display(Name = "附薦名稱_1")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string DeceasedName_1 { get; set; }

        [Display(Name = "附薦名稱_2")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string DeceasedName_2 { get; set; }

        [Display(Name = "附薦名稱_3")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string DeceasedName_3 { get; set; }

        [Display(Name = "金額")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? Sum { get; set; }

        [Display(Name = "備註")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string DSRemark { get; set; }
    }
    #endregion
}
