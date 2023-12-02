using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WalkingTec.Mvvm.Core;
using System;

namespace BMSHPMS.Models.DharmaService
{
    /// <summary>
    /// 延生消災位
    /// </summary>
    [Table("T_LongevityPlaques")]

    [Display(Name = "延生消災位")]
    public class T_LongevityPlaque:BasePoco
    {
        [Display(Name = "姓名")]
        [Comment("姓名")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Name { get; set; }

        [Display(Name = "金額")]
        [Comment("金額")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int Sum { get; set; }

        [Display(Name = "延生編號")]
        [Comment("延生編號")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Serial { get; set; }

        [Display(Name = "備註")]
        [Comment("備註")]
        public string PRemark { get; set; }

        [Display(Name = "收據號碼Id")]
        [Comment("收據號碼Id")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public Guid ReceiptID { get; set; }


        //==========

        public T_Receipt Receipt { get; set; }

        
    }
}
