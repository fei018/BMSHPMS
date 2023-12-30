using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_DonorVMs
{
    public partial class Info_DonorBatchVM : BaseBatchVM<Info_Donor, DSDonorInfo_BatchEdit>
    {
        public Info_DonorBatchVM()
        {
            ListVM = new Info_DonorListVM();
            LinkedVM = new DSDonorInfo_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class DSDonorInfo_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
