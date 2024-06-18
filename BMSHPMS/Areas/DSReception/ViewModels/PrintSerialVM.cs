using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.DSReception.ViewModels
{
    public class PrintSerialVM : BaseVM
    {
        public Info_Receipt Receipt { get; set; }

        public List<Info_Donor> Donors { get; set; }

        public List<Info_Longevity> Longevitys { get; set; }

        public List<Info_Memorial> Memorials { get; set; }

        public async Task GetSerials(string receiptNumber)
        {
            Donors = new List<Info_Donor>();
            Longevitys = new List<Info_Longevity>();
            Memorials = new List<Info_Memorial>();

            Receipt = DC.Set<Info_Receipt>().AsNoTracking()
                                            .Where(r => r.ReceiptNumber == receiptNumber)
                                            .FirstOrDefault();

            if (Receipt != null)
            {
                Donors = await DC.Set<Info_Donor>().Where(q => q.ReceiptID == Receipt.ID).OrderBy(q => q.Sum).ThenBy(x => x.SerialCode).ToListAsync();
                Longevitys = await DC.Set<Info_Longevity>().Where(q => q.ReceiptID == Receipt.ID).OrderBy(q => q.Sum).ThenBy(x => x.SerialCode).ToListAsync();
                Memorials = await DC.Set<Info_Memorial>().Where(q => q.ReceiptID == Receipt.ID).OrderBy(q => q.Sum).ThenBy(x => x.SerialCode).ToListAsync();
            }
            else
            {
                throw new Exception("收據號碼查詢為Null: " + receiptNumber);
            }
        }
    }
}
