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

        [ActionDescription("Details")]
        public IActionResult Details(string DharmaServiceID)
        {
            var vm = Wtm.CreateVM<Opt_DonationProjectCleanVM>();
            vm.InitDetail(DharmaServiceID);

            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("單獨法會歸零")]
        public async Task<IActionResult> CleanUsedNumber(Opt_DonationProjectCleanVM vm)
        {
            try
            {
                if (!vm.DharmaServiceID.HasValue)
                {
                    throw new Exception("DharmaServiceID is null");
                }
                await vm.CleanUsedNumber(vm.DharmaServiceID.Value);
                return FFResult().CloseDialog().Alert("已清除");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        #endregion
    }
}
