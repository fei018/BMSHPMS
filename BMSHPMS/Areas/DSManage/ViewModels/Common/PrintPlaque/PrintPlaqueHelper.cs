using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using NPOI.XSSF.UserModel;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BMSHPMS.DSManage.ViewModels.Common.PrintPlaque
{
    public class PrintPlaqueHelper
    {
        #region 匯出Excel範本文件
        /// <summary>
        /// 匯出Excel範本數據<br/>T1: Excel範本的類型<br/>T2: 填充數據的集合類型
        /// </summary>
        /// <typeparam name="T1"> Excel範本的類型 </typeparam>
        /// <typeparam name="T2"> 填充數據的集合類型 </typeparam>
        /// <param name="list"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public static async Task<byte[]> ExportByteAsExcel<T1, T2>(List<T2> list, PrintPlaquePost post) where T1 : class where T2 : class
        {
            IExportFileByTemplate exporter = new ExcelExporter();

            // 只有一頁excel數據的情況
            if (list.Count <= post.SeatCount)
            {
                var tpl = Activator.CreateInstance(typeof(T1), list);
                return await exporter.ExportBytesByTemplate(tpl, post.FilePath);
            }

            // 放置 每頁excel數據 的 list
            List<byte[]> tpls = new();

            int zs = list.Count / post.SeatCount; // 算出 頁數
            int ys = list.Count % post.SeatCount; // 算出最後一頁的 蓮位數

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

        #region 匯出 Word 範本
        public static async Task<byte[]> ExportByteAsWord<T1, T2>(List<T2> list, PrintPlaquePost post)
        {
            // 只有一頁數據的情況
            if (list.Count <= post.SeatCount)
            {
                var tpl = Activator.CreateInstance(typeof(T1), list);
                return await ExportByTemplate(tpl, post.FilePath);
            }

            // 放置 每頁數據 的 list
            List<byte[]> tpls = new();

            int zs = list.Count / post.SeatCount; // 算出 頁數
            int ys = list.Count % post.SeatCount; // 算出最後一頁的 蓮位數

            int index = 0;
            for (int i = 0; i < zs; i++)
            {
                List<T2> tmpList = list.GetRange(index, post.SeatCount);
                var tpl = Activator.CreateInstance(typeof(T1), tmpList);

                var bytes = await ExportByTemplate(tpl, post.FilePath);
                tpls.Add(bytes);
                index += post.SeatCount;
            }

            if (ys > 0)
            {
                List<T2> tmpList = list.GetRange(index, ys);
                var tpl = Activator.CreateInstance(typeof(T1), tmpList);

                var bytes = await ExportByTemplate(tpl, post.FilePath);
                tpls.Add(bytes);
            }

            using Document doc = new Document();
            using MemoryStream ms1 = new MemoryStream(tpls[0]);
            doc.LoadFromStream(ms1, FileFormat.Docx);

            for (int i = 1; i < tpls.Count; i++)
            {
                using MemoryStream ms2 = new MemoryStream(tpls[i]);
                doc.InsertTextFromStream(ms2, FileFormat.Docx);
            }

            using MemoryStream ms3 = new MemoryStream();
            doc.SaveToStream(ms3, FileFormat.Docx);

            return ms3.ToArray();
        }

        public async static Task<byte[]> ExportByTemplate(object data, string templateFilePath)
        {
            if (!File.Exists(templateFilePath))
            {
                throw new FileNotFoundException(null, Path.GetFileName(templateFilePath));
            }

            using MemoryStream ms = new MemoryStream();

            await Task.Run(() =>
            {
                using FileStream fileStream = new FileStream(templateFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                using Document doc = new();
                doc.LoadFromStream(fileStream, FileFormat.Docx);

                Type type = data.GetType();
                var props = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                foreach (var prop in props)
                {
                    if (prop.CanRead)
                    {
                        if (prop.GetValue(data) is string value && !string.IsNullOrEmpty(value))
                        {
                            doc.Replace("{{" + prop.Name + "}}", value, false, true);
                        }
                    }
                }

                doc.SaveToStream(ms, FileFormat.Docx);
            });

            return ms.ToArray();
        }
        #endregion
    }
}
