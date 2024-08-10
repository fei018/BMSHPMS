using BMSHPMS.DSManage.ViewModels.Common.PrintPlaque;
using BMSHPMS.Helper;
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

namespace BMSHPMS.DSManage.ViewModels.Info_DonorVMs
{
    public class PrintPlaqueDonorVM : BaseVM
    {
        #region 屬性
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
        /// 
        /// </summary>
        public PrintPlaqueResult ExportResult { get; set; }

        #endregion

        #region 返回 FileContentResult
        public async Task<FileContentResult> Export()
        {
            List<string> ids = Wtm.Cache.Get<List<string>>(WtmCacheKey);
            Wtm.Cache.Remove(WtmCacheKey);

            if (ids == null)
            {
                throw new ArgumentNullException("IDs");
            }

            PrintPlaquePost post = PrintPlaqueContext.Donor_PrintPlaquePostList.Where(x => x.Key == PrintPlaquePostKey).FirstOrDefault();

            if (post == null)
            {
                throw new Exception(nameof(PrintPlaquePost) + ".Key no found in PrintPlaqueContext");
            }

            if (!File.Exists(post.FilePath))
            {
                throw new Exception(Path.GetFileName(post.FilePath) + " not exist.");
            }

            List<Info_Donor> models = await DC.Set<Info_Donor>().AsNoTracking().CheckIDs(ids).OrderBy(x => x.SerialCode).ToListAsync();

            string filenameprefix = "功德主_";
            #region switch
            switch (post.Key)
            {
                #region 延生 case

                case PrintPlaqueContext.延生1蓮位小紅筒紅紙:
                    ExportResult = await PrintPlaqueHelper.ExportWordAsByte<PrintPlaqueData_Donor_Long, Info_Donor>(models, post);
                    //Mimetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    //DownloadFileName = "功德主延生_" + models.FirstOrDefault().SerialCode + "_" + models.LastOrDefault()?.SerialCode + ".docx";
                    filenameprefix = "功德主延生_";
                    break;

                case PrintPlaqueContext.延生1蓮位中紅筒紅紙:
                    ExportResult = await PrintPlaqueHelper.ExportWordAsByte<PrintPlaqueData_Donor_Long, Info_Donor>(models, post);
                    //Mimetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    filenameprefix = "功德主延生_";
                    break;

                case PrintPlaqueContext.延生1蓮位大紅筒紅紙:
                    ExportResult = await PrintPlaqueHelper.ExportWordAsByte<PrintPlaqueData_Donor_Long, Info_Donor>(models, post);
                    //Mimetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    filenameprefix = "功德主延生_";
                    break;

                case PrintPlaqueContext.延生4蓮位小紅筒A4紅紙:
                    ExportResult = await PrintPlaqueHelper.ExportWordAsByte<PrintPlaqueData_Donor_Long, Info_Donor>(models, post);
                    filenameprefix = "功德主延生_";
                    break;
                #endregion

                #region 附薦 case

                case PrintPlaqueContext.附薦5蓮位善字牌位A4紙:
                    ProcessDeceasedName(ref models);
                    //models.ForEach(x => x.BenefactorName = $"陽上：{x.BenefactorName}拜荐");
                    ExportResult = await PrintPlaqueHelper.ExportByteAsExcel<PrintPlaqueData_Donor_Memo, Info_Donor>(models, post);
                    //Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    filenameprefix = "功德主附薦_";
                    break;

                case PrintPlaqueContext.附薦3蓮位萬字牌位A4紙:
                    ProcessDeceasedName(ref models);
                    ExportResult = await PrintPlaqueHelper.ExportByteAsExcel<PrintPlaqueData_Donor_Memo, Info_Donor>(models, post);
                    //Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    filenameprefix = "功德主附薦_";
                    break;

                case PrintPlaqueContext.附薦2蓮位全字牌位A4紙:
                    ProcessDeceasedName(ref models);
                    ExportResult = await PrintPlaqueHelper.ExportByteAsExcel<PrintPlaqueData_Donor_Memo, Info_Donor>(models, post);
                    //Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    filenameprefix = "功德主附薦_";
                    break;

                #endregion

                default:
                    throw new Exception(nameof(PrintPlaquePost) + " switch key not found: " + post.ButtonDisplayName);
            }
            #endregion

            DownloadFileName = filenameprefix + models.FirstOrDefault().SerialCode + "_" + models.LastOrDefault()?.SerialCode + ExportResult.FileExtention;

            FileContentResult fileContentResult = new(ExportResult.FileBytes, ExportResult.Mimetype)
            {
                FileDownloadName = DownloadFileName
            };

            return fileContentResult;
        }
        #endregion

        #region 處理附薦名
        private void ProcessDeceasedName(ref List<Info_Donor> models)
        {
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
        }
        #endregion
    }
}

