﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_Donor_delVMs
{
    public partial class Info_Donor_delSearcher : BaseSearcher
    {
        [Display(Name = "延生位姓名")]
        public String LongevityName { get; set; }
        [Display(Name = "附薦宗親名及稱呼_1")]
        public String DeceasedName_1 { get; set; }
        [Display(Name = "附薦宗親名及稱呼_2")]
        public String DeceasedName_2 { get; set; }
        [Display(Name = "附薦宗親名及稱呼_3")]
        public String DeceasedName_3 { get; set; }
        [Display(Name = "陽居姓名")]
        public String BenefactorName { get; set; }
        [Display(Name = "金額")]
        public Int32? Sum { get; set; }
        [Display(Name = "功德主編號")]
        public String SerialCode { get; set; }

        protected override void InitVM()
        {
        }

    }
}