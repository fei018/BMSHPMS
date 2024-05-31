using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.GeneralDharmaService;


namespace BMSHPMS.GeneralManage.ViewModels.GeneralDonorVMs
{
    public partial class GeneralDonorTemplateVM : BaseTemplateVM
    {
        [Display(Name = "功德芳名")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<GeneralDonor>(x => x.Name);
        [Display(Name = "功德金額")]
        public ExcelPropety Sum_Excel = ExcelPropety.CreateProperty<GeneralDonor>(x => x.Sum);
        [Display(Name = "備註")]
        public ExcelPropety GeneralRemark_Excel = ExcelPropety.CreateProperty<GeneralDonor>(x => x.GeneralRemark);

	    protected override void InitVM()
        {
        }

    }

    public class GeneralDonorImportVM : BaseImportVM<GeneralDonorTemplateVM, GeneralDonor>
    {

    }

}
