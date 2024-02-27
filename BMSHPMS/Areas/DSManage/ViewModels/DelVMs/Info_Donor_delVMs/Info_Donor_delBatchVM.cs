using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_Donor_delVMs
{
    public partial class Info_Donor_delBatchVM : BaseBatchVM<Info_Donor_del, Info_Donor_del_BatchEdit>
    {
        public Info_Donor_delBatchVM()
        {
            ListVM = new Info_Donor_delListVM();
            LinkedVM = new Info_Donor_del_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class Info_Donor_del_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
