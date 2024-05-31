using BMSHPMS.Models.GeneralDharmaService;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;


namespace BMSHPMS.GeneralManage.ViewModels.GeneralReceiptVMs
{
    public partial class GeneralReceiptVM : BaseCRUDVM<GeneralReceipt>
    {

        public List<GeneralDonor> DonorList { get; set; } = new();

        public List<ComboSelectListItem> DonationCategory { get; set; }

        public GeneralReceiptVM()
        {
        }

        protected override void InitVM()
        {
            DonationCategory = DC.Set<GeneralDonationCategory>().AsNoTracking().GetSelectListItems(Wtm, x => x.CategoryName, y => y.CategoryName);
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
