﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.DharmaService
{
    [Table("DServiceProject")]
    [Display(Name = "法會項目")]
    public class DServiceProject : BasePoco
    {
        [Display(Name = "法會名")]
        [Comment("法會名")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string ProjectName { get; set; }

        [Display(Name = "編號代碼")]
        [Comment("編號代碼")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string SerialCode { get; set; }

    }
}