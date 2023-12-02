using BMSHPMS.Areas.Reception.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace BMSHPMS.Areas.Reception.Controllers
{
    //[NoLog]
    //[Area("Reception")]
    //[ActionDescription("功德主登記")]
    public class RegLeadDonorController : BaseController
    {
        [ActionDescription("登記")]
        public IActionResult Register()
        {
            var vm = new RegLeadDonorVM();
            return PartialView(vm);
        }

        [ActionDescription("登記")]
        [HttpPost]
        public IActionResult Register(RegLeadDonorVM vm)
        {
            try
            {
                if (Wtm.MSD.IsValid)
                {
                    vm.ReceptionSubmited();
                    return PartialView("Serial", vm);
                }
                else
                {
                    return PartialView("Serial", vm);
                }
            }
            catch (Exception ex)
            {
                return View("Exception", ex);
            }
        }
    }
}
