using BMSHPMS.Models.CommonDService;
using System;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;


namespace BMSHPMS.CommonManage.ViewModels.CommonReceiptVMs
{
    public partial class CommonReceiptBatchVM : BaseBatchVM<CommonReceipt, CommonReceipt_BatchEdit>
    {
        public CommonReceiptBatchVM()
        {
            ListVM = new CommonReceiptListVM();
            LinkedVM = new CommonReceipt_BatchEdit();
        }

        public override bool DoBatchDelete()
        {
            try
            {
                if (Ids.Length > 0)
                {
                    var entitys = DC.Set<CommonReceipt>().CheckIDs(Ids.ToList(), x => x.ID).ToList();

                    //foreach (var item in entitys)
                    //{
                    //    switch (item.DonationCategory)
                    //    {
                    //        case CommonDonateCategoryEnum.全年大悲法會:

                    //            break;
                    //        case CommonDonateCategoryEnum.全年光明燈:
                    //            break;
                    //        default:
                    //            break;
                    //    }
                    //}
                    return true;
                }
                else
                {
                    MSD.AddModelError("Ids", "Ids is 0");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MSD.AddModelError("Exception", ex.GetBaseException().Message);
                return false;
            }
        }
    }

    /// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class CommonReceipt_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
