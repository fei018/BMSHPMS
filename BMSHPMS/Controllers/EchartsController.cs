using BMSHPMS.Helper;
using BMSHPMS.Models.DharmaService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;

namespace BMSHPMS.Controllers
{
    [NoLog]
    [AllRights]
    public class EchartsController : BaseController
    {
        public async Task<IActionResult> GetProjectChart()
        {
            var data = new List<ChartData>();

            var dharmaServices = await DC.Set<Opt_DharmaService>().ToListAsync();

            var donations = await DC.Set<Opt_DonationProject>().ToListAsync();

            data.Add(new ChartData
            {
                Category = "項目",
                Series = "法會",
                Value = (double)dharmaServices?.Count
            });

            data.Add(new ChartData
            {
                Category = "項目",
                Series = "功德主",
                Value = (double)donations.Where(x => x.DonationCategory == DharmaServiceSelectHelper.DonationCategory.功德主)?.Count(),
            });

            data.Add(new ChartData
            {
                Category = "項目",
                Series = "延生位",
                Value = (double)donations.Where(x => x.DonationCategory == DharmaServiceSelectHelper.DonationCategory.延生位)?.Count(),
            });
            data.Add(new ChartData
            {
                Category = "項目",
                Series = "附薦位",
                Value = (double)donations.Where(x => x.DonationCategory == DharmaServiceSelectHelper.DonationCategory.附薦位)?.Count(),
            });

            return Json(data.ToChartData());
        }

        public async Task<IActionResult> GetDSRegInfoChart()
        {
            var data = new List<ChartData>();

            var donorCount = await DC.Set<Info_Donor>().CountAsync();
            var longevityCount = await DC.Set<Info_Longevity>().CountAsync();
            var memoCount = await DC.Set<Info_Memorial>().CountAsync();

            data.Add(new ChartData
            {
                Category = "已登記功德",
                Series = "功德主",
                Value = donorCount
            });
            data.Add(new ChartData
            {
                Category = "已登記功德",
                Series = "延生",
                Value = longevityCount
            });
            data.Add(new ChartData
            {
                Category = "已登記功德",
                Series = "附薦",
                Value = memoCount
            });

            return Json(data.ToChartData());
        }

        //public async Task<IActionResult> GetDonorChart()
        //{
        //    var data = new List<ChartData>();

        //    Dictionary<int, Info_Donor> dic_donors = new Dictionary<int, Info_Donor>();

        //    var donors = await DC.Set<Info_Donor>().ToListAsync();



        //    foreach (var item in donors)
        //    {
        //        dic_donors.Add(item.Sum,)
        //    }
        //}
    }
}
