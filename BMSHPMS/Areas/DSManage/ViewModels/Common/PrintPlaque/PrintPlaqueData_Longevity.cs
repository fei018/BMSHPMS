using BMSHPMS.Models.DharmaService;
using System.Collections.Generic;
using System.Linq;

namespace BMSHPMS.DSManage.ViewModels.Common.PrintPlaque
{
    /// <summary>
    /// 延生 20格 205x254mm 紅紙 (100元)
    /// </summary>
    public class PrintPlaqueData_Longevity
    {
        #region string
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Name5 { get; set; }
        public string Name6 { get; set; }
        public string Name7 { get; set; }
        public string Name8 { get; set; }
        public string Name9 { get; set; }
        public string Name10 { get; set; }
        public string Name11 { get; set; }
        public string Name12 { get; set; }
        public string Name13 { get; set; }
        public string Name14 { get; set; }
        public string Name15 { get; set; }
        public string Name16 { get; set; }
        public string Name17 { get; set; }
        public string Name18 { get; set; }
        public string Name19 { get; set; }
        public string Name20 { get; set; }

        public string Serial1 { get; set; }
        public string Serial2 { get; set; }
        public string Serial3 { get; set; }
        public string Serial4 { get; set; }
        public string Serial5 { get; set; }
        public string Serial6 { get; set; }
        public string Serial7 { get; set; }
        public string Serial8 { get; set; }
        public string Serial9 { get; set; }
        public string Serial10 { get; set; }
        public string Serial11 { get; set; }
        public string Serial12 { get; set; }
        public string Serial13 { get; set; }
        public string Serial14 { get; set; }
        public string Serial15 { get; set; }
        public string Serial16 { get; set; }
        public string Serial17 { get; set; }
        public string Serial18 { get; set; }
        public string Serial19 { get; set; }
        public string Serial20 { get; set; }
        #endregion

        public PrintPlaqueData_Longevity(List<Info_Longevity> list)
        {
            #region 填充數據
            Name1 = list.ElementAtOrDefault(0) != null ? list[0].Name : "增福延壽";
            Name2 = list.ElementAtOrDefault(1) != null ? list[1].Name : "增福延壽";
            Name3 = list.ElementAtOrDefault(2) != null ? list[2].Name : "增福延壽";
            Name4 = list.ElementAtOrDefault(3) != null ? list[3].Name : "增福延壽";
            Name5 = list.ElementAtOrDefault(4) != null ? list[4].Name : "增福延壽";
            Name6 = list.ElementAtOrDefault(5) != null ? list[5].Name : "增福延壽";
            Name7 = list.ElementAtOrDefault(6) != null ? list[6].Name : "增福延壽";
            Name8 = list.ElementAtOrDefault(7) != null ? list[7].Name : "增福延壽";
            Name9 = list.ElementAtOrDefault(8) != null ? list[8].Name : "增福延壽";
            Name10 = list.ElementAtOrDefault(9) != null ? list[9].Name : "增福延壽";
            Name11 = list.ElementAtOrDefault(10) != null ? list[10].Name : "增福延壽";
            Name12 = list.ElementAtOrDefault(11) != null ? list[11].Name : "增福延壽";
            Name13 = list.ElementAtOrDefault(12) != null ? list[12].Name : "增福延壽";
            Name14 = list.ElementAtOrDefault(13) != null ? list[13].Name : "增福延壽";
            Name15 = list.ElementAtOrDefault(14) != null ? list[14].Name : "增福延壽";
            Name16 = list.ElementAtOrDefault(15) != null ? list[15].Name : "增福延壽";
            Name17 = list.ElementAtOrDefault(16) != null ? list[16].Name : "增福延壽";
            Name18 = list.ElementAtOrDefault(17) != null ? list[17].Name : "增福延壽";
            Name19 = list.ElementAtOrDefault(18) != null ? list[18].Name : "增福延壽";
            Name20 = list.ElementAtOrDefault(19) != null ? list[19].Name : "增福延壽";

            Serial1 = list.ElementAtOrDefault(0) != null ? list[0].SerialCode : "";
            Serial2 = list.ElementAtOrDefault(1) != null ? list[1].SerialCode : "";
            Serial3 = list.ElementAtOrDefault(2) != null ? list[2].SerialCode : "";
            Serial4 = list.ElementAtOrDefault(3) != null ? list[3].SerialCode : "";
            Serial5 = list.ElementAtOrDefault(4) != null ? list[4].SerialCode : "";
            Serial6 = list.ElementAtOrDefault(5) != null ? list[5].SerialCode : "";
            Serial7 = list.ElementAtOrDefault(6) != null ? list[6].SerialCode : "";
            Serial8 = list.ElementAtOrDefault(7) != null ? list[7].SerialCode : "";
            Serial9 = list.ElementAtOrDefault(8) != null ? list[8].SerialCode : "";
            Serial10 = list.ElementAtOrDefault(9) != null ? list[9].SerialCode : "";
            Serial11 = list.ElementAtOrDefault(10) != null ? list[10].SerialCode : "";
            Serial12 = list.ElementAtOrDefault(11) != null ? list[11].SerialCode : "";
            Serial13 = list.ElementAtOrDefault(12) != null ? list[12].SerialCode : "";
            Serial14 = list.ElementAtOrDefault(13) != null ? list[13].SerialCode : "";
            Serial15 = list.ElementAtOrDefault(14) != null ? list[14].SerialCode : "";
            Serial16 = list.ElementAtOrDefault(15) != null ? list[15].SerialCode : "";
            Serial17 = list.ElementAtOrDefault(16) != null ? list[16].SerialCode : "";
            Serial18 = list.ElementAtOrDefault(17) != null ? list[17].SerialCode : "";
            Serial19 = list.ElementAtOrDefault(18) != null ? list[18].SerialCode : "";
            Serial20 = list.ElementAtOrDefault(19) != null ? list[19].SerialCode : "";
            #endregion
        }

