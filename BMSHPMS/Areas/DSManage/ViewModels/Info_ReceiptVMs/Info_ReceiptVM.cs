using BMSHPMS.DSManage.ViewModels.Common;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;


namespace BMSHPMS.DSManage.ViewModels.Info_ReceiptVMs
{
    public partial class Info_ReceiptVM : BaseCRUDVM<Info_Receipt>
    {

        public List<Info_Donor> DonorInfos { get; set; } = new();

        public List<Info_Longevity> LongevityInfos { get; set; } = new();

        public List<Info_Memorial> MemorialInfos { get; set; } = new();

        public List<ComboSelectListItem> AllOpt_DharmaServiceName { get; set; }

        [Display(Name = "總金額")]
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
                // 檢查提交的 收據號碼 是否改變， 如有改變， 檢查 數據庫中是否已存在 提交的 收據號碼
                if (Entity.ReceiptNumber.ToUpper() != old.ReceiptNumber.ToUpper())
                {
                    var receiptList = DC.Set<Info_Receipt>().Where(x => x.ReceiptNumber.ToUpper() == Entity.ReceiptNumber.ToUpper()).ToList();
                    if (receiptList != null && receiptList.Count > 0)
                    {
                        foreach (var receipt in receiptList)
                        {
                            if (receipt.ID != Entity.ID)
                            {
                                MSD.AddModelError("ReceiptNumber", $"收據號碼:{Entity.ReceiptNumber}已存在數據庫中.提交的收據ID:{Entity.ID},已存在的收據ID:{receipt.ID}");
                                return;
                            }
                        }
                    }
                }

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
            using var trans = DC.BeginTransaction();
            try
            {
                Info_ReceiptHelper.ReceiptMoveToDeleteTable(Wtm, Entity.ID);
                trans.Commit();
            }
            catch (Exception)
            {
                trans.Rollback();
                throw;
            }
        }

        public void InitialDetails()
        {
            InitListVM(ReceiptPageMode.Detials);

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

        #region FillData ListVM
        public FillData_DonorListVM FillData_DonorListVM { get; set; }
        public FillData_MemorialListVM FillData_MemorialListVM { get; set; }
        public FillData_LongevityListVM FillData_LongevityListVM { get; set; }

        public void InitListVM(ReceiptPageMode pageMode)
        {
            DonorInfos = DC.Set<Info_Donor>().Where(q => q.ReceiptID == Entity.ID).OrderBy(q => q.Sum).ThenBy(q => q.SerialCode).ToList();
            LongevityInfos = DC.Set<Info_Longevity>().Where(q => q.ReceiptID == Entity.ID).OrderBy(q => q.Sum).ThenBy(q => q.SerialCode).ToList();
            MemorialInfos = DC.Set<Info_Memorial>().Where(q => q.ReceiptID == Entity.ID).OrderBy(q => q.Sum).ThenBy(q => q.SerialCode).ToList();

            if (DonorInfos.Count > 0)
            {
                FillData_DonorListVM = new FillData_DonorListVM() { PageMode = pageMode };
                FillData_DonorListVM.Searcher.ReceiptID = Entity.ID;
                FillData_DonorListVM.CopyContext(this);
            }

            if (MemorialInfos.Count > 0)
            {
                FillData_MemorialListVM = new FillData_MemorialListVM() { PageMode = pageMode };
                FillData_MemorialListVM.Searcher.ReceiptID = Entity.ID;
                FillData_MemorialListVM.CopyContext(this);
            }

            if (LongevityInfos.Count > 0)
            {
                FillData_LongevityListVM = new FillData_LongevityListVM() { PageMode = pageMode };
                FillData_LongevityListVM.Searcher.ReceiptID = Entity.ID;
                FillData_LongevityListVM.CopyContext(this);
            }
        }
        #endregion
    }
}
