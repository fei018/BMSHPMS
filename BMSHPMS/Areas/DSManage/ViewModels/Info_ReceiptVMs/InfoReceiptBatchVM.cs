using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_ReceiptVMs
{
    public partial class InfoReceiptBatchVM : BaseBatchVM<Info_Receipt, DSReceiptInfo_BatchEdit>
    {
        public InfoReceiptBatchVM()
        {
            ListVM = new Info_ReceiptListVM();
            LinkedVM = new DSReceiptInfo_BatchEdit();
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
