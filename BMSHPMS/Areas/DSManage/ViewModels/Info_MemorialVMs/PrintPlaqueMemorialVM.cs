using BMSHPMS.DSManage.ViewModels.Common.PrintPlaque;
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
    public class PrintPlaqueMemorialVM : BaseVM
    {
        /// <summary>
        /// Wtm cache key, 保存 Entity IDs
        /// </summary>
        public string WtmCacheKey { get; set; }

        /// <summary>
        /// 提交頁面的 Excel範本key
        /// </summary>
        public string PrintPlaquePostKey { get; set; }

        /// <summary>
        /// 文件下載名
        /// </summary>
        public string DownloadFileName { get; set; }

        /// <summary>
        /// 匯出excel result as byte[]
        /// </summary>
        public PrintPlaqueResult ExportResult { get; set; }

        public string Mimetype { get; set; }


        public async Task<FileContentResult> Export()
        {
            List<string> ids = Wtm.Cache.Get<List<string>>(WtmCacheKey);
            Wtm.Cache.Remove(WtmCacheKey);

            if (ids == null)
            {
                throw new ArgumentNullException("IDs");
            }

            PrintPlaquePost post = PrintPlaqueContext.Memorial_PrintPlaquePostList.Where(x => x.Key == PrintPlaquePostKey).FirstOrDefault();

            if (post == null)
            {
                throw new Exception(nameof(PrintPlaquePost) + ".Key no found in PrintPlaqueContext");
            }

            if (!File.Exists(post.FilePath))
            {
                throw new Exception(Path.GetFileName(post.FilePath) + " not exist.");
            }

            var models = DC.Set<Info_Memorial>().AsNoTracking().CheckIDs(ids).OrderBy(x => x.SerialCode).ToList();

            #region 處理附薦名, 附薦名3合1
            foreach (var item in models)
            {
                string tmp = null;

                if (!string.IsNullOrWhiteSpace(item.DeceasedName_1))
                {
                    tmp += item.DeceasedName_1 + "\n";
                }

                if (!string.IsNullOrWhiteSpace(item.DeceasedName_2))
                {
                    tmp += item.DeceasedName_2 + "\n";
                }

                if (!string.IsNullOrWhiteSpace(item.DeceasedName_3))
                {
                    tmp += item.DeceasedName_3 + "\n";
                }

                if (string.IsNullOrEmpty(tmp))
                {
                    throw new Exception($"附薦編號:{item.SerialCode}, 沒有可用的附薦名稱.");
                }

                item.DeceasedName_1 = tmp.TrimEnd('\n');
            }
            #endregion

            switch (post.Key)
            {
                case PrintPlaqueContext.附薦小10蓮位140x420黃紙:
                    ExportResult = await PrintPlaqueHelper.ExportExcel<PrintPlaqueData_Memorial, Info_Memorial>(models, post);
                    //Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //DownloadFileName = "附薦_" + models.FirstOrDefault().SerialCode + "_" + models.LastOrDefault()?.SerialCode + ".xlsx";
                    break;

                case PrintPlaqueContext.附薦大10蓮位181x490黃紙:
                    ExportResult = await PrintPlaqueHelper.ExportExcel<PrintPlaqueData_Memorial, Info_Memorial>(models, post);
                    //Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //DownloadFileName = "附薦_" + models.FirstOrDefault().SerialCode + "_" + models.LastOrDefault()?.SerialCode + ".xlsx";
                    break;

                case PrintPlaqueContext.附薦大黄5莲A4:
                    ProcessBenefactorName(ref models);
                    ExportResult = await PrintPlaqueHelper.ExportExcel<PrintPlaqueData_Memorial, Info_Memorial>(models, post);
                    break;

                case PrintPlaqueContext.附薦20格黃紙A4:
                    ProcessBenefactorName(ref models);
                    ExportResult = await PrintPlaqueHelper.ExportExcel<PrintPlaqueData_Memorial, Info_Memorial>(models, post);
                    break;

                default:
                    throw new Exception(nameof(PrintPlaquePost) + " switch key not found: " + post.ButtonDisplayName);
            }

            DownloadFileName = "附薦_" + models.FirstOrDefault().SerialCode + "_" + models.LastOrDefault()?.SerialCode + ExportResult.FileExtention;

            FileContentResult fileContentResult = new(ExportResult.FileBytes, ExportResult.Mimetype)
            {
                FileDownloadName = DownloadFileName
            };

            return fileContentResult;
        }

        /// <summary>
        /// 處理陽上名
        /// </summary>
        /// <param name="models"></param>
        private void ProcessBenefactorName(ref List<Info_Memorial> models)
        {
            foreach (var m in models)
            {
                if (!string.IsNullOrEmpty(m.BenefactorName))
                {
                    m.BenefactorName = "陽上" + m.BenefactorName + "拜荐";
                }
            }
        }
    }
}

