using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_Memorial_delVMs
{
    public partial class Info_Memorial_delBatchVM : BaseBatchVM<Info_Memorial_del, Info_Memorial_del_BatchEdit>
    {
        public Info_Memorial_delBatchVM()
        {
            ListVM = new Info_Memorial_delListVM();
            LinkedVM = new Info_Memorial_del_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class Info_Memorial_del_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
