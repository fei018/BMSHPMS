using System.Collections.Generic;
using System.IO;

namespace BMSHPMS.DSManage.ViewModels.Common.PrintPlaque
{
    public static class PrintPlaqueContext
    {
        #region 延生範本key
        public const string 延生20格205x254mm紅紙 = "e37b7d2266ad4e329ff6770b960589a1";

        public const string 延生小5蓮210x130mm紅紙 = "e47f5c2e676847249af65c1d924d2349";

        public const string 延生大5蓮160x255mm紅紙 = "d02daa2668b3456d8763301d981fa250";

        public const string 延生4蓮位小紅筒紅紙A4 = "d6cb22cd8fdb4810979135edaf9ae31e";

        public const string 延生1蓮位小紅筒紅紙 = "c00c5e94de0047739f4ebdc4af8f579c";

        public const string 延生1蓮位中紅筒紅紙 = "b7f1a6bb684040549e81d39c70e452fe";

        public const string 延生1蓮位大紅筒紅紙 = "dc903509829f4e6ead9c3e9527e8e54d";
        #endregion

        #region 附薦範本key
        public const string 附薦小10蓮位140x420黃紙 = "fb66bbd9420c460aa76cf4f5bee048ad";

        public const string 附薦大10蓮位181x490黃紙 = "e86a36ec3b6246f1933e72cbb9030bf1";

        public const string 附薦5蓮位善字牌位A4 = "b87312a99e10402b9b22549b01fc1d95";

        public const string 附薦2蓮位全字牌位A4 = "eccc5c074b3548119deadce02570187e";

        public const string 附薦3蓮位萬字牌位A4 = "a5128aae8f5e471da4719fd6c87dd72a";

        public const string 附薦大黄5莲A4 = "f3c45d93a3514d709eb232cb50e336a4";

        public const string 附薦20格黃紙A4 = "c0bd15e143d44b88be18c8341c5429bc";

        public const string 附薦中黃1莲 = "e6f4803d3a6a45618d8b090913cbee21";
        #endregion

        public static List<PrintPlaquePost> Longevity_PrintPlaquePostList { get; private set; }

        public static List<PrintPlaquePost> Memorial_PrintPlaquePostList { get; private set; }

        public static List<PrintPlaquePost> Donor_PrintPlaquePostList { get; private set; }

