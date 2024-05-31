using System;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;


namespace BMSHPMS.GeneralManage.ViewModels.GeneralDonationCategoryVMs
{
    public partial class GeneralDonationCategorySearcher : BaseSearcher
    {
        [Display(Name = "功德類別")]
        public String CategoryName { get; set; }

        protected override void InitVM()
        {
        }

    }
}
