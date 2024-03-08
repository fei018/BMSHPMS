using BMSHPMS.DSReception.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace BMSHPMS.Areas.DSReception.Controllers
{
    [NoLog]
    [Area("DSReception")]
    [ActionDescription("功德數目登記")]
    public class DSRegisterController : BaseController
    {
        [ActionDescription("Index")]
        public IActionResult Index()
        {
            DSRegisterVM vm = Wtm.CreateVM<DSRegisterVM>();
            vm.FillDharmaServiceList();
            return PartialView(vm);
        }

        [ActionDescription("登記")]
        public IActionResult Register(DSRegisterVM vm)
        {
            try
            {
                vm.FillDonationProjectList(vm.DharmaServiceID);
                return PartialView(vm);
            }
            catch (Exception ex)
            {
                return PartialView("Exception", ex);
            }
        }

        [ActionDescription("登記")]
        [HttpPost]
        public async Task<IActionResult> Register()
        {
            IFormCollection form = Wtm.HttpContext.Request.Form;
            var vm = Wtm.CreateVM<DSRegisterVM>();

            try
            {
                DSRegResultVM resultVM = await vm.Submitted(form);

                return PartialView("RegResult", resultVM);
            }
            catch (Exception ex)
            {
                return PartialView("Exception", ex);
            }
        }
    }
}
