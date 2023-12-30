using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Opt_DonationProjectVMs
{
    public partial class Opt_DonationProjectSearcher : BaseSearcher
    {
        [Display(Name = "金額")]
        public Int32? Sum { get; set; }

        [Display(Name = "金額代碼")]
        public String SumCode { get; set; }

        [Display(Name = "功德類別")]
        public String DonationCategory { get; set; }

        public List<ComboSelectListItem> AllDharmaService { get; set; }

        [Display(Name = "法會項目")]
        public Guid? DharmaServiceID { get; set; }

        public List<ComboSelectListItem> AllDSDonationCategory { get; set; }

        protected override void InitVM()
        {
            AllDharmaService = DC.Set<Opt_DharmaService>().GetSelectListItems(Wtm, y => y.ServiceName);
        }

    }
}
