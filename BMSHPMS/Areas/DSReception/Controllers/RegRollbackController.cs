using BMSHPMS.DSReception.ViewModels;
using Microsoft.AspNetCore.Mvc;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace BMSHPMS.DSReception.Controllers
{
    [NoLog]
    [Area("DSReception")]
    [ActionDescription("功德登記撤銷")]
    public class RegRollbackController : BaseController
    {
        [ActionDescription("Index")]
        public IActionResult Index()
        {
            return PartialView(new RegRollbackVM());
        }

        [ActionDescription("登記撤銷")]
        public IActionResult Rollback(RegRollbackVM vm)
        {
            vm.Rollback();
            return PartialView(vm);
        }
    }
}
