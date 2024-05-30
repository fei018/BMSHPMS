using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DabeiDharmaService;


namespace BMSHPMS.CommonManage.ViewModels.DabeiReceiptVMs
{
    public partial class DabeiReceiptTemplateVM : BaseTemplateVM
    {

	    protected override void InitVM()
        {
        }

    }

    public class DabeiReceiptImportVM : BaseImportVM<DabeiReceiptTemplateVM, DabeiReceipt>
    {

    }

}
