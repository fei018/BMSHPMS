using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSMemorialInfoVMs
{
    public partial class DSMemorialInfoBatchVM : BaseBatchVM<DSMemorialInfo, DSMemorialInfo_BatchEdit>
    {
        public DSMemorialInfoBatchVM()
        {
            ListVM = new DSMemorialInfoListVM();
            LinkedVM = new DSMemorialInfo_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class DSMemorialInfo_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
