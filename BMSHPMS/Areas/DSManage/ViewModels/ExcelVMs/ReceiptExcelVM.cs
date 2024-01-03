using BMSHPMS.Helper;
using BMSHPMS.Models.DharmaService;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BMSHPMS.DSManage.ViewModels.ExcelVMs
{
    [ExcelExporter(Name = "收據")]
    public class ReceiptExcelVM
    {
        [ExporterHeader(DisplayName = "收據號碼")]
        public string ReceiptNumber { get; set; }

        [ExporterHeader(DisplayName = "收據日期", Format = "yyyy-MM-dd")]
        public DateTime? ReceiptDate { get; set; }

        [ExporterHeader(DisplayName = "法會名")]
        public string DharmaServiceName { get; set; }

        [ExporterHeader(DisplayName = "收據人姓名")]
        public string ReceiptOwn { get; set; }

        [ExporterHeader(DisplayName = "聯絡人姓名")]
        public string ContactName { get; set; }

        [ExporterHeader(DisplayName = "聯絡人電話")]
        public string ContactPhone { get; set; }

        [ExporterHeader(DisplayName = "金額")]
        public int? Sum { get; set; }

        [ExporterHeader(DisplayName = "備註")]
        public string DSRemark { get; set; }

        [ExporterHeader(DisplayName = "資料登記人")]
        public string CreateBy { get; set; }

        [ExporterHeader(DisplayName = "登記時間", Format = "yyyy-MM-dd HH:mm:ss")]
        public DateTime? CreateTime { get; set; }

        [ExporterHeader(DisplayName = "ID")]
        public Guid? ID { get; set; }


        #region 收據 List 匯出 Excel 數據 byte[], 包括 功德主,延生,附薦
        /// <summary>
        /// 收據 List 匯出 Excel 數據 byte[], 包括 功德主,延生,附薦
        /// </summary>
        /// <param name="receipts"></param>
        /// <returns></returns>
        public static async Task<byte[]> ExportExcelAsBytes(List<Info_Receipt> receipts)
        {
            List<ReceiptExcelVM> allReceiptExcelVMs = new();
            List<DonorExcelVM> allDonorExcelVMs = new();
            List<LongevityExcelVM> allLongevityExcelVMs = new();
            List<MemorialExcelVM> allMemoryExcelVMs = new();

            foreach (var receipt in receipts)
            {
                var reVM = ToolsHelper.CreateNewObjectUseProperties<Info_Receipt, ReceiptExcelVM>(receipt);
                allReceiptExcelVMs.Add(reVM);

                foreach (var item in receipt.Info_Donors)
                {
                    var vm = ToolsHelper.CreateNewObjectUseProperties<Info_Donor, DonorExcelVM>(item);
                    vm.ReceiptNumber = receipt.ReceiptNumber;
                    allDonorExcelVMs.Add(vm);
                }

                foreach (var item in receipt.Info_Longevitys)
                {
                    var vm = ToolsHelper.CreateNewObjectUseProperties<Info_Longevity, LongevityExcelVM>(item);
                    vm.ReceiptNumber = receipt.ReceiptNumber;
                    allLongevityExcelVMs.Add(vm);
                }

                foreach (var item in receipt.Info_Memorials)
                {
                    var vm = ToolsHelper.CreateNewObjectUseProperties<Info_Memorial, MemorialExcelVM>(item);
                    vm.ReceiptNumber = receipt.ReceiptNumber;
                    allMemoryExcelVMs.Add(vm);
                }
            }

            allReceiptExcelVMs =  allReceiptExcelVMs.OrderByDescending(x => x.ReceiptDate).ThenByDescending(x => x.ReceiptNumber).ToList();
            allDonorExcelVMs = allDonorExcelVMs.OrderBy(x => x.Sum).ThenByDescending(x => x.SerialCode).ToList();
            allLongevityExcelVMs = allLongevityExcelVMs.OrderBy(x => x.Sum).ThenByDescending(x => x.SerialCode).ToList();
            allMemoryExcelVMs = allMemoryExcelVMs.OrderBy(x => x.Sum).ThenByDescending(x => x.SerialCode).ToList();

            var exporter = new ExcelExporter();
            var result = await exporter.Append(allReceiptExcelVMs)
                                .SeparateBySheet()
                                .Append(allDonorExcelVMs)
                                .SeparateBySheet()
                                .Append(allLongevityExcelVMs)
                                .SeparateBySheet()
                                .Append(allMemoryExcelVMs)
                                .ExportAppendDataAsByteArray();

            return result;
        }
        #endregion

    }
}
