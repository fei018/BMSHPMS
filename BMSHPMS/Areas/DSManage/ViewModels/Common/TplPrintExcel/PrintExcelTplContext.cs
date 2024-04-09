using System.Collections.Generic;
using System.IO;

namespace BMSHPMS.DSManage.ViewModels.Common.TplPrintExcel
{
    public static class PrintExcelTplContext
    {
        #region 延生範本key
        public const string 延生20格205x254mm紅紙 = "e37b7d2266ad4e329ff6770b960589a1";

        public const string 延生小5蓮210x130mm紅紙 = "e47f5c2e676847249af65c1d924d2349";

        public const string 延生大5蓮154x255mm紅紙 = "d02daa2668b3456d8763301d981fa250";

        public const string 延生4蓮位小紅筒A4紙 = "d6cb22cd8fdb4810979135edaf9ae31e";

        public const string 延生1蓮位小紅筒紅紙 = "c00c5e94de0047739f4ebdc4af8f579c";

        public const string 延生1蓮位中紅筒紅紙 = "b7f1a6bb684040549e81d39c70e452fe";

        public const string 延生1蓮位大紅筒紅紙 = "dc903509829f4e6ead9c3e9527e8e54d";
        #endregion

        #region 附薦範本key
        public const string 附薦小10蓮位140x420 = "fb66bbd9420c460aa76cf4f5bee048ad";

        public const string 附薦大10蓮位181x490 = "e86a36ec3b6246f1933e72cbb9030bf1";

        public const string 附薦5蓮位善字牌位 = "b87312a99e10402b9b22549b01fc1d95";
        #endregion

        public static List<PrintPlaqueTplPost> LongevityTplPostList { get; private set; }

        public static List<PrintPlaqueTplPost> MemorialTplPostList { get; private set; }

        public static void SetTemplateList(string wwwPath)
        {
            string excelTplPath = Path.Combine(wwwPath, "excelTemplate");

            #region LongevityTplPostList
            LongevityTplPostList = new()
            {
                new PrintPlaqueTplPost()
                {
                    Key = 延生20格205x254mm紅紙,
                    ButtonDisplayName = nameof(延生20格205x254mm紅紙) ,
                    FilePath = Path.Combine(excelTplPath,"Longevity_20cell_205x254mm_RedPaper.xlsx"),
                    SeatCount = 20,
                },
                new PrintPlaqueTplPost()
                {
                    Key = 延生小5蓮210x130mm紅紙,
                    ButtonDisplayName = nameof(延生小5蓮210x130mm紅紙) ,
                    FilePath = Path.Combine(excelTplPath,"Longevity_small5cell_210x130mm_redpaper.xlsx"),
                    SeatCount = 5,
                },
                new PrintPlaqueTplPost()
                {
                    Key = 延生大5蓮154x255mm紅紙,
                    ButtonDisplayName = nameof(延生大5蓮154x255mm紅紙) ,
                    FilePath = Path.Combine(excelTplPath,"Longevity_Big5cell_154x255m_RedPaper.xlsx"),
                    SeatCount = 5,
                },
                new PrintPlaqueTplPost()
                {
                    Key = 延生4蓮位小紅筒A4紙,
                    ButtonDisplayName = nameof(延生4蓮位小紅筒A4紙),
                    FilePath = Path.Combine(excelTplPath,"Longevity_4Seat_LittleBox_A4.xlsx"),
                    SeatCount = 4,
                },
                new PrintPlaqueTplPost()
                {
                    Key = 延生1蓮位小紅筒紅紙,
                    ButtonDisplayName = nameof(延生1蓮位小紅筒紅紙),
                    //FilePath = Path.Combine(excelTplPath,"Longevity_1Seat_LittleBox_Red.xlsx"),
                    FilePath = Path.Combine(excelTplPath,"Longevity_1Seat_LittleBox_152X215.docx"),
                    SeatCount = 1,
                },
                new PrintPlaqueTplPost()
                {
                    Key = 延生1蓮位中紅筒紅紙,
                    ButtonDisplayName = nameof(延生1蓮位中紅筒紅紙),
                    FilePath = Path.Combine(excelTplPath,"Longevity_1Seat_MiddleBox_215X260.docx"),
                    SeatCount = 1,
                },
                new PrintPlaqueTplPost()
                {
                    Key = 延生1蓮位大紅筒紅紙,
                    ButtonDisplayName = nameof(延生1蓮位大紅筒紅紙),
                    FilePath = Path.Combine(excelTplPath,"Longevity_1Seat_BigBox_215X343.docx"),
                    SeatCount = 1,
                }
            };
            #endregion

            #region MemorialTplPostList
            MemorialTplPostList = new()
            {
                new PrintPlaqueTplPost()
                {
                    Key = 附薦小10蓮位140x420,
                    ButtonDisplayName = nameof(附薦小10蓮位140x420) ,
                    FilePath = Path.Combine(excelTplPath,"Memorial_10Seat_Small_140x420mm.xlsx"),
                    SeatCount = 10,
                },
                new PrintPlaqueTplPost()
                {
                    Key = 附薦大10蓮位181x490,
                    ButtonDisplayName = nameof(附薦大10蓮位181x490) ,
                    FilePath = Path.Combine(excelTplPath,"Memorial_10Seat_Big_181x490.xlsx"),
                    SeatCount=10,
                },
                new PrintPlaqueTplPost()
                {
                    Key = 附薦5蓮位善字牌位,
                    ButtonDisplayName = "附薦 5蓮位 善字牌位",
                    FilePath = Path.Combine(excelTplPath,"Memorial_5Seat_Kind.xlsx"),
                    SeatCount = 5
                }
            };
            #endregion
        }
    }

}
