using BMSHPMS.Models.DharmaService;
using System.Collections.Generic;
using System.Linq;

namespace BMSHPMS.DSManage.ViewModels.Common.TplPrintExcel
{
    public class Memorial_PrintExcelFillData
    {
        #region string
        public string ADece1 { get; set; }
        public string ADece2 { get; set; }
        public string ADece3 { get; set; }
        public string ABene1 { get; set; }
        public string ASerial { get; set; }


        public string BDece1 { get; set; }
        public string BDece2 { get; set; }
        public string BDece3 { get; set; }
        public string BBene1 { get; set; }
        public string BSerial { get; set; }

        public string CDece1 { get; set; }
        public string CDece2 { get; set; }
        public string CDece3 { get; set; }
        public string CBene1 { get; set; }
        public string CSerial { get; set; }

        public string DDece1 { get; set; }
        public string DDece2 { get; set; }
        public string DDece3 { get; set; }
        public string DBene1 { get; set; }
        public string DSerial { get; set; }

        public string EDece1 { get; set; }
        public string EDece2 { get; set; }
        public string EDece3 { get; set; }
        public string EBene1 { get; set; }
        public string ESerial { get; set; }

        public string FDece1 { get; set; }
        public string FDece2 { get; set; }
        public string FDece3 { get; set; }
        public string FBene1 { get; set; }
        public string FSerial { get; set; }

        public string GDece1 { get; set; }
        public string GDece2 { get; set; }
        public string GDece3 { get; set; }
        public string GBene1 { get; set; }
        public string GSerial { get; set; }

        public string HDece1 { get; set; }
        public string HDece2 { get; set; }
        public string HDece3 { get; set; }
        public string HBene1 { get; set; }
        public string HSerial { get; set; }

        public string IDece1 { get; set; }
        public string IDece2 { get; set; }
        public string IDece3 { get; set; }
        public string IBene1 { get; set; }
        public string ISerial { get; set; }

        public string JDece1 { get; set; }
        public string JDece2 { get; set; }
        public string JDece3 { get; set; }
        public string JBene1 { get; set; }
        public string JSerial { get; set; }
        #endregion

