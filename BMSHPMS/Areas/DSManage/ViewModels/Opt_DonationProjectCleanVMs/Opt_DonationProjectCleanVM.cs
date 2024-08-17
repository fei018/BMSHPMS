using BMSHPMS.Models.DharmaService;
using System;
using System.Collections.Generic;
using WalkingTec.Mvvm.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSManage.ViewModels.Opt_DonationProjectCleanVMs
{
    public class Opt_DonationProjectCleanVM : BaseVM
    {
        //public List<Opt_DharmaService> DharmaServices { get; set; } = new List<Opt_DharmaService>();

        public List<DSUsedNumberCountVM> DSUsedNumberCountVMs { get; set; } = new List<DSUsedNumberCountVM>();

        public Guid? DharmaServiceID { get; set; }

        public List<Opt_DonationProject> DonationProjectList { get; set; }

        protected override void InitVM()
        {
            var DharmaServices = DC.Set<Opt_DharmaService>().ToList();
            foreach (var item in DharmaServices)
            {
                int totalusedNumberCount = 0;
                
                // 計算功德總使用數
                DC.Set<Opt_DonationProject>().Where(x => x.DharmaServiceID == item.ID).ToList().ForEach(x => totalusedNumberCount += x.UsedNumber);

                DSUsedNumberCountVM vm = new()
                {
                    DharmaServiceName = item.ServiceName,
                    DharmaServiceID = item.ID,
                    TotalUsedNumberCount = totalusedNumberCount,
                };

                DSUsedNumberCountVMs.Add(vm);
            }
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
                DC.UpdateProperty(donation, x => x.UpdateBy);
                DC.UpdateProperty(donation, x => x.UpdateTime);
            }

            await DC.SaveChangesAsync();
        }

        //public async Task CleanUsedNumberAll()
        //{
        //    var donations = await DC.Set<Opt_DonationProject>().ToListAsync();
        //    foreach (var donation in donations)
        //    {
        //        donation.UsedNumber = 0;
        //        donation.UpdateBy = LoginUserInfo.Name;
        //        donation.UpdateTime = DateTime.Now;
        //        DC.UpdateProperty(donation, x => x.UsedNumber);
        //        DC.UpdateProperty(donation, x => x.UpdateBy);
        //        DC.UpdateProperty(donation, x => x.UpdateTime);
        //    }

        //    await DC.SaveChangesAsync();
        //}
    }
}
