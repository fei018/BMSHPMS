using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.CommonDService;


namespace BMSHPMS.CommonManage.ViewModels.AnnualLightInfoVMs
{
    public partial class AnnualLightInfoBatchVM : BaseBatchVM<AnnualLightInfo, AnnualLightInfo_BatchEdit>
    {
        public AnnualLightInfoBatchVM()
        {
            ListVM = new AnnualLightInfoListVM();
            LinkedVM = new AnnualLightInfo_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class AnnualLightInfo_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
