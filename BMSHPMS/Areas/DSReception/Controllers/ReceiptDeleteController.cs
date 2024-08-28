using BMSHPMS.DSReception.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace BMSHPMS.DSReception.Controllers
{
    [NoLog]
    [Area("DSReception")]
    [ActionDescription("收據作廢")]
    public class ReceiptDeleteController : BaseController
    {
        [ActionDescription("Index")]
        public IActionResult Index()
        {
            var vm = Wtm.CreateVM<ReceiptDeleteVM>();
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Search")]
        public async Task<IActionResult> Search(ReceiptDeleteVM vm)
        {
            try
            {
                await vm.GetVMList();
                return PartialView(vm);
            }
            catch (Exception ex)
            {
                return PartialView("Exception2", ex);
            }
        }

        [HttpPost]
        [ActionDescription("刪除")]
        public IActionResult Delete(ReceiptDeleteVM vm)
        {
            try
            {
                vm.Delete();
                return PartialView(vm);
            }
            catch (Exception ex)
            {
                return PartialView("Exception2", ex);
            }
        }
    }
}
