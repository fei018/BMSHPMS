using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSLongevityInfoVMs
{
    public partial class DSLongevityInfoBatchVM : BaseBatchVM<DSLongevityInfo, DSLongevityInfo_BatchEdit>
    {
        public DSLongevityInfoBatchVM()
        {
            ListVM = new DSLongevityInfoListVM();
            LinkedVM = new DSLongevityInfo_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class DSLongevityInfo_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
