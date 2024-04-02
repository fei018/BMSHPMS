using BMSHPMS.DSManage.ViewModels.Common.TplPrintExcel;
using BMSHPMS.DSManage.ViewModels.Info_LongevityVMs;
using BMSHPMS.Models.DharmaService;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WalkingTec.Mvvm.Core;
using System.Linq;
using WalkingTec.Mvvm.Core.Extensions;
using System.IO;

namespace BMSHPMS.DSManage.ViewModels.Info_MemorialVMs
{
    public class MemorialExcelTplVM : BaseVM
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

        public MemorialExcelTplVM()
        {
            TplList = PrintExcelTplContext.MemorialTplPostList;
        }


        public async Task<MemorialExcelTplVM> Export()
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

            var list = DC.Set<Info_Memorial>().CheckIDs(ids).OrderBy(x => x.SerialCode).ToList();

            string filename = list.FirstOrDefault().SerialCode + "_" + list.LastOrDefault().SerialCode;
            DownloadFileName = "附薦_" + filename + ".xlsx";

            switch (post.Key)
            {
                case PrintExcelTplContext.附薦小10蓮位140x420mm100元:
                    ExcelResult = await PrintExcelTplPost.Export<Memorial_10SeatSmall_140x420mm, Info_Memorial>(list, post);
                    break;

                case PrintExcelTplContext.附薦大10蓮位100元:
                    ExcelResult = await PrintExcelTplPost.Export<Memorial_10SeatSmall_140x420mm, Info_Memorial>(list, post);
                    break;

                case PrintExcelTplContext.附薦5蓮位善字牌位:
                    list.ForEach(x =>
                    {
                        x.BenefactorName = $"陽上：{x.BenefactorName}拜荐";
                    });
                    ExcelResult = await PrintExcelTplPost.Export<Memorial_10SeatSmall_140x420mm, Info_Memorial>(list, post);
                    break;

                default:
                    throw new Exception("PrintExcelTplPost switch key not found: " + post.PaperDisplayName);
            }

            return this;
        }

    }
}

