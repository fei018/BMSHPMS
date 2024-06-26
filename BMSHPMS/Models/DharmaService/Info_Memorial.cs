﻿using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.DharmaService
{
    /// <summary>
    /// 附薦牌位
    /// </summary>
	[Table("Info_Memorial")]

    [Display(Name = "附薦位")]
    public class Info_Memorial : BasePoco
    {
        [Display(Name = "附薦編號")]
        [Comment("附薦編號")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string SerialCode { get; set; }

        [Display(Name = "陽居姓名")]
        [Comment("陽居姓名")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string BenefactorName { get; set; }

        [Display(Name = "附薦名稱_1")]
        [Comment("附薦名稱_1")]
        public string DeceasedName_1 { get; set; }

        [Display(Name = "附薦名稱_2")]
        [Comment("附薦名稱_2")]
        public string DeceasedName_2 { get; set; }

        [Display(Name = "附薦名稱_3")]
        [Comment("附薦名稱_3")]
        public string DeceasedName_3 { get; set; }

        [Display(Name = "金額")]
        [Comment("金額")]
        public int? Sum { get; set; }

        [Display(Name = "備註")]
        [Comment("備註")]
        public string DSRemark { get; set; }

        [Display(Name = "功德項目Id")]
        public Guid? DonationProjectId { get; set; }

        [Display(Name = "功德項目編號")]
        public string DProjectSerial { get; set; }

        [Display(Name = "功德項目編號號碼")]
        public int? DProjectSerialNumber { get; set; }

        [Display(Name = "收據ID")]
        [Comment("收據ID")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public Guid? ReceiptID { get; set; }

        [Display(Name = "收據")]
        [Comment("收據")]
        public Info_Receipt Receipt { get; set; }
    }

}
