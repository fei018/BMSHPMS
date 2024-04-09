using BMSHPMS.DSManage.ViewModels.Common.TplPrintExcel;
using BMSHPMS.Models.DharmaService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

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
        public List<PrintPlaqueTplPost> TplList { get; set; }

        /// <summary>
        /// 文件下載名
        /// </summary>
        public string DownloadFileName { get; set; }

        /// <summary>
        /// 匯出excel result as byte[]
        /// </summary>
        public byte[] ResultAsBytes { get; set; }

        public string Mimetype { get; set; }

        public MemorialExcelTplVM()
        {
            TplList = PrintExcelTplContext.MemorialTplPostList;
        }


        public async Task<FileContentResult> Export()
        {
            List<string> ids = Wtm.Cache.Get<List<string>>(WtmCacheKey);
            Wtm.Cache.Remove(WtmCacheKey);

            if (ids == null)
            {
                throw new ArgumentNullException("IDs");
            }

            PrintPlaqueTplPost post = TplList.Where(x => x.Key == TemplateKey).FirstOrDefault();

            if (post == null || !File.Exists(post.FilePath))
            {
                throw new Exception("PrintExcelTplPost is null or FilePath not exist.");
            }

            var list = DC.Set<Info_Memorial>().AsNoTracking().CheckIDs(ids).OrderBy(x => x.SerialCode).ToList();


            string fileExtension;

            switch (post.Key)
            {
                case PrintExcelTplContext.附薦小10蓮位140x420:
                    ResultAsBytes = await PrintPlaqueExcelHelper.Export<Memorial_PrintExcelFillData, Info_Memorial>(list, post);
                    fileExtension = ".xlsx";
                    Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

                case PrintExcelTplContext.附薦大10蓮位181x490:
                    ResultAsBytes = await PrintPlaqueExcelHelper.Export<Memorial_PrintExcelFillData, Info_Memorial>(list, post);
                    fileExtension = ".xlsx";
                    Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

                case PrintExcelTplContext.附薦5蓮位善字牌位:
                    list.ForEach(x =>
                    {
                        x.BenefactorName = $"陽上：{x.BenefactorName}拜荐";
                    });
                    ResultAsBytes = await PrintPlaqueExcelHelper.Export<Memorial_PrintExcelFillData, Info_Memorial>(list, post);
                    fileExtension = ".xlsx";
                    Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

                default:
                    throw new Exception(nameof(PrintPlaqueTplPost) + " switch key not found: " + post.ButtonDisplayName);
            }

            string filename = list.FirstOrDefault().SerialCode + "_" + list.LastOrDefault().SerialCode;
            DownloadFileName = "附薦_" + filename + fileExtension;

            FileContentResult fileContentResult = new(ResultAsBytes, Mimetype)
            {
                FileDownloadName = DownloadFileName
            };

            return fileContentResult;
        }

    }
}

