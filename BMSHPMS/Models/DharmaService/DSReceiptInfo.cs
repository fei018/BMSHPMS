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
    [Table("DSReceiptInfo")]
    [Index(nameof(ReceiptNumber))]
    [Display(Name = "收據")]
    public class DSReceiptInfo : BasePoco
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

        [Display(Name = "法會名")]
        [Comment("法會名")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DSProjectName { get; set; }

        //[Display(Name = "銀行名")]
        //[Comment("銀行名")]
        //public string BankName { get; set; }

        //[Display(Name = "支票號碼")]
        //[Comment("支票號碼")]
        //public string ChequeNumber { get; set; }

        [Display(Name = "備註")]
        [Comment("備註")]
        public string DSRemark { get; set; }

        [Display(Name = "收據日期")]
        [Comment("收據日期")]
        public DateTime? ReceiptDate { get; set; }


        //==========


        public List<DSDonorInfo> DSDonorInfos { get; set; } = new List<DSDonorInfo>();

        public List<DSMemorialInfo> DSMemorialInfos { get; set; } = new List<DSMemorialInfo>();

        public List<DSLongevityInfo> DSLongevityInfos { get; set; } = new List<DSLongevityInfo>();
    }
}
