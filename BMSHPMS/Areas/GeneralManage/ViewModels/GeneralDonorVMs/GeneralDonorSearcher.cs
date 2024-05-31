using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.GeneralDharmaService;


namespace BMSHPMS.GeneralManage.ViewModels.GeneralDonorVMs
{
    public partial class GeneralDonorSearcher : BaseSearcher
    {
        [Display(Name = "功德芳名")]
        public String Name { get; set; }
        [Display(Name = "功德金額")]
        public Int32? Sum { get; set; }
        [Display(Name = "備註")]
        public String GeneralRemark { get; set; }

        protected override void InitVM()
        {
        }

    }
}
