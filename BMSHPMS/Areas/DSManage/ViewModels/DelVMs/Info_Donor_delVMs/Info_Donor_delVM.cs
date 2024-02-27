using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_Donor_delVMs
{
    public partial class Info_Donor_delVM : BaseCRUDVM<Info_Donor_del>
    {
        public List<ComboSelectListItem> AllReceipt_dels { get; set; }

        public Info_Donor_delVM()
        {
            SetInclude(x => x.Receipt_del);
        }

        protected override void InitVM()
        {
            AllReceipt_dels = DC.Set<Info_Receipt_del>().GetSelectListItems(Wtm, y => y.ContactName);
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
