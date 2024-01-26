using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Opt_DonationProjectVMs
{
    public partial class Opt_DonationProjectListVM : BasePagedListVM<Opt_DonationProject_View, Opt_DonationProjectSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Opt_DonationProject", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800,dialogHeight:600),
                this.MakeStandardAction("Opt_DonationProject", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800,dialogHeight:600),
                this.MakeStandardAction("Opt_DonationProject", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800,dialogHeight:400),
                this.MakeStandardAction("Opt_DonationProject", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800,dialogHeight:400),
                //this.MakeStandardAction("DSDonationProject", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800,dialogHeight:500),
                //this.MakeStandardAction("DSDonationProject", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800,dialogHeight:500),
                //this.MakeStandardAction("DSDonationProject", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800,dialogHeight:500),
                //this.MakeStandardAction("Opt_DonationProject", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
                this.MakeAction("Opt_DonationProject","CleanUsedNumber","清除已使用數","清除已使用數", GridActionParameterTypesEnum.NoId,"DSManage"),
            };
        }


        protected override IEnumerable<IGridColumn<Opt_DonationProject_View>> InitGridHeader()
        {
            return new List<GridColumn<Opt_DonationProject_View>>{
                this.MakeGridHeader(x => x.Sum),
                this.MakeGridHeader(x => x.SerialCode),
                this.MakeGridHeader(x => x.DonationCategory),
                this.MakeGridHeader(x => x.UsedNumber),
                this.MakeGridHeader(x => x.ProjectName_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Opt_DonationProject_View> GetSearchQuery()
        {
            var query = DC.Set<Opt_DonationProject>()
                .CheckEqual(Searcher.Sum, x=>x.Sum)
                .CheckContain(Searcher.SumCode, x=>x.SerialCode)
                .CheckContain(Searcher.DonationCategory, x=>x.DonationCategory)
                .CheckEqual(Searcher.DharmaServiceID, x=>x.DharmaServiceID)
                .Select(x => new Opt_DonationProject_View
                {
				    ID = x.ID,
                    Sum = x.Sum,
                    SerialCode = x.SerialCode,
                    DonationCategory = x.DonationCategory,
                    UsedNumber = x.UsedNumber,
                    ProjectName_view = x.DharmaService.ServiceName,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Opt_DonationProject_View : Opt_DonationProject{
        [Display(Name = "法會名")]
        public String ProjectName_view { get; set; }

    }
}
