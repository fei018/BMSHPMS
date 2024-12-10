using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Opt_DharmaServiceVMs
{
    public partial class Opt_DharmaServiceBatchVM : BaseBatchVM<Opt_DharmaService, DServiceProject_BatchEdit>
    {
        public Opt_DharmaServiceBatchVM()
        {
            ListVM = new Opt_DharmaServiceListVM();
            LinkedVM = new DServiceProject_BatchEdit();
        }

        public override bool DoBatchEdit()
        {

            return base.DoBatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class DServiceProject_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

        [Display(Name = "法會啓用")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public bool Enable { get; set; } = false;



    }

}
