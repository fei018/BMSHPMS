using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.GeneralDharmaService
{
    /// <summary>
    /// 通用功德類別
    /// </summary>
    [Table("Opt_GeneralDonationCategory")]
    public class GeneralDonationCategory : TopBasePoco
    {
        [Display(Name = "功德類別")]
        [Required(ErrorMessage = "{0}必填")]
        public string CategoryName { get; set; }
    }
}
