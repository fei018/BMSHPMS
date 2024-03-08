using BMSHPMS.Models.DharmaService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Helper
{
    public static class ExtentionHelper
    {
        #region 返回 最小 count 數量的 List
        /// <summary>
        /// 返回 最小 count 數量的 List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="keySelector"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<T> GetMinListByCount<T>(this List<T> list, Func<T, string> keySelector, int count)
        {
            var newlist = new List<T>();

            // 順序獲取最小的 提交 個數
            for (int i = 0; i < count; i++)
            {
                var min = list.MinBy(keySelector);

                newlist.Add(min);

                list.Remove(min);
            }

            return newlist;
        }
        #endregion


    }
}
