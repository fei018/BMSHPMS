using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_Receipt_delVMs
{
    public partial class Info_Receipt_delTemplateVM : BaseTemplateVM
    {

	    protected override void InitVM()
        {
        }

    }

    public class Info_Receipt_delImportVM : BaseImportVM<Info_Receipt_delTemplateVM, Info_Receipt_del>
    {

    }

}
