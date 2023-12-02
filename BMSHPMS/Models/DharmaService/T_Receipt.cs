using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.DharmaService
{
    /// <summary>
    /// 收據
    /// </summary>
    [Table("T_Receipts")]
    [Index(nameof(ReceiptNumber))]
    [Display(Name = "收據")]
    public class T_Receipt : BasePoco
    {
        [Display(Name = "收據號碼")]
        [Comment("收據號碼")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string ReceiptNumber { get; set; }

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
        public string PRemark { get; set; }

        [Display(Name = "開收據日期")]
        [Comment("開收據日期")]
        public DateTime? ReceiptDate { get; set; }


        //==========


        /// <summary>
        /// 關聯 護法功德主編號
        /// </summary>
        public ICollection<T_LeadDonorSerial> LeadDonorSerials { get; set; } = new List<T_LeadDonorSerial>();

        /// <summary>
        /// 關聯 附薦編號
        /// </summary>
        public List<T_MemorialSerial> MemorialSerials { get; set; } = new List<T_MemorialSerial>();

        /// <summary>
        /// 關聯 延生編號
        /// </summary>
        public List<T_LongevitySerial> LongevitySerials { get; } = new List<T_LongevitySerial>();
    }
}
