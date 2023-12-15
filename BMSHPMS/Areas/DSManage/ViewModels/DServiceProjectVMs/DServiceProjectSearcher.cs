using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DServiceProjectVMs
{
    public partial class DServiceProjectSearcher : BaseSearcher
    {
        [Display(Name = "法會名")]
        public String ProjectName { get; set; }
        [Display(Name = "編號代碼")]
        public String ProjectCode { get; set; }

        protected override void InitVM()
        {
        }

    }
}
