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


        public DSReceiptInfoVM()
        {
        }

        protected override void InitVM()
        {
            base.InitVM();
        }

        public override void DoAdd()
        {           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }

        public async Task InitialDetails()
        {
            DonorInfos = await DC.Set<DSDonorInfo>().Where(q => q.ReceiptInfoID == Entity.ID).ToListAsync();
            LongevityInfos = await DC.Set<DSLongevityInfo>().Where(q => q.ReceiptInfoID == Entity.ID).ToListAsync();
            MemorialInfos = await DC.Set<DSMemorialInfo>().Where(q => q.ReceiptInfoID == Entity.ID).ToListAsync();

            this.SortList();
        }

        private void SortList()
        {
            DonorInfos?.Sort((x, y) => x.SerialCode.CompareTo(y.SerialCode));
            DonorInfos.Sort((x, y) => x.Sum.Value.CompareTo(y.Sum.Value));

            LongevityInfos?.Sort((x, y) => x.SerialCode.CompareTo(y.SerialCode));
            LongevityInfos.Sort((x, y) => x.Sum.Value.CompareTo(y.Sum.Value));

            MemorialInfos?.Sort((x, y) => x.SerialCode.CompareTo(y.SerialCode));
            MemorialInfos.Sort((x, y) => x.Sum.Value.CompareTo(y.Sum.Value));
        }
    }
}
