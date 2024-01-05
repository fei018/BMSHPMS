using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.DharmaService
{
    /// <summary>
    /// 功德主
    /// </summary>
    [Table("Info_Donor")]
    [Display(Name = "功德主位")]
    public class Info_Donor : BasePoco, IDataValid
    {
        [Display(Name = "延生位姓名")]
        [Comment("延生位姓名")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string LongevityName { get; set; }

        [Display(Name = "附薦宗親名及稱呼")]
        [Comment("附薦宗親名及稱呼")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string DeceasedName { get; set; }

        [Display(Name = "陽居姓名")]
        [Comment("陽居姓名")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string BenefactorName { get; set; }

        [Display(Name = "金額")]
        [Comment("金額")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public int? Sum { get; set; }

        [Display(Name = "功德主編號")]
        [Comment("功德主編號")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string SerialCode { get; set; }

        [Display(Name = "備註")]
        [Comment("備註")]
        public string DSRemark { get; set; }

        [Display(Name = "數據有效")]
        [Comment("數據有效")]
        public bool IsDataValid { get; set; } = true;

        [Display(Name = "收據ID")]
        [Comment("收據ID")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public Guid? ReceiptID { get; set; }

        [Display(Name = "收據")]
        [Comment("收據")]
        public Info_Receipt Receipt { get; set; }



    }
}
