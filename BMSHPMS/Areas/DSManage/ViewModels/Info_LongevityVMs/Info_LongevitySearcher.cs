using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;


namespace BMSHPMS.DSManage.ViewModels.Info_LongevityVMs
{
    public partial class Info_LongevitySearcher : BaseSearcher
    {
        [Display(Name = "姓名")]
        public String Name { get; set; }

        [Display(Name = "金額")]
        public Int32? Sum { get; set; }

        [Display(Name = "延生編號")]
        public string SerialCode { get; set; }

        [Display(Name = "延生編號2")]
        public string SerialCodeEnd { get; set; }

        [Display(Name = "收據號碼")]
        public String ReceiptNumber { get; set; }

        [Display(Name = "收據日期")]
        public DateRange ReceiptDate { get; set; }

        [Display(Name = "法會年份")]
        public int? DharmaServiceYear { get; set; }

        [Display(Name = "法會名")]
        public string DharmaServiceName { get; set; }

        [Display(Name = "備註")]
        public string DSRemark { get; set; }

        public List<ComboSelectListItem> AllDharmaServiceName { get; set; }

        protected override void InitVM()
        {
            AllDharmaServiceName = DC.Set<Opt_DharmaService>().GetSelectListItems(Wtm, x => x.ServiceName, y => y.ServiceName);
        }

    }
}
