using BMSHPMS.DSManage.ViewModels.FindDeletedSerialVMs;
using Microsoft.AspNetCore.Mvc;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace BMSHPMS.Areas.DSManage.Controllers
{
    [NoLog]
    [Area("DSManage")]
    [ActionDescription("查找功德編號")]
    public class FindDeletedSerialController : BaseController
    {
        public IActionResult Index()
        {
            var vm = Wtm.CreateVM<FindSearchVM>();
            return PartialView(vm);
        }

        public IActionResult DonationProjectSelectItems(string id)
        {
            var vm = Wtm.CreateVM<FindSearchVM>();
            var list = vm.GetDonationProjectSelectItems(id);
            return JsonMore(list);
        }
    }
}
