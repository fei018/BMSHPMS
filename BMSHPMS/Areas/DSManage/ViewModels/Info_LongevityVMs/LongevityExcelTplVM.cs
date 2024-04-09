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


        public LongevityExcelTplVM()
        {
            TplList = PrintExcelTplContext.LongevityTplPostList;
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

            if (post == null)
            {
                throw new Exception("PrintExcelTplPost is null.");
            }
            if (!File.Exists(post.FilePath))
            {
                throw new Exception("FilePath not exist.");
            }

            var list = DC.Set<Info_Longevity>().AsNoTracking().CheckIDs(ids).OrderBy(x => x.SerialCode).ToList();

            string fileExtension;

            switch (post.Key)
            {
                case PrintExcelTplContext.延生20格205x254mm紅紙:
                    ResultAsBytes = await PrintPlaqueExcelHelper.Export<Longevity_PrintExcelFillData, Info_Longevity>(list, post);
                    fileExtension = ".xlsx";
                    Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

                case PrintExcelTplContext.延生小5蓮210x130mm紅紙:
                    ResultAsBytes = await PrintPlaqueExcelHelper.Export<Longevity_PrintExcelFillData, Info_Longevity>(list, post);
                    fileExtension = ".xlsx";
                    Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

                case PrintExcelTplContext.延生大5蓮154x255mm紅紙:
                    ResultAsBytes = await PrintPlaqueExcelHelper.Export<Longevity_PrintExcelFillData, Info_Longevity>(list, post);
                    fileExtension = ".xlsx";
                    Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

                case PrintExcelTplContext.延生4蓮位小紅筒A4紙:
                    ResultAsBytes = await PrintPlaqueExcelHelper.Export<Longevity_PrintExcelFillData, Info_Longevity>(list, post);
                    fileExtension = ".xlsx";
                    Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

                case PrintExcelTplContext.延生1蓮位小紅筒紅紙:
                    ResultAsBytes = await PrintPlaqueWordHelper.Export<Longevity_PrintExcelFillData, Info_Longevity>(list, post);
                    fileExtension = ".docx";
                    Mimetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;

                case PrintExcelTplContext.延生1蓮位中紅筒紅紙:
                    ResultAsBytes = await PrintPlaqueWordHelper.Export<Longevity_PrintExcelFillData, Info_Longevity>(list, post);
                    fileExtension = ".docx";
                    Mimetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;

                case PrintExcelTplContext.延生1蓮位大紅筒紅紙:
                    ResultAsBytes = await PrintPlaqueWordHelper.Export<Longevity_PrintExcelFillData, Info_Longevity>(list, post);
                    fileExtension = ".docx";
                    Mimetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;

                default:
                    throw new Exception(nameof(PrintPlaqueTplPost) + " switch key not found: " + post.ButtonDisplayName);
            }

            string filename = list.FirstOrDefault().SerialCode + "_" + list.LastOrDefault().SerialCode;
            DownloadFileName = "延生_" + filename + fileExtension;

            FileContentResult fileContentResult = new(ResultAsBytes, Mimetype)
            {
                FileDownloadName = DownloadFileName
            };

            return fileContentResult;
        }


    }
}
