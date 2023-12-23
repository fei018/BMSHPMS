using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSMemorialInfoVMs
{
    public partial class DSMemorialInfoVM : BaseCRUDVM<DSMemorialInfo>
    {

        public DSMemorialInfoVM()
        {
            SetInclude(x => x.ReceiptInfo);
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
            var old = DC.Set<DSMemorialInfo>().Find(Entity.ID);
            if (old != null)
            {
                old.BenefactorName = Entity.BenefactorName;
                old.DeceasedName = Entity.DeceasedName;
                old.DSRemark = Entity.DSRemark;
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
