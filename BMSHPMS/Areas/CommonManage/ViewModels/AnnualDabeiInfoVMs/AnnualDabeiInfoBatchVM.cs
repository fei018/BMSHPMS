using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.CommonDService;


namespace BMSHPMS.CommonManage.ViewModels.AnnualDabeiInfoVMs
{
    public partial class AnnualDabeiInfoBatchVM : BaseBatchVM<AnnualDabeiInfo, AnnualDabeiInfo_BatchEdit>
    {
        public AnnualDabeiInfoBatchVM()
        {
            ListVM = new AnnualDabeiInfoListVM();
            LinkedVM = new AnnualDabeiInfo_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class AnnualDabeiInfo_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
