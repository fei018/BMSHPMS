namespace BMSHPMS.DSManage.ViewModels.DSReportVMs
{
    public class ProjectCategoryVM
    {
        public string ProjectName { get; set; }

        public string ProjectSum { get; set; }

        public string ProjectTotalSum { get; set; }

        public string Key => ProjectName + ProjectSum;
    }
}
