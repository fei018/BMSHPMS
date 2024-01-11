using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;
using BMSHPMS.Helper;


namespace BMSHPMS.DSManage.ViewModels.Opt_DharmaServiceVMs
{
    public partial class Opt_DharmaServiceVM : BaseCRUDVM<Opt_DharmaService>
    {

        public Opt_DharmaServiceVM()
        {
        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {
            if(DC.Set<Opt_DharmaService>().Any(d => d.SerialCode == Entity.SerialCode))
            {
                Wtm.MSD.AddModelError("SerialCode", $"{ToolsHelper.GetDisplayName(()=>Entity.SerialCode)} 已存在.");
                return;
            }

            if (DC.Set<Opt_DharmaService>().Any(d => d.ServiceName == Entity.ServiceName))
            {
                Wtm.MSD.AddModelError("ServiceName", $"{ToolsHelper.GetDisplayName(() => Entity.ServiceName)} 已存在.");
                return;
            }

            Entity.CreateBy = LoginUserInfo.Name;
            Entity.UpdateBy = LoginUserInfo.Name;
            Entity.CreateTime = DateTime.Now;
            Entity.UpdateTime = DateTime.Now;

            DC.Set<Opt_DharmaService>().Add(Entity);
            DC.SaveChanges();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            var old = DC.Set<Opt_DharmaService>().Find(Entity.ID);

            if (!string.IsNullOrEmpty(Entity.ServiceName)) old.ServiceName = Entity.ServiceName;
            if (!string.IsNullOrEmpty(Entity.SerialCode)) old.SerialCode = Entity.SerialCode;

            DC.UpdateEntity(old);
            DC.SaveChanges();
        }

        public override void DoDelete()
        {
            DC.Set<Opt_DharmaService>().Remove(Entity);
            DC.SaveChanges();
        }
    }
}
