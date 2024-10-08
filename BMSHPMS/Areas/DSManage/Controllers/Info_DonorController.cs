﻿using BMSHPMS.DSManage.ViewModels.Info_DonorVMs;
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
    [ActionDescription("功德主")]
    public partial class Info_DonorController : BaseController
    {
        #region Search
        [ActionDescription("Sys.Search")]
        public ActionResult Index()
        {
            var vm = Wtm.CreateVM<Info_DonorListVM>();
            return PartialView(vm);
        }

        [ActionDescription("Sys.Search")]
        [HttpPost]
        public string Search(Info_DonorSearcher searcher)
        {
            var vm = Wtm.CreateVM<Info_DonorListVM>(passInit: true);
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
            var vm = Wtm.CreateVM<InfoDonorCreateVM>();
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Sys.Create")]
        public ActionResult Create(InfoDonorCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                var vm2 = Wtm.CreateVM<Info_DonorVM>();
                vm2.CreateVMEntity = vm;

                try
                {
                    vm2.DoAdd();
                    if (!ModelState.IsValid)
                    {
                        vm.DoReInit();
                        return PartialView(vm);
                    }
                    else
                    {
                        //return FFResult().CloseDialog().RefreshGrid();
                        return FFResult().CloseDialog().RefreshGrid().Alert($"功德主編號:{vm2.CreateVMEntity.SerialCode}", title: "新增成功");
                    }
                }
                catch (Exception ex)
                {
                    return FFResult().Alert(ex.GetBaseException().Message, "發生錯誤");
                }
            }
        }
        #endregion

        #region Edit
        [ActionDescription("Sys.Edit")]
        public ActionResult Edit(string id)
        {
            var vm = Wtm.CreateVM<Info_DonorVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Edit")]
        [HttpPost]
        [ValidateFormItemOnly]
        public ActionResult Edit(Info_DonorVM vm)
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

        #region EditFill 收據 -> 填寫資料 -> 修改
        [ActionDescription("填寫資料")]
        public ActionResult EditFill(string id)
        {
            var vm = Wtm.CreateVM<Info_DonorVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("填寫資料")]
        [HttpPost]
        [ValidateFormItemOnly]
        public ActionResult EditFill(Info_DonorVM vm)
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
                    return FFResult().CloseDialog().AddCustomScript("getDSReceiptFillDonationDataGrid()");
                }
            }
        }
        #endregion

        #region Delete
        [ActionDescription("Sys.Delete")]
        public ActionResult Delete(string id)
        {
            var vm = Wtm.CreateVM<Info_DonorVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        [HttpPost]
        public ActionResult Delete(string id, IFormCollection nouse)
        {
            var vm = Wtm.CreateVM<Info_DonorVM>(id);
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

        #region DeleteFill
        [ActionDescription("填寫刪除")]
        public ActionResult DeleteFill(string id)
        {
            var vm = Wtm.CreateVM<Info_DonorVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("填寫刪除")]
        [HttpPost]
        public ActionResult DeleteFill(string id, IFormCollection nouse)
        {
            var vm = Wtm.CreateVM<Info_DonorVM>(id);
            vm.DoDelete();
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                return FFResult().CloseDialog().AddCustomScript("getDSReceiptFillDonationDataGrid()");
            }
        }
        #endregion

        #region Details
        [ActionDescription("Sys.Details")]
        public ActionResult Details(string id)
        {
            var vm = Wtm.CreateVM<Info_DonorVM>(id);
            return PartialView(vm);
        }
        #endregion

        #region BatchEdit
        //[HttpPost]
        //[ActionDescription("Sys.BatchEdit")]
        //public ActionResult BatchEdit(string[] IDs)
        //{
        //    var vm = Wtm.CreateVM<DSDonorInfoBatchVM>(Ids: IDs);
        //    return PartialView(vm);
        //}

        //[HttpPost]
        //[ActionDescription("Sys.BatchEdit")]
        //public ActionResult DoBatchEdit(DSDonorInfoBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<Info_DonorBatchVM>(Ids: IDs);
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Sys.BatchDelete")]
        public ActionResult DoBatchDelete(Info_DonorBatchVM vm, IFormCollection nouse)
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
        //          var vm = Wtm.CreateVM<DSDonorInfoImportVM>();
        //          return PartialView(vm);
        //      }

        //      [HttpPost]
        //      [ActionDescription("Sys.Import")]
        //      public ActionResult Import(DSDonorInfoImportVM vm, IFormCollection nouse)
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
        public async Task<IActionResult> ExportExcel(Info_DonorListVM vm)
        {
            //return vm.GetExportData();
            var result = await vm.ExportExcel();
            string fileName = "功德主_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        #endregion

        #region 牌位Excel
        [ActionDescription("牌位Excel")]
        [HttpPost]
        public IActionResult ExportExcelTemplate(Info_DonorListVM vm)
        {
            try
            {
                var tplVM = Wtm.CreateVM<PrintPlaqueDonorVM>();

                string key = Guid.NewGuid().ToString();
                tplVM.WtmCacheKey = key;

                // 緩存 提交的 Entity Ids
                Wtm.Cache.Add(key, vm.Ids);

                return PartialView(tplVM);
            }
            catch (Exception ex)
            {
                return PartialView("Exception2", ex);
            }
        }

        [ActionDescription("牌位Excel")]
        [HttpPost]
        public async Task<IActionResult> ExportExcelTemplate2(PrintPlaqueDonorVM vm)
        {
            try
            {
                var result = await vm.Export();


                return result;
            }
            catch (Exception ex)
            {
                return View("Exception", ex);
            }
        }
        #endregion
    }
}
