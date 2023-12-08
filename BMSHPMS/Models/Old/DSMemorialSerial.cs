using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System;

namespace BMSHPMS.Models.DharmaService
{
    /// <summary>
    /// 附薦編號
    /// </summary>
    [Table("DSMemorialSerials")]
    [Index(nameof(Serial), IsUnique = true)]
    [Display(Name = "附薦編號")]
    public class DSMemorialSerial : BasePoco
    {
        [Display(Name = "編號")]
        [Comment("編號")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Serial { get; set; }

        [Display(Name = "已使用")]
        [Comment("已使用")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [DefaultValue(false)]
        public bool Used { get; set; }

        [Display(Name = "已廢棄")]
        [Comment("已廢棄")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [DefaultValue(false)]
        public bool Disused { get; set; }

        [Display(Name = "收據號碼Id")]
        [Comment("收據號碼Id")]
        public Guid? ReceiptID { get; set; }

        //==========

        public DSReceiptInfo Receipt { get; set; }

        
    }
}
