using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.DSReception.ViewModels.DSReceiptAppendVMs
{
    public class AppendResultVM : BaseVM
    {
        [Display(Name = "編號")]
        public string SerialCode { get; set; }
    }
}
