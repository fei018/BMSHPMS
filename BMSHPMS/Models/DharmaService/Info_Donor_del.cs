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
    [Table("Info_Donor_del")]
    [Display(Name = "功德主位_del")]
    public class Info_Donor_del : BasePoco
    {
        [Display(Name = "延生位姓名")]
        [Comment("延生位姓名")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string LongevityName { get; set; }

        [Display(Name = "附薦宗親名及稱呼1")]
        [Comment("附薦宗親名及稱呼1")]
        public string DeceasedName_1 { get; set; }

        [Display(Name = "附薦宗親名及稱呼2")]
        [Comment("附薦宗親名及稱呼2")]
        public string DeceasedName_2 { get; set; }

        [Display(Name = "附薦宗親名及稱呼3")]
        [Comment("附薦宗親名及稱呼3")]
        public string DeceasedName_3 { get; set; }

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

        [Display(Name = "收據ID")]
        [Comment("收據ID")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public Guid? Receipt_delID { get; set; }

        [Display(Name = "收據")]
        [Comment("收據")]
        public Info_Receipt_del Receipt_del { get; set; }



    }
}
