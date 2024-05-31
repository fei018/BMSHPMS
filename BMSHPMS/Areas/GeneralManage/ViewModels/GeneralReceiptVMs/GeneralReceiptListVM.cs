using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.GeneralDharmaService;


namespace BMSHPMS.GeneralManage.ViewModels.GeneralReceiptVMs
{
    public partial class GeneralReceiptListVM : BasePagedListVM<GeneralReceipt_View, GeneralReceiptSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("GeneralReceipt", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"GeneralManage", dialogWidth: 800),
                this.MakeStandardAction("GeneralReceipt", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "GeneralManage", dialogWidth: 800),
                this.MakeStandardAction("GeneralReceipt", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "GeneralManage", dialogWidth: 800),
                this.MakeStandardAction("GeneralReceipt", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "GeneralManage", dialogWidth: 800),
                this.MakeStandardAction("GeneralReceipt", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "GeneralManage", dialogWidth: 800),
                this.MakeStandardAction("GeneralReceipt", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "GeneralManage", dialogWidth: 800),
                this.MakeStandardAction("GeneralReceipt", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "GeneralManage", dialogWidth: 800),
                this.MakeStandardAction("GeneralReceipt", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "GeneralManage"),
            };
        }


        protected override IEnumerable<IGridColumn<GeneralReceipt_View>> InitGridHeader()
        {
            return new List<GridColumn<GeneralReceipt_View>>{
                this.MakeGridHeader(x => x.ReceiptNumber),
                this.MakeGridHeader(x => x.ReceiptDate),
                this.MakeGridHeader(x => x.ContactName),
                this.MakeGridHeader(x => x.Phone),
                this.MakeGridHeader(x => x.DonationCategory),
                this.MakeGridHeader(x => x.GeneralRemark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<GeneralReceipt_View> GetSearchQuery()
        {
            var query = DC.Set<GeneralReceipt>()
                    .CheckContain(Searcher.ReceiptNumber, x=>x.ReceiptNumber)
                    .CheckBetween(Searcher.ReceiptDate?.GetStartTime(), Searcher.ReceiptDate?.GetEndTime(), x=>x.ReceiptDate)
                    .CheckContain(Searcher.ContactName, x=>x.ContactName)
                    .CheckContain(Searcher.Phone, x=>x.Phone)
                    .CheckContain(Searcher.DonationCategory, x=>x.DonationCategory)
                    .Select(x => new GeneralReceipt_View
                    {
				        ID = x.ID,
                        ReceiptNumber = x.ReceiptNumber,
                        ReceiptDate = x.ReceiptDate,
                        ContactName = x.ContactName,
                        Phone = x.Phone,
                        DonationCategory = x.DonationCategory,
                        GeneralRemark = x.GeneralRemark,
                    })
                    .OrderBy(x => x.ID);
            return query;
        }

    }

    public class GeneralReceipt_View : GeneralReceipt{

    }
}
