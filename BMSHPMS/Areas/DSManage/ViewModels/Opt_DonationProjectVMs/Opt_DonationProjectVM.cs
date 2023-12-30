using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;
using BMSHPMS.Helper;


namespace BMSHPMS.DSManage.ViewModels.Opt_DonationProjectVMs
{
    public partial class Opt_DonationProjectVM : BaseCRUDVM<Opt_DonationProject>
    {
        public List<ComboSelectListItem> AllOpt_DharmaService { get; set; }

        public List<ComboSelectListItem> AllOpt_DonationCategory { get; set; }

        public Opt_DonationProjectVM()
        {
            SetInclude(x => x.DharmaService);
        }

        protected override void InitVM()
        {
            AllOpt_DharmaService = DC.Set<Opt_DharmaService>().GetSelectListItems(Wtm, y => y.ServiceName);
            AllOpt_DonationCategory = DharmaServiceSelectHelper.GetDonationCategoryComboSelectItems();
        }

        public override void DoAdd()
        {
            DC.Set<Opt_DonationProject>().Add(Entity);
            DC.SaveChanges();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            DC.Set<Opt_DonationProject>().Update(Entity);
            DC.SaveChanges();
        }

        public override void DoDelete()
        {
            DC.Set<Opt_DonationProject>().Remove(Entity);
            DC.SaveChanges();
        }
    }
}
