using BMSHPMS.DSManage.ViewModels.Info_LongevityVMs;
using BMSHPMS.Helper;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMSHPMS.DSManage.ViewModels.Common
{
    [ExcelExporter(Name = "延生")]
    public class LongevityExcelVM
    {
        [ExporterHeader(DisplayName = "收據號碼")]
        public string ReceiptNumber { get; set; }

        [ExporterHeader(DisplayName = "收據日期", Format = "yyyy-MM-dd")]
        public DateTime ReceiptDate { get; set; }

        [ExporterHeader(DisplayName = "法會")]
        public string DharmaServiceFullName { get; set; }

        [ExporterHeader(DisplayName = "延生編號")]
        public string SerialCode { get; set; }

        [ExporterHeader(DisplayName = "金額")]
        public int? Sum { get; set; }

        [ExporterHeader(DisplayName = "姓名")]
        public string Name { get; set; }

        [ExporterHeader(DisplayName = "備註")]
        public string DSRemark { get; set; }

        //[ExporterHeader(DisplayName = "資料更新者")]
        //public string UpdateBy { get; set; }

        //[ExporterHeader(DisplayName = "更新時間", Format = "yyyy-MM-dd HH:mm:ss")]
        //public DateTime? UpdateTime { get; set; }

        //[ExporterHeader(DisplayName = "ID")]
        //public Guid? ID { get; set; }


        public static async Task<byte[]> ExportExcelAsBytes(List<Info_Longevity_View> longevities)
        {
            List<LongevityExcelVM> allLongevityExcelVMs = new();

            foreach (var item in longevities)
            {
                var vm = ToolsHelper.CreateInstanceUseProperties<Info_Longevity_View, LongevityExcelVM>(item);
                //vm.ReceiptNumber = item.Receipt?.ReceiptNumber;
                //if(item.Receipt.ReceiptDate.HasValue) vm.ReceiptDate = item.Receipt.ReceiptDate.Value;

                //vm.DharmaServiceFullName = item.Receipt?.DharmaServiceFullName;

                allLongevityExcelVMs.Add(vm);
            }

            allLongevityExcelVMs = allLongevityExcelVMs.OrderBy(x => x.Sum).ThenBy(x => x.SerialCode).ToList();

            var exporter = new ExcelExporter();
            var result = await exporter.Append(allLongevityExcelVMs).ExportAppendDataAsByteArray();

            return result;
        }
    }
}
