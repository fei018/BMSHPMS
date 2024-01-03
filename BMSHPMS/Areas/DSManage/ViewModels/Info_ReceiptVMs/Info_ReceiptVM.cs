using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;
using NetBox.Extensions;
using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions;
using BMSHPMS.Helper;


namespace BMSHPMS.DSManage.ViewModels.Info_ReceiptVMs
{
    public partial class Info_ReceiptVM : BaseCRUDVM<Info_Receipt>
    {

        public List<Info_Donor> DonorInfos { get; set; }

        public List<Info_Longevity> LongevityInfos { get; set; }

        public List<Info_Memorial> MemorialInfos { get; set; }

        public List<ComboSelectListItem> AllOpt_DharmaServiceName { get; set; }

        

        public Info_ReceiptVM()
        {
        }

        protected override void InitVM()
        {
            AllOpt_DharmaServiceName = DC.Set<Opt_DharmaService>().GetSelectListItems(Wtm, x => x.ServiceName, y => y.ServiceName);
        }

        public override void DoAdd()
        {
            DC.Set<Info_Receipt>().Add(Entity);
            DC.SaveChanges();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            var old = DC.Set<Info_Receipt>().Find(Entity.ID);
            if (old != null)
            {
                old.ContactName = Entity.ContactName;
                old.ReceiptOwn = Entity.ReceiptOwn;
                old.Sum = Entity.Sum;
                old.DSRemark = Entity.DSRemark;
                old.ContactPhone = Entity.ContactPhone;
                old.ReceiptNumber = Entity.ReceiptNumber;
                old.DharmaServiceName = Entity.DharmaServiceName;
                old.UpdateBy = LoginUserInfo.Name;
                old.UpdateTime = DateTime.Now;
                old.ReceiptDate = Entity.ReceiptDate;

                DC.UpdateEntity(old);
                DC.SaveChanges();
            }
        }

        public override void DoDelete()
        {
            var dc = DC as DataContext;

            if (Entity.IsDataValid)
            {
                //Entity.IsDataValid = false;
                //Entity.UpdateBy = LoginUserInfo.Name;
                //Entity.UpdateTime = DateTime.Now;
                //DC.UpdateProperty(Entity, x => x.IsDataValid);
                //DC.UpdateProperty(Entity, x => x.UpdateBy);
                //DC.UpdateProperty(Entity, x => x.UpdateTime);

                dc.FakeDeleteEntity(Entity, Wtm);

                // 功德主 標記刪除
                DC.Set<Info_Donor>().Where(x => x.ReceiptID == Entity.ID).ToList().ForEach(x =>
                {
                    dc.FakeDeleteEntity(x, Wtm);
                });

                // 延生 標記刪除
                DC.Set<Info_Longevity>().Where(x => x.ReceiptID == Entity.ID).ToList().ForEach(x =>
                {
                    dc.FakeDeleteEntity(x, Wtm);
                });

                // 附薦 標記刪除
                DC.Set<Info_Memorial>().Where(x => x.ReceiptID == Entity.ID).ToList().ForEach(x =>
                {
                    dc.FakeDeleteEntity(x, Wtm);
                });
            }
            else
            {
                DC.DeleteEntity(Entity);
            }

            DC.SaveChanges();
        }

        public async Task InitialDetails()
        {
            DonorInfos = await DC.Set<Info_Donor>().Where(q => q.ReceiptID == Entity.ID).OrderBy(q => q.Sum).ThenBy(q => q.SerialCode).ToListAsync();
            LongevityInfos = await DC.Set<Info_Longevity>().Where(q => q.ReceiptID == Entity.ID).OrderBy(q => q.Sum).ThenBy(q => q.SerialCode).ToListAsync();
            MemorialInfos = await DC.Set<Info_Memorial>().Where(q => q.ReceiptID == Entity.ID).OrderBy(q => q.Sum).ThenBy(q => q.SerialCode).ToListAsync();
        }

        #region MyRegion

        #endregion
    }
}
