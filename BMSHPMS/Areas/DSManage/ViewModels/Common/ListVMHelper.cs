using System;
using System.Collections.Generic;
using System.Linq;

namespace BMSHPMS.DSManage.ViewModels.Common
{
    /// <summary>
    /// ListVM 幫助類
    /// </summary>
    public class ListVMHelper
    {
        /// <summary>
        /// 範圍查詢編號，返回連續編號
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
                int index = serialStart.ToList().FindLastIndex(x => char.IsLetter(x));
                string serialStartLetter = serialStart[..(index + 1)];
                int serialStartNum = int.Parse(serialStart[(index + 1)..]);

                index = serialEnd.ToList().FindLastIndex(x => char.IsLetter(x));
                string serialEndLetter = serialEnd[..(index + 1)];
                int serialEndNum = int.Parse(serialEnd[(index + 1)..]);

                if (serialStartNum >= serialEndNum)
                {
                    list.Add(serialStart);
                    return list;
                }

                int diff = serialEndNum - serialStartNum;

                if (diff > 1000)
                {
                    diff = 1000;
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
