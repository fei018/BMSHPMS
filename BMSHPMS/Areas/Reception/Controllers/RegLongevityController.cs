using BMSHPMS.Areas.Reception.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace BMSHPMS.Areas.Reception.Controllers
{
    //[Area("Reception")]
    //[ActionDescription("延生登記")]
    public class RegLongevityController : BaseController
    {
        [ActionDescription("登記")]
        public IActionResult Register()
        {
            var vm = new RegLongevityVM();
            return PartialView(vm);
        }

        [ActionDescription("登記")]
        [HttpPost]
        public IActionResult Register(RegLongevityVM vm)
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
