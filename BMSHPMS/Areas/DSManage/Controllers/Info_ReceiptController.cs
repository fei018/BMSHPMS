﻿using BMSHPMS.DSManage.ViewModels.Info_ReceiptVMs;
using Magicodes.ExporterAndImporter.Excel.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;

namespace BMSHPMS.DSManage.Controllers
{
    [NoLog]
    [Area("DSManage")]
    [ActionDescription("收據")]
    public partial class Info_ReceiptController : BaseController
    {
        #region Search
        [ActionDescription("Sys.Search")]
        public ActionResult Index()
        {
            var vm = Wtm.CreateVM<Info_ReceiptListVM>();
            return PartialView(vm);
        }

        [ActionDescription("Sys.Search")]
        [HttpPost]
        public string Search(Info_ReceiptSearcher searcher)
        {
            var vm = Wtm.CreateVM<Info_ReceiptListVM>(passInit: true);
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

        #region Create
        //[ActionDescription("Sys.Create")]
        //public ActionResult Create()
        //{
        //    var vm = Wtm.CreateVM<Info_ReceiptVM>();
        //    return PartialView(vm);
        //}

        //[HttpPost]
        //[ActionDescription("Sys.Create")]
        //public ActionResult Create(Info_ReceiptVM vm)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return PartialView(vm);
        //    }
        //    else
        //    {
        //        vm.DoAdd();
        //        if (!ModelState.IsValid)
        //        {
        //            vm.DoReInit();
        //            return PartialView(vm);
        //        }
        //        else
        //        {
        //            return FFResult().CloseDialog().RefreshGrid();
        //        }
        //    }
        //}
        #endregion

        #region Edit
        [ActionDescription("Sys.Edit")]
        public ActionResult Edit(string id)
        {
            var vm = Wtm.CreateVM<Info_ReceiptVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Edit")]
        [HttpPost]
        [ValidateFormItemOnly]
        public ActionResult Edit(Info_ReceiptVM vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                vm.DoEdit();
                if (!ModelState.IsValid)
                {
                    vm.DoReInit();
                    return PartialView(vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGridRow(vm.Entity.ID);
                }
            }
        }
        #endregion

        #region Delete
        [ActionDescription("Sys.Delete")]
        public ActionResult Delete(string id)
        {
            var vm = Wtm.CreateVM<Info_ReceiptVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        [HttpPost]
        public ActionResult Delete(string id, IFormCollection nouse)
        {
            var vm = Wtm.CreateVM<Info_ReceiptVM>(id);
            vm.DoDelete();
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid();
            }
        }
        #endregion

        #region Details
        [ActionDescription("Sys.Details")]
        public IActionResult Details(string id)
        {
            var vm = Wtm.CreateVM<Info_ReceiptVM>(id);
            vm.InitialDetails();
            return PartialView(vm);
        }
        #endregion

        #region BatchEdit
        //[HttpPost]
        //[ActionDescription("Sys.BatchEdit")]
        //public ActionResult BatchEdit(string[] IDs)
        //{
        //    var vm = Wtm.CreateVM<InfoReceiptBatchVM>(Ids: IDs);
        //    return PartialView(vm);
        //}

        //[HttpPost]
        //[ActionDescription("Sys.BatchEdit")]
        //public ActionResult DoBatchEdit(InfoReceiptBatchVM vm, IFormCollection nouse)
        //{
        //    if (!ModelState.IsValid || !vm.DoBatchEdit())
        //    {
        //        return PartialView("BatchEdit", vm);
        //    }
        //    else
        //    {
        //        return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.BatchEditSuccess", vm.Ids.Length]);
        //    }
        //}
        #endregion

        #region BatchDelete
        [HttpPost]
        [ActionDescription("Sys.BatchDelete")]
        public ActionResult BatchDelete(string[] IDs)
        {
            var vm = Wtm.CreateVM<InfoReceiptBatchVM>(Ids: IDs);
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Sys.BatchDelete")]
        public ActionResult DoBatchDelete(InfoReceiptBatchVM vm, IFormCollection nouse)
        {
            if (!ModelState.IsValid || !vm.DoBatchDelete())
            {
                return PartialView("BatchDelete", vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.BatchDeleteSuccess", vm.Ids.Length]);
            }
        }
        #endregion

        #region Import
        //[ActionDescription("Sys.Import")]
        //      public ActionResult Import()
        //      {
        //          var vm = Wtm.CreateVM<DSReceiptInfoImportVM>();
        //          return PartialView(vm);
        //      }

        //      [HttpPost]
        //      [ActionDescription("Sys.Import")]
        //      public ActionResult Import(DSReceiptInfoImportVM vm, IFormCollection nouse)
        //      {
        //          if (vm.ErrorListVM.EntityList.Count > 0 || !vm.BatchSaveData())
        //          {
        //              return PartialView(vm);
        //          }
        //          else
        //          {
        //              return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.ImportSuccess", vm.EntityList.Count.ToString()]);
        //          }
        //      }
        #endregion

        #region Export
        [ActionDescription("Sys.Export")]
        [HttpPost]
        public async Task<IActionResult> ExportExcel(Info_ReceiptListVM vm)
        {
            //return vm.GetExportData();
            try
            {
                var result = await vm.ExportExcel();

                string fileName = "法會收據_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                return new XlsxFileResult(bytes: result, fileName);
            }
            catch (Exception ex)
            {
                //return PartialView("Exception", ex);
                return FFResult().Alert(ex.GetBaseException().Message);
            }
        }

        //[ActionDescription("匯出今日")]
        //public async Task<IActionResult> ExportExcelToday()
        //{
        //    try
        //    {
        //        var vm = Wtm.CreateVM<Info_ReceiptReportVM>();
        //        var result = await vm.ExportExcelByToday();

        //        string fileName = "法會收據_" + DateTime.Today.ToString("yyyy-MM-dd") + ".xlsx";

        //        return new XlsxFileResult(bytes: result, fileName);
        //    }
        //    catch (Exception ex)
        //    {
        //        return PartialView("Exception", ex);
        //        //return FFResult().Alert(ex.GetBaseException().Message);
        //    }
        //}
        #endregion

        #region FillDonationData
        [ActionDescription("填寫數據")]
        public IActionResult FillDonationData(string id)
        {
            var vm = Wtm.CreateVM<Info_ReceiptVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("填寫數據")]
        public IActionResult FillDonationDataGrid(string id)
        {
            var vm = Wtm.CreateVM<Info_ReceiptVM>(id);
            vm.InitListVM(ReceiptPageMode.FillData);
            return PartialView(vm);
        }
        #endregion
    }
}
