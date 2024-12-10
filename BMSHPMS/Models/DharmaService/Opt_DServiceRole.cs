using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using System;

namespace BMSHPMS.Models.DharmaService
{
    [Table("Opt_DServiceRoles")]
    [Display(Name = "法會角色")]
    public class Opt_DServiceRole : TopBasePoco
    {
        [Display(Name = "法會Id")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public Guid? DSId { get; set; }

        [Display(Name = "用戶角色Id")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public Guid? FrameworkRoleId { get; set; }
    }
}
