using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DharmaService;
using BMSHPMS.DSManage.ViewModels.Common;


namespace BMSHPMS.DSManage.ViewModels.Info_LongevityVMs
{
    public partial class Info_LongevityListVM : BasePagedListVM<Info_Longevity_View, Info_LongevitySearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800, dialogHeight : 600),
                this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800, dialogHeight : 400),               
                //this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
                this.MakeAction("Info_Longevity","ExportExcelTemplate","匯出Excel範本","匯出Excel範本", GridActionParameterTypesEnum.MultiIds,"DSManage",dialogWidth:800,dialogHeight:600),
            };
        }


        protected override IEnumerable<IGridColumn<Info_Longevity_View>> InitGridHeader()
        {
            return new List<GridColumn<Info_Longevity_View>>{
                this.MakeGridHeader(x => x.ReceiptDate_view,width:110).SetSort(),
                this.MakeGridHeader(x => x.DharmaServiceFullName,width:150).SetSort(),
                this.MakeGridHeader(x => x.ReceiptNumber_view,width:120).SetSort(),
                this.MakeGridHeader(x => x.SerialCode, width : 110).SetSort(),
                this.MakeGridHeader(x => x.Sum,width:80).SetSort(),
                this.MakeGridHeader(x => x.Name).SetSort(),                             
                this.MakeGridHeader(x => x.DSRemark),               
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Info_Longevity_View> GetSearchQuery()
        {
            var serial = new ListVMHelper().GetQuerySerialCodes(Searcher.SerialCode, Searcher.SerialCodeEnd);

            var query = DC.Set<Info_Longevity>()
                .CheckContain(Searcher.Name, x => x.Name)
                .CheckEqual(Searcher.Sum, x => x.Sum)
                //.CheckContain(Searcher.SerialCode, x => x.SerialCode)
                .CheckContain(serial, x => x.SerialCode)
                .CheckContain(Searcher.ReceiptNumber, x => x.Receipt.ReceiptNumber)
                .CheckBetween(Searcher.ReceiptDate?.GetStartTime(), Searcher.ReceiptDate?.GetEndTime(), x => x.Receipt.ReceiptDate)
                .CheckEqual(Searcher.DharmaServiceName, x => x.Receipt.DharmaServiceName)
                .CheckEqual(Searcher.DharmaServiceYear, x => x.Receipt.DharmaServiceYear);

            //if (Searcher.ReceiptDate.HasValue)
            //{
            //    query = query.Where(x => DateTime.Compare(Searcher.ReceiptDate.Value.Date, x.Receipt.ReceiptDate.Value.Date) == 0);
            //}

            var query1 = query.Select(x => new Info_Longevity_View
            {
                ID = x.ID,
                Name = x.Name,
                Sum = x.Sum,
                SerialCode = x.SerialCode,
                DSRemark = x.DSRemark,
                ReceiptNumber_view = x.Receipt.ReceiptNumber,
                ReceiptDate_view = x.Receipt.ReceiptDate.Value,
                ReceiptUpdateTime_view = x.Receipt.UpdateTime.Value,
                DharmaServiceFullName = x.Receipt.DharmaServiceFullName,
                UpdateTime = x.UpdateTime,
            })
            .OrderBy(x=>x.SerialCode)
            .OrderByDescending(x => x.UpdateTime);

            return query1;
        }

    }

    public class Info_Longevity_View : Info_Longevity{

        [Display(Name = "收據號碼")]
        public String ReceiptNumber_view { get; set; }

        [Display(Name = "收據日期")]
        public DateTime ReceiptDate_view { get; set; }

        [Display(Name = "收據更新日期")]
        public DateTime ReceiptUpdateTime_view { get; set; }

        [Display(Name = "法會")]
        public string DharmaServiceFullName { get; set; }
    }
}
