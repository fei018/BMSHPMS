using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.DharmaService
{
    /// <summary>
    /// 收據
    /// </summary>
    [Table("Info_Receipt")]
    [Index(nameof(ReceiptNumber))]
    [Display(Name = "收據")]
    public class Info_Receipt : BasePoco
    {
        [Display(Name = "收據號碼")]
        [Comment("收據號碼")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string ReceiptNumber { get; set; }

        [Display(Name = "收據日期")]
        [Comment("收據日期")]
        public DateTime? ReceiptDate { get; set; }

        [Display(Name = "法會年份")]
        [Comment("法會年份")]
        public int? DharmaServiceYear { get; set; }

        [Display(Name = "法會名")]
        [Comment("法會名")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DharmaServiceName { get; set; }

        [Display(Name = "收據人姓名")]
        [Comment("收據人姓名")]
        public string ReceiptOwn { get; set; }

        [Display(Name = "聯絡人姓名")]
        [Comment("聯絡人姓名")]
        public string ContactName { get; set; }

        [Display(Name = "聯絡人電話")]
        [Comment("聯絡人電話")]
        public string ContactPhone { get; set; }

        [Display(Name = "金額")]
        [Comment("金額")]
        public int? Sum { get; set; }

        [Display(Name = "備註")]
        [Comment("備註")]
        public string DSRemark { get; set; }

        //[Display(Name = "銀行名")]
        //[Comment("銀行名")]
        //public string BankName { get; set; }

        //[Display(Name = "支票號碼")]
        //[Comment("支票號碼")]
        //public string ChequeNumber { get; set; }
        //==========


        public List<Info_Donor> Info_Donors { get; set; } = new List<Info_Donor>();

        public List<Info_Memorial> Info_Memorials { get; set; } = new List<Info_Memorial>();

        public List<Info_Longevity> Info_Longevitys { get; set; } = new List<Info_Longevity>();
    }
}
