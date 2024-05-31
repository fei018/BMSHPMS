using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.GeneralDharmaService;


namespace BMSHPMS.GeneralManage.ViewModels.GeneralDonorVMs
{
    public partial class GeneralDonorBatchVM : BaseBatchVM<GeneralDonor, GeneralDonor_BatchEdit>
    {
        public GeneralDonorBatchVM()
        {
            ListVM = new GeneralDonorListVM();
            LinkedVM = new GeneralDonor_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class GeneralDonor_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
