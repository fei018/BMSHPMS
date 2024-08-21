using BMSHPMS.Models.DharmaService;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSManage.ViewModels.FindDeletedSerialVMs
{
    public class FindDeletedVM : BaseVM
    {
        public FindSearchVM FindSearch { get; set; }

        public List<ComboSelectListItem> DharmaServiceSelectItems { get; set; }


        protected override void InitVM()
        {
            DharmaServiceSelectItems = DC.Set<Opt_DharmaService>()
                                         .OrderBy(x => x.SerialCode)
                                         .GetSelectListItems(Wtm, x => x.ServiceName, y => y.ID);
        }

        public List<ComboSelectListItem> GetDonationProjectSelectItems(string serviceID)
        {
            var list = DC.Set<Opt_DonationProject>().CheckID(serviceID, x => x.DharmaServiceID)
                        .OrderBy(x => x.DonationCategory)
                        .ThenBy(x => x.Sum)
                        .GetSelectListItems(Wtm, x => $"{x.DonationCategory},${x.Sum}", y => y.ID);
            return list;
        }
    }
}
