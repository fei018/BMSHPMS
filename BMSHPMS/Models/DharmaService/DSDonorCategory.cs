using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System;

namespace BMSHPMS.Models.DharmaService
{
    /// <summary>
    /// 功德主分類
    /// </summary>
    [Table("DSDonorCategory")]
    [Display(Name = "功德主分類")]
    public class DSDonorCategory : BasePoco
    {
        [Display(Name = "金額")]
        [Comment("金額")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int Sum { get; set; }

        [Display(Name = "代號")]
        [Comment("代號")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Code { get; set; }

        [Display(Name = "已使用數")]
        [Comment("已使用數")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [DefaultValue(0)]
        public int UsedNo { get; set; }

        [Display(Name = "法會ID")]
        [Comment("法會ID")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public Guid DSNameCategID { get; set; }

        [Display(Name = "法會")]
        public DSProject DSNameCateg { get; set; }
    }
}
