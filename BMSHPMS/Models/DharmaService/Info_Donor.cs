﻿using Microsoft.EntityFrameworkCore;
using System;
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
    public class Info_Donor : BasePoco
    {
        [Display(Name = "延生位姓名")]
        [Comment("延生位姓名")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string LongevityName { get; set; }

        [Display(Name = "附薦名稱_1")]
        [Comment("附薦名稱_1")]
        public string DeceasedName_1 { get; set; }

        [Display(Name = "附薦名稱_2")]
        [Comment("附薦名稱_2")]
        public string DeceasedName_2 { get; set; }

        [Display(Name = "附薦名稱_3")]
        [Comment("附薦名稱_3")]
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
        public string SerialCode { get; set; }

        [Display(Name = "備註")]
        [Comment("備註")]
        public string DSRemark { get; set; }

        [Display(Name = "收據ID")]
        [Comment("收據ID")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public Guid? ReceiptID { get; set; }

        [Display(Name = "收據")]
        [Comment("收據")]
        public Info_Receipt Receipt { get; set; }



    }
}
