using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;

namespace BMSHPMS.DSManage.ViewModels.Common.TplPrintExcel
{
    public class PrintExcelTplPost
    {
        public string Key { get; set; }

        public string PaperDisplayName { get; set; }

        public string FilePath { get; set; }

        public int SeatCount { get; set; }

        #region 匯出Excel範本數據
        /// <summary>
        /// 匯出Excel範本數據<br/>T1: Excel範本的類型<br/>T2: 填充數據的集合類型
        /// </summary>
        /// <typeparam name="T1"> Excel範本的類型 </typeparam>
        /// <typeparam name="T2"> 填充數據的集合類型 </typeparam>
        /// <param name="list"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public static async Task<byte[]> Export<T1, T2>(List<T2> list, PrintExcelTplPost post)
        {
            IExportFileByTemplate exporter = new ExcelExporter();

            if (list.Count <= post.SeatCount)
            {
                var tpl = Activator.CreateInstance(typeof(T1), list);
                return await exporter.ExportBytesByTemplate(tpl, post.FilePath);
            }

            List<byte[]> tpls = new();

            int zs = list.Count / post.SeatCount;
            int ys = list.Count % post.SeatCount;

            int index = 0;
            for (int i = 0; i < zs; i++)
            {
                List<T2> tmpList = list.GetRange(index, post.SeatCount);
                var tpl = Activator.CreateInstance(typeof(T1), tmpList);

                var bytes = await exporter.ExportBytesByTemplate(tpl, post.FilePath);
                tpls.Add(bytes);
                index += post.SeatCount;
            }

            if (ys > 0)
            {
                List<T2> tmpList = list.GetRange(index, ys);
                var tpl = Activator.CreateInstance(typeof(T1), tmpList);

                var bytes = await exporter.ExportBytesByTemplate(tpl, post.FilePath);
                tpls.Add(bytes);
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
    }
}
