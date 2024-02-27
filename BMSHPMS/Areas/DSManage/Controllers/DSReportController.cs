using BMSHPMS.DSManage.ViewModels.Info_ReceiptVMs;
using Magicodes.ExporterAndImporter.Excel.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using BMSHPMS.DSManage.ViewModels.DSReportVMs;

namespace BMSHPMS.DSManage.Controllers
{
    [NoLog]
    [Area("DSManage")]
    [ActionDescription("法會報表")]
    public class DSReportController : BaseController
    {
        [ActionDescription("Index")]
        public IActionResult Index()
        {
            var vm = Wtm.CreateVM<DSReportVM>();
            return View(vm);
        }

        [ActionDescription("匯出今日收據")]
        public async Task<IActionResult> ReceiptToday()
        {
            try
            {
                var vm = Wtm.CreateVM<Info_ReceiptReportVM>();
                var result = await vm.ExportExcelByToday();

                string fileName = "法會收據_" + DateTime.Today.ToString("yyyy-MM-dd") + ".xlsx";

                return new XlsxFileResult(bytes: result, fileName);
            }
            catch (Exception ex)
            {
                return PartialView("Exception", ex);
                //return FFResult().Alert(ex.GetBaseException().Message);
            }
        }

        [ActionDescription("匯出日期收據")]
        public async Task<IActionResult> ReceiptDate(DSReportVM vm)
        {
            try
            {
                var result = await Wtm.CreateVM<ReceiptFuncVM>().ExportExcelByDate(vm.ReceiptReportDate);

                string fileName = "法會收據_" + vm.ReceiptReportDate.ToString("yyyy-MM-dd") + ".xlsx";

                return new XlsxFileResult(bytes: result, fileName);
            }
            catch (Exception ex)
            {
                return PartialView("Exception", ex);
                //return FFResult().Alert(ex.GetBaseException().Message);
            }
        }
    }
}
