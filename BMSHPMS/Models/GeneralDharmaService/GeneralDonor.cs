﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.GeneralDharmaService
{
    [Display(Name = "通用功德")]
    [Table("Info_GeneralDonor")]
    public class GeneralDonor : BasePoco
    {
        [Display(Name = "功德芳名")]
        [Required(ErrorMessage = "{0}必填")]
        public string Name { get; set; }

        [Display(Name = "功德金額(元)")]
        [Required(ErrorMessage = "{0}必填")]
        public int? Sum { get; set; }

        [Display(Name = "備註")]
        public string GeneralRemark { get; set; }

        [Display(Name = "自訂欄1")]
        public string CustomCol1 { get; set; }

        [Display(Name = "自訂欄2")]
        public string CustomCol2 { get; set; }

        [Display(Name = "自訂欄3")]
        public string CustomCol3 { get; set; }


        public Guid? ReceiptId { get; set; }

        public GeneralReceipt Receipt { get; set; }
    }
}
