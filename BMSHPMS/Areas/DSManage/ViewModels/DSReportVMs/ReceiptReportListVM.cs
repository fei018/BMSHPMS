using BMSHPMS.Areas.DSManage.ViewModels.DSReportVMs;
using BMSHPMS.DSManage.ViewModels.Info_ReceiptVMs;
using BMSHPMS.Helper;
using BMSHPMS.Models.DharmaService;
using BMSHPMS.Models.DharmaServiceExtention;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSManage.ViewModels.DSReportVMs
{
    public class ReceiptReportListVM : BasePagedListVM<ProjectCategoryVM, ReceiptReportSearcher>
    {

        protected override void InitVM()
        {
            NeedPage = false;
        }

        /// <summary>
        /// 功德項目歸類
        /// </summary>
        private Dictionary<string, ProjectCategoryVM> _ProjectCategoryDic { get; set; } = new();

        private List<ProjectCategoryVM> _AllProjects { get; } = new();

        #region InitGridAction
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("DSReport","ExportReportExcel","匯出報表","匯出報表", GridActionParameterTypesEnum.NoId,"DSManage").SetOnClickScript("downloadDSReportExcel"),
            };
        }

        #endregion

        #region GetSearchQuery
        public override IOrderedQueryable<ProjectCategoryVM> GetSearchQuery()
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

            if (list == null || list.Count <= 0)
            {
                return null;
            }

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
                            ProjectName = DonationProjectOptions.Category.功德主,
                            ProjectSum = item.Sum.Value,
                        };
                        _ProjectCategoryDic.TryAdd(pro.Key, pro);

                        _AllProjects.Add(pro);
                    }
                }

                foreach (var item in receipt.Info_Memorials)
                {
                    if (item.Sum.HasValue)
                    {
                        sum += item.Sum.Value;

                        var pro = new ProjectCategoryVM()
                        {
                            ProjectName = DonationProjectOptions.Category.附薦位,
                            ProjectSum = item.Sum.Value,
                        };
                        _ProjectCategoryDic.TryAdd(pro.Key, pro);

                        _AllProjects.Add(pro);
                    }
                }

                foreach (var item in receipt.Info_Longevitys)
                {
                    if (item.Sum.HasValue)
                    {
                        sum += item.Sum.Value;

                        var pro = new ProjectCategoryVM()
                        {
                            ProjectName = DonationProjectOptions.Category.延生位,
                            ProjectSum = item.Sum.Value,
                        };
                        _ProjectCategoryDic.TryAdd(pro.Key, pro);
                        _AllProjects.Add(pro);
                    }
                }

                receipt.CalculateSum = sum;
            }

            foreach (var category in _ProjectCategoryDic)
            {
                var categoryList = _AllProjects.Where(x => x.Key == category.Key).ToList();

                category.Value.ProjectCount = categoryList.Count;

                category.Value.ProjectTotalSum = categoryList.Sum(x => x.ProjectSum);
            }

            // 繼續嘗試添加數據庫的功德項目，補齊 projectcount=0 的情況
            var projectsInDb = GetProjectCategoriesFromDatabase();
            foreach (var category in projectsInDb)
            {
                _ProjectCategoryDic.TryAdd(category.Key,category);
            }

            return _ProjectCategoryDic.Values.AsQueryable()
                        .OrderBy(x=>x.ProjectName)
                        .ThenBy(x=>x.ProjectSum);
        }
        #endregion

        #region InitGridHeader
        protected override IEnumerable<IGridColumn<ProjectCategoryVM>> InitGridHeader()
        {
            return new List<GridColumn<ProjectCategoryVM>>{
                this.MakeGridHeader(x => x.ProjectName),
                this.MakeGridHeader(x => x.ProjectSum),
                this.MakeGridHeader(x => x.ProjectCount),
                this.MakeGridHeader(x => x.ProjectTotalSum).SetShowTotal(),
            };
        }
        #endregion

        #region GetProjectCategoriesFromDatabase
        public List<ProjectCategoryVM> GetProjectCategoriesFromDatabase()
        {
            var result = new List<ProjectCategoryVM>();

            var ds = DC.Set<Opt_DharmaService>().AsNoTracking()
                                        .Include(x => x.Opt_DonationProjects)
                                        .CheckContain(Searcher.DharmaServiceName, x => x.ServiceName)
                                        .OrderBy(x => x.ID)
                                        .FirstOrDefault();

            var projects = ds?.Opt_DonationProjects;

            if (projects == null || projects.Count <= 0)
            {
                return result;
            }

            foreach (var item in projects)
            {
                result.Add(new ProjectCategoryVM
                {
                    ProjectName = item.DonationCategory,
                    ProjectSum = item.Sum ?? 0,
                });
            }

            return result;
        }
        #endregion

        #region ExportReportExcel

        
        public byte[] ExportReportExcelAsBytes(out string fileName)
        {
            var vm = new ReceipReportExportVM();

            vm.ServiceName = $"{Searcher.DharmaServiceYear.Value}_{Searcher.DharmaServiceName}";

            fileName = $"{vm.ServiceName}_報表.xlsx";

            var list = GetSearchQuery().ToList();
            int sum = 0;
            foreach (var item in list)
            {
                sum += item.ProjectTotalSum;
            }

            vm.AllTotalSum = sum.ToString();

            string wwwpath = Wtm.GetWebHostEnvironment().WebRootPath;
            string tpl = Path.Combine(wwwpath, "ExcelTemplate", "DSReport.xlsx");

            IExportFileByTemplate exporter = new ExcelExporter();

            return exporter.ExportBytesByTemplate(vm, tpl).Result;
        }

        public string GetReportExcelId()
        {
            var id = Guid.NewGuid().ToString();
            var data = ExportReportExcelAsBytes(out string filename);
            var vm = new ReceiptReportDownloadExcelVM() {
                Key = id,
                Data = data,
                FileName = filename,
            };
            Wtm.Cache.Add(id, vm, new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(30)));
            return id;
        }
        #endregion
    }
}
