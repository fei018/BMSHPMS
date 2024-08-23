using BMSHPMS.DSManage.ViewModels.FindMissingSerialVMs;
using Microsoft.AspNetCore.Mvc;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace BMSHPMS.DSManage.Controllers
{
    [NoLog]
    [Area("DSManage")]
    [ActionDescription("查找遺失編號")]
    public class FindMissingSerialController : BaseController
    {
        [ActionDescription("主頁")]
        public IActionResult Index()
        {
            var vm = Wtm.CreateVM<FindSearchVM>();
            return PartialView(vm);
        }

        [ActionDescription("SelectItems")]
        public IActionResult DonationProjectSelectItems(string id)
        {
            var vm = Wtm.CreateVM<FindMissingVM>();
            var list = vm.GetDonationProjectSelectItems(id);
            return JsonMore(list);
        }

        [ActionDescription("Find")]
        [HttpPost]
        public IActionResult Find(FindSearchVM vm)
        {
            var vm2 = Wtm.CreateVM<FindMissingVM>();
            vm2.FindMissingSerials(vm);

            return View(vm2);
        }
    }
}
