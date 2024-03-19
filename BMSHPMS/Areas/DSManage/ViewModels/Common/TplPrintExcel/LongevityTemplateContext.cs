using System;
using System.Collections.Generic;
using System.IO;

namespace BMSHPMS.DSManage.ViewModels.Common.TplPrintExcel
{
    public class LongevityTemplateContext
    {

        public static List<PrintExcelTplPost> TplPostList { get; private set; }

        public static void SetTemplateList(string wwwPath)
        {
            string excelTplPath = Path.Combine(wwwPath, "excelTemplate");

            TplPostList = new()
            {
                // new LongevityTemplate() { Key = "", FilePath = "", PaperDisplayName = ""},
                new PrintExcelTplPost()
                { 
                    Key = "e37b7d2266ad4e329ff6770b960589a1",
                    PaperDisplayName = "延生 20格 205x254mm 紅紙 (100元)" , 
                    FilePath = Path.Combine(excelTplPath,"Longevity_20cell_205x254mm_RedPaper.xlsx"),
                    CellCount = 20,
                },
                new PrintExcelTplPost() 
                { 
                    Key = "e47f5c2e676847249af65c1d924d2349", 
                    PaperDisplayName = "延生 小5蓮 210x130mm 紅紙 (300元,500元)" , 
                    FilePath = Path.Combine(excelTplPath,"Longevity_small5cell_210x130mm_redpaper.xlsx"),
                    CellCount = 5,
                },
                new PrintExcelTplPost()
                {
                    Key = "d02daa2668b3456d8763301d981fa250",
                    PaperDisplayName = "延生 大5蓮 154x255mm 紅紙 (300元,500元)" ,
                    FilePath = Path.Combine(excelTplPath,"Longevity_Big5cell_154x255m_RedPaper.xlsx"),
                    CellCount = 5,
                },
            };

        }
    }

}
