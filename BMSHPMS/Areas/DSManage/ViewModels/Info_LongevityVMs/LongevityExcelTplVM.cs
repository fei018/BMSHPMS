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

        public LongevityExcelTplVM()
        {
            TplList = LongevityTemplateContext.TplPostList;
        }

        protected override void InitVM()
        {

        }

        public async Task<byte[]> Export()
        {
            List<string> ids = Wtm.Cache.Get<List<string>>(WtmCacheKey);
            Wtm.Cache.Remove(WtmCacheKey);

            if (ids == null)
            {
                throw new ArgumentNullException("IDs");
            }

            PrintExcelTplPost tpl = TplList.SingleOrDefault(x => x.Key == TemplateKey);

            if (tpl == null || !File.Exists(tpl.FilePath))
            {
                throw new Exception("DonationTemplate is null or FilePath not exist.");
            }

            var list = DC.Set<Info_Longevity>().CheckIDs(ids).OrderBy(x => x.SerialCode).ToList();

            byte[] result = null;

            switch (tpl.Key)
            {
                case "e37b7d2266ad4e329ff6770b960589a1":
                    result = await LongevityTpl_20cell_205x254mm_RedPaper.Export(list, tpl);
                    break;

                case "e47f5c2e676847249af65c1d924d2349":
                    result = await LongevityTpl_20cell_205x254mm_RedPaper.Export(list, tpl);
                    break;

                case "d02daa2668b3456d8763301d981fa250":
                    result = await LongevityTpl_20cell_205x254mm_RedPaper.Export(list, tpl);
                    break;

                default:
                    throw new Exception("範本switch case key 匹配不到.");
            }

            return result;
        }


    }
}
