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
    public class Memorial_Small10Cell_140x420mm
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


        private const int _tplCount = 10;

        private static async Task<byte[]> ExportExcel(List<Info_Memorial> list, string tplPath)
        {
            if (list.Count > _tplCount)
            {
                throw new System.Exception($"數據超出{_tplCount}個");
            }

            Memorial_Small10Cell_140x420mm tpl = new Memorial_Small10Cell_140x420mm();

            #region 填充數據
            tpl.ABene1 = list.ElementAtOrDefault(0) != null ? list[0].BenefactorName : "";
            tpl.ADece1 = list.ElementAtOrDefault(0) != null ? list[0].DeceasedName_1 : "";
            tpl.ADece2 = list.ElementAtOrDefault(0) != null ? list[0].DeceasedName_2 : "";
            tpl.ADece3 = list.ElementAtOrDefault(0) != null ? list[0].DeceasedName_3 : "";
            tpl.ASerial = list.ElementAtOrDefault(0) != null ? list[0].SerialCode : "";

            tpl.BBene1 = list.ElementAtOrDefault(1) != null ? list[1].BenefactorName : "";
            tpl.BDece1 = list.ElementAtOrDefault(1) != null ? list[1].DeceasedName_1 : "";
            tpl.BDece2 = list.ElementAtOrDefault(1) != null ? list[1].DeceasedName_2 : "";
            tpl.BDece3 = list.ElementAtOrDefault(1) != null ? list[1].DeceasedName_3 : "";
            tpl.BSerial = list.ElementAtOrDefault(1) != null ? list[1].SerialCode : "";

            tpl.CBene1 = list.ElementAtOrDefault(2) != null ? list[2].BenefactorName : "";
            tpl.CDece1 = list.ElementAtOrDefault(2) != null ? list[2].DeceasedName_1 : "";
            tpl.CDece2 = list.ElementAtOrDefault(2) != null ? list[2].DeceasedName_2 : "";
            tpl.CDece3 = list.ElementAtOrDefault(2) != null ? list[2].DeceasedName_3 : "";
            tpl.CSerial = list.ElementAtOrDefault(2) != null ? list[2].SerialCode : "";

            tpl.DBene1 = list.ElementAtOrDefault(3) != null ? list[3].BenefactorName : "";
            tpl.DDece1 = list.ElementAtOrDefault(3) != null ? list[3].DeceasedName_1 : "";
            tpl.DDece2 = list.ElementAtOrDefault(3) != null ? list[3].DeceasedName_2 : "";
            tpl.DDece3 = list.ElementAtOrDefault(3) != null ? list[3].DeceasedName_3 : "";
            tpl.DSerial = list.ElementAtOrDefault(3) != null ? list[3].SerialCode : "";

            tpl.EBene1 = list.ElementAtOrDefault(4) != null ? list[4].BenefactorName : "";
            tpl.EDece1 = list.ElementAtOrDefault(4) != null ? list[4].DeceasedName_1 : "";
            tpl.EDece2 = list.ElementAtOrDefault(4) != null ? list[4].DeceasedName_2 : "";
            tpl.EDece3 = list.ElementAtOrDefault(4) != null ? list[4].DeceasedName_3 : "";
            tpl.ESerial = list.ElementAtOrDefault(4) != null ? list[4].SerialCode : "";

            tpl.FBene1 = list.ElementAtOrDefault(5) != null ? list[5].BenefactorName : "";
            tpl.FDece1 = list.ElementAtOrDefault(5) != null ? list[5].DeceasedName_1 : "";
            tpl.FDece2 = list.ElementAtOrDefault(5) != null ? list[5].DeceasedName_2 : "";
            tpl.FDece3 = list.ElementAtOrDefault(5) != null ? list[5].DeceasedName_3 : "";
            tpl.FSerial = list.ElementAtOrDefault(5) != null ? list[5].SerialCode : "";

            tpl.GBene1 = list.ElementAtOrDefault(6) != null ? list[6].BenefactorName : "";
            tpl.GDece1 = list.ElementAtOrDefault(6) != null ? list[6].DeceasedName_1 : "";
            tpl.GDece2 = list.ElementAtOrDefault(6) != null ? list[6].DeceasedName_2 : "";
            tpl.GDece3 = list.ElementAtOrDefault(6) != null ? list[6].DeceasedName_3 : "";
            tpl.GSerial = list.ElementAtOrDefault(6) != null ? list[6].SerialCode : "";

            tpl.HBene1 = list.ElementAtOrDefault(7) != null ? list[7].BenefactorName : "";
            tpl.HDece1 = list.ElementAtOrDefault(7) != null ? list[7].DeceasedName_1 : "";
            tpl.HDece2 = list.ElementAtOrDefault(7) != null ? list[7].DeceasedName_2 : "";
            tpl.HDece3 = list.ElementAtOrDefault(7) != null ? list[7].DeceasedName_3 : "";
            tpl.HSerial = list.ElementAtOrDefault(7) != null ? list[7].SerialCode : "";

            tpl.IBene1 = list.ElementAtOrDefault(8) != null ? list[8].BenefactorName : "";
            tpl.IDece1 = list.ElementAtOrDefault(8) != null ? list[8].DeceasedName_1 : "";
            tpl.IDece2 = list.ElementAtOrDefault(8) != null ? list[8].DeceasedName_2 : "";
            tpl.IDece3 = list.ElementAtOrDefault(8) != null ? list[8].DeceasedName_3 : "";
            tpl.ISerial = list.ElementAtOrDefault(8) != null ? list[8].SerialCode : "";

            tpl.JBene1 = list.ElementAtOrDefault(9) != null ? list[9].BenefactorName : "";
            tpl.JDece1 = list.ElementAtOrDefault(9) != null ? list[9].DeceasedName_1 : "";
            tpl.JDece2 = list.ElementAtOrDefault(9) != null ? list[9].DeceasedName_2 : "";
            tpl.JDece3 = list.ElementAtOrDefault(9) != null ? list[9].DeceasedName_3 : "";
            tpl.JSerial = list.ElementAtOrDefault(9) != null ? list[9].SerialCode : "";
            #endregion

            IExportFileByTemplate exporter = new ExcelExporter();
            return await exporter.ExportBytesByTemplate(tpl, tplPath);
        }

        public static async Task<byte[]> Export(List<Info_Memorial> list, string tplPath)
        {
            if (list.Count <= _tplCount)
            {
                return await ExportExcel(list, tplPath);
            }

            List<byte[]> tpls = new();

            int zs = list.Count / _tplCount;
            int ys = list.Count % _tplCount;

            int index = 0;
            for (int i = 0; i < zs; i++)
            {
                var tmp = await ExportExcel(list.GetRange(index, _tplCount), tplPath);
                tpls.Add(tmp);
                index += _tplCount;
            }

            if (ys > 0)
            {
                var tmp2 = await ExportExcel(list.GetRange(index, ys), tplPath);
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
    }
}
