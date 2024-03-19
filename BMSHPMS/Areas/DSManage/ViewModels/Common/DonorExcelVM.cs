﻿using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;

namespace BMSHPMS.DSManage.ViewModels.Common
{
    [ExcelExporter(Name = "功德主")]
    public class DonorExcelVM
    {
        [ExporterHeader(DisplayName = "收據號碼")]
        public string ReceiptNumber { get; set; }

        [ExporterHeader(DisplayName = "功德主編號")]
        public string SerialCode { get; set; }

        [ExporterHeader(DisplayName = "金額")]
        public int? Sum { get; set; }

        [ExporterHeader(DisplayName = "延生位姓名")]
        public string LongevityName { get; set; }

        [ExporterHeader(DisplayName = "附薦宗親名及稱呼")]
        public string DeceasedName { get; set; }

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
    }
}
