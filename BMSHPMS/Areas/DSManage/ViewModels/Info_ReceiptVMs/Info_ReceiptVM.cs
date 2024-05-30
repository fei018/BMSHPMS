using BMSHPMS.DSManage.ViewModels.Common;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;


namespace BMSHPMS.DSManage.ViewModels.Info_ReceiptVMs
{
    public partial class Info_ReceiptVM : BaseCRUDVM<Info_Receipt>
    {

        public List<Info_Donor> DonorInfos { get; set; }

        public List<Info_Longevity> LongevityInfos { get; set; }

        public List<Info_Memorial> MemorialInfos { get; set; }

        public List<ComboSelectListItem> AllOpt_DharmaServiceName { get; set; }

        [Display(Name ="總金額")]
        public int? TotalSum { get; set; }

        public Info_ReceiptVM()
        {
        }

        protected override void InitVM()
        {
            AllOpt_DharmaServiceName = DC.Set<Opt_DharmaService>().GetSelectListItems(Wtm, x => x.ServiceName, y => y.ServiceName);
        }

        public override void DoAdd()
        {
            DC.Set<Info_Receipt>().Add(Entity);
            DC.SaveChanges();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            var old = DC.Set<Info_Receipt>().Find(Entity.ID);
            if (old != null)
            {
                if (Entity.DharmaServiceYear.HasValue) old.DharmaServiceYear = Entity.DharmaServiceYear;
                old.DharmaServiceName = Entity.DharmaServiceName;
                old.ReceiptNumber = Entity.ReceiptNumber;
                if (Entity.Sum.HasValue) old.Sum = Entity.Sum;
                old.ReceiptOwn = Entity.ReceiptOwn;
                old.ContactName = Entity.ContactName;
                old.ContactPhone = Entity.ContactPhone;
                if (Entity.ReceiptDate.HasValue) old.ReceiptDate = Entity.ReceiptDate;
                old.DSRemark = Entity.DSRemark;

                old.UpdateBy = LoginUserInfo.Name;
                old.UpdateTime = DateTime.Now;
                old.ReceiptDate = Entity.ReceiptDate;

                DC.UpdateEntity(old);
                DC.SaveChanges();
            }
        }

        public override void DoDelete()
        {
            Info_ReceiptHelper.ReceiptMoveToDeleteTable(Wtm, Entity.ID);
        }

        public async Task InitialDetails()
        {
            DonorInfos = await DC.Set<Info_Donor>().Where(q => q.ReceiptID == Entity.ID).OrderBy(q => q.Sum).ThenBy(q => q.SerialCode).ToListAsync();
            LongevityInfos = await DC.Set<Info_Longevity>().Where(q => q.ReceiptID == Entity.ID).OrderBy(q => q.Sum).ThenBy(q => q.SerialCode).ToListAsync();
            MemorialInfos = await DC.Set<Info_Memorial>().Where(q => q.ReceiptID == Entity.ID).OrderBy(q => q.Sum).ThenBy(q => q.SerialCode).ToListAsync();

            TotalSum = GetCalculateSum(Entity.ID);
        }

        #region GetCalculateSum
        /// <summary>
        /// 計算收據縂金額
        /// </summary>
        /// <param name="receiptID"></param>
        /// <returns></returns>
        public int GetCalculateSum(Guid receiptID)
        {
            var donors = DC.Set<Info_Donor>().AsNoTracking().Where(x => x.ReceiptID == receiptID).Select(x => x.Sum).ToList();
            var mems = DC.Set<Info_Memorial>().AsNoTracking().Where(x => x.ReceiptID == receiptID).Select(x => x.Sum).ToList();
            var longs = DC.Set<Info_Longevity>().AsNoTracking().Where(x => x.ReceiptID == receiptID).Select(x => x.Sum).ToList();

            int sum = 0;
            foreach (var item in donors)
            {
                if (item.HasValue)
                {
                    sum += item.Value;
                }
            }
            foreach (var item in mems)
            {
                if (item.HasValue)
                {
                    sum += item.Value;
                }
            }
            foreach (var item in longs)
            {
                if (item.HasValue)
                {
                    sum += item.Value;
                }
            }
            return sum;
        }
        #endregion
    }
}
