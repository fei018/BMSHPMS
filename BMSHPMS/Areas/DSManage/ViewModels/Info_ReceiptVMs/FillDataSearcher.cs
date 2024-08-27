using System;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;


namespace BMSHPMS.DSManage.ViewModels.Info_ReceiptVMs
{
    public partial class FillDataSearcher : BaseSearcher
    {
        [Display(Name = "收據號碼Id")]
        public Guid ReceiptID { get; set; }

        protected override void InitVM()
        {
        }

    }
}
