using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.CommonDService;


namespace BMSHPMS.CommonManage.ViewModels.AnnualLightInfoVMs
{
    public partial class AnnualLightInfoVM : BaseCRUDVM<AnnualLightInfo>
    {
        public List<ComboSelectListItem> AllCommonReceipts { get; set; }

        public AnnualLightInfoVM()
        {
            SetInclude(x => x.CommonReceipt);
        }

        protected override void InitVM()
        {
            AllCommonReceipts = DC.Set<CommonReceipt>().GetSelectListItems(Wtm, y => y.ReceiptNumber);
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
