using BMSHPMS.Helper;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using Spire.Doc;
using Storage.Net.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

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
        /// <param name="models"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public static async Task<byte[]> ExportByteAsExcel2<T1, T2>(List<T2> models, PrintPlaquePost post) where T1 : class where T2 : class
        {
            IExportFileByTemplate exporter = new ExcelExporter();

            // 只有一頁excel數據的情況
            if (models.Count <= post.SeatCount)
            {
                var tpl = Activator.CreateInstance(typeof(T1), models);
                return await exporter.ExportBytesByTemplate(tpl, post.FilePath);
            }

            // 放置 每頁excel數據 的 list
            List<byte[]> excelSheetList = new();

            int sheetPages = models.Count / post.SeatCount; // 算出 頁數
            int ys = models.Count % post.SeatCount; // 算出最後一頁的 蓮位數

            int index = 0;
            for (int i = 0; i < sheetPages; i++)
            {
                List<T2> tmpList = models.GetRange(index, post.SeatCount);
                var modelObj = Activator.CreateInstance(typeof(T1), tmpList);

                var bytes = await exporter.ExportBytesByTemplate(modelObj, post.FilePath);
                excelSheetList.Add(bytes);
                index += post.SeatCount;
            }

            if (ys > 0)
            {
                List<T2> tmpList = models.GetRange(index, ys);
                var tpl = Activator.CreateInstance(typeof(T1), tmpList);

                var bytes = await exporter.ExportBytesByTemplate(tpl, post.FilePath);
                excelSheetList.Add(bytes);
            }

            XSSFWorkbook mergeWorkBook = new();
            MemoryStream mergeMS = new();

            try
            {
                for (int i = 0; i < excelSheetList.Count; i++)
                {
                    using MemoryStream ms = new(excelSheetList[i]);
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
        public static async Task<PrintPlaqueResult> ExportWordAsByte<T1, T2>(List<T2> models, PrintPlaquePost post)
        {
            var result = new PrintPlaqueResult();

            // 只有一頁數據的情況
            if (models.Count <= post.SeatCount)
            {
                var tpl = Activator.CreateInstance(typeof(T1), models);
                result.FileBytes = await ExportWordByTemplate(tpl, post.FilePath);
                result.FileExtention = ".docx";
                result.Mimetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                return result;
            }

            // 放置 每頁數據 的 list
            List<byte[]> wordPageList = new();

            int pages = models.Count / post.SeatCount; // 算出 頁數
            int ys = models.Count % post.SeatCount; // 算出最後一頁的 蓮位數

            int index = 0;
            for (int i = 0; i < pages; i++)
            {
                List<T2> tmpList = models.GetRange(index, post.SeatCount);
                var tpl = Activator.CreateInstance(typeof(T1), tmpList);

                var bytes = await ExportWordByTemplate(tpl, post.FilePath);
                wordPageList.Add(bytes);
                index += post.SeatCount;
            }

            if (ys > 0)
            {
                List<T2> tmpList = models.GetRange(index, ys);
                var tpl = Activator.CreateInstance(typeof(T1), tmpList);

                var bytes = await ExportWordByTemplate(tpl, post.FilePath);
                wordPageList.Add(bytes);
            }

            using Document doc = new Document();
            using MemoryStream ms1 = new MemoryStream(wordPageList[0]);
            doc.LoadFromStream(ms1, Spire.Doc.FileFormat.Docx);

            for (int i = 1; i < wordPageList.Count; i++)
            {
                using MemoryStream ms2 = new MemoryStream(wordPageList[i]);
                doc.InsertTextFromStream(ms2, Spire.Doc.FileFormat.Docx);
            }

            using MemoryStream ms3 = new MemoryStream();
            doc.SaveToStream(ms3, Spire.Doc.FileFormat.Docx);

            result.FileBytes = ms3.ToArray();
            result.FileExtention = ".docx";
            result.Mimetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            return result;
        }

        public async static Task<byte[]> ExportWordByTemplate(object data, string templateFilePath)
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
                doc.LoadFromStream(fileStream, Spire.Doc.FileFormat.Docx);

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

                doc.SaveToStream(ms, Spire.Doc.FileFormat.Docx);
            });

            return ms.ToArray();
        }
        #endregion

        #region 多頁合併， 
        /// <summary>
        /// 匯出Excel範本數據<br/>T1: Excel範本的類型<br/>T2: 填充數據的集合類型
        /// </summary>
        /// <typeparam name="T1"> Excel範本的類型 </typeparam>
        /// <typeparam name="T2"> 填充數據的集合類型 </typeparam>
        /// <param name="models"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        //public static async Task<byte[]> ExportByteAsExcel<T1, T2>(List<T2> models, PrintPlaquePost post) where T1 : class where T2 : class
        //{
        //    IExportFileByTemplate exporter = new ExcelExporter();

        //    // 只有一頁excel數據的情況
        //    if (models.Count <= post.SeatCount)
        //    {
        //        var tpl = Activator.CreateInstance(typeof(T1), models);
        //        return await exporter.ExportBytesByTemplate(tpl, post.FilePath);
        //    }

        //    // 放置 每頁excel數據 的 list
        //    List<byte[]> excelSheetList = new();

        //    int sheetCount = models.Count / post.SeatCount; // 算出 頁數
        //    int ys = models.Count % post.SeatCount; // 算出最後一頁的 蓮位數

        //    if (sheetCount > 5)
        //    {
        //        throw new Exception($"資料不能多過{sheetCount}頁.");
        //    }
        //    else if (sheetCount == 5 && ys > 0)
        //    {
        //        throw new Exception($"資料不能多過{sheetCount}頁.");
        //    }

        //    int index = 0;
        //    for (int i = 0; i < sheetCount; i++)
        //    {
        //        List<T2> tmpList = models.GetRange(index, post.SeatCount);
        //        var tpl = Activator.CreateInstance(typeof(T1), tmpList);

        //        var bytes = await exporter.ExportBytesByTemplate(tpl, post.FilePath);
        //        excelSheetList.Add(bytes);
        //        index += post.SeatCount;
        //    }

        //    if (ys > 0)
        //    {
        //        List<T2> tmpList = models.GetRange(index, ys);
        //        var tpl = Activator.CreateInstance(typeof(T1), tmpList);

        //        var bytes = await exporter.ExportBytesByTemplate(tpl, post.FilePath);
        //        excelSheetList.Add(bytes);
        //    }

        //    // 開始合併

        //    RecyclableMemoryStreamManager managerms = new();
        //    var ms1 = managerms.GetStream();

        //    Workbook workbook = new();
        //    Workbook tmpWB = new();

        //    try
        //    {
        //        // 數據第一頁 寫入 workbook
        //        ms1.Write(excelSheetList[0], 0, excelSheetList[0].Length);
        //        workbook.LoadFromStream(ms1);

        //        var sheet1 = workbook.Worksheets[0];

        //        int rowNum = post.SheetLastRowIndex + 1;

        //        for (int i = 1; i < excelSheetList.Count; i++)
        //        {
        //            using var ms2 = managerms.GetStream();
        //            ms2.Write(excelSheetList[i], 0, excelSheetList[i].Length);
        //            tmpWB.LoadFromStream(ms2);
        //            CellRange destRange = sheet1.Range[rowNum, 1];
        //            rowNum += post.SheetLastRowIndex;
        //            tmpWB.Worksheets[0].AllocatedRange.Copy(destRange);
        //        }

        //        using var ms3 = managerms.GetStream();
        //        workbook.SaveToStream(ms3, Spire.Xls.FileFormat.Version2016);
        //        var result = ms3.ToArray();

        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        workbook.Dispose();
        //        tmpWB.Dispose();
        //        ms1.Dispose();
        //    }
        //}
        #endregion

        #region 匯出Excel範本文件
        /// <summary>
        /// 匯出Excel範本數據<br/>T1: Excel範本的類型<br/>T2: 填充數據的集合類型
        /// </summary>
        /// <typeparam name="T1"> Excel範本的類型 </typeparam>
        /// <typeparam name="T2"> 填充數據的集合類型 </typeparam>
        /// <param name="list"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public static async Task<byte[]> ExportExcelAsByte3<T1, T2>(WTMContext wtm, List<T2> models, PrintPlaquePost post) where T1 : class where T2 : class
        {
            IExportFileByTemplate exporter = new ExcelExporter();

            // 只有一頁excel數據的情況
            if (models.Count <= post.SeatCount)
            {
                var tpl = Activator.CreateInstance(typeof(T1), models);
                return await exporter.ExportBytesByTemplate(tpl, post.FilePath);
            }

            string tempDir = Path.Combine(wtm.ConfigInfo.HostRoot, "excel" + Guid.NewGuid().ToString());
            DirectoryInfo tempDirInfo = new(tempDir);
            tempDirInfo.Create();

            string tempExcelDir = Path.Combine(tempDir, "excel");
            DirectoryInfo excelDirInfo = new(tempExcelDir);
            excelDirInfo.Create();

            // 放置 每頁excel數據 的 list
            List<byte[]> excelFiletList = new();

            int sheetCount = models.Count / post.SeatCount; // 算出 頁數
            int ys = models.Count % post.SeatCount; // 算出最後一頁的 蓮位數

            int index = 0;
            for (int i = 0; i < sheetCount; i++)
            {
                List<T2> modelGroup = models.GetRange(index, post.SeatCount);
                var modelObject = Activator.CreateInstance(typeof(T1), modelGroup);

                string tmpFile = Path.Combine(tempExcelDir, i + ".xlsx");
                await exporter.ExportByTemplate(tmpFile, modelObject, post.FilePath);
                //excelFiletList.Add(bytes);
                index += post.SeatCount;
            }

            if (ys > 0)
            {
                List<T2> modelGroup = models.GetRange(index, ys);
                var modelObject = Activator.CreateInstance(typeof(T1), modelGroup);

                string tmpFile = Path.Combine(tempExcelDir, sheetCount + ".xlsx");
                await exporter.ExportByTemplate(tmpFile, modelObject, post.FilePath);
                //excelFiletList.Add(bytes);
            }

            string zipFile = Path.Combine(tempDir, "tmp.zip");
            ZipFile.CreateFromDirectory(tempExcelDir, zipFile);
            byte[] result = File.ReadAllBytes(zipFile);

            tempDirInfo.Delete(true);

            return result;
        }
        #endregion

        #region 匯出Excel範本文件
        /// <summary>
        /// 匯出Excel範本數據<br/>T1: Excel範本的類型<br/>T2: 填充數據的集合類型
        /// </summary>
        /// <typeparam name="T1"> Excel範本的類型 </typeparam>
        /// <typeparam name="T2"> 填充數據的集合類型 </typeparam>
        /// <param name="models"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public static async Task<PrintPlaqueResult> ExportByteAsExcel<T1, T2>(List<T2> models, PrintPlaquePost post) where T1 : class where T2 : class
        {
            var result = new PrintPlaqueResult();

            IExportFileByTemplate exporter = new ExcelExporter();

            // 只有一頁excel數據的情況
            if (models.Count <= post.SeatCount)
            {
                var tpl = Activator.CreateInstance(typeof(T1), models);
                result.FileBytes = await exporter.ExportBytesByTemplate(tpl, post.FilePath);
                result.FileExtention = ".xlsx";
                result.Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return result;
            }

            // 放置 每頁excel數據 的 list
            List<byte[]> excelSheetList = new();

            int sheetPages = models.Count / post.SeatCount; // 算出 頁數
            int ys = models.Count % post.SeatCount; // 算出最後一頁的 蓮位數

            int index = 0;
            for (int i = 0; i < sheetPages; i++)
            {
                List<T2> tmpList = models.GetRange(index, post.SeatCount);
                var modelObj = Activator.CreateInstance(typeof(T1), tmpList);

                var bytes = await exporter.ExportBytesByTemplate(modelObj, post.FilePath);
                excelSheetList.Add(bytes);
                index += post.SeatCount;
            }

            if (ys > 0)
            {
                List<T2> tmpList = models.GetRange(index, ys);
                var tpl = Activator.CreateInstance(typeof(T1), tmpList);

                var bytes = await exporter.ExportBytesByTemplate(tpl, post.FilePath);
                excelSheetList.Add(bytes);
            }

            result.FileBytes = ToolsHelper.GetZipArchive(excelSheetList);
            result.FileExtention = ".zip";
            result.Mimetype = "application/zip";

            return result;
        }
        #endregion
    }
}
