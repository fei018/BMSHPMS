using BMSHPMS.DSManage.ViewModels.Opt_DonationProjectVMs;
using BMSHPMS.Models.DharmaService;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSManage.ViewModels.Opt_DonationProjectCleanVMs
{
    public class DonationProjectListVM : BasePagedListVM<Opt_DonationProject_View, Opt_DonationProjectSearcher>
    {
        protected override IEnumerable<IGridColumn<Opt_DonationProject_View>> InitGridHeader()
        {
            return new List<GridColumn<Opt_DonationProject_View>>{
                this.MakeGridHeader(x => x.DonationCategory),
                this.MakeGridHeader(x => x.SerialCode),
                this.MakeGridHeader(x => x.Sum),
                this.MakeGridHeader(x => x.UsedNumber),
            };
        }

        public override IOrderedQueryable<Opt_DonationProject_View> GetSearchQuery()
        {
            if (!Searcher.DharmaServiceID.HasValue)
            {
                throw new Exception("Searcher.DharmaServiceID 沒有值");
            }

            var query = DC.Set<Opt_DonationProject>()
                .CheckEqual(Searcher.DharmaServiceID, x => x.DharmaServiceID)
                .Select(x => new Opt_DonationProject_View
                {
                    ID = x.ID,
                    Sum = x.Sum,
                    SerialCode = x.SerialCode,
                    DonationCategory = x.DonationCategory,
                    UsedNumber = x.UsedNumber,
                })
                .OrderBy(x => x.DonationCategory)
                .ThenByDescending(x => x.Sum);

            return query;
        }
    }
}
