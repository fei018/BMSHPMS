using BMSHPMS.Helper;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Areas.Reception.ViewModels
{
    public class RegisterVM : BaseVM
    {
        #region 提交頁面綁定屬性
        [Display(Name = "編號個數")]
        [Comment("編號個數")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Range(1, 1000)]
        public int? SubmitCount { get; set; }

        [Display(Name = "收據號碼")]
        [Comment("收據號碼")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string ReceiptNumber { get; set; }

        #endregion

        public bool Succed { get; set; } = false;

        public string Message { get; set; }
        


        #region 檢查 收據號碼 是否已存在, 並關聯 延生編號
        /// <summary>
        /// 檢查 收據號碼 是否已存在, 並關聯 延生編號
        /// </summary>
        /// <param name="receipt"></param>
        /// <param name="longevitieSerials"></param>
        /// <returns></returns>
        protected bool ReceiptNumberExist(out T_Receipt receipt, out List<T_LongevitySerial> longevitieSerials)
        {
            receipt = null;
            longevitieSerials = null;

            receipt = DC.Set<T_Receipt>().Include(r => r.LongevitySerials).SingleOrDefault(r => r.ReceiptNumber.ToLower() == ReceiptNumber.ToLower());
            if (receipt == null)
            {
                return false;
            }

            longevitieSerials = receipt.LongevitySerials.OrderBy(s => s.Serial).ToList();

            return true;
        }
        #endregion

        #region 檢查 收據號碼 是否已存在, 並關聯 附薦編號
        /// <summary>
        /// 檢查 收據號碼 是否已存在, 並關聯 附薦編號
        /// </summary>
        /// <param name="receipt"></param>
        /// <param name="memorialSerials"></param>
        /// <returns></returns>
        protected bool ReceiptNumberExist(out T_Receipt receipt, out List<T_MemorialSerial> memorialSerials)
        {
            receipt = null;
            memorialSerials = null;

            receipt = DC.Set<T_Receipt>().Include(r => r.MemorialSerials).FirstOrDefault(r => r.ReceiptNumber.ToLower() == ReceiptNumber.ToLower());
            if (receipt == null)
            {
                return false;
            }

            memorialSerials = receipt.MemorialSerials.OrderBy(s => s.Serial).ToList();

            return true;
        }
        #endregion

        #region 檢查 收據號碼 是否已存在, 並關聯 護法功德主編號
        /// <summary>
        /// 檢查 收據號碼 是否已存在, 並關聯 護法功德主編號
        /// </summary>
        /// <param name="receipt"></param>
        /// <param name="memorialSerials"></param>
        /// <returns></returns>
        protected bool ReceiptNumberExist(out T_Receipt receipt, out List<T_LeadDonorSerial> leadDonorSerials)
        {
            receipt = null;
            leadDonorSerials = null;

            receipt = DC.Set<T_Receipt>().Include(r => r.LeadDonorSerials).FirstOrDefault(r => r.ReceiptNumber.ToLower() == ReceiptNumber.ToLower());
            if (receipt == null)
            {
                return false;
            }

            leadDonorSerials = receipt.LeadDonorSerials.OrderBy(s => s.Serial).ToList();

            return true;
        }
        #endregion

        #region 向數據庫 收據表 新增收據(ID)
        /// <summary>
        /// 向數據庫 收據表 新增收據(ID)
        /// </summary>
        /// <param name="receiptNumber"></param>
        /// <returns></returns>
        protected T_Receipt AddNewReceipt(string receiptNumber)
        {
            lock (DbTableLocker.T_Receipt)
            {
                T_Receipt receipt = new()
                {
                    ReceiptNumber = receiptNumber,
                    ID = Guid.NewGuid(),
                    ReceiptDate = DateTime.Now,
                    CreateBy = Wtm.LoginUserInfo.Name,
                    CreateTime = DateTime.Now,
                    UpdateBy = Wtm.LoginUserInfo.Name,
                    UpdateTime = DateTime.Now
                };

                Wtm.DC.Set<T_Receipt>().Add(receipt);
                Wtm.DC.SaveChanges();
                return receipt;
            }
        }
        #endregion
    }
}
