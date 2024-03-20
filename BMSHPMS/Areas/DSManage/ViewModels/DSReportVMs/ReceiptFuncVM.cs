using BMSHPMS.DSManage.ViewModels.Common;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.DSManage.ViewModels.DSReportVMs
{
    public class ReceiptFuncVM : BaseVM
    {
        public async Task<byte[]> ExportExcelByToday()
        {
            List<Info_Receipt> receiptList;

            receiptList = await DC.Set<Info_Receipt>().Where(q => q.ReceiptDate.Value.Date == DateTime.Today).ToListAsync();

            if (receiptList == null || receiptList.Count <= 0)
            {
                throw new Exception("查詢沒有今天收據數據.");
            }

            foreach (var r in receiptList)
            {
                r.Info_Donors = DC.Set<Info_Donor>().Where(x => x.ReceiptID == r.ID).ToList();
                r.Info_Longevitys = DC.Set<Info_Longevity>().Where(x => x.ReceiptID == r.ID).ToList();
                r.Info_Memorials = DC.Set<Info_Memorial>().Where(x => x.ReceiptID == r.ID).ToList();
            }

            return await ReceiptExcelVM.ExportExcelAsBytes(receiptList);
        }

        public async Task<byte[]> ExportExcelByDate(DateTime date)
        {
            List<Info_Receipt> receiptList;

            receiptList = await DC.Set<Info_Receipt>().Where(q => q.ReceiptDate.Value.Date == date).ToListAsync();

            if (receiptList == null || receiptList.Count <= 0)
            {
                throw new Exception($"查詢沒有 {date:yyyy-MM-dd} 收據數據.");
            }

            foreach (var r in receiptList)
            {
                r.Info_Donors = DC.Set<Info_Donor>().Where(x => x.ReceiptID == r.ID).ToList();
                r.Info_Longevitys = DC.Set<Info_Longevity>().Where(x => x.ReceiptID == r.ID).ToList();
                r.Info_Memorials = DC.Set<Info_Memorial>().Where(x => x.ReceiptID == r.ID).ToList();
            }

            return await ReceiptExcelVM.ExportExcelAsBytes(receiptList);
        }
    }
}
