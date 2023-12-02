using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.Areas.BackStage.ViewModels.T_LongevityPlaqueVMs
{
    public partial class T_LongevityPlaqueListVM : BasePagedListVM<T_LongevityPlaque_View, T_LongevityPlaqueSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("T_LongevityPlaque", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"BackStage", dialogWidth: 800),
                this.MakeStandardAction("T_LongevityPlaque", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "BackStage", dialogWidth: 800),
                this.MakeStandardAction("T_LongevityPlaque", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "BackStage", dialogWidth: 800),
                this.MakeStandardAction("T_LongevityPlaque", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "BackStage", dialogWidth: 800),
                this.MakeStandardAction("T_LongevityPlaque", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "BackStage", dialogWidth: 800),
                this.MakeStandardAction("T_LongevityPlaque", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "BackStage", dialogWidth: 800),
                this.MakeStandardAction("T_LongevityPlaque", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "BackStage", dialogWidth: 800),
                this.MakeStandardAction("T_LongevityPlaque", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "BackStage"),
            };
        }


        protected override IEnumerable<IGridColumn<T_LongevityPlaque_View>> InitGridHeader()
        {
            return new List<GridColumn<T_LongevityPlaque_View>>{
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Sum),
                this.MakeGridHeader(x => x.Serial),
                this.MakeGridHeader(x => x.PRemark),
                this.MakeGridHeader(x => x.ReceiptNumber_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<T_LongevityPlaque_View> GetSearchQuery()
        {
            var query = DC.Set<T_LongevityPlaque>()
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckEqual(Searcher.Sum, x=>x.Sum)
                .CheckContain(Searcher.Serial, x=>x.Serial)
                .CheckContain(Searcher.Remark, x=>x.PRemark)
                .CheckEqual(Searcher.ReceiptID, x=>x.ReceiptID)
                .Select(x => new T_LongevityPlaque_View
                {
				    ID = x.ID,
                    Name = x.Name,
                    Sum = x.Sum,
                    Serial = x.Serial,
                    PRemark = x.PRemark,
                    ReceiptNumber_view = x.Receipt.ReceiptNumber,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class T_LongevityPlaque_View : T_LongevityPlaque{
        [Display(Name = "收據號碼")]
        public String ReceiptNumber_view { get; set; }

    }
}
