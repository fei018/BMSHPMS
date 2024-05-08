using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_AutoCompleteVMs
{
    public partial class Info_AutoCompleteSearcher : BaseSearcher
    {
        public String Content { get; set; }

        protected override void InitVM()
        {
        }

    }
}
