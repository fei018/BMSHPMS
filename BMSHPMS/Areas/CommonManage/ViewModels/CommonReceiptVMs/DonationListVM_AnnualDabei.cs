using BMSHPMS.CommonManage.ViewModels.AnnualDabeiInfoVMs;
using BMSHPMS.Models.CommonDService;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.CommonManage.ViewModels.CommonReceiptVMs
{
    public class DonationListVM_AnnualDabei : BasePagedListVM<AnnualDabeiInfo_View, DonationListVMSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("AnnualDabeiInfo", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("AnnualDabeiInfo", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "CommonManage", dialogWidth: 800),
            };
        }

        protected override IEnumerable<IGridColumn<AnnualDabeiInfo_View>> InitGridHeader()
        {
            return new List<GridColumn<AnnualDabeiInfo_View>>{
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Sum),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<AnnualDabeiInfo_View> GetSearchQuery()
        {
            var query = DC.Set<AnnualDabeiInfo>()
                .CheckEqual(Searcher.ReceiptId, x => x.CommonReceipt.ID)
                .Select(x => new AnnualDabeiInfo_View
                {
                    ID = x.ID,
                    Name = x.Name,
                    Sum = x.Sum,
                    CreateTime = x.CreateTime,
                })
                .OrderBy(x => x.CreateTime);
            return query;
        }
    }
}
