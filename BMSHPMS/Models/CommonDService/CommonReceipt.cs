using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using WalkingTec.Mvvm.Core;
using System.Collections.Generic;
using System.Linq;

namespace BMSHPMS.Models.CommonDService
{
    [Display(Name = "普通收據")]
    [Table("Info_CommonReceipt")]
    public class CommonReceipt : BasePoco
    {
        [Display(Name = "收據號碼")]
        [Required(ErrorMessage = "{0}必填")]
        public string ReceiptNumber { get; set; }

        [Display(Name = "收據日期")]
        [Required(ErrorMessage = "{0}必填")]
        public DateTime? ReceiptDate { get; set; }

        [Display(Name = "收據金額")]
        public int? Sum { get; set; }

        [Display(Name = "聯絡人")]
        public string ContactName { get; set; }

        [Display(Name = "聯絡電話")]
        public string Phone { get; set; }

        [Display(Name = "功德類別")]     
        public string DonationCategory { get; set; }

        [Display(Name = "備註")]
        public string CRemark { get; set; }


        public List<AnnualDabeiInfo> AnnualDabeiInfos { get; set; } = new();
        public List<AnnualLightInfo> AnnualLightInfos { get; set; } = new();

        [NotMapped] 
        [Display(Name = "功德類別")]
        public string[] DonationCategoryArray { get; set; }

        public bool CheckDonationCategory()
        {
            string[] donations = DonationCategory.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in DonationCategoryArray)
            {
                if (donations.Any(x => x.ToLower() == item.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        public string[] GetDonationCategory()
        {
            return DonationCategory.Split(',', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
