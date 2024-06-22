using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.CommonDService;


namespace BMSHPMS.CommonManage.ViewModels.CommonReceiptVMs
{
    public partial class CommonReceiptBatchVM : BaseBatchVM<CommonReceipt, CommonReceipt_BatchEdit>
    {
        public CommonReceiptBatchVM()
        {
            ListVM = new CommonReceiptListVM();
            LinkedVM = new CommonReceipt_BatchEdit();
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
