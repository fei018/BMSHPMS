using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.CommonDService
{
    [Display(Name = "通用收據")]
    [Table("Info_CommonReceipt")]
    public class CommonReceipt : BasePoco
    {
        [Display(Name = "收據號碼")]
        [Required(ErrorMessage = "{0}必填")]
        public string ReceiptNumber { get; set; }

        [Display(Name = "收據日期")]
        [Required(ErrorMessage = "{0}必填")]
        public DateTime? ReceiptDate { get; set; }

        [Display(Name = "收據金額")]
        public int? Sum { get; set; }

        [Display(Name = "聯絡人")]
        public string ContactName { get; set; }

        [Display(Name = "聯絡電話")]
        public string Phone { get; set; }

        [Display(Name = "功德類別")]
        [Required(ErrorMessage = "{0}必填")]
        public string DonationCategory { get; set; }

        [Display(Name = "備註")]
        public string GeneralRemark { get; set; }
    }
}
