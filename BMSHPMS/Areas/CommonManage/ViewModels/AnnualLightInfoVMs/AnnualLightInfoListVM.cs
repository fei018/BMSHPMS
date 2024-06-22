using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.CommonDService;


namespace BMSHPMS.CommonManage.ViewModels.AnnualLightInfoVMs
{
    public partial class AnnualLightInfoListVM : BasePagedListVM<AnnualLightInfo_View, AnnualLightInfoSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("AnnualLightInfo", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"CommonManage", dialogWidth: 800),
                this.MakeStandardAction("AnnualLightInfo", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("AnnualLightInfo", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("AnnualLightInfo", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("AnnualLightInfo", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("AnnualLightInfo", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("AnnualLightInfo", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("AnnualLightInfo", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "CommonManage"),
            };
        }


        protected override IEnumerable<IGridColumn<AnnualLightInfo_View>> InitGridHeader()
        {
            return new List<GridColumn<AnnualLightInfo_View>>{
                this.MakeGridHeader(x=>x.ReceiptDate_view,width:200),
                this.MakeGridHeader(x=>x.ReceiptNumber_view,width:200),
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Sum),
                this.MakeGridHeader(x => x.Count),
                this.MakeGridHeader(x => x.WishNumber),
                this.MakeGridHeader(x => x.DonateLightMode),
                this.MakeGridHeader(x => x.ContactAddress),
                this.MakeGridHeader(x => x.Phone),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<AnnualLightInfo_View> GetSearchQuery()
        {
            var query = DC.Set<AnnualLightInfo>()
                .CheckContain(Searcher.Name, x => x.Name)
                .CheckEqual(Searcher.CommonReceiptNumber, x => x.CommonReceipt.ReceiptNumber)
                .Select(x => new AnnualLightInfo_View
                {
                    ID = x.ID,
                    Name = x.Name,
                    Sum = x.Sum,
                    Count = x.Count,
                    WishNumber = x.WishNumber,
                    DonateLightMode = x.DonateLightMode,
                    ContactAddress = x.ContactAddress,
                    Phone = x.Phone,
                    ReceiptNumber_view = x.CommonReceipt.ReceiptNumber,
                    ReceiptDate_view = x.CommonReceipt.ReceiptDate,
                })
                .OrderByDescending(x => x.ReceiptDate_view);
            return query;
        }

    }

    public class AnnualLightInfo_View : AnnualLightInfo
    {
        [Display(Name = "收據號碼")]
        public String ReceiptNumber_view { get; set; }

        [Display(Name = "收據日期")]
        public DateTime? ReceiptDate_view { get; set; }
    }
}
