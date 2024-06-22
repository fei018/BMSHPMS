using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.CommonDService;


namespace BMSHPMS.CommonManage.ViewModels.AnnualDabeiInfoVMs
{
    public partial class AnnualDabeiInfoListVM : BasePagedListVM<AnnualDabeiInfo_View, AnnualDabeiInfoSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("AnnualDabeiInfo", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"CommonManage", dialogWidth: 800),
                this.MakeStandardAction("AnnualDabeiInfo", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("AnnualDabeiInfo", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("AnnualDabeiInfo", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("AnnualDabeiInfo", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("AnnualDabeiInfo", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("AnnualDabeiInfo", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("AnnualDabeiInfo", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "CommonManage"),
            };
        }


        protected override IEnumerable<IGridColumn<AnnualDabeiInfo_View>> InitGridHeader()
        {
            return new List<GridColumn<AnnualDabeiInfo_View>>{
                this.MakeGridHeader(x=>x.ReceiptDate_view,width:200),
                this.MakeGridHeader(x=>x.ReceiptNumber_view,width:200),
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Sum),
                this.MakeGridHeader(x => x.ReceiptNumber_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<AnnualDabeiInfo_View> GetSearchQuery()
        {
            var query = DC.Set<AnnualDabeiInfo>()
                .CheckContain(Searcher.Name, x => x.Name)
                .CheckEqual(Searcher.CommonReceiptNumber, x => x.CommonReceipt.ReceiptNumber)
                .Select(x => new AnnualDabeiInfo_View
                {
                    ID = x.ID,
                    Name = x.Name,
                    Sum = x.Sum,
                    ReceiptNumber_view = x.CommonReceipt.ReceiptNumber,
                    ReceiptDate_view = x.CommonReceipt.ReceiptDate,
                })
                .OrderByDescending(x => x.ReceiptDate_view);
            return query;
        }

    }

    public class AnnualDabeiInfo_View : AnnualDabeiInfo
    {
        [Display(Name = "收據號碼")]
        public String ReceiptNumber_view { get; set; }

        [Display(Name = "收據日期")]
        public DateTime? ReceiptDate_view { get; set; }
    }
}
