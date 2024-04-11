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

namespace BMSHPMS.DSManage.ViewModels.Info_LongevityVMs
{
    public class PrintPlaqueLongevityVM : BaseVM
    {
        /// <summary>
        /// Wtm cache key, 保存 Entity IDs
        /// </summary>
        public string WtmCacheKey { get; set; }

        /// <summary>
        /// 提交頁面的 PrintPlaquePost key
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

            PrintPlaquePost post = PrintPlaqueContext.Longevity_PrintPlaquePostList.Where(x => x.Key == PrintPlaquePostKey).FirstOrDefault();

            if (post == null)
            {
                throw new Exception(nameof(PrintPlaquePost) + ".Key no found in PrintPlaqueContext");
            }

            if (!File.Exists(post.FilePath))
            {
                throw new Exception(Path.GetFileName(post.FilePath) + " not exist.");
            }

            var models = DC.Set<Info_Longevity>().AsNoTracking().CheckIDs(ids).OrderBy(x => x.SerialCode).ToList();

            string fileExtension;

            #region switch
            switch (post.Key)
            {
                case PrintPlaqueContext.延生20格205x254mm紅紙:
                    ResultBytes = await PrintPlaqueHelper.ExportByteAsExcel<PrintPlaqueData_Longevity, Info_Longevity>(models, post);
                    fileExtension = ".xlsx";
                    Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

                case PrintPlaqueContext.延生小5蓮210x130mm紅紙:
                    ResultBytes = await PrintPlaqueHelper.ExportByteAsExcel<PrintPlaqueData_Longevity, Info_Longevity>(models, post);
                    fileExtension = ".xlsx";
                    Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

                case PrintPlaqueContext.延生大5蓮154x255mm紅紙:
                    ResultBytes = await PrintPlaqueHelper.ExportByteAsExcel<PrintPlaqueData_Longevity, Info_Longevity>(models, post);
                    fileExtension = ".xlsx";
                    Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

                case PrintPlaqueContext.延生4蓮位小紅筒A4紅紙:
                    ResultBytes = await PrintPlaqueHelper.ExportByteAsExcel<PrintPlaqueData_Longevity, Info_Longevity>(models, post);
                    fileExtension = ".xlsx";
                    Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

                //case PrintPlaqueContext.延生1蓮位小紅筒紅紙:
                //    ResultBytes = await PrintPlaqueHelper.ExportByteAsWord<PrintPlaqueData_Longevity, Info_Longevity>(models, post);
                //    fileExtension = ".docx";
                //    Mimetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                //    break;

                //case PrintPlaqueContext.延生1蓮位中紅筒紅紙:
                //    ResultBytes = await PrintPlaqueHelper.ExportByteAsWord<PrintPlaqueData_Longevity, Info_Longevity>(list, post);
                //    fileExtension = ".docx";
                //    Mimetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                //    break;

                //case PrintPlaqueContext.延生1蓮位大紅筒紅紙:
                //    ResultBytes = await PrintPlaqueHelper.ExportByteAsWord<PrintPlaqueData_Longevity, Info_Longevity>(list, post);
                //    fileExtension = ".docx";
                //    Mimetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                //    break;

                default:
                    throw new Exception(nameof(PrintPlaquePost) + " switch key not found: " + post.ButtonDisplayName);
            }
            #endregion

            DownloadFileName = "延生_" + models.FirstOrDefault().SerialCode + "_" + models.LastOrDefault()?.SerialCode + fileExtension;

            FileContentResult fileContentResult = new(ResultBytes, Mimetype)
            {
                FileDownloadName = DownloadFileName
            };

            return fileContentResult;
        }


    }
}
