using BMSHPMS.CommonManage.ViewModels.AnnualLightInfoVMs;
using BMSHPMS.Models.CommonDService;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.CommonManage.ViewModels.CommonReceiptVMs
{
    public class DonationListVM_AnnualLight : BasePagedListVM<AnnualLightInfo_View, DonationListVMSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("AnnualLightInfo", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "CommonManage", dialogWidth: 800),
                this.MakeStandardAction("AnnualLightInfo", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "CommonManage", dialogWidth: 800),
            };
        }


        protected override IEnumerable<IGridColumn<AnnualLightInfo_View>> InitGridHeader()
        {
            return new List<GridColumn<AnnualLightInfo_View>>{
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Sum),
                this.MakeGridHeader(x => x.Count),
                this.MakeGridHeader(x => x.WishNumber),
                this.MakeGridHeader(x => x.DonateLightMode),
                this.MakeGridHeader(x => x.ContactAddress),
                this.MakeGridHeader(x => x.Phone),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<AnnualLightInfo_View> GetSearchQuery()
        {
            var query = DC.Set<AnnualLightInfo>()
                .CheckEqual(Searcher.ReceiptId, x => x.CommonReceipt.ID)
                .Select(x => new AnnualLightInfo_View
                {
                    ID = x.ID,
                    Name = x.Name,
                    Sum = x.Sum,
                    Count = x.Count,
                    WishNumber = x.WishNumber,
                    DonateLightMode = x.DonateLightMode,
                    ContactAddress = x.ContactAddress,
                    Phone = x.Phone,
                    CreateTime = x.CreateTime,
                })
                .OrderBy(x => x.CreateTime);
            return query;
        }
    }
}
