using BMSHPMS.Helper;
using BMSHPMS.Models.DharmaService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMSHPMS.Areas.Reception.ViewModels
{
    /// <summary>
    /// 前台登記 護法功德主 VM
    /// </summary>
    public class RegLeadDonorVM : RegisterVM
    {
        public List<T_LeadDonorSerial> SerialList { get; set; }

        #region 前台提交後，返回 護法功德主編號
        /// <summary>
        /// 前台提交後，返回 護法功德主編號
        /// </summary>
        /// <returns></returns>
        public void ReceptionSubmited()
        {
            #region 檢查 收據號碼, 如果存在則返回
            // 查詢未使用 編號
            List<T_LeadDonorSerial> querySerials = DC.Set<T_LeadDonorSerial>().Where(s => !s.Used).ToList();

            if (querySerials == null)
            {
                Message = "查詢數據庫 延生編號 為 Null";
                return;
            }

            // 檢查數量是否足夠， 數據庫中未使用 延生編號 已經少於 xx 個，請管理員添加數量後再使用。
            if (querySerials.Count < SubmitCount)
            {
                Message = $"數據庫中未使用 延生編號 已經少於 {SubmitCount} 個. \r\n請管理員添加數量後再使用.";
                return;
            }
            #endregion

            #region 檢查 護法功德主編號
            // 查詢未使用 護法功德主編號  
            if (ReceiptNumberExist(out T_Receipt receipt, out List<T_LeadDonorSerial> serials))
            {
                if (serials.Count >= 1) // 收據號碼已存在 並且 登記過本編號時
                {
                    SerialList = serials;

                    Message = $"編號已登記過此收據號碼,有{SerialList.Count}個編號";
                    return;
                }
                else // 收據號碼已存在 還沒有登記過本編號
                {
                    // 編號表寫入收據ID
                    SerialList = UpdateReceiptIDToSerialTable(receipt, querySerials);

                    Message = $"收據號碼已存在, 新登記編號如下({SerialList.Count}個):";
                    return;
                }
            }
            else // 收據號碼還從未登記過
            {
                // 添加 新收據
                SerialList = AddNewReceiptAndUpdateSerialTable(ReceiptNumber, querySerials);

                Message = $"新收據添加成功, 新編號如下({SerialList.Count}個):";
                return;
            }
            #endregion
        }
        #endregion

        #region 向 編號表里 更新 收據ID
        /// <summary>
        /// 向 編號表里 更新 收據ID
        /// </summary>
        /// <param name="receipt"></param>
        /// <param name="querySerials"></param>
        /// <returns></returns>
        private List<T_LeadDonorSerial> UpdateReceiptIDToSerialTable(T_Receipt receipt, List<T_LeadDonorSerial> querySerials)
        {
            var list = querySerials.GetMinListByCount(s => s.Serial, SubmitCount.Value);

            // 延生編號 更新添加 收據ID
            foreach (var serial in list)
            {
                serial.ReceiptID = receipt.ID;
                serial.Used = true;
                serial.UpdateBy = Wtm.LoginUserInfo.Name;
                serial.UpdateTime = DateTime.Now;
            }

            lock (DbTableLocker.T_LeadDonorSerial)
            {
                Wtm.DC.Set<T_LeadDonorSerial>().UpdateRange(list);
                Wtm.DC.SaveChanges();
                return list;
            }
        }
        #endregion

        #region 向 收據表 插入新數據， 延生表 更新 收據ID 
        /// <summary>
        /// 向 收據表 插入新數據， 延生表 更新 收據ID 
        /// </summary>
        private List<T_LeadDonorSerial> AddNewReceiptAndUpdateSerialTable(string receiptNumber, List<T_LeadDonorSerial> querySerials)
        {
            // 添加 新收據
            var receipt = AddNewReceipt(receiptNumber);

            return UpdateReceiptIDToSerialTable(receipt, querySerials);
        }
        #endregion
    }
}
