using BMSHPMS.DSManage.ViewModels.Common.TplPrintExcel;
using BMSHPMS.Models.DharmaService;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMSHPMS.DSManage.ViewModels.Common.TplPrintExcel
{
    public class Longevity_Small5cell_210x130mm_RedPaper
    {
        #region string
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Name5 { get; set; }

        public string Serial1 { get; set; }
        public string Serial2 { get; set; }
        public string Serial3 { get; set; }
        public string Serial4 { get; set; }
        public string Serial5 { get; set; }
        #endregion


        public static async Task<byte[]> ExportExcel(List<Info_Longevity> list, string tplPath)
        {
            if (list.Count > 5)
            {
                throw new System.Exception("數據超出5個");
            }

            LongevityTpl_20cell_205x254mm_RedPaper tpl = new();

            tpl.Name1 = list.ElementAtOrDefault(0) != null ? list[0].Name : "";
            tpl.Name2 = list.ElementAtOrDefault(1) != null ? list[1].Name : "";
            tpl.Name3 = list.ElementAtOrDefault(2) != null ? list[2].Name : "";
            tpl.Name4 = list.ElementAtOrDefault(3) != null ? list[3].Name : "";
            tpl.Name5 = list.ElementAtOrDefault(4) != null ? list[4].Name : "";

            tpl.Serial1 = list.ElementAtOrDefault(0) != null ? list[0].SerialCode : "";
            tpl.Serial2 = list.ElementAtOrDefault(1) != null ? list[1].SerialCode : "";
            tpl.Serial3 = list.ElementAtOrDefault(2) != null ? list[2].SerialCode : "";
            tpl.Serial4 = list.ElementAtOrDefault(3) != null ? list[3].SerialCode : "";
            tpl.Serial5 = list.ElementAtOrDefault(4) != null ? list[4].SerialCode : "";

            IExportFileByTemplate exporter = new ExcelExporter();
            return await exporter.ExportBytesByTemplate(tpl, tplPath);
        }
    }
}
