using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_AutoCompleteVMs
{
    public partial class Info_AutoCompleteBatchVM : BaseBatchVM<Info_AutoComplete, Info_AutoComplete_BatchEdit>
    {
        public Info_AutoCompleteBatchVM()
        {
            ListVM = new Info_AutoCompleteListVM();
            LinkedVM = new Info_AutoComplete_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class Info_AutoComplete_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
