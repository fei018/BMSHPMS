using BMSHPMS.Models.DharmaService;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSManage.ViewModels.FindMissingSerialVMs
{
    public class FindSearchVM : BaseVM
    {
        [Display(Name = "法會")]
        [Required(ErrorMessage = "{0}必填")]
        public string DharmaServiceID { get; set; }

        [Display(Name = "法會年份")]
        [Required(ErrorMessage = "{0}必填")]
        public int? DharmaServiceYear { get; set; }

        //[Display(Name = "功德類別")]
        //[Required(ErrorMessage = "{0}必填")]
        //public string DonationProjectCategory { get; set; }

        [Display(Name = "功德項目")]
        [Required(ErrorMessage = "{0}必填")]
        public string DonationProjectID { get; set; }

        public List<ComboSelectListItem> DharmaServiceSelectItems { get; set; }

        protected override void InitVM()
        {
            DharmaServiceSelectItems = DC.Set<Opt_DharmaService>()
                                         .OrderBy(x => x.SerialCode)
                                         .GetSelectListItems(Wtm, x => x.ServiceName, y => y.ID);
        }
    }
}
