using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.GeneralDharmaService
{
    [Display(Name = "通用收據")]
    [Table("Info_GeneralReceipt")]
    public class GeneralReceipt : BasePoco
    {
        [Display(Name = "收據號碼")]
        [Required(ErrorMessage = "{0}必填")]
        public string ReceiptNumber { get; set; }

        [Display(Name = "收據日期")]
        [Required(ErrorMessage = "{0}必填")]
        public DateTime? ReceiptDate { get; set; }

        [Display(Name = "收據金額")]
        public int? Sum { get; set; }

        [Display(Name ="聯絡人")]
        public string ContactName { get; set; }

        [Display(Name = "聯絡電話")]
        public string Phone {  get; set; }

        [Display(Name = "功德類別")]
        public string DonationCategory { get; set; }

        [Display(Name ="備註")]
        public string GeneralRemark { get; set; }

        public List<GeneralDonor> DonorList { get; set; } = new();
    }
}
