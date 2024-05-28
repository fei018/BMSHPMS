using BMSHPMS.Models.DharmaService;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.Areas.DSManage.ViewModels.DSReportVMs
{
    public class ReceiptReportSearcher : BaseSearcher
    {
        [Display(Name = "收據日期")]
        public DateRange ReceiptDate { get; set; }

        [Display(Name = "法會項目")]
        public string DharmaServiceName { get; set; }

        [Display(Name = "法會年份")]
        public int? DharmaServiceYear { get; set; }

        public List<ComboSelectListItem> AllDharmaServiceName { get; set; }

        protected override void InitVM()
        {
            AllDharmaServiceName = DC.Set<Opt_DharmaService>().GetSelectListItems(Wtm, x => x.ServiceName, y => y.ServiceName);
        }
    }
}
