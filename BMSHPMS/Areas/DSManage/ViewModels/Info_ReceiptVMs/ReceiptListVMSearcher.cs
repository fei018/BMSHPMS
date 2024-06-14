using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_ReceiptVMs
{
    public partial class ReceiptListVMSearcher : BaseSearcher
    {
        [Display(Name = "收據號碼Id")]
        public Guid ReceiptID { get; set; }

        protected override void InitVM()
        {
        }

    }
}
