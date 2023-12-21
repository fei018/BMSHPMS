using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;
using BMSHPMS.Helper;


namespace BMSHPMS.DSManage.ViewModels.DServiceProjectVMs
{
    public partial class DServiceProjectVM : BaseCRUDVM<DServiceProject>
    {

        public DServiceProjectVM()
        {
        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {
            if(DC.Set<DServiceProject>().Any(d => d.SerialCode == Entity.SerialCode))
            {
                Wtm.MSD.AddModelError("ProjectCode", $"{ToolsHelper.GetDisplayName(()=>Entity.SerialCode)} 已存在.");
                return;
            }

            if (DC.Set<DServiceProject>().Any(d => d.ProjectName == Entity.ProjectName))
            {
                Wtm.MSD.AddModelError("ProjectName", $"{ToolsHelper.GetDisplayName(() => Entity.ProjectName)} 已存在.");
                return;
            }

            DC.Set<DServiceProject>().Add(Entity);
            DC.SaveChanges();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            DC.Set<DServiceProject>().Update(Entity);
            DC.SaveChanges();
        }

        public override void DoDelete()
        {
            DC.Set<DServiceProject>().Remove(Entity);
            DC.SaveChanges();
        }
    }
}
