using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DabeiDharmaService;


namespace BMSHPMS.CommonManage.ViewModels.DabeiDonorVMs
{
    public partial class DabeiDonorListVM : BasePagedListVM<DabeiDonor_View, DabeiDonorSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("DabeiDonor", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"CommonManage", dialogWidth: 800),
                this.MakeStandardAction("DabeiDonor", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("DabeiDonor", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("DabeiDonor", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("DabeiDonor", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("DabeiDonor", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("DabeiDonor", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("DabeiDonor", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "CommonManage"),
            };
        }


        protected override IEnumerable<IGridColumn<DabeiDonor_View>> InitGridHeader()
        {
            return new List<GridColumn<DabeiDonor_View>>{
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Sum),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DabeiDonor_View> GetSearchQuery()
        {
            var query = DC.Set<DabeiDonor>()
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckEqual(Searcher.Sum, x=>x.Sum)
                .Select(x => new DabeiDonor_View
                {
				    ID = x.ID,
                    Name = x.Name,
                    Sum = x.Sum,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class DabeiDonor_View : DabeiDonor{

    }
}
