using BMSHPMS.Models.GeneralDharmaService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;


namespace BMSHPMS.GeneralManage.ViewModels.GeneralReceiptVMs
{
    public partial class GeneralReceiptVM : BaseCRUDVM<GeneralReceipt>
    {

        public List<GeneralDonor> DonorList { get; set; } = new();

        public List<ComboSelectListItem> DonationCategorySelectItems { get; set; }

        public GeneralReceiptVM()
        {
            SetInclude(x => x.DonorList);
        }

        protected override void InitVM()
        {
            DonationCategorySelectItems = DC.Set<GeneralDonationCategory>().AsNoTracking().GetSelectListItems(Wtm, x => x.CategoryName, y => y.CategoryName);
        }

        public override void DoAdd()
        {
            using var trans = DC.BeginTransaction();

            try
            {
                var id = Guid.NewGuid();
                Entity.ID = id;
                DC.AddEntity(Entity);

                foreach (var item in DonorList)
                {
                    item.ReceiptId = id;
                    DC.AddEntity(item);
                }

                DC.SaveChanges();
                trans.Commit();
            }
            catch (Exception)
            {
                trans.Rollback();
                throw;
            }
            
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            using var trans = DC.BeginTransaction();

            try
            {
                DC.UpdateProperty(Entity,x=>x.ReceiptNumber);
                DC.UpdateProperty(Entity, x => x.ReceiptDate);
                DC.UpdateProperty(Entity, x => x.ContactName);
                DC.UpdateProperty(Entity, x => x.Phone);
                DC.UpdateProperty(Entity, x => x.DonationCategory);

                foreach (var item in DonorList)
                {
                    DC.UpdateProperty(item,x=>x.Name);
                    DC.UpdateProperty(item, x => x.Sum);
                    DC.UpdateProperty(item, x => x.CustomCol1);
                    DC.UpdateProperty(item, x => x.CustomCol2);
                    DC.UpdateProperty(item, x => x.CustomCol3);
                    DC.UpdateProperty(item, x => x.GeneralRemark);
                }

                DC.SaveChanges();
                trans.Commit();
            }
            catch (Exception)
            {
                trans.Rollback();
                throw;
            }
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
