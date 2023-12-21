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
            DC.Set<DSDonationProject>().Add(Entity);
            DC.SaveChanges();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            DC.Set<DSDonationProject>().Update(Entity);
            DC.SaveChanges();
        }

        public override void DoDelete()
        {
            DC.Set<DSDonationProject>().Remove(Entity);
            DC.SaveChanges();
        }
    }
}
