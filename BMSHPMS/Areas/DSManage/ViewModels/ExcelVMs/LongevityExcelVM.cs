using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using System;

namespace BMSHPMS.DSManage.ViewModels.ExcelVMs
{
    [ExcelExporter(Name = "延生", AutoFitAllColumn = true)]
    public class LongevityExcelVM
    {
        [ExporterHeader(DisplayName = "收據號碼")]
        public string ReceiptNumber { get; set; }

        [ExporterHeader(DisplayName = "延生編號")]
        public string SerialCode { get; set; }

        [ExporterHeader(DisplayName = "金額")]
        public int? Sum { get; set; }

        [ExporterHeader(DisplayName = "姓名")]
        public string Name { get; set; }

        [ExporterHeader(DisplayName = "備註")]
        public string DSRemark { get; set; }

        [ExporterHeader(DisplayName = "建立人")]
        public string CreateBy { get; set; }

        [ExporterHeader(DisplayName = "建立時間", Format = "yyyy-MM-dd HH:mm:ss")]
        public DateTime? CreateTime { get; set; }

        [ExporterHeader(DisplayName = "ID")]
        public Guid? ID { get; set; }
    }
}
