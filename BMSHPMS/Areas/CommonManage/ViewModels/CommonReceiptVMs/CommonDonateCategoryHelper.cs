using System.Collections.Generic;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.CommonManage.ViewModels.CommonReceiptVMs
{
    /// <summary>
    /// 通用功德類別
    /// </summary>
    //public enum CommonDonateCategoryEnum
    //{
    //    全年大悲法會= 1,
    //    全年光明燈
    //}

    public class CommonDonateCategoryHelper
    {
        public static List<ComboSelectListItem> SelectItems()
        {
            return new List<ComboSelectListItem>()
            {
                new() { Text = "全年大悲法會", Value="全年大悲法會"},
                new() { Text = "全年光明燈", Value="全年光明燈"},
            };
        }
    }
}
