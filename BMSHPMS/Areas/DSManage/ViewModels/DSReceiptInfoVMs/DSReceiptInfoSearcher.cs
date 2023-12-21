using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSReceiptInfoVMs
{
    public partial class DSReceiptInfoSearcher : BaseSearcher
    {
        [Display(Name = "收據號碼")]
        public String ReceiptNumber { get; set; }

        [Display(Name = "收據人姓名")]
        public String ReceiptOwn { get; set; }

        [Display(Name = "聯絡人姓名")]
        public String ContactName { get; set; }

        [Display(Name = "聯絡人電話")]
        public String ContactPhone { get; set; }

        [Display(Name = "金額")]
        public Int32? Sum { get; set; }

        [Display(Name = "收據日期")]
        public DateTime? ReceiptDate { get; set; }

        [Display(Name = "法會項目")]
        public string DSProjectName { get; set; }

        public List<ComboSelectListItem> AllDSProjectName { get; set; }

        protected override void InitVM()
        {
            AllDSProjectName = DC.Set<DServiceProject>().GetSelectListItems(Wtm, x => x.ProjectName, y => y.ProjectName);
        }

    }
}
