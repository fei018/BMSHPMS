using BMSHPMS.DSReception.ViewModels.DSReceiptAppendVMs;
using Microsoft.AspNetCore.Mvc;
using System;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace BMSHPMS.DSReception.Controllers
{
    [Area("DSReception")]
    [ActionDescription("收據補加")]
    public class DSReceiptAppendController : BaseController
    {
        [ActionDescription("主頁")]
        public IActionResult Index()
        {
            var vm = Wtm.CreateVM<DSReceiptAppendVM>();
            return PartialView(vm);
        }

        [ActionDescription("補加")]
        public IActionResult Append(string id)
        {
            var vm = Wtm.CreateVM<DSReceiptAppendVM>();
            vm.InitAppendInfo(id);
            return PartialView(vm.AppendInfo);
        }

        [HttpPost]
        [ActionDescription("補加")]
        public IActionResult Append(AppendInfoVM info)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView(info);
                }
                else
                {
                    var vm = Wtm.CreateVM<DSReceiptAppendVM>();
                    vm.DoAppend(info);

                    if (!ModelState.IsValid)
                    {
                        return PartialView(info);
                    }
                    else
                    {
                        return FFResult().Alert(vm.Message, "補加成功");
                    }
                }
            }
            catch (Exception ex)
            {
                return FFResult().Alert(ex.GetBaseException().Message, "發生錯誤");
            }
        }

        [ActionDescription("金額選項")]
        public IActionResult GetDonationSum(string id)
        {
            var vm = Wtm.CreateVM<DSReceiptAppendVM>();
            var list = vm.GetDonationSum(id);
            return JsonMore(list);
        }
    }
}
