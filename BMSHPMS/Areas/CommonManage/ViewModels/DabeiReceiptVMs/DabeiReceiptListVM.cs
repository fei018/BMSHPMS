using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DabeiDharmaService;


namespace BMSHPMS.CommonManage.ViewModels.DabeiReceiptVMs
{
    public partial class DabeiReceiptListVM : BasePagedListVM<DabeiReceipt_View, DabeiReceiptSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("DabeiReceipt", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"CommonManage", dialogWidth: 800),
                this.MakeStandardAction("DabeiReceipt", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("DabeiReceipt", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("DabeiReceipt", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("DabeiReceipt", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("DabeiReceipt", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("DabeiReceipt", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("DabeiReceipt", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "CommonManage"),
            };
        }


        protected override IEnumerable<IGridColumn<DabeiReceipt_View>> InitGridHeader()
        {
            return new List<GridColumn<DabeiReceipt_View>>{
                this.MakeGridHeader(x => x.ContactName),
                this.MakeGridHeader(x => x.Phone),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DabeiReceipt_View> GetSearchQuery()
        {
            var query = DC.Set<DabeiReceipt>()
                .CheckContain(Searcher.ContactName, x=>x.ContactName)
                .CheckContain(Searcher.Phone, x=>x.Phone)
                .Select(x => new DabeiReceipt_View
                {
				    ID = x.ID,
                    ContactName = x.ContactName,
                    Phone = x.Phone,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class DabeiReceipt_View : DabeiReceipt{

    }
}
