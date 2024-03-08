using BMSHPMS.Models.DharmaService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;


namespace BMSHPMS.DSManage.ViewModels.Info_MemorialVMs
{
    public partial class Info_MemorialSearcher : BaseSearcher
    {
        [Display(Name = "附薦編號")]
        public String SerialCode { get; set; }

        [Display(Name = "附薦編號2")]
        public string SerialCodeEnd { get; set; }

        [Display(Name = "陽居姓名")]
        public String BenefactorName { get; set; }

        [Display(Name = "附薦名稱")]
        public String DeceasedName { get; set; }

        [Display(Name = "金額")]
        public Int32? Sum { get; set; }

        [Display(Name = "收據號碼")]
        public String ReceiptNumber { get; set; }

        [Display(Name = "收據日期")]
        public DateRange ReceiptDate { get; set; }

        [Display(Name = "法會年份")]
        public int? DharmaServiceYear { get; set; }

        [Display(Name = "法會名")]
        public string DharmaServiceName { get; set; }

        public List<ComboSelectListItem> AllDharmaServiceName { get; set; }

        protected override void InitVM()
        {
            AllDharmaServiceName = DC.Set<Opt_DharmaService>().GetSelectListItems(Wtm, x => x.ServiceName, y => y.ServiceName);
        }

    }
}
