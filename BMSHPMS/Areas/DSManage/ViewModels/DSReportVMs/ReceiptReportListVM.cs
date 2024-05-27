using BMSHPMS.DSManage.ViewModels.Info_ReceiptVMs;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSManage.ViewModels.DSReportVMs
{
    public class ReceiptReportListVM : BaseVM
    {
        public Info_ReceiptSearcher Searcher { get; set; }

        /// <summary>
        /// 功德項目歸類
        /// </summary>
        public Dictionary<string, ProjectCategoryVM> ProjectCategoryDic { get; set; } = new();

        #region GetSearchQuery
        public List<Info_Receipt_View> GetSearchQuery()
        {
            var query = DC.Set<Info_Receipt>()
                            .AsNoTracking()
                            .CheckEqual(Searcher.DharmaServiceName, x => x.DharmaServiceName)
                            .CheckEqual(Searcher.DharmaServiceYear, x => x.DharmaServiceYear)
                            .CheckBetween(Searcher.ReceiptDate?.GetStartTime(), Searcher.ReceiptDate?.GetEndTime(), x => x.ReceiptDate)
                            .Select(x => new Info_Receipt_View
                            {
                                ID = x.ID,
                                ReceiptNumber = x.ReceiptNumber,
                                ReceiptDate = x.ReceiptDate,
                                DharmaServiceName = x.DharmaServiceName,
                                DharmaServiceYear = x.DharmaServiceYear,
                            })
                            .OrderBy(x => x.ID);

            var list = query.ToList();

            foreach (var receipt in list)
            {
                receipt.Info_Donors = DC.Set<Info_Donor>().AsNoTracking().Where(x => x.ReceiptID == receipt.ID)
                                            .Select(x => new Info_Donor
                                            {
                                                ID = x.ID,
                                                Sum = x.Sum,
                                                ReceiptID = x.ReceiptID,
                                            })
                                            .ToList();

                receipt.Info_Memorials = DC.Set<Info_Memorial>().AsNoTracking().Where(x => x.ReceiptID == receipt.ID)
                                            .Select(x => new Info_Memorial
                                            {
                                                ID = x.ID,
                                                Sum = x.Sum,
                                                ReceiptID = x.ReceiptID,
                                            })
                                            .ToList();

                receipt.Info_Longevitys = DC.Set<Info_Longevity>().AsNoTracking().Where(x => x.ReceiptID == receipt.ID)
                                                .Select(x => new Info_Longevity
                                                {
                                                    ID = x.ID,
                                                    Sum = x.Sum,
                                                    ReceiptID = x.ReceiptID,
                                                })
                                                .ToList();

                int sum = 0;

                foreach (var item in receipt.Info_Donors)
                {
                    if (item.Sum.HasValue)
                    {
                        sum += item.Sum.Value;

                        var pro = new ProjectCategoryVM()
                        {
                            ProjectName = "功德主",
                            ProjectSum = item.Sum.Value.ToString(),
                        };
                        ProjectCategoryDic.TryAdd(pro.Key, pro);
                    }                   
                }

                foreach (var item in receipt.Info_Memorials)
                {
                    if (item.Sum.HasValue)
                    {
                        sum += item.Sum.Value;

                        var pro = new ProjectCategoryVM()
                        {
                            ProjectName = "附薦",
                            ProjectSum = item.Sum.Value.ToString(),
                        };
                        ProjectCategoryDic.TryAdd(pro.Key, pro);
                    }
                }

                foreach (var item in receipt.Info_Longevitys)
                {
                    if (item.Sum.HasValue)
                    {
                        sum += item.Sum.Value;

                        var pro = new ProjectCategoryVM()
                        {
                            ProjectName = "延生",
                            ProjectSum = item.Sum.Value.ToString(),
                        };
                        ProjectCategoryDic.TryAdd(pro.Key, pro);
                    }
                }

                receipt.CalculateSum = sum;
            }

            return list;
        }
        #endregion


        public void GenReport()
        {
            var receipts = GetSearchQuery();
      
        }
    }
}
