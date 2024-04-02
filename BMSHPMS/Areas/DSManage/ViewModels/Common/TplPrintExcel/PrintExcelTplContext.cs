using System.Collections.Generic;
using System.IO;

namespace BMSHPMS.DSManage.ViewModels.Common.TplPrintExcel
{
    public static class PrintExcelTplContext
    {
        #region 延生範本key
        public const string 延生20格205x254mm紅紙100元 = "e37b7d2266ad4e329ff6770b960589a1";

        public const string 延生小5蓮210x130mm紅紙300元500元 = "e47f5c2e676847249af65c1d924d2349";

        public const string 延生大5蓮154x255mm紅紙300元500元 = "d02daa2668b3456d8763301d981fa250";
        #endregion

        #region 附薦範本key
        public const string 附薦小10蓮位140x420mm100元 = "fb66bbd9420c460aa76cf4f5bee048ad";

        public const string 附薦大10蓮位100元 = "e86a36ec3b6246f1933e72cbb9030bf1";

        public const string 附薦5蓮位善字牌位 = "b87312a99e10402b9b22549b01fc1d95";
        #endregion

        public static List<PrintExcelTplPost> LongevityTplPostList { get; private set; }

        public static List<PrintExcelTplPost> MemorialTplPostList { get; private set; }

        public static void SetTemplateList(string wwwPath)
        {
            string excelTplPath = Path.Combine(wwwPath, "excelTemplate");

            #region LongevityTplPostList
            LongevityTplPostList = new()
            {
                // new LongevityTemplate() { Key = "", FilePath = "", PaperDisplayName = ""},
                new PrintExcelTplPost()
                {
                    Key = 延生20格205x254mm紅紙100元,
                    PaperDisplayName = "延生 20格 205x254mm 紅紙 (100元)" ,
                    FilePath = Path.Combine(excelTplPath,"Longevity_20cell_205x254mm_RedPaper.xlsx"),
                    SeatCount = 20,
                },
                new PrintExcelTplPost()
                {
                    Key = 延生小5蓮210x130mm紅紙300元500元,
                    PaperDisplayName = "延生 小5蓮 210x130mm 紅紙 (300元,500元)" ,
                    FilePath = Path.Combine(excelTplPath,"Longevity_small5cell_210x130mm_redpaper.xlsx"),
                    SeatCount = 5,
                },
                new PrintExcelTplPost()
                {
                    Key = 延生大5蓮154x255mm紅紙300元500元,
                    PaperDisplayName = "延生 大5蓮 154x255mm 紅紙 (300元,500元)" ,
                    FilePath = Path.Combine(excelTplPath,"Longevity_Big5cell_154x255m_RedPaper.xlsx"),
                    SeatCount = 5,
                },
            };
            #endregion

            #region MemorialTplPostList
            MemorialTplPostList = new()
            {
                new PrintExcelTplPost()
                {
                    Key = 附薦小10蓮位140x420mm100元,
                    PaperDisplayName = "附薦 10蓮位(小) 140x420mm (100元)" ,
                    FilePath = Path.Combine(excelTplPath,"Memorial_10SeatSmall_140x420mm.xlsx"),
                    SeatCount = 10,
                },
                new PrintExcelTplPost()
                {
                    Key = 附薦大10蓮位100元,
                    PaperDisplayName = "附薦 10蓮位(大) (100元)" ,
                    FilePath = Path.Combine(excelTplPath,"Memorial_10Seat_100.xlsx"),
                    SeatCount=10,
                },
                new PrintExcelTplPost()
                {
                    Key = 附薦5蓮位善字牌位,
                    PaperDisplayName = "附薦 5蓮位 善字牌位",
                    FilePath = Path.Combine(excelTplPath,"Memorial_5Seat_Kind.xlsx"),
                    SeatCount = 5
                }
            };
            #endregion
        }
    }

}
