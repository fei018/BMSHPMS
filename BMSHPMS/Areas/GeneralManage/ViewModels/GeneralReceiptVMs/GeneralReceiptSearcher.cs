using System;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.GeneralManage.ViewModels.GeneralReceiptVMs
{
    public partial class GeneralReceiptSearcher : BaseSearcher
    {
        [Display(Name = "收據號碼")]
        public String ReceiptNumber { get; set; }
        [Display(Name = "收據日期")]
        public DateRange ReceiptDate { get; set; }
        [Display(Name = "聯絡人")]
        public String ContactName { get; set; }
        [Display(Name = "聯絡電話")]
        public String Phone { get; set; }
        [Display(Name = "功德類別")]
        public String DonationCategory { get; set; }

        protected override void InitVM()
        {
        }

    }
}
