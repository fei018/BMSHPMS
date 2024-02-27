using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DharmaService;
using NetBox.Extensions;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using BMSHPMS.Helper;
using BMSHPMS.Areas.DSManage.ViewModels.Common;



namespace BMSHPMS.DSManage.ViewModels.Info_ReceiptVMs
{
    public partial class Info_ReceiptListVM : BasePagedListVM<Info_Receipt_View, Info_ReceiptSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800,dialogHeight:400),
                this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800, dialogHeight : 400),
                this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800, dialogHeight : 600),
                this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800, dialogHeight : 400),               
                //this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
                this.MakeAction("Info_Receipt","ExportExcelToday","匯出今日","匯出今日收據", GridActionParameterTypesEnum.NoId,"DSManage").SetIsDownload(),
            };
        }


        protected override IEnumerable<IGridColumn<Info_Receipt_View>> InitGridHeader()
        {
            return new List<GridColumn<Info_Receipt_View>>{
                this.MakeGridHeader(x => x.ReceiptDate,width:150).SetSort(),
                this.MakeGridHeader(x => x.ReceiptNumber,width:150).SetSort(),                
                this.MakeGridHeader(x => x.DharmaServiceFullName,width:150).SetSort(),
                this.MakeGridHeader(x => x.Sum,width:150).SetSort(),
                this.MakeGridHeader(x => x.ReceiptOwn).SetSort(),
                this.MakeGridHeader(x => x.ContactName).SetSort(),
                this.MakeGridHeader(x => x.ContactPhone).SetSort(),
                this.MakeGridHeader(x => x.DSRemark).SetSort(),
                this.MakeGridHeaderAction(width: 200).SetSort()
            };
        }

        public override IOrderedQueryable<Info_Receipt_View> GetSearchQuery()
        {
            var query = DC.Set<Info_Receipt>()
                            .CheckContain(Searcher.ReceiptNumber, x => x.ReceiptNumber)
                            .CheckContain(Searcher.ReceiptOwn, x => x.ReceiptOwn)
                            .CheckContain(Searcher.ContactName, x => x.ContactName)
                            .CheckContain(Searcher.ContactPhone, x => x.ContactPhone)
                            .CheckEqual(Searcher.Sum, x => x.Sum)
                            .CheckEqual(Searcher.DharmaServiceName, x => x.DharmaServiceName)
                            .CheckEqual(Searcher.DharmaServiceYear, x => x.DharmaServiceYear);

            //if (Searcher.DharmaServiceYear.HasValue)
            //{
            //    query = query.Where(x => x.DharmaServiceYear.HasValue && x.DharmaServiceYear.Value.Equals(Searcher.DharmaServiceYear.Value));
            //}

            if (Searcher.ReceiptDate.HasValue)
            {
                query = query.Where(x => DateTime.Compare(Searcher.ReceiptDate.Value.Date, x.ReceiptDate.Value.Date) == 0);
            }

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
            .OrderByDescending(x => x.UpdateTime)
            .ThenByDescending(x => x.ReceiptNumber);

            return query1;
        }

        public async Task<byte[]> ExportExcel()
        {
            List<Info_Receipt> receiptList;

            if (this.Ids == null || this.Ids.Count <= 0)
            {
                receiptList = await GetSearchQuery().ToListAsync<Info_Receipt>();
            }
            else
            {
                receiptList = DC.Set<Info_Receipt>().CheckIDs(this.Ids).ToList();
            }

            if (receiptList == null || receiptList.Count <= 0)
            {
                throw new Exception("查詢數據是空.");
            }

            foreach (var r in receiptList)
            {
                r.Info_Donors = DC.Set<Info_Donor>().Where(x => x.ReceiptID == r.ID).ToList();
                r.Info_Longevitys = DC.Set<Info_Longevity>().Where(x => x.ReceiptID == r.ID).ToList();
                r.Info_Memorials = DC.Set<Info_Memorial>().Where(x => x.ReceiptID == r.ID).ToList();
            }

            return await ReceiptExcelVM.ExportExcelAsBytes(receiptList);
        }

    }


    #region public class Info_Receipt_View : Info_Receipt
    public class Info_Receipt_View : Info_Receipt
    {
        [Display(Name = "法會")]
        public new string DharmaServiceFullName { get; set; }
    }
    #endregion

}
