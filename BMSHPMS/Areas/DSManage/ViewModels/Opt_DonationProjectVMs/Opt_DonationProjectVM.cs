﻿using BMSHPMS.Models.DharmaService;
using BMSHPMS.Models.DharmaServiceExtention;
using System;
using System.Collections.Generic;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;


namespace BMSHPMS.DSManage.ViewModels.Opt_DonationProjectVMs
{
    public partial class Opt_DonationProjectVM : BaseCRUDVM<Opt_DonationProject>
    {
        public List<ComboSelectListItem> AllOpt_DharmaService { get; set; }

        public List<ComboSelectListItem> AllOpt_DonationCategory { get; set; }

        public Opt_DonationProjectVM()
        {
            SetInclude(x => x.DharmaService);
        }

        protected override void InitVM()
        {
            AllOpt_DharmaService = DC.Set<Opt_DharmaService>().GetSelectListItems(Wtm, y => y.ServiceName);
            AllOpt_DonationCategory = DonationProjectOptions.GetCategoryComboSelectItems();
        }

        public override void DoAdd()
        {
            Entity.CreateBy = LoginUserInfo.Name;
            Entity.UpdateBy = LoginUserInfo.Name;
            Entity.CreateTime = DateTime.Now;
            Entity.UpdateTime = DateTime.Now;

            DC.AddEntity(Entity);
            DC.SaveChanges();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            var old = DC.Set<Opt_DonationProject>().Find(Entity.ID);

            if (Entity.Sum.HasValue) old.Sum = Entity.Sum.Value;
            if (!string.IsNullOrEmpty(Entity.SerialCode)) old.SerialCode = Entity.SerialCode;
            if (!string.IsNullOrEmpty(Entity.DonationCategory)) old.DonationCategory = Entity.DonationCategory;
            if (Entity.DharmaServiceID.HasValue) old.DharmaServiceID = Entity.DharmaServiceID;

            old.UpdateBy = LoginUserInfo.Name;
            old.UpdateTime = DateTime.Now;

            DC.UpdateEntity(old);
            DC.SaveChanges();
        }

        public override void DoDelete()
        {
            DC.Set<Opt_DonationProject>().Remove(Entity);
            DC.SaveChanges();
        }

        public void EditUsedNumber()
        {
            var old = DC.Set<Opt_DonationProject>().Find(Entity.ID);
            old.UsedNumber = Entity.UsedNumber;

            DC.UpdateProperty(old, x => x.UsedNumber);
            DC.SaveChanges();
        }
    }
}
