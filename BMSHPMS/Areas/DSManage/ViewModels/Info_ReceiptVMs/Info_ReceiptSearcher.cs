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
    public partial class Info_ReceiptSearcher : BaseSearcher
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
        public string DharmaServiceName { get; set; }

        [Display(Name = "法會年份")]
        public int? DharmaServiceYear { get; set; }

        [Display(Name = "已刪除")]
        public bool? ShowDeleted { get; set; }

        public List<ComboSelectListItem> AllDharmaServiceName { get; set; }

        public List<ComboSelectListItem> ShowDeletedSelection { get; set; }

        protected override void InitVM()
        {
            AllDharmaServiceName = DC.Set<Opt_DharmaService>().GetSelectListItems(Wtm, x => x.ServiceName, y => y.ServiceName);

            ShowDeletedSelection = new List<ComboSelectListItem>
            {
                new(){ Text = "已刪除", Value="true"}
            };
        }

    }
}
