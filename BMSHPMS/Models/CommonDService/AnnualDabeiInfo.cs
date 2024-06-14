using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.CommonDService
{
    /// <summary>
    /// 全年大悲法會
    /// </summary>
    [Table("Info_AnnualDabei")]
    public class AnnualDabeiInfo : BasePoco
    {
        [Display(Name = "功德芳名")]
        [Required(ErrorMessage = "{0}必填")]
        public string Name { get; set; }

        [Display(Name = "金額(元)")]
        [Required(ErrorMessage = "{0}必填")]
        public int? Sum { get; set; }

    }
}
