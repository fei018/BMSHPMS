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
    public partial class Info_Longevity_delSearcher : BaseSearcher
    {
        [Display(Name = "姓名")]
        public String Name { get; set; }
        [Display(Name = "金額")]
        public Int32? Sum { get; set; }
        [Display(Name = "延生編號")]
        public String SerialCode { get; set; }
        [Display(Name = "備註")]
        public String DSRemark { get; set; }

        protected override void InitVM()
        {
        }

    }
}
