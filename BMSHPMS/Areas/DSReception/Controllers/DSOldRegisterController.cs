using BMSHPMS.DSReception.ViewModels.DSOldRegisterVMs;
using Microsoft.AspNetCore.Mvc;
using System;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;

namespace BMSHPMS.Areas.DSReception.Controllers
{
    [NoLog]
    [Area("DSReception")]
    [ActionDescription("舊收據轉新登記")]
    public class DSOldRegisterController : BaseController
    {
        [ActionDescription("主頁")]
        public IActionResult Index()
        {
            return PartialView();
        }

        [ActionDescription("登記")]
        public IActionResult Register(Guid? id)
        {
            var vm = Wtm.CreateVM<DSOldRegisterVM>();

            if (!id.HasValue)
            {
                ModelState.AddModelError("法會ID為Null", "法會ID為Null.");
            }
            else
            {
                vm.InitDS(id.Value);
            }

            return PartialView(vm);
        }

        [ActionDescription("登記")]
        [HttpPost]
        [ValidateFormItemOnly]
        public IActionResult Register(DSOldRegisterVM vm)
        {         
            ModelState.Clear();

            if (vm.Submitted())
            {
                return PartialView("Result", vm);
            }
            else
            {
                vm.InitDS(vm.DharmaService.ID);
                return PartialView(vm);
            }        
        }


    }
}
