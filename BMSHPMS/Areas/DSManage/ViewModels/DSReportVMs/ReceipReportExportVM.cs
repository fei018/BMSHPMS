using BMSHPMS.DSManage.ViewModels.DSReportVMs;
using System.Collections.Generic;

namespace BMSHPMS.Areas.DSManage.ViewModels.DSReportVMs
{
    public class ReceipReportExportVM
    {
        public string ServiceName {  get; set; }

        public string AllTotalSum { get; set; }

        public List<ProjectCategoryVM> Projects { get; set; } = new();
    }
}
