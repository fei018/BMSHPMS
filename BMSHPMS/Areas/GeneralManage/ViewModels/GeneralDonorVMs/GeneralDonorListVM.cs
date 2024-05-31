using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.GeneralDharmaService;


namespace BMSHPMS.GeneralManage.ViewModels.GeneralDonorVMs
{
    public partial class GeneralDonorListVM : BasePagedListVM<GeneralDonor_View, GeneralDonorSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("GeneralDonor", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"GeneralManage", dialogWidth: 800),
                this.MakeStandardAction("GeneralDonor", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "GeneralManage", dialogWidth: 800),
                this.MakeStandardAction("GeneralDonor", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "GeneralManage", dialogWidth: 800),
                this.MakeStandardAction("GeneralDonor", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "GeneralManage", dialogWidth: 800),
                this.MakeStandardAction("GeneralDonor", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "GeneralManage", dialogWidth: 800),
                this.MakeStandardAction("GeneralDonor", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "GeneralManage", dialogWidth: 800),
                this.MakeStandardAction("GeneralDonor", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "GeneralManage", dialogWidth: 800),
                this.MakeStandardAction("GeneralDonor", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "GeneralManage"),
            };
        }


        protected override IEnumerable<IGridColumn<GeneralDonor_View>> InitGridHeader()
        {
            return new List<GridColumn<GeneralDonor_View>>{
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Sum),
                this.MakeGridHeader(x => x.GeneralRemark),
                this.MakeGridHeader(x => x.ReceiptNumber_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<GeneralDonor_View> GetSearchQuery()
        {
            var query = DC.Set<GeneralDonor>()
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckEqual(Searcher.Sum, x=>x.Sum)
                .CheckContain(Searcher.GeneralRemark, x=>x.GeneralRemark)
                .Select(x => new GeneralDonor_View
                {
				    ID = x.ID,
                    Name = x.Name,
                    Sum = x.Sum,
                    GeneralRemark = x.GeneralRemark,
                    ReceiptNumber_view = x.Receipt.ReceiptNumber,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class GeneralDonor_View : GeneralDonor{
        [Display(Name = "收據號碼")]
        public String ReceiptNumber_view { get; set; }

    }
}
