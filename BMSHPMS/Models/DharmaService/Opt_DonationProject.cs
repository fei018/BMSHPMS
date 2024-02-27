using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.DharmaService
{
    [Table("Opt_DonationProject")]
    [Display(Name = "功德項目")]
    public class Opt_DonationProject : BasePoco
    {
        [Display(Name = "金額")]
        [Comment("金額")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? Sum { get; set; }

        [Display(Name = "編號代碼")]
        [Comment("編號代碼")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string SerialCode { get; set; }

        [Display(Name = "功德類別")]
        [Comment("功德類別")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DonationCategory { get; set; }

        [Display(Name = "編號已計數")]
        [Comment("編號已計數")]
        [DefaultValue(0)]
        public int UsedNumber { get; set; } = 0;

        [Display(Name = "法會項目ID")]
        [Comment("法會項目ID")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public Guid? DharmaServiceID { get; set; }

        [Display(Name = "法會項目")]
        public Opt_DharmaService DharmaService { get; set; }
    }
}
