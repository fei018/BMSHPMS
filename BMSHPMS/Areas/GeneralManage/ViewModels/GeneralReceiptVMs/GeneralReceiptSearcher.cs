using BMSHPMS.Models.GeneralDharmaService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.GeneralManage.ViewModels.GeneralReceiptVMs
{
    public partial class GeneralReceiptSearcher : BaseSearcher
    {
        [Display(Name = "收據號碼")]
        public String ReceiptNumber { get; set; }
        [Display(Name = "收據日期")]
        public DateRange ReceiptDate { get; set; }
        [Display(Name = "聯絡人")]
        public String ContactName { get; set; }
        [Display(Name = "聯絡電話")]
        public String Phone { get; set; }
        [Display(Name = "功德類別")]
        public String DonationCategory { get; set; }

        //====

        public List<ComboSelectListItem> GeneralDonationSelectList { get; set; }

        protected override void InitVM()
        {
            GeneralDonationSelectList = DC.Set<GeneralDonationCategory>().AsNoTracking().GetSelectListItems(Wtm, x => x.CategoryName, y => y.CategoryName);
        }

    }
}
