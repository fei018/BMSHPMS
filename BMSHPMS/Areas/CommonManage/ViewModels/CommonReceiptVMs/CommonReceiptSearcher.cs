using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;


namespace BMSHPMS.CommonManage.ViewModels.CommonReceiptVMs
{
    public partial class CommonReceiptSearcher : BaseSearcher
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
        public string[] DonationCategory { get; set; }

        [Display(Name = "備註")]
        public String CRemark { get; set; }

        public List<ComboSelectListItem> DonationCategorySelectItems { get; set; }

        protected override void InitVM()
        {
            DonationCategorySelectItems = CommonDonateCategoryHelper.SelectItems();
        }

    }
}
