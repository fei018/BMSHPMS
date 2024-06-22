using System;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;


namespace BMSHPMS.CommonManage.ViewModels.AnnualDabeiInfoVMs
{
    public partial class AnnualDabeiInfoSearcher : BaseSearcher
    {
        [Display(Name = "功德芳名")]
        public String Name { get; set; }

        [Display(Name = "收據號碼")]
        public string CommonReceiptNumber { get; set; }

    }
}
