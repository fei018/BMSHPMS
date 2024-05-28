using BMSHPMS.DSManage.ViewModels.Info_ReceiptVMs;
using Magicodes.ExporterAndImporter.Excel.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using BMSHPMS.DSManage.ViewModels.DSReportVMs;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Areas.DSManage.ViewModels.DSReportVMs;

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
            var vm = Wtm.CreateVM<ReceiptReportListVM>();
            vm.Searcher.DharmaServiceYear = 2000;
            return PartialView(vm);
        }

        #region Search
        public string Search(ReceiptReportSearcher searcher)
        {
            var vm = Wtm.CreateVM<ReceiptReportListVM>();
            if (ModelState.IsValid)
            {
                vm.Searcher = searcher;
                return vm.GetJson(false);
            }
            else
            {
                return vm.GetError();
            }
        }
        #endregion

        #region ExportReport
        [HttpPost]
        public IActionResult DownloadDSReportExcel(ReceiptReportSearcher searcher)
        {
            var vm = Wtm.CreateVM<ReceiptReportListVM>();
            vm.Searcher = searcher;
            var id = vm.GetReportExcelId();
            return PartialView("DownloadDSReportExcel",id);
        }

        public IActionResult DownloadDSReportExcel(string id)
        {
            var vm = Wtm.Cache.Get<ReceiptReportDownloadExcelVM>(id);

            return new XlsxFileResult(vm.Data, vm.FileName);
        }
        #endregion

        [ActionDescription("匯出今日收據")]
        public async Task<IActionResult> ReceiptToday()
        {
            try
            {
                var vm = Wtm.CreateVM<ReceiptFuncVM>();
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
