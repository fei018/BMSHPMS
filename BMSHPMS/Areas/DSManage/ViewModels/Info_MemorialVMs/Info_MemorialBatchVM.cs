using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_MemorialVMs
{
    public partial class Info_MemorialBatchVM : BaseBatchVM<Info_Memorial, DSMemorialInfo_BatchEdit>
    {
        public Info_MemorialBatchVM()
        {
            ListVM = new Info_MemorialListVM();
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
