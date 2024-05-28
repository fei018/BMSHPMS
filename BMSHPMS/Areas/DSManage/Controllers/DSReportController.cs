using BMSHPMS.Areas.DSManage.ViewModels.DSReportVMs;
using BMSHPMS.DSManage.ViewModels.DSReportVMs;
using Magicodes.ExporterAndImporter.Excel.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;

namespace BMSHPMS.DSManage.Controllers
{
    [NoLog]
    [Area("DSManage")]
    [ActionDescription("法會報表")]
    public class DSReportController : BaseController
    {
        [ActionDescription("搜索")]
        public IActionResult Index()
        {
            var vm = Wtm.CreateVM<ReceiptReportListVM>();

            return PartialView(vm);
        }

        #region Search
        [ActionDescription("搜索")]
        public string Search(ReceiptReportSearcher searcher)
        {
            var vm = Wtm.CreateVM<ReceiptReportListVM>();
            if (ModelState.IsValid)
            {
                vm.Searcher = searcher;

                try
                {
                    return vm.GetJson(false);
                }
                catch (Exception ex)
                {
                    return "{\"Data\":{},\"Count\":0,\"Page\":0,\"PageCount\":0,\"Msg\":\"" + ex.Message + "\",\"Code\":200}";
                }
            }
            else
            {
                return vm.GetError();
            }
        }
        #endregion

        #region ExportReport
        [ActionDescription("下載報表")]
        [HttpPost]
        public IActionResult DownloadDSReportExcel(ReceiptReportSearcher searcher)
        {
            try
            {
                var vm = Wtm.CreateVM<ReceiptReportListVM>();
                vm.Searcher = searcher;
                var id = vm.GetReportExcelId();

                return PartialView("DownloadDSReportExcel", id);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [ActionDescription("下載報表")]
        public IActionResult DownloadDSReportExcel(string id)
        {
            var vm = Wtm.Cache.Get<ReceiptReportDownloadExcelVM>(id);
            if (vm == null)
            {
                return View("Exception", new Exception("下載鏈接過期."));
            }
            return new XlsxFileResult(vm.Data, vm.FileName);
        }
        #endregion

    }
}
