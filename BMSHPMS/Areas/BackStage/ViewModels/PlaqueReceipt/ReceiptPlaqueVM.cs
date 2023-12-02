using System;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Areas.BackStage.ViewModels.PlaqueReceipt
{
    public class ReceiptPlaqueVM : BaseVM
    {
        #region 綁定頁面屬性
        /// <summary>
        /// 收據號碼
        /// </summary>
        public string ReceiptNumber { get; set; }

        /// <summary>
        /// 收據日期
        /// </summary>
        public DateTime ReceiptDate { get; set; }

        #endregion
    }
}
