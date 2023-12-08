using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WalkingTec.Mvvm.Core;
using System;

namespace BMSHPMS.Models.DharmaService
{
    /// <summary>
    /// 功德主
    /// </summary>
    [Table("DSDonorInfo")]
    [Display(Name = "功德主")]
    public class DSDonorInfo:BasePoco
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
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? Sum { get; set; }

        [Display(Name = "功德主編號")]
        [Comment("功德主編號")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string Serial { get; set; }

        [Display(Name = "備註")]
        [Comment("備註")]
        public string DSRemark { get; set; }

        [Display(Name = "收據號碼")]
        [Comment("收據號碼")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string ReceiptNumber { get; set; }



    }
}
