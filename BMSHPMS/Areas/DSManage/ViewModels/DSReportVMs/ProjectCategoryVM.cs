using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.DSManage.ViewModels.DSReportVMs
{
    public class ProjectCategoryVM : TopBasePoco
    {
        [Display(Name = "功德類別")]
        public string ProjectName { get; set; }

        [Display(Name = "功德金額")]
        public int ProjectSum { get; set; } = 0;

        [Display(Name = "功德數量")]
        public int ProjectCount { get; set; } = 0;

        [Display(Name = "功德總金額")]
        public int ProjectTotalSum { get; set; } = 0;

        [JsonIgnore]
        public string Key => ProjectName + ProjectSum;
    }
}
