using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.DharmaService
{
    [Table("DSProject")]
    [Display(Name = "法會項目")]
    public class DSProject : BasePoco
    {
        [Display(Name = "法會類別")]
        [Comment("法會類別")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DSCategory { get; set; }

        [Display(Name = "法會代號")]
        [Comment("法會代號")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Code { get; set; }

    }
}
