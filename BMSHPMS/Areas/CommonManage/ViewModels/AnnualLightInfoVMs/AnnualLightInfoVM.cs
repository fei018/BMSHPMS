using BMSHPMS.Models.CommonDService;
using WalkingTec.Mvvm.Core;


namespace BMSHPMS.CommonManage.ViewModels.AnnualLightInfoVMs
{
    public partial class AnnualLightInfoVM : BaseCRUDVM<AnnualLightInfo>
    {
        public AnnualLightInfoVM()
        {
            SetInclude(x => x.CommonReceipt);
        }

        protected override void InitVM()
        {
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
