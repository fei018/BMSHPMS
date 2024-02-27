using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_Donor_delVMs
{
    public partial class Info_Donor_delTemplateVM : BaseTemplateVM
    {

	    protected override void InitVM()
        {
        }

    }

    public class Info_Donor_delImportVM : BaseImportVM<Info_Donor_delTemplateVM, Info_Donor_del>
    {

    }

}
