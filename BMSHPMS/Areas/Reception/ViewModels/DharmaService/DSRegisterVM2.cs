using BMSHPMS.Helper;
using BMSHPMS.Models.DharmaService;
using System.Collections.Generic;
using System;
using System.Linq;
using WalkingTec.Mvvm.Core;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BMSHPMS.Areas.Reception.ViewModels
{
    public partial class DSRegisterVM : BaseVM
    {
        #region 查詢各編號數量是否足夠
        /// <summary>
        /// 查詢各編號數量是否足夠
        /// </summary>
        /// <param name="leadDonorSerials"></param>
        /// <param name="longevitySerials"></param>
        /// <param name="memorialSerials"></param>
        /// <returns></returns>
        protected bool QuerySerialCountEnough(out List<T_LeadDonorSerial> leadDonorSerials,out List<T_LongevitySerial> longevitySerials,out List<T_MemorialSerial> memorialSerials)
        {
            bool isEnough = true;
            StringBuilder msg = null;

            leadDonorSerials = null;
            longevitySerials = null;
            memorialSerials = null;

            #region 功德主
            if (RegLeadDonorCount >= 1)
            {
                leadDonorSerials = DC.Set<T_LeadDonorSerial>().Where(s => !s.Used && !s.Disused).ToList();

                if (leadDonorSerials == null)
                {
                    msg.AppendLine("查詢數據庫 功德主編號 為 Null");
                    isEnough = false;
                }

                if (leadDonorSerials.Count < RegLeadDonorCount)
                {
                    msg.AppendLine($"數據庫中未使用 功德主編號 已經少於 {RegLeadDonorCount} 個. 請管理員添加數量後再使用.");
                    isEnough = false;
                }
            }
            #endregion

            #region 延生
            if (RegLongevityCount >= 1)
            {
                longevitySerials = DC.Set<T_LongevitySerial>().Where(s => !s.Used && !s.Disused).ToList();

                if (longevitySerials == null)
                {
                    msg.AppendLine("查詢數據庫 延生編號 為 Null");
                    isEnough = false;
                }

                if (longevitySerials.Count < RegLongevityCount)
                {
                    msg.AppendLine($"數據庫中未使用 延生編號 已經少於 {RegLongevityCount} 個. 請管理員添加數量後再使用.");
                    isEnough = false;
                }
            }
            #endregion

            #region 附薦
            if (RegMemorialCount >= 1)
            {
                memorialSerials = DC.Set<T_MemorialSerial>().Where(s => !s.Used && !s.Disused).ToList();

                if (memorialSerials == null)
                {
                    msg.AppendLine("查詢數據庫 附薦編號 為 Null");
                    isEnough = false;
                }

                if (memorialSerials.Count < RegMemorialCount)
                {
                    msg.AppendLine($"數據庫中未使用 附薦編號 已經少於 {RegMemorialCount} 個. 請管理員添加數量後再使用.");
                    isEnough = false;
                }
            }
            #endregion

            Message = msg?.ToString();
            return isEnough;
        }
        #endregion

        #region 查詢 收據號碼 是否已存在
        /// <summary>
        /// 查詢 收據號碼 是否已存在
        /// </summary>
        protected bool QueryReceiptNumberExist(out T_Receipt receipt)
        {
            receipt = null;
            //leadDonorSerials = null;
            //longevitieSerials = null;
            //memorialSerials = null;

            receipt = DC.Set<T_Receipt>().SingleOrDefault(r => r.ReceiptNumber.ToLower() == RegReceiptNumber.ToLower());

            if (receipt == null)
            {
                return false;
            }
            else
            {
                //T_Receipt receipt1 = receipt;
                //leadDonorSerials = DC.Set<T_LeadDonorSerial>().Where(s => s.ReceiptID == receipt1.ID).ToList();
                //longevitieSerials = DC.Set<T_LongevitySerial>().Where(s => s.ReceiptID == receipt1.ID).ToList();
                //memorialSerials = DC.Set<T_MemorialSerial>().Where(s => s.ReceiptID == receipt1.ID).ToList();


                //leadDonorSerials.Sort((x,y) => x.Serial.CompareTo(y.Serial));
                //longevitieSerials.Sort((x, y) => x.Serial.CompareTo(y.Serial));
                //memorialSerials.Sort((x, y) => x.Serial.CompareTo(y.Serial));

                return true;
            }
        }
        #endregion

        #region 查詢 功德主編號 By 收據
        /// <summary>
        /// 查詢 功德主編號 By 收據
        /// </summary>
        /// <param name="receipt"></param>
        /// <returns></returns>
        protected List<T_LeadDonorSerial> QueryLeadDonorSerialByReceipt(T_Receipt receipt)
        {
            var list = DC.Set<T_LeadDonorSerial>().Where(s => s.ReceiptID == receipt.ID).ToList();
            list.Sort((x, y) => x.Serial.CompareTo(y.Serial));
            return list;
        }
        #endregion

        #region 查詢 延生編號 By 收據
        /// <summary>
        /// 查詢 延生編號 By 收據
        /// </summary>
        /// <param name="receipt"></param>
        /// <returns></returns>
        protected List<T_LongevitySerial> QueryLongevitySerialByReceipt(T_Receipt receipt)
        {
            var list = DC.Set<T_LongevitySerial>().Where(s => s.ReceiptID == receipt.ID).ToList();
            list.Sort((x, y) => x.Serial.CompareTo(y.Serial));
            return list;
        }
        #endregion

        #region 查詢 附薦編號 By 收據
        /// <summary>
        /// 查詢 附薦編號 By 收據
        /// </summary>
        /// <param name="receipt"></param>
        /// <returns></returns>
        protected List<T_MemorialSerial> QueryMemorialSerialByReceipt(T_Receipt receipt)
        {
            var list = DC.Set<T_MemorialSerial>().Where(s => s.ReceiptID == receipt.ID).ToList();
            list.Sort((x, y) => x.Serial.CompareTo(y.Serial));
            return list;
        }
        #endregion

        #region 向 收據表 新增收據
        /// <summary>
        /// 向 收據表 新增收據
        /// </summary>
        /// <param name="receiptNumber"></param>
        /// <returns></returns>
        protected T_Receipt AddNewReceipt()
        {
            lock (DbTableLocker.T_Receipt)
            {
                T_Receipt receipt = new()
                {
                    ReceiptNumber = RegReceiptNumber,
                    ID = Guid.NewGuid(),
                    ReceiptDate = DateTime.Now,
                    CreateBy = LoginUserInfo.Name,
                    CreateTime = DateTime.Now,
                    UpdateBy = LoginUserInfo.Name,
                    UpdateTime = DateTime.Now
                };

                Wtm.DC.Set<T_Receipt>().Add(receipt);
                Wtm.DC.SaveChanges();
                return receipt;
            }
        }
        #endregion

        #region 收據ID 寫入 功德主編號表
        /// <summary>
        /// 收據ID 寫入 功德主編號表
        /// </summary>
        /// <param name="receipt"></param>
        /// <param name="leadDonorSerials"></param>
        /// <returns></returns>
        protected List<T_LeadDonorSerial> UpdateReceiptIDToLeadDonorTable(T_Receipt receipt, List<T_LeadDonorSerial> leadDonorSerials)
        {
            if (RegLeadDonorCount >0)
            {
                var list = leadDonorSerials.GetMinListByCount(s => s.Serial, RegLeadDonorCount.Value);

                foreach (var serial in list)
                {
                    serial.ReceiptID = receipt.ID;
                    serial.Used = true;
                    serial.UpdateBy = LoginUserInfo.Name;
                    serial.UpdateTime = DateTime.Now;
                }

                lock (DbTableLocker.T_LeadDonorSerial)
                {
                    Wtm.DC.Set<T_LeadDonorSerial>().UpdateRange(list);
                    Wtm.DC.SaveChanges();
                    return list;
                }
            }
            return null;
        }
        #endregion

        #region 收據ID 寫入 延生編號表
        /// <summary>
        /// 收據ID 寫入 延生編號表
        /// </summary>
        /// <param name="receipt"></param>
        /// <param name="longevitySerials"></param>
        /// <returns></returns>
        protected List<T_LongevitySerial> UpdateReceiptIDToLongvitySerialTable(T_Receipt receipt, List<T_LongevitySerial> longevitySerials)
        {
            if (RegLongevityCount > 0)
            {
                var list = longevitySerials.GetMinListByCount(s => s.Serial, RegLongevityCount.Value);

                foreach (var serial in list)
                {
                    serial.ReceiptID = receipt.ID;
                    serial.Used = true;
                    serial.UpdateBy = LoginUserInfo.Name;
                    serial.UpdateTime = DateTime.Now;
                }

                lock (DbTableLocker.T_LongevitySerial)
                {
                    Wtm.DC.Set<T_LongevitySerial>().UpdateRange(list);
                    Wtm.DC.SaveChanges();
                    return list;
                }
            }
            return null;
        }
        #endregion

        #region 收據ID 寫入 附薦編號表
        /// <summary>
        /// 收據ID 寫入 附薦編號表
        /// </summary>
        /// <param name="receipt"></param>
        /// <param name="memorialSerials"></param>
        /// <returns></returns>
        private List<T_MemorialSerial> UpdateReceiptIDToMemorialSerialTable(T_Receipt receipt, List<T_MemorialSerial> memorialSerials)
        {
            if (RegMemorialCount > 0)
            {
                var list = memorialSerials.GetMinListByCount(s => s.Serial, RegMemorialCount.Value);

                foreach (var serial in list)
                {
                    serial.ReceiptID = receipt.ID;
                    serial.Used = true;
                    serial.UpdateBy = Wtm.LoginUserInfo.Name;
                    serial.UpdateTime = DateTime.Now;
                }

                lock (DbTableLocker.T_MemorialSerial)
                {
                    Wtm.DC.Set<T_MemorialSerial>().UpdateRange(list);
                    Wtm.DC.SaveChanges();
                    return list;
                }
            }
            return null;
        }
        #endregion
    }
}
