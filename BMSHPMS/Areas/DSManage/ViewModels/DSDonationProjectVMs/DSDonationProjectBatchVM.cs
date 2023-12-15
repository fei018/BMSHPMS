using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSDonationProjectVMs
{
    public partial class DSDonationProjectBatchVM : BaseBatchVM<DSDonationProject, DSDonationProject_BatchEdit>
    {
        public DSDonationProjectBatchVM()
        {
            ListVM = new DSDonationProjectListVM();
            LinkedVM = new DSDonationProject_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class DSDonationProject_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
