﻿using BMSHPMS.DSReception.ViewModels.DSReceiptAppendVMs;
using Microsoft.AspNetCore.Mvc;
using System;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace BMSHPMS.DSReception.Controllers
{
    [NoLog]
    [Area("DSReception")]
    [ActionDescription("收據功德補加")]
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
                        vm.InitAppendInfo(info.DharmaServiceID);
                        info.DonationProjectCategoryList = vm.AppendInfo.DonationProjectCategoryList;
                        return PartialView(info);
                    }
                    else
                    {
                        //return FFResult().Alert(vm.Message, "補加成功").CloseDialog();
                        return View("Result",vm);
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Exception2",ex);
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
