using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.DharmaService
{
    /// <summary>
    /// 附薦牌位
    /// </summary>
	[Table("DSMemorialInfo")]

    [Display(Name = "附薦牌位")]
    public class DSMemorialInfo : BasePoco
    {
        [Display(Name = "附薦編號")]
        [Comment("附薦編號")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string Serial { get; set; }

        [Display(Name = "陽居姓名")]
        [Comment("陽居姓名")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string BenefactorName { get; set; }

        [Display(Name = "附薦宗親名及稱呼")]
        [Comment("附薦宗親名及稱呼")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string DeceasedName { get; set; }

        [Display(Name = "金額")]
        [Comment("金額")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public int? Sum { get; set; }

        [Display(Name = "備註")]
        [Comment("備註")]
        public string DSRemark { get; set; }

        [Display(Name = "收據號碼")]
        [Comment("收據號碼")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string ReceiptNumber { get; set; }


        [Display(Name = "法會名")]
        [Comment("法會名")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string DSName { get; set; }
    }

}
