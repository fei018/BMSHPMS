using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DServiceProjectVMs
{
    public partial class DServiceProjectListVM : BasePagedListVM<DServiceProject_View, DServiceProjectSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("DServiceProject", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800,dialogHeight:400),
                this.MakeStandardAction("DServiceProject", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800,dialogHeight:400),
                this.MakeStandardAction("DServiceProject", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800,dialogHeight:400),
                this.MakeStandardAction("DServiceProject", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800,dialogHeight:400),             
                //this.MakeStandardAction("DServiceProject", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("DServiceProject", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("DServiceProject", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("DServiceProject", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
            };
        }


        protected override IEnumerable<IGridColumn<DServiceProject_View>> InitGridHeader()
        {
            return new List<GridColumn<DServiceProject_View>>{
                this.MakeGridHeader(x => x.ProjectName),
                this.MakeGridHeader(x => x.SerialCode),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DServiceProject_View> GetSearchQuery()
        {
            var query = DC.Set<DServiceProject>()
                .CheckContain(Searcher.ProjectName, x=>x.ProjectName)
                .CheckContain(Searcher.ProjectCode, x=>x.SerialCode)
                .Select(x => new DServiceProject_View
                {
				    ID = x.ID,
                    ProjectName = x.ProjectName,
                    SerialCode = x.SerialCode,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class DServiceProject_View : DServiceProject{

    }
}
