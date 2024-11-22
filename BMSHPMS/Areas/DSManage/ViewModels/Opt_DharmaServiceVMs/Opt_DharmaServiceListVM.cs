using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Opt_DharmaServiceVMs
{
    public partial class Opt_DharmaServiceListVM : BasePagedListVM<Opt_DharmaService_View, Opt_DharmaServiceSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Opt_DharmaService", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800,dialogHeight:400),
                this.MakeStandardAction("Opt_DharmaService", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800,dialogHeight:400),
                this.MakeStandardAction("Opt_DharmaService", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800,dialogHeight:400),
                this.MakeStandardAction("Opt_DharmaService", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800,dialogHeight:400),             
                //this.MakeStandardAction("DServiceProject", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("DServiceProject", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("DServiceProject", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Opt_DharmaService", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
            };
        }


        protected override IEnumerable<IGridColumn<Opt_DharmaService_View>> InitGridHeader()
        {
            return new List<GridColumn<Opt_DharmaService_View>>{
                this.MakeGridHeader(x => x.SerialCode),
                this.MakeGridHeader(x => x.ServiceName),
                this.MakeGridHeader(x => x.ServiceOrganizer),
                this.MakeGridHeader(x => x.ServiceDateDescription),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Opt_DharmaService_View> GetSearchQuery()
        {
            var query = DC.Set<Opt_DharmaService>()
                .CheckContain(Searcher.ProjectName, x=>x.ServiceName)
                .CheckContain(Searcher.ProjectCode, x=>x.SerialCode)
                .Select(x => new Opt_DharmaService_View
                {
				    ID = x.ID,
                    ServiceName = x.ServiceName,
                    SerialCode = x.SerialCode,
                    ServiceDateDescription = x.ServiceDateDescription,
                    ServiceOrganizer = x.ServiceOrganizer,
                })
                .OrderBy(x => x.SerialCode);
            return query;
        }

    }

    public class Opt_DharmaService_View : Opt_DharmaService{

    }
}
