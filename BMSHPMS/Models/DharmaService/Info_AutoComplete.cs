using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.DharmaService
{
    [Table("Info_AutoComplete")]
    [Display(Name = "自動補全數據")]
    public class Info_AutoComplete : TopBasePoco
    {
        [Required(ErrorMessage = "數據內容")]
        public string Content { get; set; }
    }
}
