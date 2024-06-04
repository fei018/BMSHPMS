﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.GeneralManage.ViewModels.GeneralDonorVMs;

namespace BMSHPMS.Controllers
{
    [Area("GeneralManage")]
    [ActionDescription("通用功德")]
    public partial class GeneralDonorController : BaseController
    {
        #region Search
        [ActionDescription("Sys.Search")]
        public ActionResult Index()
        {
            var vm = Wtm.CreateVM<GeneralDonorListVM>();
            return PartialView(vm);
        }

        [ActionDescription("Sys.Search")]
        [HttpPost]
        public string Search(GeneralDonorSearcher searcher)
        {
            var vm = Wtm.CreateVM<GeneralDonorListVM>(passInit: true);
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
            var vm = Wtm.CreateVM<GeneralDonorVM>();
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Sys.Create")]
        public ActionResult Create(GeneralDonorVM vm)
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
            var vm = Wtm.CreateVM<GeneralDonorVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Edit")]
        [HttpPost]
        [ValidateFormItemOnly]
        public ActionResult Edit(GeneralDonorVM vm)
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
            var vm = Wtm.CreateVM<GeneralDonorVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        [HttpPost]
        public ActionResult Delete(string id, IFormCollection nouse)
        {
            var vm = Wtm.CreateVM<GeneralDonorVM>(id);
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
            var vm = Wtm.CreateVM<GeneralDonorVM>(id);
            return PartialView(vm);
        }
        #endregion

        #region BatchEdit
        //[HttpPost]
        //[ActionDescription("Sys.BatchEdit")]
        //public ActionResult BatchEdit(string[] IDs)
        //{
        //    var vm = Wtm.CreateVM<GeneralDonorBatchVM>(Ids: IDs);
        //    return PartialView(vm);
        //}

        //[HttpPost]
        //[ActionDescription("Sys.BatchEdit")]
        //public ActionResult DoBatchEdit(GeneralDonorBatchVM vm, IFormCollection nouse)
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
        [HttpPost]
        [ActionDescription("Sys.BatchDelete")]
        public ActionResult BatchDelete(string[] IDs)
        {
            var vm = Wtm.CreateVM<GeneralDonorBatchVM>(Ids: IDs);
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Sys.BatchDelete")]
        public ActionResult DoBatchDelete(GeneralDonorBatchVM vm, IFormCollection nouse)
        {
            if (!ModelState.IsValid || !vm.DoBatchDelete())
            {
                return PartialView("BatchDelete",vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.BatchDeleteSuccess", vm.Ids.Length]);
            }
        }
        #endregion

        #region Import
        //[ActionDescription("Sys.Import")]
        //public ActionResult Import()
        //{
        //    var vm = Wtm.CreateVM<GeneralDonorImportVM>();
        //    return PartialView(vm);
        //}

        //[HttpPost]
        //[ActionDescription("Sys.Import")]
        //public ActionResult Import(GeneralDonorImportVM vm, IFormCollection nouse)
        //{
        //    if (vm.ErrorListVM.EntityList.Count > 0 || !vm.BatchSaveData())
        //    {
        //        return PartialView(vm);
        //    }
        //    else
        //    {
        //        return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.ImportSuccess", vm.EntityList.Count.ToString()]);
        //    }
        //}
        #endregion

        #region Export
        [ActionDescription("Sys.Export")]
        [HttpPost]
        public IActionResult ExportExcel(GeneralDonorListVM vm)
        {
            return vm.GetExportData();
        }
        #endregion


    }
}
