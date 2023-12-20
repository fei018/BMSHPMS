using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSDonorInfoVMs
{
    public partial class DSDonorInfoBatchVM : BaseBatchVM<DSDonorInfo, DSDonorInfo_BatchEdit>
    {
        public DSDonorInfoBatchVM()
        {
            ListVM = new DSDonorInfoListVM();
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
