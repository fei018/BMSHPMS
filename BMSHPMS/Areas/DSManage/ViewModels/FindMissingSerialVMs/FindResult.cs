using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMSHPMS.DSManage.ViewModels.FindMissingSerialVMs
{
    public class FindResult
    {
        [Display(Name = "法會")]
        public string DharmaServiceName { get; set; }

        [Display(Name = "法會年份")]
        public int? DharmaServiceYear { get; set; }

        [Display(Name = "功德項目")]
        public string DonationProjectCategory { get; set; }

        public List<string> MissingSerials { get; set; } = new();
    }
}
