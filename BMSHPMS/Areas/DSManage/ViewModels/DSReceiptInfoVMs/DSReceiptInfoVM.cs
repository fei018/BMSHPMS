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


namespace BMSHPMS.DSManage.ViewModels.DSReceiptInfoVMs
{
    public partial class DSReceiptInfoVM : BaseCRUDVM<DSReceiptInfo>
    {

        public List<DSDonorInfo> DonorInfos { get; set; }

        public List<DSLongevityInfo> LongevityInfos { get; set; }

        public List<DSMemorialInfo> MemorialInfos { get; set; }

        public List<ComboSelectListItem> AllDSProjectName { get; set; }

        public DSReceiptInfoVM()
        {
        }

        protected override void InitVM()
        {
            AllDSProjectName = DC.Set<DServiceProject>().GetSelectListItems(Wtm, x => x.ProjectName, y => y.ProjectName);
        }

        public override void DoAdd()
        {
            DC.Set<DSReceiptInfo>().Add(Entity);
            DC.SaveChanges();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            var old = DC.Set<DSReceiptInfo>().Find(Entity.ID);
            if (old != null)
            {
                old.ContactName = Entity.ContactName;
                old.ReceiptOwn = Entity.ReceiptOwn;
                old.Sum = Entity.Sum;
                old.DSRemark = Entity.DSRemark;
                old.ContactPhone = Entity.ContactPhone;
                old.ReceiptNumber = Entity.ReceiptNumber;
                old.DSProjectName = Entity.DSProjectName;
                old.UpdateBy = LoginUserInfo.Name;
                old.UpdateTime = DateTime.Now;
                old.ReceiptDate = Entity.ReceiptDate;

                DC.UpdateEntity(old);
                DC.SaveChanges();
            }
        }

        public override void DoDelete()
        {
            DC.Set<DSReceiptInfo>().Remove(Entity);
            DC.SaveChanges();
        }

        public async Task InitialDetails()
        {
            DonorInfos = await DC.Set<DSDonorInfo>().Where(q => q.ReceiptInfoID == Entity.ID).OrderBy(q=>q.Sum).ToListAsync();
            LongevityInfos = await DC.Set<DSLongevityInfo>().Where(q => q.ReceiptInfoID == Entity.ID).OrderBy(q => q.Sum).ToListAsync();
            MemorialInfos = await DC.Set<DSMemorialInfo>().Where(q => q.ReceiptInfoID == Entity.ID).OrderBy(q => q.Sum).ToListAsync();
        }
    }
}
