using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DServiceProjectVMs
{
    public partial class DServiceProjectTemplateVM : BaseTemplateVM
    {
        [Display(Name = "法會名")]
        public ExcelPropety ProjectName_Excel = ExcelPropety.CreateProperty<DServiceProject>(x => x.ProjectName);
        [Display(Name = "編號代碼")]
        public ExcelPropety ProjectCode_Excel = ExcelPropety.CreateProperty<DServiceProject>(x => x.SerialCode);

	    protected override void InitVM()
        {
        }

    }

    public class DServiceProjectImportVM : BaseImportVM<DServiceProjectTemplateVM, DServiceProject>
    {

    }

}
