using System;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.CommonManage.ViewModels.CommonReceiptVMs
{
    public class DonationListVMSearcher : BaseSearcher
    {
        [Display(Name = "收據號碼Id")]
        public Guid? ReceiptId { get; set; }
    }
}
