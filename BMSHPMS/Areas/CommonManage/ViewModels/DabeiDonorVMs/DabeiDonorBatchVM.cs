using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DabeiDharmaService;


namespace BMSHPMS.CommonManage.ViewModels.DabeiDonorVMs
{
    public partial class DabeiDonorBatchVM : BaseBatchVM<DabeiDonor, DabeiDonor_BatchEdit>
    {
        public DabeiDonorBatchVM()
        {
            ListVM = new DabeiDonorListVM();
            LinkedVM = new DabeiDonor_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class DabeiDonor_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
