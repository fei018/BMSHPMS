using BMSHPMS.DSManage.ViewModels.Common.TplPrintExcel;
using BMSHPMS.Models.DharmaService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSManage.ViewModels.Info_LongevityVMs
{
    public class LongevityExcelTplVM : BaseVM
    {
        /// <summary>
        /// Wtm cache key, 保存 Entity IDs
        /// </summary>
        public string WtmCacheKey { get; set; }

        /// <summary>
        /// 提交頁面的 Excel範本key
        /// </summary>
        public string TemplateKey { get; set; }

        /// <summary>
        /// 提交頁面的顯示按鈕數據list
        /// </summary>
        public List<PrintExcelTplPost> TplList { get; set; }

        /// <summary>
        /// 文件下載名
        /// </summary>
        public string DownloadFileName { get; set; }

        /// <summary>
        /// 匯出excel result as byte[]
        /// </summary>
        public byte[] ExcelResult { get; set; }

        public LongevityExcelTplVM()
        {
            TplList = PrintExcelTplContext.LongevityTplPostList;
        }


        public async Task<LongevityExcelTplVM> Export()
        {
            List<string> ids = Wtm.Cache.Get<List<string>>(WtmCacheKey);
            Wtm.Cache.Remove(WtmCacheKey);

            if (ids == null)
            {
                throw new ArgumentNullException("IDs");
            }

            PrintExcelTplPost post = TplList.SingleOrDefault(x => x.Key == TemplateKey);

            if (post == null || !File.Exists(post.FilePath))
            {
                throw new Exception("PrintExcelTplPost is null or FilePath not exist.");
            }

            var list = DC.Set<Info_Longevity>().CheckIDs(ids).OrderBy(x => x.SerialCode).ToList();

            string filename = list.FirstOrDefault().SerialCode + "_" + list.LastOrDefault().SerialCode;
            DownloadFileName = "延生_" + filename + ".xlsx";       

            switch (post.Key)
            {
                case PrintExcelTplContext.延生20格205x254mm紅紙100元:
                    ExcelResult = await PrintExcelTplPost.Export<Longevity_20Seat_205x254mm,Info_Longevity>(list, post);
                    break;

                case PrintExcelTplContext.延生小5蓮210x130mm紅紙300元500元:
                    ExcelResult = await PrintExcelTplPost.Export<Longevity_20Seat_205x254mm, Info_Longevity>(list, post);
                    break;

                case PrintExcelTplContext.延生大5蓮154x255mm紅紙300元500元:
                    ExcelResult = await PrintExcelTplPost.Export<Longevity_20Seat_205x254mm, Info_Longevity>(list, post);
                    break;

                default:
                    throw new Exception("PrintExcelTplPost switch key not found: " + post.PaperDisplayName);
            }

            return this;
        }


    }
}
