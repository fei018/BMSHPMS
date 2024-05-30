using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DabeiDharmaService;


namespace BMSHPMS.CommonManage.ViewModels.DabeiReceiptVMs
{
    public partial class DabeiReceiptSearcher : BaseSearcher
    {
        [Display(Name = "聯絡人")]
        public String ContactName { get; set; }
        [Display(Name = "聯絡電話")]
        public String Phone { get; set; }

        protected override void InitVM()
        {
        }

    }
}
