using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSDonorInfoVMs
{
    public partial class DSDonorInfoVM : BaseCRUDVM<DSDonorInfo>
    {

        public DSDonorInfoVM()
        {
            SetInclude(x => x.ReceiptInfo);
        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {
            DC.Set<DSDonorInfo>().Add(Entity);
            DC.SaveChanges();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            var old = DC.Set<DSDonorInfo>().Find(Entity.ID);
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
            DC.Set<DSDonorInfo>().Remove(Entity);
            DC.SaveChanges();
        }
    }
}
