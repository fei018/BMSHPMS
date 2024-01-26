using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_Longevity_delVMs
{
    public partial class Info_Longevity_delBatchVM : BaseBatchVM<Info_Longevity_del, Info_Longevity_del_BatchEdit>
    {
        public Info_Longevity_delBatchVM()
        {
            ListVM = new Info_Longevity_delListVM();
            LinkedVM = new Info_Longevity_del_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class Info_Longevity_del_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
