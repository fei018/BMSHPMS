using BMSHPMS.CommonManage.ViewModels.AnnualDabeiInfoVMs;
using BMSHPMS.CommonManage.ViewModels.CommonReceiptVMs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;

namespace BMSHPMS.Controllers
{
    [Area("CommonManage")]
    [ActionDescription("全年大悲法會")]
    public partial class AnnualDabeiInfoController : BaseController
    {
        #region Search
        [ActionDescription("Sys.Search")]
        public ActionResult Index()
        {
            var vm = Wtm.CreateVM<AnnualDabeiInfoListVM>();
            return PartialView(vm);
        }

        [ActionDescription("Sys.Search")]
        [HttpPost]
        public string Search(AnnualDabeiInfoSearcher searcher)
        {
            var vm = Wtm.CreateVM<AnnualDabeiInfoListVM>(passInit: true);
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
        public ActionResult Create(string receiptId)
        {
            var vm = Wtm.CreateVM<AnnualDabeiInfoVM>();
            vm.Entity.CommonReceiptId = Guid.Parse(receiptId);
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Sys.Create")]
        public ActionResult Create(AnnualDabeiInfoVM vm)
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
                    return FFResult().CloseDialog().RefreshGrid().AddCustomScript(CommonDonationQueryString.AddCustomScript);
                }
            }
        }


        #endregion

        #region Edit
        [ActionDescription("Sys.Edit")]
        public ActionResult Edit(string id)
        {
            var vm = Wtm.CreateVM<AnnualDabeiInfoVM>(id);

            return PartialView(vm);
        }

        [ActionDescription("Sys.Edit")]
        [HttpPost]
        [ValidateFormItemOnly]
        public ActionResult Edit(AnnualDabeiInfoVM vm)
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
                    return FFResult().CloseDialog().RefreshGridRow(vm.Entity.ID).AddCustomScript(CommonDonationQueryString.AddCustomScript);
                }
            }
        }
        #endregion

        #region Delete
        [ActionDescription("Sys.Delete")]
        public ActionResult Delete(string id)
        {
            var vm = Wtm.CreateVM<AnnualDabeiInfoVM>(id);

            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        [HttpPost]
        public ActionResult Delete(string id, IFormCollection nouse)
        {
            var vm = Wtm.CreateVM<AnnualDabeiInfoVM>(id);
            vm.DoDelete();
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().AddCustomScript(CommonDonationQueryString.AddCustomScript);
            }
        }
        #endregion

        #region Details
        [ActionDescription("Sys.Details")]
        public ActionResult Details(string id)
        {
            var vm = Wtm.CreateVM<AnnualDabeiInfoVM>(id);
            return PartialView(vm);
        }
        #endregion

        #region BatchEdit
        //[HttpPost]
        //[ActionDescription("Sys.BatchEdit")]
        //public ActionResult BatchEdit(string[] IDs)
        //{
        //    var vm = Wtm.CreateVM<AnnualDabeiInfoBatchVM>(Ids: IDs);
        //    return PartialView(vm);
        //}

        //[HttpPost]
        //[ActionDescription("Sys.BatchEdit")]
        //public ActionResult DoBatchEdit(AnnualDabeiInfoBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<AnnualDabeiInfoBatchVM>(Ids: IDs);
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Sys.BatchDelete")]
        public ActionResult DoBatchDelete(AnnualDabeiInfoBatchVM vm, IFormCollection nouse)
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
        //          var vm = Wtm.CreateVM<AnnualDabeiInfoImportVM>();
        //          return PartialView(vm);
        //      }

        //      [HttpPost]
        //      [ActionDescription("Sys.Import")]
        //      public ActionResult Import(AnnualDabeiInfoImportVM vm, IFormCollection nouse)
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
        public IActionResult ExportExcel(AnnualDabeiInfoListVM vm)
        {
            return vm.GetExportData();
        }
        #endregion


    }
}
