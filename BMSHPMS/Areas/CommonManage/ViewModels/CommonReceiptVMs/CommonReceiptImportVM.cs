using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.CommonDService;


namespace BMSHPMS.CommonManage.ViewModels.CommonReceiptVMs
{
    public partial class CommonReceiptTemplateVM : BaseTemplateVM
    {

	    protected override void InitVM()
        {
        }

    }

    public class CommonReceiptImportVM : BaseImportVM<CommonReceiptTemplateVM, CommonReceipt>
    {

    }

}
