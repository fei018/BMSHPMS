﻿using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.EntityFrameworkCore;
using System;

namespace BMSHPMS.DSManage.ViewModels.Common
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

        [ExporterHeader(DisplayName = "附薦宗親名及稱呼_1")]
        [Comment("附薦宗親名及稱呼_1")]
        public string DeceasedName_1 { get; set; }

        [ExporterHeader(DisplayName = "附薦宗親名及稱呼_2")]
        [Comment("附薦宗親名及稱呼_2")]
        public string DeceasedName_2 { get; set; }

        [ExporterHeader(DisplayName = "附薦宗親名及稱呼_3")]
        [Comment("附薦宗親名及稱呼_3")]
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
    }
}
