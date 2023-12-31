﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_DonorVMs
{
    public partial class Info_DonorVM : BaseCRUDVM<Info_Donor>
    {

        public Info_DonorVM()
        {
            SetInclude(x => x.Receipt);
        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {
            DC.Set<Info_Donor>().Add(Entity);
            DC.SaveChanges();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            var old = DC.Set<Info_Donor>().Find(Entity.ID);
            if (old != null)
            {
                old.BenefactorName = Entity.BenefactorName;
                old.DeceasedName = Entity.DeceasedName;
                old.LongevityName = Entity.LongevityName;
                old.DSRemark = Entity.DSRemark;
                old.UpdateBy = LoginUserInfo.Name;
                old.UpdateTime = DateTime.Now;
                DC.UpdateEntity(old);
                DC.SaveChanges();
            }          
        }

        public override void DoDelete()
        {
            DC.Set<Info_Donor>().Remove(Entity);
            DC.SaveChanges();
        }
    }
}
