using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DabeiDharmaService;


namespace BMSHPMS.CommonManage.ViewModels.DabeiReceiptVMs
{
    public partial class DabeiReceiptBatchVM : BaseBatchVM<DabeiReceipt, DabeiReceipt_BatchEdit>
    {
        public DabeiReceiptBatchVM()
        {
            ListVM = new DabeiReceiptListVM();
            LinkedVM = new DabeiReceipt_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class DabeiReceipt_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
