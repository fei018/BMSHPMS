using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.DharmaService
{
    [Table("Info_AutoComplete")]
    [Display(Name = "自動補全數據")]
    public class Info_AutoComplete : TopBasePoco
    {
        [Display(Name = "數據內容")]
        [Required(ErrorMessage = "{0}必填")]
        public string Content { get; set; }
    }
}
