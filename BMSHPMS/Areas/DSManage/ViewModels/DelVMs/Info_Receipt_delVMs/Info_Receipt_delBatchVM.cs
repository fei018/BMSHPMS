using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_Receipt_delVMs
{
    public partial class Info_Receipt_delBatchVM : BaseBatchVM<Info_Receipt_del, Info_Receipt_del_BatchEdit>
    {
        public Info_Receipt_delBatchVM()
        {
            ListVM = new Info_Receipt_delListVM();
            LinkedVM = new Info_Receipt_del_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class Info_Receipt_del_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
