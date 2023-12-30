using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Opt_DonationProjectVMs
{
    public partial class Opt_DonationProjectBatchVM : BaseBatchVM<Opt_DonationProject, DSDonationProject_BatchEdit>
    {
        public Opt_DonationProjectBatchVM()
        {
            ListVM = new Opt_DonationProjectListVM();
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
