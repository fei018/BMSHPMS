using BMSHPMS.DSManage.ViewModels.Info_MemorialVMs;
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
    [ActionDescription("附薦位")]
    public partial class Info_MemorialController : BaseController
    {
        #region Search
        [ActionDescription("Sys.Search")]
        public ActionResult Index()
        {
            var vm = Wtm.CreateVM<Info_MemorialListVM>();
            return PartialView(vm);
        }

        [ActionDescription("Sys.Search")]
        [HttpPost]
        public string Search(Info_MemorialSearcher searcher)
        {
            var vm = Wtm.CreateVM<Info_MemorialListVM>(passInit: true);
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
            var vm = Wtm.CreateVM<InfoMemorialCreateVM>();
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Sys.Create")]
        public ActionResult Create(InfoMemorialCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                var vm2 = Wtm.CreateVM<Info_MemorialVM>();
                vm2.CreateVMEntity = vm;

                vm2.DoAdd();
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
            var vm = Wtm.CreateVM<Info_MemorialVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Edit")]
        [HttpPost]
        [ValidateFormItemOnly]
        public ActionResult Edit(Info_MemorialVM vm)
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
            var vm = Wtm.CreateVM<Info_MemorialVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("填寫資料")]
        [HttpPost]
        [ValidateFormItemOnly]
        public ActionResult EditFill(Info_MemorialVM vm)
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
            var vm = Wtm.CreateVM<Info_MemorialVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        [HttpPost]
        public ActionResult Delete(string id, IFormCollection nouse)
        {
            var vm = Wtm.CreateVM<Info_MemorialVM>(id);
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
        [ActionDescription("Sys.Delete")]
        public ActionResult DeleteFill(string id)
        {
            var vm = Wtm.CreateVM<Info_MemorialVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        [HttpPost]
        public ActionResult DeleteFill(string id, IFormCollection nouse)
        {
            var vm = Wtm.CreateVM<Info_MemorialVM>(id);
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
            var vm = Wtm.CreateVM<Info_MemorialVM>(id);
            return PartialView(vm);
        }
        #endregion

        #region BatchEdit
        //[HttpPost]
        //[ActionDescription("Sys.BatchEdit")]
        //public ActionResult BatchEdit(string[] IDs)
        //{
        //    var vm = Wtm.CreateVM<DSMemorialInfoBatchVM>(Ids: IDs);
        //    return PartialView(vm);
        //}

        //[HttpPost]
        //[ActionDescription("Sys.BatchEdit")]
        //public ActionResult DoBatchEdit(DSMemorialInfoBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<Info_MemorialBatchVM>(Ids: IDs);
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Sys.BatchDelete")]
        public ActionResult DoBatchDelete(Info_MemorialBatchVM vm, IFormCollection nouse)
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
        //public ActionResult Import()
        //{
        //    var vm = Wtm.CreateVM<DSMemorialInfoImportVM>();
        //    return PartialView(vm);
        //}

        //[HttpPost]
        //[ActionDescription("Sys.Import")]
        //public ActionResult Import(DSMemorialInfoImportVM vm, IFormCollection nouse)
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

        #region Sys.Export
        [ActionDescription("Sys.Export")]
        [HttpPost]
        public async Task<IActionResult> ExportExcel(Info_MemorialListVM vm)
        {
            //return vm.GetExportData();
            var result = await vm.ExportExcel();
            string fileName = "附薦_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        #endregion

        #region 匯出Excel範本
        [ActionDescription("匯出Excel範本")]
        [HttpPost]
        public IActionResult ExportExcelTemplate(Info_MemorialListVM vm)
        {
            try
            {
                var tplVM = Wtm.CreateVM<PrintPlaqueMemorialVM>();

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
        public async Task<IActionResult> ExportExcelTemplate2(PrintPlaqueMemorialVM vm)
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
