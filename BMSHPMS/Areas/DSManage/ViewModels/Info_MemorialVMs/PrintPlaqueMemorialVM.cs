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
        public byte[] ResultBytes { get; set; }

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

            switch (post.Key)
            {
                case PrintPlaqueContext.附薦小10蓮位140x420黃紙:
                    ResultBytes = await PrintPlaqueHelper.ExportByteAsExcel<PrintPlaqueData_Memorial, Info_Memorial>(models, post);
                    Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    DownloadFileName = "附薦_" + models.FirstOrDefault().SerialCode + "_" + models.LastOrDefault()?.SerialCode + ".xlsx";
                    break;

                case PrintPlaqueContext.附薦大10蓮位181x490黃紙:
                    ResultBytes = await PrintPlaqueHelper.ExportByteAsExcel<PrintPlaqueData_Memorial, Info_Memorial>(models, post);
                    Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    DownloadFileName = "附薦_" + models.FirstOrDefault().SerialCode + "_" + models.LastOrDefault()?.SerialCode + ".xlsx";
                    break;

                case PrintPlaqueContext.附薦5蓮位善字牌位A4紙:
                    models.ForEach(x => x.BenefactorName = $"陽上：{x.BenefactorName}拜荐");
                    ResultBytes = await PrintPlaqueHelper.ExportByteAsExcel<PrintPlaqueData_Memorial, Info_Memorial>(models, post);
                    Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    DownloadFileName = "附薦_" + models.FirstOrDefault().SerialCode + "_" + models.LastOrDefault()?.SerialCode + ".xlsx";
                    break;

                default:
                    throw new Exception(nameof(PrintPlaquePost) + " switch key not found: " + post.ButtonDisplayName);
            }

            FileContentResult fileContentResult = new(ResultBytes, Mimetype)
            {
                FileDownloadName = DownloadFileName
            };

            return fileContentResult;
        }

    }
}

