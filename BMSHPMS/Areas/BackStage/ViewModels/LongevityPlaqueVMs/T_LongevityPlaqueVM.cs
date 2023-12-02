using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.Areas.BackStage.ViewModels.T_LongevityPlaqueVMs
{
    public partial class T_LongevityPlaqueVM : BaseCRUDVM<T_LongevityPlaque>
    {
        public List<ComboSelectListItem> AllReceipts { get; set; }

        public T_LongevityPlaqueVM()
        {
            SetInclude(x => x.Receipt);
        }

        protected override void InitVM()
        {
            AllReceipts = DC.Set<T_Receipt>().GetSelectListItems(Wtm, y => y.ReceiptNumber);
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
