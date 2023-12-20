using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;
using BMSHPMS.Helper;


namespace BMSHPMS.DSManage.ViewModels.DSDonationProjectVMs
{
    public partial class DSDonationProjectVM : BaseCRUDVM<DSDonationProject>
    {
        public List<ComboSelectListItem> AllDServiceProjs { get; set; }

        public List<ComboSelectListItem> AllDSDonationCategory { get; set; }

        public DSDonationProjectVM()
        {
            SetInclude(x => x.DServiceProj);
        }

        protected override void InitVM()
        {
            AllDServiceProjs = DC.Set<DServiceProject>().GetSelectListItems(Wtm, y => y.ProjectName);
            AllDSDonationCategory = DSProjectSelectHelper.GetDonationCategoryComboSelectItems();
        }

        public override void DoAdd()
        {           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
