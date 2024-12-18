﻿using BMSHPMS.DSManage.ViewModels.Common.PrintPlaque;
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

            // 檢查延生空名字
            foreach (var model in models)
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    throw new Exception($"延生編號:{model.SerialCode}, 沒有可用的名字");
                }
            }

            //string fileExtension;

            #region switch
            switch (post.Key)
            {
                case PrintPlaqueContext.延生20格205x254mm紅紙:
                    ExportResult = await PrintPlaqueHelper.ExportExcel<PrintPlaqueData_Longevity, Info_Longevity>(models, post);
                    //fileExtension = ".xlsx";
                    //Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

                case PrintPlaqueContext.延生小5蓮210x130mm紅紙:
                    ExportResult = await PrintPlaqueHelper.ExportExcel<PrintPlaqueData_Longevity, Info_Longevity>(models, post);
                    //fileExtension = ".xlsx";
                    //Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

                case PrintPlaqueContext.延生大5蓮160x255mm紅紙:
                    ExportResult = await PrintPlaqueHelper.ExportExcel<PrintPlaqueData_Longevity, Info_Longevity>(models, post);
                    //fileExtension = ".xlsx";
                    //Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

                case PrintPlaqueContext.延生4蓮位小紅筒紅紙A4:
                    ExportResult = await PrintPlaqueHelper.ExportExcel<PrintPlaqueData_Longevity, Info_Longevity>(models, post);
                    //fileExtension = ".xlsx";
                    //Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

                case PrintPlaqueContext.延生1蓮位小紅筒紅紙:
                    ExportResult = await PrintPlaqueHelper.ExportWord<PrintPlaqueData_Longevity, Info_Longevity>(models, post);
                    break;

                case PrintPlaqueContext.延生1蓮位中紅筒紅紙:
                    ExportResult = await PrintPlaqueHelper.ExportWord<PrintPlaqueData_Longevity, Info_Longevity>(models, post);
                    break;

                case PrintPlaqueContext.延生1蓮位大紅筒紅紙:
                    ExportResult = await PrintPlaqueHelper.ExportWord<PrintPlaqueData_Longevity, Info_Longevity>(models, post);
                    break;

                default:
                    throw new Exception(nameof(PrintPlaquePost) + " switch key not found: " + post.ButtonDisplayName);
            }
            #endregion

            DownloadFileName = "延生_" + models.FirstOrDefault().SerialCode + "_" + models.LastOrDefault()?.SerialCode + ExportResult.FileExtention;

            FileContentResult fileContentResult = new(ExportResult.FileBytes, ExportResult.Mimetype)
            {
                FileDownloadName = DownloadFileName
            };

            return fileContentResult;
        }


    }
}
