﻿using BMSHPMS.DSManage.ViewModels.Info_LongevityVMs;
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
    [ActionDescription("延生位")]
    public partial class Info_LongevityController : BaseController
    {
        #region Search
        [ActionDescription("Sys.Search")]
        public ActionResult Index()
        {
            var vm = Wtm.CreateVM<Info_LongevityListVM>();
            return PartialView(vm);
        }

        [ActionDescription("Sys.Search")]
        [HttpPost]
        public string Search(Info_LongevitySearcher searcher)
        {
            var vm = Wtm.CreateVM<Info_LongevityListVM>(passInit: true);
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
        [ActionDescription("Sys.Create")]
        public ActionResult Create()
        {
            var vm = Wtm.CreateVM<Info_LongevityVM>();
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Sys.Create")]
        public ActionResult Create(Info_LongevityVM vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    vm.DoReInit();
                    return PartialView(vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGrid();
                }
            }
        }
        #endregion

        #region Edit
        [ActionDescription("Sys.Edit")]
        public ActionResult Edit(string id)
        {
            var vm = Wtm.CreateVM<Info_LongevityVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Edit")]
        [HttpPost]
        [ValidateFormItemOnly]
        public ActionResult Edit(Info_LongevityVM vm)
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
            var vm = Wtm.CreateVM<Info_LongevityVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        [HttpPost]
        public ActionResult Delete(string id, IFormCollection nouse)
        {
            var vm = Wtm.CreateVM<Info_LongevityVM>(id);
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
        public ActionResult Details(string id)
        {
            var vm = Wtm.CreateVM<Info_LongevityVM>(id);
            return PartialView(vm);
        }
        #endregion

        #region BatchEdit
        //[HttpPost]
        //[ActionDescription("Sys.BatchEdit")]
        //public ActionResult BatchEdit(string[] IDs)
        //{
        //    var vm = Wtm.CreateVM<DSLongevityInfoBatchVM>(Ids: IDs);
        //    return PartialView(vm);
        //}

        //[HttpPost]
        //[ActionDescription("Sys.BatchEdit")]
        //public ActionResult DoBatchEdit(DSLongevityInfoBatchVM vm, IFormCollection nouse)
        //{
        //    if (!ModelState.IsValid || !vm.DoBatchEdit())
        //    {
        //        return PartialView("BatchEdit",vm);
        //    }
        //    else
        //    {
        //        return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.BatchEditSuccess", vm.Ids.Length]);
        //    }
        //}
        #endregion

        #region BatchDelete
        //[HttpPost]
        //[ActionDescription("Sys.BatchDelete")]
        //public ActionResult BatchDelete(string[] IDs)
        //{
        //    var vm = Wtm.CreateVM<DSLongevityInfoBatchVM>(Ids: IDs);
        //    return PartialView(vm);
        //}

        //[HttpPost]
        //[ActionDescription("Sys.BatchDelete")]
        //public ActionResult DoBatchDelete(DSLongevityInfoBatchVM vm, IFormCollection nouse)
        //{
        //    if (!ModelState.IsValid || !vm.DoBatchDelete())
        //    {
        //        return PartialView("BatchDelete",vm);
        //    }
        //    else
        //    {
        //        return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.BatchDeleteSuccess", vm.Ids.Length]);
        //    }
        //}
        #endregion

        #region Import
        //[ActionDescription("Sys.Import")]
        //      public ActionResult Import()
        //      {
        //          var vm = Wtm.CreateVM<DSLongevityInfoImportVM>();
        //          return PartialView(vm);
        //      }

        //      [HttpPost]
        //      [ActionDescription("Sys.Import")]
        //      public ActionResult Import(DSLongevityInfoImportVM vm, IFormCollection nouse)
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

        #region Sys.Export
        [ActionDescription("Sys.Export")]
        [HttpPost]
        public async Task<IActionResult> ExportExcel(Info_LongevityListVM vm)
        {
            //return vm.GetExportData();
            var result = await vm.ExportExcel();
            string fileName = "延生_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        #endregion

        #region 匯出Excel範本
        [ActionDescription("匯出Excel範本")]
        [HttpPost]
        public IActionResult ExportExcelTemplate(Info_LongevityListVM vm)
        {
            try
            {
                var tplVM = Wtm.CreateVM<LongevityExcelTplVM>();

                string key = Guid.NewGuid().ToString();
                tplVM.WtmCacheKey = key;

                Wtm.Cache.Add(key, vm.Ids);

                return PartialView(tplVM);
            }
            catch (Exception ex)
            {
                return PartialView("Exception2", ex);
            }
        }

        [ActionDescription("匯出Excel範本2")]
        [HttpPost]
        public async Task<IActionResult> ExportExcelTemplate2(LongevityExcelTplVM vm)
        {
            try
            {
                var result = await vm.Export();
                //string fileName = "延生範本_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                //return new XlsxFileResult(bytes: result, fileName);
                return File(result.ExcelResult, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.DownloadFileName);
            }
            catch (Exception ex)
            {
                return View("Exception", ex);
            }
        }
        #endregion

    }
}
