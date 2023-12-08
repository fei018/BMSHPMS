using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.DharmaService
{
    [Table("DSDonationProject")]
    [Display(Name = "功德項目")]
    public class DSDonationProject : BasePoco
    {
        [Display(Name = "類別")]
        [Comment("類別")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Category { get; set; }

    }
}
