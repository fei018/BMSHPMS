using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using Microsoft.EntityFrameworkCore;

namespace BMSHPMS.Models.DharmaService
{
    /// <summary>
    /// 法會功德登記回退上次記錄
    /// </summary>
    [Table("Reg_RollbackInfo")]
    [Display(Name = "功德登記回退")]
    public class Reg_RollbackInfo : TopBasePoco
    {
        [Display(Name = "功德ID")]
        [Comment("功德ID")]
        public Guid? DonationProjectID { get; set; }

        /// <summary>
        /// 記錄 功德的前一次的 已使用數
        /// </summary>
        [Display(Name = "功德已使用數")]
        [Comment("功德已使用數")]
        public int? PreUsedNumber { get; set; }

        [Display(Name = "收據號碼")]
        [Comment("收據號碼")]
        public string LastReceiptNumber { get; set; }
    }
}