        public Memorial_PrintExcelFillData(List<Info_Memorial> list)
        {
            #region 填充數據
            ABene1 = list.ElementAtOrDefault(0) != null ? list[0].BenefactorName : "";
            ADece1 = list.ElementAtOrDefault(0) != null ? list[0].DeceasedName_1 : "";
            ADece2 = list.ElementAtOrDefault(0) != null ? list[0].DeceasedName_2 : "";
            ADece3 = list.ElementAtOrDefault(0) != null ? list[0].DeceasedName_3 : "";
            ASerial = list.ElementAtOrDefault(0) != null ? list[0].SerialCode : "";

            BBene1 = list.ElementAtOrDefault(1) != null ? list[1].BenefactorName : "";
            BDece1 = list.ElementAtOrDefault(1) != null ? list[1].DeceasedName_1 : "";
            BDece2 = list.ElementAtOrDefault(1) != null ? list[1].DeceasedName_2 : "";
            BDece3 = list.ElementAtOrDefault(1) != null ? list[1].DeceasedName_3 : "";
            BSerial = list.ElementAtOrDefault(1) != null ? list[1].SerialCode : "";

            CBene1 = list.ElementAtOrDefault(2) != null ? list[2].BenefactorName : "";
            CDece1 = list.ElementAtOrDefault(2) != null ? list[2].DeceasedName_1 : "";
            CDece2 = list.ElementAtOrDefault(2) != null ? list[2].DeceasedName_2 : "";
            CDece3 = list.ElementAtOrDefault(2) != null ? list[2].DeceasedName_3 : "";
            CSerial = list.ElementAtOrDefault(2) != null ? list[2].SerialCode : "";

            DBene1 = list.ElementAtOrDefault(3) != null ? list[3].BenefactorName : "";
            DDece1 = list.ElementAtOrDefault(3) != null ? list[3].DeceasedName_1 : "";
            DDece2 = list.ElementAtOrDefault(3) != null ? list[3].DeceasedName_2 : "";
            DDece3 = list.ElementAtOrDefault(3) != null ? list[3].DeceasedName_3 : "";
            DSerial = list.ElementAtOrDefault(3) != null ? list[3].SerialCode : "";

            EBene1 = list.ElementAtOrDefault(4) != null ? list[4].BenefactorName : "";
            EDece1 = list.ElementAtOrDefault(4) != null ? list[4].DeceasedName_1 : "";
            EDece2 = list.ElementAtOrDefault(4) != null ? list[4].DeceasedName_2 : "";
            EDece3 = list.ElementAtOrDefault(4) != null ? list[4].DeceasedName_3 : "";
            ESerial = list.ElementAtOrDefault(4) != null ? list[4].SerialCode : "";

            FBene1 = list.ElementAtOrDefault(5) != null ? list[5].BenefactorName : "";
            FDece1 = list.ElementAtOrDefault(5) != null ? list[5].DeceasedName_1 : "";
            FDece2 = list.ElementAtOrDefault(5) != null ? list[5].DeceasedName_2 : "";
            FDece3 = list.ElementAtOrDefault(5) != null ? list[5].DeceasedName_3 : "";
            FSerial = list.ElementAtOrDefault(5) != null ? list[5].SerialCode : "";

            GBene1 = list.ElementAtOrDefault(6) != null ? list[6].BenefactorName : "";
            GDece1 = list.ElementAtOrDefault(6) != null ? list[6].DeceasedName_1 : "";
            GDece2 = list.ElementAtOrDefault(6) != null ? list[6].DeceasedName_2 : "";
            GDece3 = list.ElementAtOrDefault(6) != null ? list[6].DeceasedName_3 : "";
            GSerial = list.ElementAtOrDefault(6) != null ? list[6].SerialCode : "";

            HBene1 = list.ElementAtOrDefault(7) != null ? list[7].BenefactorName : "";
            HDece1 = list.ElementAtOrDefault(7) != null ? list[7].DeceasedName_1 : "";
            HDece2 = list.ElementAtOrDefault(7) != null ? list[7].DeceasedName_2 : "";
            HDece3 = list.ElementAtOrDefault(7) != null ? list[7].DeceasedName_3 : "";
            HSerial = list.ElementAtOrDefault(7) != null ? list[7].SerialCode : "";

            IBene1 = list.ElementAtOrDefault(8) != null ? list[8].BenefactorName : "";
            IDece1 = list.ElementAtOrDefault(8) != null ? list[8].DeceasedName_1 : "";
            IDece2 = list.ElementAtOrDefault(8) != null ? list[8].DeceasedName_2 : "";
            IDece3 = list.ElementAtOrDefault(8) != null ? list[8].DeceasedName_3 : "";
            ISerial = list.ElementAtOrDefault(8) != null ? list[8].SerialCode : "";

            JBene1 = list.ElementAtOrDefault(9) != null ? list[9].BenefactorName : "";
            JDece1 = list.ElementAtOrDefault(9) != null ? list[9].DeceasedName_1 : "";
            JDece2 = list.ElementAtOrDefault(9) != null ? list[9].DeceasedName_2 : "";
            JDece3 = list.ElementAtOrDefault(9) != null ? list[9].DeceasedName_3 : "";
            JSerial = list.ElementAtOrDefault(9) != null ? list[9].SerialCode : "";
            #endregion
        }

        //private static async Task<byte[]> ExportExcel(List<Info_Memorial> list, PrintExcelTplPost post)
        //{
        //    if (list.Count > post.SeatCount)
        //    {
        //        throw new System.Exception($"數據超出{post.SeatCount}個");
        //    }

        //    MemorialExcelTemplate tpl = new(list);

        //    IExportFileByTemplate exporter = new ExcelExporter();
        //    return await exporter.ExportBytesByTemplate(tpl, post.FilePath);
        //}

        //public static async Task<byte[]> Export(List<Info_Memorial> list, PrintExcelTplPost post)
        //{
        //    if (list.Count <= post.SeatCount)
        //    {
        //        return await ExportExcel(list, post);
        //    }

        //    List<byte[]> tpls = new();

        //    int zs = list.Count / post.SeatCount;
        //    int ys = list.Count % post.SeatCount;

        //    int index = 0;
        //    for (int i = 0; i < zs; i++)
        //    {
        //        var tmp = await ExportExcel(list.GetRange(index, post.SeatCount), post);
        //        tpls.Add(tmp);
        //        index += post.SeatCount;
        //    }

        //    if (ys > 0)
        //    {
        //        var tmp2 = await ExportExcel(list.GetRange(index, ys), post);
        //        tpls.Add(tmp2);
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
    }
}
