using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.GeneralDharmaService;


namespace BMSHPMS.GeneralManage.ViewModels.GeneralReceiptVMs
{
    public partial class GeneralReceiptBatchVM : BaseBatchVM<GeneralReceipt, GeneralReceipt_BatchEdit>
    {
        public GeneralReceiptBatchVM()
        {
            ListVM = new GeneralReceiptListVM();
            LinkedVM = new GeneralReceipt_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class GeneralReceipt_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
