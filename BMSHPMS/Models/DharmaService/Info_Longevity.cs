using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.DharmaService
{
    /// <summary>
    /// 延生消災位
    /// </summary>
    [Table("Info_Longevity")]

    [Display(Name = "延生位")]
    public class Info_Longevity : BasePoco, IDataValid
    {
        [Display(Name = "姓名")]
        [Comment("姓名")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string Name { get; set; }

        [Display(Name = "金額")]
        [Comment("金額")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public int? Sum { get; set; }

        [Display(Name = "延生編號")]
        [Comment("延生編號")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string SerialCode { get; set; }

        [Display(Name = "備註")]
        [Comment("備註")]
        public string DSRemark { get; set; }

        [Display(Name = "數據有效")]
        [Comment("數據有效")]
        [DefaultValue(true)]
        public bool IsDataValid { get; set; }

        [Display(Name = "收據ID")]
        [Comment("收據ID")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public Guid? ReceiptID { get; set; }

        [Display(Name = "收據")]
        [Comment("收據")]
        public Info_Receipt Receipt{ get; set; }


    }
}
