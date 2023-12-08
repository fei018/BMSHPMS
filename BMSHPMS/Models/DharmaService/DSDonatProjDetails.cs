using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.DharmaService
{
    [Table("DSDonationProject")]
    [Display(Name = "功德項目信息")]
    public class DSDonatProjDetails : BasePoco
    {
        [Display(Name = "金額")]
        [Comment("金額")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int Sum { get; set; }

        [Display(Name = "金額代號")]
        [Comment("金額代號")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Code { get; set; }

        [Display(Name = "功德項目ID")]
        [Comment("功德項目ID")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public Guid DonationProjectID { get; set; }

        public DSDonationProject DonationProject { get; set; }

        [Display(Name = "法會項目ID")]
        [Comment("法會項目ID")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public Guid DSProjectID { get; set; }

        public DSProject DSProject { get; set; }
    }
}
