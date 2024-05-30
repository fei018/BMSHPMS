using System;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.DabeiDharmaService
{
    [Display(Name = "大悲法會功德芳名")]
    public class DabeiDonor : BasePoco
    {
        [Display(Name = "功德芳名")]
        [Required(ErrorMessage = "{0}必填")]
        public string Name { get; set; }

        [Display(Name = "功德金額")]
        [Required(ErrorMessage = "{0}必填")]
        public int? Sum { get; set; }

        public Guid? ReceiptId { get; set; }

        public DabeiReceipt Receipt { get; set; }
    }
}