        #region 匯出單頁單excel文件
        //private static async Task<byte[]> ExportExcel(List<Info_Longevity> list, PrintExcelTplPost post)
        //{
        //    Longevity_20cell205x254mm tpl = new(list);

        //    IExportFileByTemplate exporter = new ExcelExporter();
        //    return await exporter.ExportBytesByTemplate(tpl, post.FilePath);
        //}
        #endregion




        #region 匯出Excel範本數據
        /// <summary>
        /// 匯出Excel範本數據
        /// </summary>
        /// <param name="list"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        //public static async Task<byte[]> Export(List<Info_Longevity> list, PrintExcelTplPost post)
        //{
        //    IExportFileByTemplate exporter = new ExcelExporter();

        //    if (list.Count <= post.CellCount)
        //    {
        //        Longevity_20cell205x254mm tpl = new(list);
        //        return await exporter.ExportBytesByTemplate(tpl, post.FilePath);
        //    }

        //    List<byte[]> tpls = new();

        //    int zs = list.Count / post.CellCount;
        //    int ys = list.Count % post.CellCount;

        //    int index = 0;
        //    for (int i = 0; i < zs; i++)
        //    {
        //        //var tmp = await ExportExcel(list.GetRange(index, post.CellCount), post);
        //        Longevity_20cell205x254mm tpl = new(list.GetRange(index, post.CellCount)); Activator.CreateInstance()
        //        var bytes = await exporter.ExportBytesByTemplate(tpl, post.FilePath);
        //        tpls.Add(bytes);
        //        index += post.CellCount;
        //    }

        //    if (ys > 0)
        //    {
        //        //var tmp2 = await ExportExcel(list.GetRange(index, ys), post);
        //        Longevity_20cell205x254mm tpl = new(list.GetRange(index, ys));
        //        var bytes = await exporter.ExportBytesByTemplate(tpl, post.FilePath);
        //        tpls.Add(bytes);
        //    }

        //    XSSFWorkbook mergeWorkBook = new();
        //    MemoryStream mergeMS = new();

        //    try
        //    {
        //        for (int i = 0; i < tpls.Count; i++)
        //        {
        //            using MemoryStream ms = new(tpls[i]);
        //            using XSSFWorkbook tmpWorkBook = new(ms);
        //            XSSFSheet tmpSheet = tmpWorkBook.GetSheetAt(0) as XSSFSheet;
        //            tmpSheet.CopyTo(mergeWorkBook, "Sheet" + i, true, true);
        //        }

        //        mergeWorkBook.Write(mergeMS);
        //        return mergeMS.ToArray();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        mergeWorkBook.Close();
        //        mergeMS.Close();
        //    }
        //}
        #endregion
    }
}
