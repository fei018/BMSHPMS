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
    public partial class Info_ReceiptListVM : BasePagedListVM<Info_Receipt_View, Info_ReceiptSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("Info_Receipt","FillDonationData","填寫","填寫資料", GridActionParameterTypesEnum.SingleId,"DSManage").SetShowInRow().SetHideOnToolBar().SetShowDialog().SetIsRedirect(),
                //this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800,dialogHeight:400),
                this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800, dialogHeight : 600),
                this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 1000, dialogHeight : 800).SetMax(),
                this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800, dialogHeight : 400),               
                //this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
                
            };
        }


        protected override IEnumerable<IGridColumn<Info_Receipt_View>> InitGridHeader()
        {
            return new List<GridColumn<Info_Receipt_View>>{
                this.MakeGridHeader(x => x.ReceiptDate,width:150).SetSort(),
                this.MakeGridHeader(x => x.ReceiptNumber,width:150).SetSort(),
                this.MakeGridHeader(x => x.DharmaServiceFullName,width:150),
                this.MakeGridHeader(x => x.CalculateSum,width:150).SetFormat(CalculateSumFormat).SetSort().SetShowTotal(),
                this.MakeGridHeader(x => x.ReceiptOwn).SetSort(),
                this.MakeGridHeader(x => x.ContactName).SetSort(),
                this.MakeGridHeader(x => x.ContactPhone).SetSort(),
                this.MakeGridHeader(x => x.DSRemark).SetSort(),
                this.MakeGridHeaderAction(width: 250).SetSort()
            };
        }

        private string CalculateSumFormat(Info_Receipt_View receipt, object val)
        {
            var vm = Wtm.CreateVM<Info_ReceiptVM>();
            var sum = vm.GetCalculateSum(receipt.ID);
            return sum.ToString();
        }

        public override IOrderedQueryable<Info_Receipt_View> GetSearchQuery()
        {
            var query = DC.Set<Info_Receipt>()
                            .AsNoTracking()
                            .CheckContain(Searcher.ReceiptNumber, x => x.ReceiptNumber)
                            .CheckContain(Searcher.ReceiptOwn, x => x.ReceiptOwn)
                            .CheckContain(Searcher.ContactName, x => x.ContactName)
                            .CheckContain(Searcher.ContactPhone, x => x.ContactPhone)
                            //.CheckEqual(Searcher.Sum, x => x.Sum)
                            .CheckEqual(Searcher.DharmaServiceName, x => x.DharmaServiceName)
                            .CheckEqual(Searcher.DharmaServiceYear, x => x.DharmaServiceYear)
                            .CheckContain(Searcher.DSRemark, x => x.DSRemark)
                            .CheckBetween(Searcher.ReceiptDate?.GetStartTime(), Searcher.ReceiptDate?.GetEndTime(), x => x.ReceiptDate,includeMax:false);

            var query1 = query.Select(x => new Info_Receipt_View
            {
                ID = x.ID,
                ReceiptNumber = x.ReceiptNumber,
                ReceiptOwn = x.ReceiptOwn,
                ContactName = x.ContactName,
                ContactPhone = x.ContactPhone,
                Sum = x.Sum,
                DSRemark = x.DSRemark,
                ReceiptDate = x.ReceiptDate,
                DharmaServiceFullName = x.DharmaServiceFullName,
                UpdateTime = x.UpdateTime,
                CreateBy = x.CreateBy,
                CreateTime = x.CreateTime,
            })
            .OrderByDescending(x => x.ReceiptDate)
            .ThenBy(x => x.ReceiptNumber);

            return query1;
        }

        #region ExportExcel()
        public async Task<byte[]> ExportExcel()
        {
            List<Info_Receipt> receiptList;

            if (this.Ids == null || this.Ids.Count <= 0)
            {
                receiptList = await GetSearchQuery().ToListAsync<Info_Receipt>();
            }
            else
            {
                receiptList = DC.Set<Info_Receipt>().AsNoTracking().CheckIDs(this.Ids).ToList();
            }

            if (receiptList == null || receiptList.Count <= 0)
            {
                throw new Exception("查詢數據是空.");
            }

            foreach (var r in receiptList)
            {
                r.Info_Donors = DC.Set<Info_Donor>().AsNoTracking().Where(x => x.ReceiptID == r.ID).ToList();
                r.Info_Longevitys = DC.Set<Info_Longevity>().AsNoTracking().Where(x => x.ReceiptID == r.ID).ToList();
                r.Info_Memorials = DC.Set<Info_Memorial>().AsNoTracking().Where(x => x.ReceiptID == r.ID).ToList();
            }

            return await ReceiptExcelVM.ExportExcelAsBytes(receiptList);
        }
        #endregion

    }


    #region public class Info_Receipt_View : Info_Receipt
    public class Info_Receipt_View : Info_Receipt
    {
        [Display(Name = "法會")]
        public new string DharmaServiceFullName { get; set; }

        [Display(Name = "合計金額")]
        public int CalculateSum { get; set; }
    }
    #endregion

}
