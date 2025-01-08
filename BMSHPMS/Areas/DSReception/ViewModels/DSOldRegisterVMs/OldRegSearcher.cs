using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.DSReception.ViewModels.DSOldRegisterVMs
{
    public class OldRegSearcher : BaseSearcher
    {
        [Display(Name = "舊收據號碼")]
        public string ReceiptNumber { get; set; }

        [Display(Name = "法會")]
        public string DharmaServiceName { get; set; }
    }
}
