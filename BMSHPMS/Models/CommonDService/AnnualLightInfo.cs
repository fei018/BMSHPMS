using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.CommonDService
{
    /// <summary>
    /// 全年光明燈
    /// </summary>
    [Table("Info_AnnualLight")]
    public class AnnualLightInfo : BasePoco
    {
        [Display(Name = "供燈芳名")]
        [Required(ErrorMessage = "{0}必填")]
        public string Name { get; set; }

        [Display(Name = "金額(元)")]
        [Required(ErrorMessage = "{0}必填")]
        public int? Sum { get; set; }

        [Display(Name = "數量")]
        [Required(ErrorMessage = "{0}必填")]
        public int? Count { get; set; }

        [Display(Name = "吉祥願編號")]
        [Required(ErrorMessage = "{0}必填")]
        public string WishNumber { get; set; }

        [Display(Name = "供燈方式")]
        [Required(ErrorMessage = "{0}必填")]
        public DonateLightModeEnum DonateLightMode { get; set; }

        [Display(Name = "通訊地址")]
        public string ContactAddress { get; set; }

        [Display(Name = "電話")]
        public string Phone { get; set; }

        public Guid? CommonReceiptId { get; set; }

        public CommonReceipt CommonReceipt { get; set; }
    }

    public enum DonateLightModeEnum
    {
        [Display(Name = "新燈")]
        NewLight,
        [Display(Name = "續燈")]
        KeepLight
    }
}
