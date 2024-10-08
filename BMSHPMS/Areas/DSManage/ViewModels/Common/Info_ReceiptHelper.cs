﻿using BMSHPMS.Helper;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSManage.ViewModels.Common
{
    public class Info_ReceiptHelper
    {
        //public static T2 CopyNewProperties<T1, T2>(in T1 t1)
        //{
        //    T2 t2 = Activator.CreateInstance<T2>();

        //    var props = t2.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        //    foreach (var p2 in props)
        //    {
        //        var value = t1.GetPropertyValue(p2.Name);

        //        if (value != null)
        //        {
        //            if (value is string value2 && string.IsNullOrEmpty(value2))
        //            {
        //                continue;
        //            }

        //            if (p2.CanWrite)
        //            {
        //                p2.SetValue(t2, value);
        //            }
        //        }
        //    }

        //    return t2;
        //}

        /// <summary>
        /// 刪除收據， 把收據 複製到 Delete 表里
        /// </summary>
        /// <param name="wtm"></param>
        /// <param name="receiptID"></param>
        /// <exception cref="Exception"></exception>
        public static void ReceiptMoveToDeleteTable(WTMContext wtm, Guid receiptID)
        {
            var dc = wtm.DC;

            try
            {
                var oldReceipt = dc.Set<Info_Receipt>().Where(x => x.ID == receiptID).FirstOrDefault();

                if (oldReceipt == null)
                {
                    throw new Exception("Info_Receipt 查詢 receiptID 為Null: " + receiptID);
                }

                //用反射方式把 收據內容 複製到 del 收據
                var r_del = ToolsHelper.CreateInstanceUseProperties<Info_Receipt, Info_Receipt_del>(oldReceipt);

                r_del.ID = Guid.NewGuid();

                lock (DbTableLocker.T_Receipt_del)
                {
                    dc.Set<Info_Receipt_del>().Add(r_del);
                    dc.SaveChanges();

                    var r_del2 = dc.Set<Info_Receipt_del>().Find(r_del.ID);
                    if (r_del2 == null)
                    {
                        throw new Exception("Info_Receipt_del 添加收據不成功");
                    }

                    // 複製功德主
                    var donors = dc.Set<Info_Donor>().Where(x => x.ReceiptID == receiptID).ToList();
                    if (donors != null && donors.Count > 0)
                    {
                        foreach (var item in donors)
                        {
                            var d = ToolsHelper.CreateInstanceUseProperties<Info_Donor, Info_Donor_del>(item);
                            d.ID = Guid.NewGuid();
                            d.Receipt_delID = r_del2.ID;
                            dc.Set<Info_Donor_del>().Add(d);
                        }
                    }

                    // 複製延生
                    var longevitys = dc.Set<Info_Longevity>().Where(x => x.ReceiptID == receiptID).ToList();
                    if (longevitys != null && longevitys.Count > 0)
                    {
                        foreach (var item in longevitys)
                        {
                            var d = ToolsHelper.CreateInstanceUseProperties<Info_Longevity, Info_Longevity_del>(item);
                            d.ID = Guid.NewGuid();
                            d.Receipt_delID = r_del2.ID;
                            dc.Set<Info_Longevity_del>().Add(d);
                        }
                    }

                    // 複製附薦
                    var memorials = dc.Set<Info_Memorial>().Where(x => x.ReceiptID == receiptID).ToList();
                    if (memorials != null && memorials.Count > 0)
                    {
                        foreach (var item in memorials)
                        {
                            var d = ToolsHelper.CreateInstanceUseProperties<Info_Memorial, Info_Memorial_del>(item);
                            d.ID = Guid.NewGuid();
                            d.Receipt_delID = r_del2.ID;
                            dc.Set<Info_Memorial_del>().Add(d);
                        }
                    }

                    dc.SaveChanges();

                    // 刪除舊數據
                    dc.Set<Info_Receipt>().Remove(oldReceipt);
                    dc.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }           
        }
    }
}
