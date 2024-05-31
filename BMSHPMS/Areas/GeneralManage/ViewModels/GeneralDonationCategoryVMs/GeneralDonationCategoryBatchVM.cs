using BMSHPMS.Models.GeneralDharmaService;
using WalkingTec.Mvvm.Core;


namespace BMSHPMS.GeneralManage.ViewModels.GeneralDonationCategoryVMs
{
    public partial class GeneralDonationCategoryBatchVM : BaseBatchVM<GeneralDonationCategory, GeneralDonationCategory_BatchEdit>
    {
        public GeneralDonationCategoryBatchVM()
        {
            ListVM = new GeneralDonationCategoryListVM();
            LinkedVM = new GeneralDonationCategory_BatchEdit();
        }

    }

    /// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class GeneralDonationCategory_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
