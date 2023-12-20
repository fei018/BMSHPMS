using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSMemorialInfoVMs
{
    public partial class DSMemorialInfoSearcher : BaseSearcher
    {
        [Display(Name = "附薦編號")]
        public String SerialCode { get; set; }
        [Display(Name = "陽居姓名")]
        public String BenefactorName { get; set; }
        [Display(Name = "附薦宗親名及稱呼")]
        public String DeceasedName { get; set; }
        [Display(Name = "金額")]
        public Int32? Sum { get; set; }

        [Display(Name = "收據號碼")]
        public String ReceiptNumber { get; set; }

        protected override void InitVM()
        {
        }

    }
}
