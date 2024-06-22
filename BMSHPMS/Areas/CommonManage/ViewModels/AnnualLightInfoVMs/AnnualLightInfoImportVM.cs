﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.CommonDService;


namespace BMSHPMS.CommonManage.ViewModels.AnnualLightInfoVMs
{
    public partial class AnnualLightInfoTemplateVM : BaseTemplateVM
    {

	    protected override void InitVM()
        {
        }

    }

    public class AnnualLightInfoImportVM : BaseImportVM<AnnualLightInfoTemplateVM, AnnualLightInfo>
    {

    }

}
