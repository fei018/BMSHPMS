using BMSHPMS.DSManage.ViewModels.Info_DonorVMs;
using BMSHPMS.Helper;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMSHPMS.DSManage.ViewModels.Common
{
    [ExcelExporter(Name = "功德主")]
    public class DonorExcelVM
    {
        [ExporterHeader(DisplayName = "收據號碼")]
        public string ReceiptNumber { get; set; }

        [ExporterHeader(DisplayName = "收據日期", Format = "yyyy-MM-dd")]
        public DateTime ReceiptDate { get; set; }

        [ExporterHeader(DisplayName = "法會")]
        public string DharmaServiceFullName { get; set; }

        [ExporterHeader(DisplayName = "功德主編號")]
        public string SerialCode { get; set; }

        [ExporterHeader(DisplayName = "金額")]
        public int? Sum { get; set; }

        [ExporterHeader(DisplayName = "延生位姓名")]
        public string LongevityName { get; set; }

        [ExporterHeader(DisplayName = "附薦名稱1")]
        public string DeceasedName_1 { get; set; }

        [ExporterHeader(DisplayName = "附薦名稱2")]
        public string DeceasedName_2 { get; set; }

        [ExporterHeader(DisplayName = "附薦名稱3")]
        public string DeceasedName_3 { get; set; }

        [ExporterHeader(DisplayName = "陽居姓名")]
        public string BenefactorName { get; set; }

        [ExporterHeader(DisplayName = "備註")]
        public string DSRemark { get; set; }

        //[ExporterHeader(DisplayName = "資料更新者")]
        //public string UpdateBy { get; set; }

        //[ExporterHeader(DisplayName = "更新時間", Format = "yyyy-MM-dd HH:mm:ss")]
        //public DateTime? UpdateTime { get; set; }

        //[ExporterHeader(DisplayName = "ID")]
        //public Guid? ID { get; set; }

        public static async Task<byte[]> ExportExcelAsBytes(List<Info_Donor_View> list)
        {
            List<DonorExcelVM> newlist = new();

            foreach (var item in list)
            {
                var vm = ToolsHelper.CreateInstanceUseProperties<Info_Donor_View, DonorExcelVM>(item);

                newlist.Add(vm);
            }

            newlist = newlist.OrderBy(x => x.Sum).ThenBy(x => x.SerialCode).ToList();

            var exporter = new ExcelExporter();
            var result = await exporter.Append(newlist).ExportAppendDataAsByteArray();

            return result;
        }
    }
}
