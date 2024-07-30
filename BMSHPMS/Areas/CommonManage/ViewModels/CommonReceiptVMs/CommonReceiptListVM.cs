using BMSHPMS.Models.CommonDService;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;


namespace BMSHPMS.CommonManage.ViewModels.CommonReceiptVMs
{
    public partial class CommonReceiptListVM : BasePagedListVM<CommonReceipt_View, CommonReceiptSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("CommonReceipt", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"CommonManage", dialogWidth: 800).SetMax(),
                this.MakeStandardAction("CommonReceipt", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("CommonReceipt", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("CommonReceipt", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "CommonManage", dialogWidth: 800).SetMax(),
                //this.MakeStandardAction("CommonReceipt", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("CommonReceipt", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "CommonManage", dialogWidth: 800),
                //this.MakeStandardAction("CommonReceipt", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("CommonReceipt", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "CommonManage"),
            };
        }


        protected override IEnumerable<IGridColumn<CommonReceipt_View>> InitGridHeader()
        {
            return new List<GridColumn<CommonReceipt_View>>{
                this.MakeGridHeader(x => x.ReceiptNumber),
                this.MakeGridHeader(x => x.ReceiptDate),
                this.MakeGridHeader(x => x.Sum),
                this.MakeGridHeader(x => x.ContactName),
                this.MakeGridHeader(x => x.Phone),
                this.MakeGridHeader(x => x.DonationCategory),
                this.MakeGridHeader(x => x.CRemark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<CommonReceipt_View> GetSearchQuery()
        {
            string donationcate = null;
            if (Searcher.DonationCategory == null || Searcher.DonationCategory.Length == 0)
            {
                donationcate = null;
            }
            else if (Searcher.DonationCategory.Length == 1)
            {
                donationcate = Searcher.DonationCategory[0];
            }
            else if (Searcher.DonationCategory.Length >= 2)
            {
                donationcate = string.Join(',', Searcher.DonationCategory);
            }

            var query = DC.Set<CommonReceipt>()
                .CheckContain(Searcher.ReceiptNumber, x => x.ReceiptNumber)
                .CheckBetween(Searcher.ReceiptDate?.GetStartTime(), Searcher.ReceiptDate?.GetEndTime(), x => x.ReceiptDate, includeMax: false)
                .CheckContain(Searcher.ContactName, x => x.ContactName)
                .CheckContain(Searcher.Phone, x => x.Phone)
                //.CheckContain(donationcate, x => x.DonationCategory)
                .CheckContain(Searcher.CRemark, x => x.CRemark)
                .Select(x => new CommonReceipt_View
                {
                    ID = x.ID,
                    ReceiptNumber = x.ReceiptNumber,
                    ReceiptDate = x.ReceiptDate,
                    Sum = x.Sum,
                    ContactName = x.ContactName,
                    Phone = x.Phone,
                    DonationCategory = x.DonationCategory,
                    CRemark = x.CRemark,
                });

            var query1 = query.OrderByDescending(x => x.ReceiptDate);
            return query1;
        }

    }

    public class CommonReceipt_View : CommonReceipt
    {

    }
}
