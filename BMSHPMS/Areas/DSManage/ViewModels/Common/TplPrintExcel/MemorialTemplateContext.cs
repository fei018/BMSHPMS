using System;
using System.Collections.Generic;
using System.IO;

namespace BMSHPMS.DSManage.ViewModels.Common.TplPrintExcel
{
    public class MemorialTemplateContext
    {

        public static List<PrintExcelTplPost> TplPostList { get; private set; }

        public static void SetTemplateList(string wwwPath)
        {
            string excelTplPath = Path.Combine(wwwPath, "excelTemplate");

            TplPostList = new()
            {             
                new PrintExcelTplPost()
                { 
                    Key = "fb66bbd9420c460aa76cf4f5bee048ad",
                    PaperDisplayName = "附薦 10蓮位(小) (140x420mm) (100元)" , 
                    FilePath = Path.Combine(excelTplPath,".xlsx"),
                },
                new PrintExcelTplPost() 
                { 
                    Key = "e86a36ec3b6246f1933e72cbb9030bf1", 
                    PaperDisplayName = "" , 
                    FilePath = Path.Combine(excelTplPath,".xlsx")
                },
            };

        }
    }

}
