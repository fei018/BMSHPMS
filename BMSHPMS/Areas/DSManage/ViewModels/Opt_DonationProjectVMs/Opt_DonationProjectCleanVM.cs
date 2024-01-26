using BMSHPMS.Models.DharmaService;
using System;
using System.Collections.Generic;
using WalkingTec.Mvvm.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSManage.ViewModels.Opt_DonationProjectVMs
{
    public class Opt_DonationProjectCleanVM : BaseVM
    {
        public List<Opt_DharmaService> DharmaServices { get; set; }

        public Guid? DharmaServiceID { get; set; }


        protected override void InitVM()
        {
            DharmaServices = DC.Set<Opt_DharmaService>().ToList();
        }

        public async Task CleanUsedNumber()
        {
            var donations = await DC.Set<Opt_DonationProject>().WhereIf(DharmaServiceID.HasValue, x => x.DharmaServiceID == DharmaServiceID.Value).ToListAsync();
            foreach (var donation in donations)
            {
                donation.UsedNumber = 0;
                donation.UpdateBy = LoginUserInfo.Name;
                donation.UpdateTime = DateTime.Now;
                DC.UpdateProperty(donation, x => x.UsedNumber);
            }

            await DC.SaveChangesAsync();
        }

        public async Task CleanUsedNumberAll()
        {
            var donations = await DC.Set<Opt_DonationProject>().ToListAsync();
            foreach (var donation in donations)
            {
                donation.UsedNumber = 0;
                donation.UpdateBy = LoginUserInfo.Name;
                donation.UpdateTime = DateTime.Now;
                DC.UpdateProperty(donation, x => x.UsedNumber);
            }

            await DC.SaveChangesAsync();
        }
    }
}
