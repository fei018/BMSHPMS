using BMSHPMS.DSManage.ViewModels.Common;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;


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
                if (Entity.DharmaServiceYear.HasValue) old.DharmaServiceYear = Entity.DharmaServiceYear;
                if (!string.IsNullOrEmpty(Entity.DharmaServiceName)) old.DharmaServiceName = Entity.DharmaServiceName;
                if (!string.IsNullOrEmpty(Entity.ReceiptNumber)) old.ReceiptNumber = Entity.ReceiptNumber;
                if (Entity.Sum.HasValue) old.Sum = Entity.Sum;
                if (!string.IsNullOrEmpty(Entity.ReceiptOwn)) old.ReceiptOwn = Entity.ReceiptOwn;
                if (!string.IsNullOrEmpty(Entity.ContactName)) old.ContactName = Entity.ContactName;
                if (!string.IsNullOrEmpty(Entity.ContactPhone)) old.ContactPhone = Entity.ContactPhone;
                if (Entity.ReceiptDate.HasValue) old.ReceiptDate = Entity.ReceiptDate;
                if (!string.IsNullOrEmpty(Entity.DSRemark)) old.DSRemark = Entity.DSRemark;

                old.UpdateBy = LoginUserInfo.Name;
                old.UpdateTime = DateTime.Now;
                old.ReceiptDate = Entity.ReceiptDate;

                DC.UpdateEntity(old);
                DC.SaveChanges();
            }
        }

        public override void DoDelete()
        {
            Info_ReceiptHelper.ReceiptMoveToDeleteTable(Wtm, Entity.ID);
        }

        public async Task InitialDetails()
        {
            DonorInfos = await DC.Set<Info_Donor>().Where(q => q.ReceiptID == Entity.ID).OrderBy(q => q.Sum).ThenBy(q => q.SerialCode).ToListAsync();
            LongevityInfos = await DC.Set<Info_Longevity>().Where(q => q.ReceiptID == Entity.ID).OrderBy(q => q.Sum).ThenBy(q => q.SerialCode).ToListAsync();
            MemorialInfos = await DC.Set<Info_Memorial>().Where(q => q.ReceiptID == Entity.ID).OrderBy(q => q.Sum).ThenBy(q => q.SerialCode).ToListAsync();
        }
    }
}
