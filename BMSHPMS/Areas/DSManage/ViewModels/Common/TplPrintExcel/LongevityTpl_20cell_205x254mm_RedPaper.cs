using BMSHPMS.Models.DharmaService;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BMSHPMS.DSManage.ViewModels.Common.TplPrintExcel
{
    /// <summary>
    /// 延生 20格 205x254mm 紅紙 (100元)
    /// </summary>
    public class LongevityTpl_20cell_205x254mm_RedPaper
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


        #region 匯出單頁單excel文件
        private static async Task<byte[]> ExportExcel(List<Info_Longevity> list, PrintExcelTplPost post)
        {
            if (list.Count > post.CellCount)
            {
                throw new System.Exception($"數據超出{post.CellCount}個");
            }

            LongevityTpl_20cell_205x254mm_RedPaper tpl = new();

            #region 填充數據
            tpl.Name1 = list.ElementAtOrDefault(0) != null ? list[0].Name : "增福延壽";
            tpl.Name2 = list.ElementAtOrDefault(1) != null ? list[1].Name : "增福延壽";
            tpl.Name3 = list.ElementAtOrDefault(2) != null ? list[2].Name : "增福延壽";
            tpl.Name4 = list.ElementAtOrDefault(3) != null ? list[3].Name : "增福延壽";
            tpl.Name5 = list.ElementAtOrDefault(4) != null ? list[4].Name : "增福延壽";
            tpl.Name6 = list.ElementAtOrDefault(5) != null ? list[5].Name : "增福延壽";
            tpl.Name7 = list.ElementAtOrDefault(6) != null ? list[6].Name : "增福延壽";
            tpl.Name8 = list.ElementAtOrDefault(7) != null ? list[7].Name : "增福延壽";
            tpl.Name9 = list.ElementAtOrDefault(8) != null ? list[8].Name : "增福延壽";
            tpl.Name10 = list.ElementAtOrDefault(9) != null ? list[9].Name : "增福延壽";
            tpl.Name11 = list.ElementAtOrDefault(10) != null ? list[10].Name : "增福延壽";
            tpl.Name12 = list.ElementAtOrDefault(11) != null ? list[11].Name : "增福延壽";
            tpl.Name13 = list.ElementAtOrDefault(12) != null ? list[12].Name : "增福延壽";
            tpl.Name14 = list.ElementAtOrDefault(13) != null ? list[13].Name : "增福延壽";
            tpl.Name15 = list.ElementAtOrDefault(14) != null ? list[14].Name : "增福延壽";
            tpl.Name16 = list.ElementAtOrDefault(15) != null ? list[15].Name : "增福延壽";
            tpl.Name17 = list.ElementAtOrDefault(16) != null ? list[16].Name : "增福延壽";
            tpl.Name18 = list.ElementAtOrDefault(17) != null ? list[17].Name : "增福延壽";
            tpl.Name19 = list.ElementAtOrDefault(18) != null ? list[18].Name : "增福延壽";
            tpl.Name20 = list.ElementAtOrDefault(19) != null ? list[19].Name : "增福延壽";

            tpl.Serial1 = list.ElementAtOrDefault(0) != null ? list[0].SerialCode : "";
            tpl.Serial2 = list.ElementAtOrDefault(1) != null ? list[1].SerialCode : "";
            tpl.Serial3 = list.ElementAtOrDefault(2) != null ? list[2].SerialCode : "";
            tpl.Serial4 = list.ElementAtOrDefault(3) != null ? list[3].SerialCode : "";
            tpl.Serial5 = list.ElementAtOrDefault(4) != null ? list[4].SerialCode : "";
            tpl.Serial6 = list.ElementAtOrDefault(5) != null ? list[5].SerialCode : "";
            tpl.Serial7 = list.ElementAtOrDefault(6) != null ? list[6].SerialCode : "";
            tpl.Serial8 = list.ElementAtOrDefault(7) != null ? list[7].SerialCode : "";
            tpl.Serial9 = list.ElementAtOrDefault(8) != null ? list[8].SerialCode : "";
            tpl.Serial10 = list.ElementAtOrDefault(9) != null ? list[9].SerialCode : "";
            tpl.Serial11 = list.ElementAtOrDefault(10) != null ? list[10].SerialCode : "";
            tpl.Serial12 = list.ElementAtOrDefault(11) != null ? list[11].SerialCode : "";
            tpl.Serial13 = list.ElementAtOrDefault(12) != null ? list[12].SerialCode : "";
            tpl.Serial14 = list.ElementAtOrDefault(13) != null ? list[13].SerialCode : "";
            tpl.Serial15 = list.ElementAtOrDefault(14) != null ? list[14].SerialCode : "";
            tpl.Serial16 = list.ElementAtOrDefault(15) != null ? list[15].SerialCode : "";
            tpl.Serial17 = list.ElementAtOrDefault(16) != null ? list[16].SerialCode : "";
            tpl.Serial18 = list.ElementAtOrDefault(17) != null ? list[17].SerialCode : "";
            tpl.Serial19 = list.ElementAtOrDefault(18) != null ? list[18].SerialCode : "";
            tpl.Serial20 = list.ElementAtOrDefault(19) != null ? list[19].SerialCode : "";
            #endregion


            IExportFileByTemplate exporter = new ExcelExporter();
            return await exporter.ExportBytesByTemplate(tpl, post.FilePath);
        }
        #endregion

        #region 匯出Excel範本數據
        /// <summary>
        /// 匯出Excel範本數據
        /// </summary>
        /// <param name="list"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public static async Task<byte[]> Export(List<Info_Longevity> list, PrintExcelTplPost post)
        {

            if (list.Count <= post.CellCount)
            {
                return await ExportExcel(list, post);
            }

            List<byte[]> tpls = new();

            int zs = list.Count / post.CellCount;
            int ys = list.Count % post.CellCount;

            int index = 0;
            for (int i = 0; i < zs; i++)
            {
                var tmp = await ExportExcel(list.GetRange(index, post.CellCount), post);
                tpls.Add(tmp);
                index += post.CellCount;
            }

            if (ys > 0)
            {
                var tmp2 = await ExportExcel(list.GetRange(index, ys), post);
                tpls.Add(tmp2);
            }

            XSSFWorkbook mergeWorkBook = new();
            MemoryStream mergeMS = new();

            try
            {
                for (int i = 0; i < tpls.Count; i++)
                {
                    using MemoryStream ms = new(tpls[i]);
                    using XSSFWorkbook tmpWorkBook = new(ms);
                    XSSFSheet tmpSheet = tmpWorkBook.GetSheetAt(0) as XSSFSheet;
                    tmpSheet.CopyTo(mergeWorkBook, "Sheet" + i, true, true);
                }

                mergeWorkBook.Write(mergeMS);
                return mergeMS.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                mergeWorkBook.Close();
                mergeMS.Close();
            }
        }
        #endregion


        //public byte[] UseBinaryReader(Stream stream)
        //{
        //    byte[] bytes;
        //    stream.Position = 0;
        //    using (var binaryReader = new BinaryReader(stream))
        //    {
        //        bytes = binaryReader.ReadBytes((int)stream.Length);
        //    }
        //    return bytes;
        //}
    }
}
