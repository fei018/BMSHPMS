using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_Longevity_delVMs
{
    public partial class Info_Longevity_delTemplateVM : BaseTemplateVM
    {

	    protected override void InitVM()
        {
        }

    }

    public class Info_Longevity_delImportVM : BaseImportVM<Info_Longevity_delTemplateVM, Info_Longevity_del>
    {

    }

}
