﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BMSHPMS.DSManage.ViewModels.Common
{
    /// <summary>
    /// ListVM 幫助類
    /// </summary>
    public class ListVMHelper
    {
        public const int MaxNumber = 10000;

        public static string MaxNumberString => $"結尾編號,範圍最大{MaxNumber}個";

        /// <summary>
        /// 範圍查詢編號，返回連續編號, 最多連續10000個
        /// </summary>
        /// <param name="serialStart"></param>
        /// <param name="serialEnd"></param>
        /// <returns></returns>
        public List<string> GetQuerySerialCodes(string serialStart, string serialEnd)
        {
            List<string> list = new List<string>();

            if (string.IsNullOrWhiteSpace(serialStart))
            {
                return list;
            }

            if (string.IsNullOrWhiteSpace(serialEnd))
            {
                list.Add(serialStart);
                return list;
            }

            try
            {
                // 編號最後一個非數字字符的index
                int index = serialStart.ToList().FindLastIndex(x => char.IsLetter(x));

                string serialStartLetter = serialStart.Substring(0, index + 1); //serialStart[..(index + 1)];
                int serialStartNum = int.Parse(serialStart.Substring(index + 1)); //int.Parse(serialStart[(index + 1)..]);

                index = serialEnd.ToList().FindLastIndex(x => char.IsLetter(x));
                int serialEndNum = int.Parse(serialEnd[(index + 1)..]);

                if (serialStartNum >= serialEndNum)
                {
                    list.Add(serialStart);
                    return list;
                }

                int diff = serialEndNum - serialStartNum;

                if (diff > MaxNumber)
                {
                    diff = MaxNumber;
                }

                list.Add(serialStart);

                for (int i = 0; i < diff; i++)
                {
                    var s = serialStartLetter + (serialStartNum += 1).ToString();
                    list.Add(s);
                }

                return list;
            }
            catch (Exception)
            {
                list.Add(serialStart);
                return list;
            }
        }
    }
}
