using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSDonationProjectVMs
{
    public partial class DSDonationProjectListVM : BasePagedListVM<DSDonationProject_View, DSDonationProjectSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("DSDonationProject", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800,dialogHeight:400),
                this.MakeStandardAction("DSDonationProject", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800,dialogHeight:400),              
                this.MakeStandardAction("DSDonationProject", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800,dialogHeight:400),
                this.MakeStandardAction("DSDonationProject", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800,dialogHeight:400),
                //this.MakeStandardAction("DSDonationProject", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800,dialogHeight:500),
                //this.MakeStandardAction("DSDonationProject", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800,dialogHeight:500),
                //this.MakeStandardAction("DSDonationProject", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800,dialogHeight:500),
                this.MakeStandardAction("DSDonationProject", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
            };
        }


        protected override IEnumerable<IGridColumn<DSDonationProject_View>> InitGridHeader()
        {
            return new List<GridColumn<DSDonationProject_View>>{
                this.MakeGridHeader(x => x.Sum),
                this.MakeGridHeader(x => x.SerialCode),
                this.MakeGridHeader(x => x.DonationCategory),
                this.MakeGridHeader(x => x.UsedNumber),
                this.MakeGridHeader(x => x.ProjectName_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DSDonationProject_View> GetSearchQuery()
        {
            var query = DC.Set<DSDonationProject>()
                .CheckEqual(Searcher.Sum, x=>x.Sum)
                .CheckContain(Searcher.SumCode, x=>x.SerialCode)
                .CheckContain(Searcher.DonationCategory, x=>x.DonationCategory)
                .CheckEqual(Searcher.DServiceProjID, x=>x.DServiceProjID)
                .Select(x => new DSDonationProject_View
                {
				    ID = x.ID,
                    Sum = x.Sum,
                    SerialCode = x.SerialCode,
                    DonationCategory = x.DonationCategory,
                    UsedNumber = x.UsedNumber,
                    ProjectName_view = x.DServiceProj.ProjectName,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class DSDonationProject_View : DSDonationProject{
        [Display(Name = "法會名")]
        public String ProjectName_view { get; set; }

    }
}
