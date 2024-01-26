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


        public List<Opt_DonationProject> Opt_DonationProjects { get; set; } = new List<Opt_DonationProject>();
    }
}
