﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSLongevityInfoVMs
{
    public partial class DSLongevityInfoVM : BaseCRUDVM<DSLongevityInfo>
    {

        public DSLongevityInfoVM()
        {
            SetInclude(x => x.ReceiptInfo);
        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {
            DC.Set<DSLongevityInfo>().Add(Entity);
            DC.SaveChanges();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            var old = DC.Set<DSLongevityInfo>().Find(Entity.ID);
            if (old != null)
            {
                old.Name = Entity.Name;
                old.DSRemark = Entity.DSRemark;
                old.UpdateBy = LoginUserInfo.Name;
                old.UpdateTime = DateTime.Now;
                DC.UpdateEntity(old);
                DC.SaveChanges();
            }
        }

        public override void DoDelete()
        {
            DC.Set<DSLongevityInfo>().Remove(Entity);
            DC.SaveChanges();
        }
    }
}
