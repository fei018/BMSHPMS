using BMSHPMS.DSReception.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace BMSHPMS.DSReception.Controllers
{
    [NoLog]
    [Area("DSReception")]
    [ActionDescription("打印功德編號")]
    public class ReceiptPrintController : BaseController
    {
        [ActionDescription("主頁")]
        public IActionResult Index()
        {
            var vm = Wtm.CreateVM<PrintSerialVM>();
            return PartialView(vm);
        }

        [ActionDescription("列印編號")]
        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> PrintSerial(string receiptNumber)
        {
            try
            {
                var vm = Wtm.CreateVM<PrintSerialVM>();
                await vm.GetSerials(receiptNumber);
                return View(vm);
            }
            catch (Exception ex)
            {
                return View("Exception", ex);
            }
        }
    }
}
