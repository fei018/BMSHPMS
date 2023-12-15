using BMSHPMS.DSReception.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace BMSHPMS.Areas.DSReception.Controllers
{
    [Area("DSReception")]
    [ActionDescription("功德登記")]
    public class DSRegisterController : BaseController
    {
        [ActionDescription("登記")]
        public IActionResult Index()
        {
            DSRegisterVM vm = Wtm.CreateVM<DSRegisterVM>();
            vm.FillDSProjectList();
            return PartialView(vm);
        }

        public IActionResult Register(DSRegisterVM vm)
        {
            try
            {
                vm.FillDSDonationProjectList(vm.DSProjectID);
                return PartialView(vm);
            }
            catch (Exception ex)
            {
                return PartialView("Exception", ex);
            }
        }

        [HttpPost]
        public IActionResult Register()
        {
            IFormCollection form = Wtm.HttpContext.Request.Form;
            var vm = Wtm.CreateVM<DSRegisterVM>();

            try
            {
                DSRegResultVM resultVM = vm.Submitted(form);

                return PartialView("RegResult", resultVM);
            }
            catch (Exception ex)
            {
                return PartialView("Exception", ex);
            }
        }
    }
}
