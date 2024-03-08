using BMSHPMS.DSManage.ViewModels.Common;
using BMSHPMS.Models.DharmaService;
using DotLiquid.Util;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.DSReception.ViewModels
{
    public class DSReceiptVM : BaseVM
    {
        [Display(Name = "收據號碼")]
        public string ReceiptNumber { get; set; }


        public Info_Receipt Receipt { get; set; }

        public List<Info_Donor> Donors { get; set; } = new List<Info_Donor>();

        public List<Info_Longevity> Longevitys { get; set; } = new List<Info_Longevity>();

        public List<Info_Memorial> Memorials { get; set; } = new List<Info_Memorial>();

        public string DeleteResult { get; set; }


        public async Task GetVMList()
        {
            Receipt = await DC.Set<Info_Receipt>().Where(x => x.ReceiptNumber.ToUpper() == ReceiptNumber.ToUpper()).FirstOrDefaultAsync();
            if (Receipt != null)
            {
                Donors = await DC.Set<Info_Donor>().Where(x => x.ReceiptID == Receipt.ID).OrderBy(x => x.Sum).ThenBy(x => x.SerialCode).ToListAsync();
                Longevitys = await DC.Set<Info_Longevity>().Where(x => x.ReceiptID == Receipt.ID).OrderBy(x => x.Sum).ThenBy(x => x.SerialCode).ToListAsync();
                Memorials = await DC.Set<Info_Memorial>().Where(x => x.ReceiptID == Receipt.ID).OrderBy(x => x.Sum).ThenBy(x => x.SerialCode).ToListAsync();
            }
            else
            {
                throw new System.Exception("查詢收據為Null.");
            }
        }

        public void Delete()
        {
            Info_ReceiptHelper.ReceiptMoveToDeleteTable(Wtm, Receipt.ID);
            DC.SaveChanges();
            DeleteResult = $"收據：{Receipt.ReceiptNumber} 已刪除.";
        }
    }
}
