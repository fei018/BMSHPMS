using BMSHPMS.Models.GeneralDharmaService;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;


namespace BMSHPMS.GeneralManage.ViewModels.GeneralDonationCategoryVMs
{
    public partial class GeneralDonationCategoryListVM : BasePagedListVM<GeneralDonationCategory_View, GeneralDonationCategorySearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("GeneralDonationCategory", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"GeneralManage", dialogWidth: 800),
                this.MakeStandardAction("GeneralDonationCategory", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "GeneralManage", dialogWidth: 800),
                this.MakeStandardAction("GeneralDonationCategory", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "GeneralManage", dialogWidth: 800),
                this.MakeStandardAction("GeneralDonationCategory", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "GeneralManage", dialogWidth: 800),
                //this.MakeStandardAction("GeneralDonationCategory", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "GeneralManage", dialogWidth: 800),
                this.MakeStandardAction("GeneralDonationCategory", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "GeneralManage", dialogWidth: 800),
                //this.MakeStandardAction("GeneralDonationCategory", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "GeneralManage", dialogWidth: 800),
                //this.MakeStandardAction("GeneralDonationCategory", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "GeneralManage"),
            };
        }


        protected override IEnumerable<IGridColumn<GeneralDonationCategory_View>> InitGridHeader()
        {
            return new List<GridColumn<GeneralDonationCategory_View>>{
                this.MakeGridHeader(x => x.CategoryName),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<GeneralDonationCategory_View> GetSearchQuery()
        {
            var query = DC.Set<GeneralDonationCategory>()
                .CheckContain(Searcher.CategoryName, x => x.CategoryName)
                .Select(x => new GeneralDonationCategory_View
                {
                    ID = x.ID,
                    CategoryName = x.CategoryName,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class GeneralDonationCategory_View : GeneralDonationCategory
    {

    }
}
