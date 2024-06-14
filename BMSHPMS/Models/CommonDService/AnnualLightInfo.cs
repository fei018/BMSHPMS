using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMSHPMS.Models.CommonDService
{
    /// <summary>
    /// 全年光明燈
    /// </summary>
    [Table("Info_AnnualLight")]
    public class AnnualLightInfo
    {
        [Display(Name = "供燈芳名")]
        [Required(ErrorMessage = "{0}必填")]
        public string Name { get; set; }

        [Display(Name = "金額(元)")]
        [Required(ErrorMessage = "{0}必填")]
        public int? Sum { get; set; }
    }
}
