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
    public partial class T_LongevityPlaqueSearcher : BaseSearcher
    {
        [Display(Name = "姓名")]
        public String Name { get; set; }
        [Display(Name = "金額")]
        public Int32? Sum { get; set; }
        [Display(Name = "延生編號")]
        public String Serial { get; set; }
        [Display(Name = "備註")]
        public String Remark { get; set; }
        public List<ComboSelectListItem> AllReceipts { get; set; }
        public Guid? ReceiptID { get; set; }

        protected override void InitVM()
        {
            AllReceipts = DC.Set<T_Receipt>().GetSelectListItems(Wtm, y => y.ReceiptNumber);
        }

    }
}
