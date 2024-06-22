using System;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;


namespace BMSHPMS.CommonManage.ViewModels.AnnualLightInfoVMs
{
    public partial class AnnualLightInfoSearcher : BaseSearcher
    {
        [Display(Name = "供燈芳名")]
        public String Name { get; set; }

        [Display(Name = "收據號碼")]
        public string CommonReceiptNumber { get; set; }
    }
}
