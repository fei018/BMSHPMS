using BMSHPMS.Helper;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using System.Linq;

namespace BMSHPMS.Areas.Reception.ViewModels
{
    public partial class DSRegisterVM:BaseVM
    {
        #region 提交頁面綁定屬性

        [Display(Name = "收據號碼")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string RegReceiptNumber { get; set; }

        [Display(Name = "功德主數")]
        public int? RegLeadDonorCount { get; set; }

        [Display(Name = "延生位數")]
        public int? RegLongevityCount { get; set; }

        [Display(Name = "附薦位數")]
        public int? RegMemorialCount { get; set; }

        #endregion

        #region
        public List<T_LeadDonorSerial> ShowLeadDonorSerials { get; set; }

        public List<T_LongevitySerial> ShowLongevitySerials { get; set; }

        public List<T_MemorialSerial> ShowMemorialSerials { get; set; }
        #endregion

        public string Message { get; set; }


        #region MyRegion
        public void RegisterSubmitted()
        {
            if ((!RegLeadDonorCount.HasValue || RegLeadDonorCount.Value <= 0)
                 && (!RegLongevityCount.HasValue || RegLongevityCount.Value <= 0)
                 && (!RegMemorialCount.HasValue || RegMemorialCount.Value <= 0))
            {
                Message = "請至少輸入一項數目.";
                return;
            }

            // 先查詢 收據號碼是否存在, 存在就返回 各關聯編號
            if (QueryReceiptNumberExist(out T_Receipt receipt))
            {
                Message = $"此收據號碼已經登記過, 登記時間:{receipt.ReceiptDate:yyyy-MM-dd}";

                ShowLeadDonorSerials = QueryLeadDonorSerialByReceipt(receipt);
                ShowLongevitySerials = QueryLongevitySerialByReceipt(receipt);
                ShowMemorialSerials = QueryMemorialSerialByReceipt(receipt);

                return;
            }

            // 查詢各編號是否足夠
            if (QuerySerialCountEnough(out List<T_LeadDonorSerial> leadSerials,out List<T_LongevitySerial> longeSerials,out List<T_MemorialSerial> memoSerials))
            {
                //新增收據
                var newReceipt = AddNewReceipt();

                if (newReceipt != null)
                {
                    Message = "新增收據成功.";
                }

                //更新各編號表
                ShowLeadDonorSerials = UpdateReceiptIDToLeadDonorTable(newReceipt, leadSerials);
                ShowLongevitySerials = UpdateReceiptIDToLongvitySerialTable(newReceipt, longeSerials);
                ShowMemorialSerials = UpdateReceiptIDToMemorialSerialTable(newReceipt, memoSerials);

                return;
            }
        }
        #endregion
    }
}
