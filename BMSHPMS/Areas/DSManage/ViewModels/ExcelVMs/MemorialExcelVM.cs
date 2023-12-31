﻿using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using System;

namespace BMSHPMS.DSManage.ViewModels.ExcelVMs
{
    [ExcelExporter(Name = "附薦")]
    public class MemorialExcelVM
    {
        [ExporterHeader(DisplayName = "收據號碼")]
        public string ReceiptNumber { get; set; }

        [ExporterHeader(DisplayName = "附薦編號")]
        public string SerialCode { get; set; }

        [ExporterHeader(DisplayName = "金額")]
        public int? Sum { get; set; }

        [ExporterHeader(DisplayName = "附薦宗親名及稱呼")]
        public string DeceasedName { get; set; }

        [ExporterHeader(DisplayName = "陽居姓名")]
        public string BenefactorName { get; set; }

        [ExporterHeader(DisplayName = "備註")]
        public string DSRemark { get; set; }

        [ExporterHeader(DisplayName = "資料登記人")]
        public string CreateBy { get; set; }

        [ExporterHeader(DisplayName = "登記時間", Format = "yyyy-MM-dd HH:mm:ss")]
        public DateTime? CreateTime { get; set; }

        [ExporterHeader(DisplayName = "ID")]
        public Guid? ID { get; set; }
    }
}
