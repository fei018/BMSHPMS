using BMSHPMS.DSManage.ViewModels.Common;
using BMSHPMS.Models.DharmaService;
using System;
using WalkingTec.Mvvm.Core;


namespace BMSHPMS.DSManage.ViewModels.Info_ReceiptVMs
{
    public partial class InfoReceiptBatchVM : BaseBatchVM<Info_Receipt, DSReceiptInfo_BatchEdit>
    {
        public InfoReceiptBatchVM()
        {
            ListVM = new Info_ReceiptListVM();
            LinkedVM = new DSReceiptInfo_BatchEdit();
        }


        public override bool DoBatchDelete()
        {
            try
            {
                foreach (var item in Ids)
                {
                    if (Guid.TryParse(item, out Guid id))
                    {
                        Info_ReceiptHelper.ReceiptMoveToDeleteTable(Wtm, id);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MSD.AddModelError("", ex.Message);
                return false;
            }
        }

    }

    /// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class DSReceiptInfo_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
