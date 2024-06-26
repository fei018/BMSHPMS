﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.CommonDService;
using BMSHPMS.CommonManage.ViewModels.CommonReceiptVMs;


namespace BMSHPMS.CommonManage.ViewModels.AnnualDabeiInfoVMs
{
    public partial class AnnualDabeiInfoVM : BaseCRUDVM<AnnualDabeiInfo>
    {
        //public string JSFunc { get; set; }
        public CommonDonationQueryString DonationQueryString { get; set; }

        public AnnualDabeiInfoVM()
        {
            SetInclude(x=>x.CommonReceipt);
        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
