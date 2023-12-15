using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSDonationProjectVMs
{
    public partial class DSDonationProjectSearcher : BaseSearcher
    {
        [Display(Name = "金額")]
        public Int32? Sum { get; set; }

        [Display(Name = "金額代碼")]
        public String SumCode { get; set; }

        [Display(Name = "功德類別")]
        public String DonationCategory { get; set; }

        public List<ComboSelectListItem> AllDServiceProjs { get; set; }

        [Display(Name = "法會項目")]
        public Guid? DServiceProjID { get; set; }

        public List<ComboSelectListItem> AllDSDonationCategory { get; set; }

        protected override void InitVM()
        {
            AllDServiceProjs = DC.Set<DServiceProject>().GetSelectListItems(Wtm, y => y.ProjectName);
        }

    }
}
