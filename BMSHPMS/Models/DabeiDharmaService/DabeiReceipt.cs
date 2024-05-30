using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.DabeiDharmaService
{
    [Display(Name = "大悲法會收據")]
    public class DabeiReceipt : BasePoco
    {
        [Display(Name = "收據號碼")]
        [Required(ErrorMessage = "{0}必填")]
        public string ReceiptNumber { get; set; }

        [Display(Name ="聯絡人")]
        public string ContactName { get; set; }

        [Display(Name = "聯絡電話")]
        public string Phone {  get; set; }

        public List<DabeiDonor> DabeiDonors { get; set; } = new();
    }
}
