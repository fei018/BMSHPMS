using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMSHPMS.DSReception.ViewModels.DSOldRegisterVMs
{
    public class OldRegisterSubmitInfo
    {
        [Display(Name = "收據號碼")]
        [Required(ErrorMessage = "收據號碼必填")]
        public string ReceiptNumber { get; set; }

        [Display(Name = "聯絡人")]
        public string ContactName { get; set; }

        [Display(Name = "聯絡電話")]
        public string ContactPhone { get; set; }

        [Display(Name = "功德主")]
        public List<Guid> DonorIDListSelect { get; set; }

        [Display(Name = "延生位")]
        public List<Guid> LongevityIDListSelect { get; set; }

        [Display(Name = "附薦位")]
        public List<Guid> MemorialIDListSelect { get; set; }
    }
}
