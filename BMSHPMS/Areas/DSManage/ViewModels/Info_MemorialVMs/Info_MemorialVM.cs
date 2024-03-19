﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_MemorialVMs
{
    public partial class Info_MemorialVM : BaseCRUDVM<Info_Memorial>
    {

        public Info_MemorialVM()
        {
            SetInclude(x => x.Receipt);
        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {           
            //base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            var old = DC.Set<Info_Memorial>().Find(Entity.ID);
            if (old != null)
            {
                old.BenefactorName = Entity.BenefactorName;
                old.DeceasedName_1 = Entity.DeceasedName_1;
                old.DeceasedName_2 = Entity.DeceasedName_2;
                old.DeceasedName_3 = Entity.DeceasedName_3;
                old.DSRemark = Entity.DSRemark;
                if(Entity.Sum.HasValue) old.Sum = Entity.Sum.Value;

                old.UpdateBy = LoginUserInfo.Name;
                old.UpdateTime = DateTime.Now;
                DC.UpdateEntity(old);
                DC.SaveChanges();
            }
        }

        public override void DoDelete()
        {
            //base.DoDelete();
        }
    }
}
