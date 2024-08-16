using BMSHPMS.DSManage.ViewModels.Opt_DonationProjectCleanVMs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace BMSHPMS.Areas.DSManage.Controllers
{
    [Area("DSManage")]
    [ActionDescription("功德編號歸零")]
    public class Opt_DonationProjectCleanController : BaseController
    {
        #region CleanUsedNumber

        [ActionDescription("Index")]
        public IActionResult Index()
        {
            var vm = Wtm.CreateVM<Opt_DonationProjectCleanVM>();

            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("單獨法會歸零")]
        public async Task<IActionResult> CleanUsedNumber(Opt_DonationProjectCleanVM vm)
        {
            try
            {
                await vm.CleanUsedNumber();
                return Json(new { code = 200, msg = "此法會功德編號已歸零" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 400, msg = ex.Message });
            }
        }

        //[HttpPost]
        //[ActionDescription("所有功德編號歸零")]
        //public async Task<IActionResult> CleanUsedNumberAll(Opt_DonationProjectCleanVM vm)
        //{
        //    try
        //    {
        //        await vm.CleanUsedNumberAll();
        //        return Json(new { code = 200, msg = "所有功德編號已歸零" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { code = 400, msg = ex.Message });
        //    }
        //}
        #endregion
    }
}
