using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.DharmaService
{
    [Table("Opt_DharmaService")]
    [Display(Name = "法會項目")]
    public class Opt_DharmaService : BasePoco
    {
        [Display(Name = "法會名")]
        [Comment("法會名")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string ServiceName { get; set; }

        [Display(Name = "編號代碼")]
        [Comment("編號代碼")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string SerialCode { get; set; }

        [Display(Name = "法會主辦")]
        public string ServiceOrganizer { get; set; }

        [Display(Name = "法會日期描述")]
        public string ServiceDateDescription { get; set; }

        [Display(Name = "法會啓用")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public bool Enable { get; set; } = false;

        [Display(Name = "法會年份")]
        public int? ServiceYear { get; set; }

        [Display(Name = "備注")]
        public string RemarkHtml { get; set; }

        //notmap
        [NotMapped]
        [Display(Name = "法會")]
        public string ServiceFullName => ServiceYear?.ToString() + " " + ServiceName;

        public List<Opt_DonationProject> Opt_DonationProjects { get; set; } = new List<Opt_DonationProject>();

        
    }
}
