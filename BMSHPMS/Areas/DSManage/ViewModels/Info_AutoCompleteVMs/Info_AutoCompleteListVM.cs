using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_AutoCompleteVMs
{
    public partial class Info_AutoCompleteListVM : BasePagedListVM<Info_AutoComplete_View, Info_AutoCompleteSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Info_AutoComplete", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_AutoComplete", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_AutoComplete", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_AutoComplete", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_AutoComplete", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_AutoComplete", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_AutoComplete", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_AutoComplete", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
            };
        }


        protected override IEnumerable<IGridColumn<Info_AutoComplete_View>> InitGridHeader()
        {
            return new List<GridColumn<Info_AutoComplete_View>>{
                this.MakeGridHeader(x => x.Content),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Info_AutoComplete_View> GetSearchQuery()
        {
            var query = DC.Set<Info_AutoComplete>()
                .CheckContain(Searcher.Content, x=>x.Content)
                .Select(x => new Info_AutoComplete_View
                {
				    ID = x.ID,
                    Content = x.Content,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Info_AutoComplete_View : Info_AutoComplete{

    }
}
