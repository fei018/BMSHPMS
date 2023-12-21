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
            DC.Set<DSMemorialInfo>().Update(Entity);
            DC.SaveChanges();
        }

        public override void DoDelete()
        {
            //base.DoDelete();
        }
    }
}
