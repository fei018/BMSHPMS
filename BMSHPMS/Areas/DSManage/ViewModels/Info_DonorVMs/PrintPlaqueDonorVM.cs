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
        /// 匯出excel result as byte[]
        /// </summary>
        public byte[] ResultBytes { get; set; }


        public string Mimetype { get; set; }
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

            PrintPlaquePost post = PrintPlaqueContext.All_PrintPlaquePost.Where(x => x.Key == PrintPlaquePostKey).FirstOrDefault();

            if (post == null)
            {
                throw new Exception(nameof(PrintPlaquePost) + ".Key no found in PrintPlaqueContext");
            }

            if (!File.Exists(post.FilePath))
            {
                throw new Exception("FilePath not exist.");
            }

            List<Info_Donor> models = await DC.Set<Info_Donor>().CheckIDs(ids).OrderBy(x => x.SerialCode).ToListAsync();

            if (post.PlaqueType == PlaqueTypeEnum.延生 && post.FileType == FileTypeEnum.Excel)
            {
                ResultBytes = await PrintPlaqueHelper.ExportByteAsExcel<PrintPlaqueData_Donor_Long, Info_Donor>(models, post);
                Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                DownloadFileName = "延生_" + models.FirstOrDefault()?.SerialCode + "_" + models.LastOrDefault()?.SerialCode + ".xlsx";
            }

            if (post.PlaqueType == PlaqueTypeEnum.延生 && post.FileType == FileTypeEnum.Word)
            {
                ResultBytes = await PrintPlaqueHelper.ExportByteAsWord<PrintPlaqueData_Donor_Long, Info_Donor>(models, post);
                Mimetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                DownloadFileName = "延生_" + models.FirstOrDefault().SerialCode + "_" + models.LastOrDefault()?.SerialCode + ".docx";
            }

            if (post.PlaqueType == PlaqueTypeEnum.附薦 && post.FileType == FileTypeEnum.Excel)
            {
                ResultBytes = await PrintPlaqueHelper.ExportByteAsExcel<PrintPlaqueData_Donor_Memo, Info_Donor>(models, post);
                Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                DownloadFileName = "附薦_" + models.FirstOrDefault().SerialCode + "_" + models.LastOrDefault()?.SerialCode + ".xlsx";
            }

            #region drop
            //List<Info_Longevity> longevityList;
            //List<Info_Memorial> memorialList;

            //switch (post.Key)
            //{
            //    #region 延生 case
            //    case PrintPlaqueContext.延生20格205x254mm紅紙:
            //        longevityList = await DC.Set<Info_Longevity>().CheckIDs(ids).OrderBy(x => x.SerialCode).ToListAsync();
            //        ResultBytes = await PrintPlaqueHelper.ExportByteAsExcel<Longevity_PrintExcelFillData, Info_Longevity>(longevityList, post);
            //        Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //        DownloadFileName = "延生_" + longevityList.FirstOrDefault().SerialCode + "_" + longevityList.LastOrDefault()?.SerialCode + ".xlsx";
            //        break;

            //    case PrintPlaqueContext.延生小5蓮210x130mm紅紙:
            //        longevityList = await DC.Set<Info_Longevity>().CheckIDs(ids).OrderBy(x => x.SerialCode).ToListAsync();
            //        ResultBytes = await PrintPlaqueHelper.ExportByteAsExcel<Longevity_PrintExcelFillData, Info_Longevity>(longevityList, post);
            //        Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //        DownloadFileName = "延生_" + longevityList.FirstOrDefault().SerialCode + "_" + longevityList.LastOrDefault()?.SerialCode + ".xlsx";
            //        break;

            //    case PrintPlaqueContext.延生大5蓮154x255mm紅紙:
            //        longevityList = await DC.Set<Info_Longevity>().CheckIDs(ids).OrderBy(x => x.SerialCode).ToListAsync();
            //        ResultBytes = await PrintPlaqueHelper.ExportByteAsExcel<Longevity_PrintExcelFillData, Info_Longevity>(longevityList, post);
            //        Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //        DownloadFileName = "延生_" + longevityList.FirstOrDefault().SerialCode + "_" + longevityList.LastOrDefault()?.SerialCode + ".xlsx";
            //        break;

            //    case PrintPlaqueContext.延生4蓮位小紅筒A4紙:
            //        longevityList = await DC.Set<Info_Longevity>().CheckIDs(ids).OrderBy(x => x.SerialCode).ToListAsync();
            //        ResultBytes = await PrintPlaqueHelper.ExportByteAsExcel<Longevity_PrintExcelFillData, Info_Longevity>(longevityList, post);
            //        Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //        DownloadFileName = "延生_" + longevityList.FirstOrDefault().SerialCode + "_" + longevityList.LastOrDefault()?.SerialCode + ".xlsx";
            //        break;

            //    case PrintPlaqueContext.延生1蓮位小紅筒紅紙:
            //        longevityList = await DC.Set<Info_Longevity>().CheckIDs(ids).OrderBy(x => x.SerialCode).ToListAsync();
            //        ResultBytes = await PrintPlaqueHelper.ExportByteAsWord<Longevity_PrintExcelFillData, Info_Longevity>(longevityList, post);
            //        Mimetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            //        DownloadFileName = "延生_" + longevityList.FirstOrDefault().SerialCode + "_" + longevityList.LastOrDefault()?.SerialCode + ".docx";
            //        break;

            //    case PrintPlaqueContext.延生1蓮位中紅筒紅紙:
            //        longevityList = await DC.Set<Info_Longevity>().CheckIDs(ids).OrderBy(x => x.SerialCode).ToListAsync();
            //        ResultBytes = await PrintPlaqueHelper.ExportByteAsWord<Longevity_PrintExcelFillData, Info_Longevity>(longevityList, post);
            //        Mimetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            //        DownloadFileName = "延生_" + longevityList.FirstOrDefault().SerialCode + "_" + longevityList.LastOrDefault()?.SerialCode + ".docx";
            //        break;

            //    case PrintPlaqueContext.延生1蓮位大紅筒紅紙:
            //        longevityList = await DC.Set<Info_Longevity>().CheckIDs(ids).OrderBy(x => x.SerialCode).ToListAsync();
            //        ResultBytes = await PrintPlaqueHelper.ExportByteAsWord<Longevity_PrintExcelFillData, Info_Longevity>(longevityList, post);
            //        Mimetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            //        DownloadFileName = "延生_" + longevityList.FirstOrDefault().SerialCode + "_" + longevityList.LastOrDefault()?.SerialCode + ".docx";
            //        break;
            //    #endregion

            //    #region 附薦 case
            //    case PrintPlaqueContext.附薦小10蓮位140x420:
            //        memorialList = await DC.Set<Info_Memorial>().CheckIDs(ids).OrderBy(x => x.SerialCode).ToListAsync();
            //        ResultBytes = await PrintPlaqueHelper.ExportByteAsExcel<Memorial_PrintExcelFillData, Info_Memorial>(memorialList, post);
            //        Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //        DownloadFileName = "附薦_" + memorialList.FirstOrDefault().SerialCode + "_" + memorialList.LastOrDefault()?.SerialCode + ".xlsx";
            //        break;

            //    case PrintPlaqueContext.附薦大10蓮位181x490:
            //        memorialList = await DC.Set<Info_Memorial>().CheckIDs(ids).OrderBy(x => x.SerialCode).ToListAsync();
            //        ResultBytes = await PrintPlaqueHelper.ExportByteAsExcel<Memorial_PrintExcelFillData, Info_Memorial>(memorialList, post);
            //        Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //        DownloadFileName = "附薦_" + memorialList.FirstOrDefault().SerialCode + "_" + memorialList.LastOrDefault()?.SerialCode + ".xlsx";
            //        break;

            //    case PrintPlaqueContext.附薦5蓮位善字牌位:
            //        memorialList = await DC.Set<Info_Memorial>().CheckIDs(ids).OrderBy(x => x.SerialCode).ToListAsync();memorialList.Sort()
            //        memorialList.ForEach(x => x.BenefactorName = $"陽上：{x.BenefactorName}拜荐");
            //        ResultBytes = await PrintPlaqueHelper.ExportByteAsExcel<Memorial_PrintExcelFillData, Info_Memorial>(memorialList, post);
            //        Mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //        DownloadFileName = "附薦_" + memorialList.FirstOrDefault().SerialCode + "_" + memorialList.LastOrDefault()?.SerialCode + ".xlsx";
            //        break;
            //    #endregion

            //    default:
            //        throw new Exception(nameof(PrintPlaquePost) + " switch key not found: " + post.ButtonDisplayName);
            //}
            #endregion


            FileContentResult fileContentResult = new(ResultBytes, Mimetype)
            {
                FileDownloadName = DownloadFileName
            };

            return fileContentResult;
        }
        #endregion
    }
}

