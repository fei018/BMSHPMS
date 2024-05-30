using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DabeiDharmaService;


namespace BMSHPMS.CommonManage.ViewModels.DabeiDonorVMs
{
    public partial class DabeiDonorVM : BaseCRUDVM<DabeiDonor>
    {
        public List<ComboSelectListItem> AllReceipts { get; set; }

        public DabeiDonorVM()
        {
            SetInclude(x => x.Receipt);
        }

        protected override void InitVM()
        {
            AllReceipts = DC.Set<DabeiReceipt>().GetSelectListItems(Wtm, y => y.ContactName);
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
