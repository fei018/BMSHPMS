using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.GeneralDharmaService;


namespace BMSHPMS.GeneralManage.ViewModels.GeneralReceiptVMs
{
    public partial class GeneralReceiptTemplateVM : BaseTemplateVM
    {

	    protected override void InitVM()
        {
        }

    }

    public class GeneralReceiptImportVM : BaseImportVM<GeneralReceiptTemplateVM, GeneralReceipt>
    {

    }

}
