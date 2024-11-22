using BMSHPMS.Helper;
using BMSHPMS.Models.DharmaService;
using System;
using System.Linq;
using WalkingTec.Mvvm.Core;


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
            if (DC.Set<Opt_DharmaService>().Any(d => d.SerialCode == Entity.SerialCode))
            {
                Wtm.MSD.AddModelError("SerialCode", $"{ToolsHelper.GetDisplayName(() => Entity.SerialCode)} 已存在.");
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
            if (!string.IsNullOrEmpty(Entity.ServiceDateDescription)) old.ServiceDateDescription = Entity.ServiceDateDescription;
            if (!string.IsNullOrEmpty(Entity.ServiceOrganizer)) old.ServiceOrganizer = Entity.ServiceOrganizer;

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
