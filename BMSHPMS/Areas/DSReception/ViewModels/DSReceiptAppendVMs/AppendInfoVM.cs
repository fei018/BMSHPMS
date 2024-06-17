using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.DSReception.ViewModels.DSReceiptAppendVMs
{
    public class AppendInfoVM : BaseVM
    {
        [Display(Name = "法會")]
        public string DharmaServiceID { get; set; }

        [Display(Name = "收據號碼")]
        [Required(ErrorMessage = "{0}必填")]
        public string ReceiptNumber { get; set; }

        [Display(Name = "功德類別")]
        [Required(ErrorMessage = "{0}必填")]
        public string DonationProjectCategory { get; set; }

        [Display(Name = "金額")]
        [Required(ErrorMessage = "{0}必填")]
        public string DonationProjectID { get; set; }

        [Display(Name = "功德數目")]
        [Required(ErrorMessage = "{0}必填")]
        public int? DonationProjectCount { get; set; }

        public List<ComboSelectListItem> DonationProjectCategoryList { get; set; }

    }
}
