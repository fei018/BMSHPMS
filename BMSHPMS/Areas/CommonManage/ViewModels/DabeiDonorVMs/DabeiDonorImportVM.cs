using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DabeiDharmaService;


namespace BMSHPMS.CommonManage.ViewModels.DabeiDonorVMs
{
    public partial class DabeiDonorTemplateVM : BaseTemplateVM
    {

	    protected override void InitVM()
        {
        }

    }

    public class DabeiDonorImportVM : BaseImportVM<DabeiDonorTemplateVM, DabeiDonor>
    {

    }

}