        public static void SetTemplateList(string wwwPath)
        {
            string excelTplPath = Path.Combine(wwwPath, "excelTemplate");

            #region 延生模版資料
            Longevity_PrintPlaquePostList = new()
            {
                new PrintPlaquePost()
                {
                    Key = 延生20格205x254mm紅紙,
                    ButtonDisplayName = nameof(延生20格205x254mm紅紙) ,
                    FilePath = Path.Combine(excelTplPath,"Longevity_20cell_205x254mm_RedPaper.xlsx"),
                    SeatCount = 20,
                    FileType = FileTypeEnum.Excel,
                    PlaqueType = PlaqueTypeEnum.延生,
                },
                new PrintPlaquePost()
                {
                    Key = 延生小5蓮210x130mm紅紙,
                    ButtonDisplayName = nameof(延生小5蓮210x130mm紅紙) ,
                    FilePath = Path.Combine(excelTplPath,"Longevity_small5cell_210x130mm_redpaper.xlsx"),
                    SeatCount = 5,
                    FileType = FileTypeEnum.Excel,
                    PlaqueType = PlaqueTypeEnum.延生,
                },
                new PrintPlaquePost()
                {
                    Key = 延生大5蓮160x255mm紅紙,
                    ButtonDisplayName = nameof(延生大5蓮160x255mm紅紙) ,
                    FilePath = Path.Combine(excelTplPath,"Longevity_Big5cell_160x255mm_RedPaper.xlsx"),
                    SeatCount = 5,
                    FileType = FileTypeEnum.Excel,
                    PlaqueType = PlaqueTypeEnum.延生
                },
                new PrintPlaquePost()
                {
                    Key = 延生4蓮位小紅筒紅紙A4,
                    ButtonDisplayName = nameof(延生4蓮位小紅筒紅紙A4),
                    FilePath = Path.Combine(excelTplPath,"Longevity_4Seat_SmallRedBox_A4.xlsx"),
                    SeatCount = 4,
                    FileType = FileTypeEnum.Excel,
                    PlaqueType = PlaqueTypeEnum.延生
                },
                new PrintPlaquePost()
                {
                    Key = 延生1蓮位小紅筒紅紙,
                    ButtonDisplayName = nameof(延生1蓮位小紅筒紅紙),
                    FilePath = Path.Combine(excelTplPath,"Longevity_1Seat_SmallRedBox_152X215.docx"),
                    SeatCount = 1,
                    FileType = FileTypeEnum.Word,
                    PlaqueType = PlaqueTypeEnum.延生
                },
                new PrintPlaquePost()
                {
                    Key = 延生1蓮位中紅筒紅紙,
                    ButtonDisplayName = nameof(延生1蓮位中紅筒紅紙),
                    FilePath = Path.Combine(excelTplPath,"Longevity_1Seat_MiddleRedBox_215X260.docx"),
                    SeatCount = 1,
                    FileType = FileTypeEnum.Word,
                    PlaqueType = PlaqueTypeEnum.延生
                },
                new PrintPlaquePost()
                {
                    Key = 延生1蓮位大紅筒紅紙,
                    ButtonDisplayName = nameof(延生1蓮位大紅筒紅紙),
                    FilePath = Path.Combine(excelTplPath,"Longevity_1Seat_BigRedBox_215X343.docx"),
                    SeatCount = 1,
                    FileType = FileTypeEnum.Word,
                    PlaqueType = PlaqueTypeEnum.延生
                },
            };
            #endregion

            #region 附薦模版資料
            Memorial_PrintPlaquePostList = new()
            {
                new PrintPlaquePost()
                {
                    Key = 附薦小10蓮位140x420黃紙,
                    ButtonDisplayName = nameof(附薦小10蓮位140x420黃紙) ,
                    FilePath = Path.Combine(excelTplPath,"Memorial_10Seat_Small_140x420mm.xlsx"),
                    SeatCount = 10,
                    FileType = FileTypeEnum.Excel,
                    PlaqueType = PlaqueTypeEnum.附薦,
                },
                new PrintPlaquePost()
                {
                    Key = 附薦大10蓮位181x490黃紙,
                    ButtonDisplayName = nameof(附薦大10蓮位181x490黃紙) ,
                    FilePath = Path.Combine(excelTplPath,"Memorial_10Seat_Big_181x490.xlsx"),
                    SeatCount=10,
                    FileType = FileTypeEnum.Excel,
                    PlaqueType = PlaqueTypeEnum.附薦,
                },
                new PrintPlaquePost()
                {
                    Key = 附薦大黄5莲A4,
                    ButtonDisplayName = nameof(附薦大黄5莲A4) ,
                    FilePath = Path.Combine(excelTplPath,"memorial_bigyellow_5seat_A4.xlsx"),
                    SeatCount= 5,
                    FileType = FileTypeEnum.Excel,
                    PlaqueType = PlaqueTypeEnum.附薦,
                },
                new PrintPlaquePost()
                {
                    Key = 附薦20格黃紙A4,
                    ButtonDisplayName = nameof(附薦20格黃紙A4) ,
                    FilePath = Path.Combine(excelTplPath,"memorial_20Seat_A4.xlsx"),
                    SeatCount= 20,
                    FileType = FileTypeEnum.Excel,
                    PlaqueType = PlaqueTypeEnum.附薦,
                },
                new PrintPlaquePost()
                {
                    Key = 附薦5蓮位善字牌位A4,
                    ButtonDisplayName = nameof(附薦5蓮位善字牌位A4),
                    FilePath = Path.Combine(excelTplPath,"Memorial_5Seat_Kind.xlsx"),
                    SeatCount = 5,
                    FileType = FileTypeEnum.Excel,
                    PlaqueType = PlaqueTypeEnum.附薦,
                },
                new PrintPlaquePost()
                {
                    Key = 附薦3蓮位萬字牌位A4,
                    ButtonDisplayName = nameof(附薦3蓮位萬字牌位A4),
                    FilePath = Path.Combine(excelTplPath,"Memorial_3Seat_wan.xlsx"),
                    SeatCount = 3,
                    FileType = FileTypeEnum.Excel,
                    PlaqueType = PlaqueTypeEnum.附薦,
                },
                new PrintPlaquePost()
                {
                    Key = 附薦2蓮位全字牌位A4,
                    ButtonDisplayName = nameof(附薦2蓮位全字牌位A4),
                    FilePath = Path.Combine(excelTplPath,"Memorial_2Seat_quan.xlsx"),
                    SeatCount = 2,
                    FileType = FileTypeEnum.Excel,
                    PlaqueType = PlaqueTypeEnum.附薦,
                },
            };
            #endregion

            #region 功德主模版資料
            Donor_PrintPlaquePostList = new()
            {
                new PrintPlaquePost()
                {
                    Key = 延生大5蓮160x255mm紅紙,
                    ButtonDisplayName = nameof(延生大5蓮160x255mm紅紙) ,
                    FilePath = Path.Combine(excelTplPath,"Longevity_Big5cell_160x255mm_RedPaper.xlsx"),
                    SeatCount = 5,
                    FileType = FileTypeEnum.Excel,
                    PlaqueType = PlaqueTypeEnum.延生
                },
                new PrintPlaquePost()
                {
                    Key = 延生4蓮位小紅筒紅紙A4,
                    ButtonDisplayName = nameof(延生4蓮位小紅筒紅紙A4),
                    FilePath = Path.Combine(excelTplPath,"Longevity_4Seat_SmallRedBox_A4.xlsx"),
                    SeatCount = 4,
                    FileType = FileTypeEnum.Excel,
                    PlaqueType = PlaqueTypeEnum.延生
                },
                new PrintPlaquePost()
                {
                    Key = 延生1蓮位小紅筒紅紙,
                    ButtonDisplayName = nameof(延生1蓮位小紅筒紅紙),
                    FilePath = Path.Combine(excelTplPath,"Longevity_1Seat_SmallRedBox_152X215.docx"),
                    SeatCount = 1,
                    FileType = FileTypeEnum.Word,
                    PlaqueType = PlaqueTypeEnum.延生
                },
                new PrintPlaquePost()
                {
                    Key = 延生1蓮位中紅筒紅紙,
                    ButtonDisplayName = nameof(延生1蓮位中紅筒紅紙),
                    FilePath = Path.Combine(excelTplPath,"Longevity_1Seat_MiddleRedBox_215X260.docx"),
                    SeatCount = 1,
                    FileType = FileTypeEnum.Word,
                    PlaqueType = PlaqueTypeEnum.延生
                },
                new PrintPlaquePost()
                {
                    Key = 延生1蓮位大紅筒紅紙,
                    ButtonDisplayName = nameof(延生1蓮位大紅筒紅紙),
                    FilePath = Path.Combine(excelTplPath,"Longevity_1Seat_BigRedBox_215X343.docx"),
                    SeatCount = 1,
                    FileType = FileTypeEnum.Word,
                    PlaqueType = PlaqueTypeEnum.延生
                },
                new PrintPlaquePost()
                {
                    Key = 附薦5蓮位善字牌位A4,
                    ButtonDisplayName = nameof(附薦5蓮位善字牌位A4),
                    FilePath = Path.Combine(excelTplPath,"Memorial_5Seat_Kind.xlsx"),
                    SeatCount = 5,
                    FileType = FileTypeEnum.Excel,
                    PlaqueType = PlaqueTypeEnum.附薦,
                },
                new PrintPlaquePost()
                {
                    Key = 附薦3蓮位萬字牌位A4,
                    ButtonDisplayName = nameof(附薦3蓮位萬字牌位A4),
                    FilePath = Path.Combine(excelTplPath,"Memorial_3Seat_wan.xlsx"),
                    SeatCount = 3,
                    FileType = FileTypeEnum.Excel,
                    PlaqueType = PlaqueTypeEnum.附薦,
                },
                new PrintPlaquePost()
                {
                    Key = 附薦2蓮位全字牌位A4,
                    ButtonDisplayName = nameof(附薦2蓮位全字牌位A4),
                    FilePath = Path.Combine(excelTplPath,"Memorial_2Seat_quan.xlsx"),
                    SeatCount = 2,
                    FileType = FileTypeEnum.Excel,
                    PlaqueType = PlaqueTypeEnum.附薦,
                },
                new PrintPlaquePost()
                {
                    Key = 附薦大黄5莲A4,
                    ButtonDisplayName = nameof(附薦大黄5莲A4) ,
                    FilePath = Path.Combine(excelTplPath,"memorial_bigyellow_5seat_A4.xlsx"),
                    SeatCount= 5,
                    FileType = FileTypeEnum.Excel,
                    PlaqueType = PlaqueTypeEnum.附薦,
                },
                new PrintPlaquePost()
                {
                    Key = 附薦中黃1莲,
                    ButtonDisplayName = nameof(附薦中黃1莲) ,
                    FilePath = Path.Combine(excelTplPath,"memorial_middleyellow_1Seat.xlsx"),
                    SeatCount= 1,
                    FileType = FileTypeEnum.Excel,
                    PlaqueType = PlaqueTypeEnum.附薦,
                },
            };
            #endregion
        }
    }

}
