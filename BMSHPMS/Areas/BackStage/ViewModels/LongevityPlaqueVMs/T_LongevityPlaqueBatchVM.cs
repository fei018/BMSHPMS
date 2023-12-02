using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.Areas.BackStage.ViewModels.T_LongevityPlaqueVMs
{
    public partial class T_LongevityPlaqueBatchVM : BaseBatchVM<T_LongevityPlaque, T_LongevityPlaque_BatchEdit>
    {
        public T_LongevityPlaqueBatchVM()
        {
            ListVM = new T_LongevityPlaqueListVM();
            LinkedVM = new T_LongevityPlaque_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class T_LongevityPlaque_BatchEdit : BaseVM
    {
        [Display(Name = "姓名")]
        public String Name { get; set; }
        [Display(Name = "金額")]
        public Int32? Sum { get; set; }
        [Display(Name = "延生編號")]
        public String Serial { get; set; }
        [Display(Name = "備註")]
        public String PRemark { get; set; }

        protected override void InitVM()
        {
        }

    }

}
