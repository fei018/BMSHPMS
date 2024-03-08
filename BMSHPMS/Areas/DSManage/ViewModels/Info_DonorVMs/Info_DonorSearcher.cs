using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_DonorVMs
{
    public partial class Info_DonorSearcher : BaseSearcher
    {
        [Display(Name = "延生位姓名")]
        public String LongevityName { get; set; }
        [Display(Name = "附薦名稱")]
        public String DeceasedName { get; set; }
        [Display(Name = "陽居姓名")]
        public String BenefactorName { get; set; }
        [Display(Name = "金額")]
        public Int32? Sum { get; set; }
        [Display(Name = "功德主編號")]
        public String SerialCode { get; set; }

        [Display(Name = "功德主編號2")]
        public string SerialCodeEnd { get; set; }

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
