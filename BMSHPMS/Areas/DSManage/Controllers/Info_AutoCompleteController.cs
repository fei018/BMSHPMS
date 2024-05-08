using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.DSManage.ViewModels.Info_AutoCompleteVMs;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DotLiquid.Util;

namespace BMSHPMS.DSManage.Controllers
{
    [Area("DSManage")]
    [ActionDescription("數據自動補全")]
    [AllRights]
    public partial class Info_AutoCompleteController : BaseController
    {
        #region Search
        [ActionDescription("Sys.Search")]
        public ActionResult Index()
        {
            var vm = Wtm.CreateVM<Info_AutoCompleteListVM>();
            return PartialView(vm);
        }

        [ActionDescription("Sys.Search")]
        [HttpPost]
        public string Search(Info_AutoCompleteSearcher searcher)
        {
            var vm = Wtm.CreateVM<Info_AutoCompleteListVM>(passInit: true);
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
            var vm = Wtm.CreateVM<Info_AutoCompleteVM>();
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Sys.Create")]
        public ActionResult Create(Info_AutoCompleteVM vm)
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
            var vm = Wtm.CreateVM<Info_AutoCompleteVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Edit")]
        [HttpPost]
        [ValidateFormItemOnly]
        public ActionResult Edit(Info_AutoCompleteVM vm)
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
            var vm = Wtm.CreateVM<Info_AutoCompleteVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        [HttpPost]
        public ActionResult Delete(string id, IFormCollection nouse)
        {
            var vm = Wtm.CreateVM<Info_AutoCompleteVM>(id);
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
            var vm = Wtm.CreateVM<Info_AutoCompleteVM>(id);
            return PartialView(vm);
        }
        #endregion

        #region BatchEdit
        //[HttpPost]
        //[ActionDescription("Sys.BatchEdit")]
        //public ActionResult BatchEdit(string[] IDs)
        //{
        //    var vm = Wtm.CreateVM<Info_AutoCompleteBatchVM>(Ids: IDs);
        //    return PartialView(vm);
        //}

        //[HttpPost]
        //[ActionDescription("Sys.BatchEdit")]
        //public ActionResult DoBatchEdit(Info_AutoCompleteBatchVM vm, IFormCollection nouse)
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
        //    var vm = Wtm.CreateVM<Info_AutoCompleteBatchVM>(Ids: IDs);
        //    return PartialView(vm);
        //}

        //[HttpPost]
        //[ActionDescription("Sys.BatchDelete")]
        //public ActionResult DoBatchDelete(Info_AutoCompleteBatchVM vm, IFormCollection nouse)
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
        //          var vm = Wtm.CreateVM<Info_AutoCompleteImportVM>();
        //          return PartialView(vm);
        //      }

        //      [HttpPost]
        //      [ActionDescription("Sys.Import")]
        //      public ActionResult Import(Info_AutoCompleteImportVM vm, IFormCollection nouse)
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

        //[ActionDescription("Sys.Export")]
        //[HttpPost]
        //public IActionResult ExportExcel(Info_AutoCompleteListVM vm)
        //{
        //    return vm.GetExportData();
        //}


        [ActionDescription("獲取補全數據")]
        public IActionResult GetContent(string keywords)
        {
            var vm = DC.Set<Info_AutoComplete>().AsNoTracking()
                        .Where(x=>x.Content.StartsWith(keywords))
                        .Select(x => new { Text = x.Content, Value = x.Content })
                        .ToList();
            return JsonMore(vm);
        }
    }
}
