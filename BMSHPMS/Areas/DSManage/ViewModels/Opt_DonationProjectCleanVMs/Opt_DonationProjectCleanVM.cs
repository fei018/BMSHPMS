using BMSHPMS.Models.DharmaService;
using System;
using System.Collections.Generic;
using WalkingTec.Mvvm.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.DSManage.ViewModels.Opt_DonationProjectVMs;

namespace BMSHPMS.DSManage.ViewModels.Opt_DonationProjectCleanVMs
{
    public class Opt_DonationProjectCleanVM : BaseVM
    {
        public List<Opt_DharmaService> DharmaServiceList { get; set; } = new List<Opt_DharmaService>();

        public Guid? DharmaServiceID { get; set; }

        public DonationProjectListVM DonationProjectListVM { get; set; }

        public Opt_DharmaService DharmaService { get; set; }

        public string DonationTotalUsedNumberCount { get; set; }

        protected override void InitVM()
        {
            DharmaServiceList = DC.Set<Opt_DharmaService>().OrderBy(x=>x.SerialCode).ToList();
        }

        public void InitDetail(string serviceid)
        {
            DharmaService = DC.Set<Opt_DharmaService>().Include(x=>x.Opt_DonationProjects).CheckID(serviceid).Single();

            int totalusedNumberCount = 0;
            DharmaService.Opt_DonationProjects.ForEach(x => totalusedNumberCount += x.UsedNumber);
            DonationTotalUsedNumberCount = totalusedNumberCount.ToString();

            DharmaServiceID = DharmaService.ID;

            DonationProjectListVM = new DonationProjectListVM();
            DonationProjectListVM.Searcher.DharmaServiceID = Guid.Parse(serviceid);
            DonationProjectListVM.CopyContext(this);
        }

        public async Task CleanUsedNumber(Guid serviceid)
        {
            var donations = await DC.Set<Opt_DonationProject>().CheckID(serviceid, x => x.DharmaServiceID).ToListAsync();
            foreach (var donation in donations)
            {
                donation.UsedNumber = 0;
                donation.UpdateBy = LoginUserInfo.Name;
                donation.UpdateTime = DateTime.Now;
                DC.UpdateProperty(donation, x => x.UsedNumber);
                DC.UpdateProperty(donation, x => x.UpdateBy);
                DC.UpdateProperty(donation, x => x.UpdateTime);
            }

            await DC.SaveChangesAsync();
        }

    }
}
