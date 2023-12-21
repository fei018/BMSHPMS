using BMSHPMS.Areas.DSReception.ViewModels;
using BMSHPMS.Helper;
using BMSHPMS.Models.DharmaService;
using DotLiquid;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSReception.ViewModels
{
    public class DSRegisterVM : BaseVM
    {
        #region Index page
        public List<DServiceProject> DSProjects { get; set; }

        public Guid DSProjectID { get; set; }

        public void FillDSProjectList()
        {
            DSProjects = DC.Set<DServiceProject>().OrderBy(x => x.SerialCode).ToList();
        }
        #endregion

        #region Register page       
        public List<DSDonationProject> DSDonationProject_Donors { get; set; }

        public List<DSDonationProject> DSDonationProject_Longevitys { get; set; }

        public List<DSDonationProject> DSDonationProject_Memorials { get; set; }

        public void FillDSDonationProjectList(Guid dsProjectID)
        {
            DSDonationProject_Donors = new List<DSDonationProject>();
            DSDonationProject_Longevitys = new List<DSDonationProject>();
            DSDonationProject_Memorials = new List<DSDonationProject>();

            var donations = DC.Set<DSDonationProject>().Where(d => d.DServiceProjID == dsProjectID).ToList();
            foreach (var donation in donations)
            {
                if (donation.DonationCategory == DSProjectSelectHelper.DonationCategory.功德主)
                {
                    DSDonationProject_Donors.Add(donation);                    
                }

                if (donation.DonationCategory == DSProjectSelectHelper.DonationCategory.延生位)
                {
                    DSDonationProject_Longevitys.Add(donation);                    
                }

                if (donation.DonationCategory == DSProjectSelectHelper.DonationCategory.附薦位)
                {
                    DSDonationProject_Memorials.Add(donation);                   
                }
            }

            DSDonationProject_Donors?.Sort((x, y) => x.Sum.Value.CompareTo(y.Sum));
            DSDonationProject_Longevitys?.Sort((x, y) => x.Sum.Value.CompareTo(y.Sum));
            DSDonationProject_Memorials?.Sort((x, y) => x.Sum.Value.CompareTo(y.Sum));
        }
        #endregion

        #region Submitted(IFormCollection form)
        public async Task<DSRegResultVM> Submitted(IFormCollection form)
        {
            DSRegResultVM regResultVM = new();

            string receiptNumber = form["ReceiptNumber"];
            if (string.IsNullOrWhiteSpace(receiptNumber))
            {
                regResultVM.Message = "收據號碼是Null.";
                return regResultVM;
            }

            //收據號碼已存在
            DSReceiptInfo exsitReceipt = DC.Set<DSReceiptInfo>().Where(r => r.ReceiptNumber.Equals(receiptNumber)).FirstOrDefault();
            if (exsitReceipt != null)
            {
                regResultVM.DonorInfos = await   DC.Set<DSDonorInfo>().Where(q => q.ReceiptInfoID == exsitReceipt.ID).OrderBy(q => q.Sum).ToListAsync();
                regResultVM.LongevityInfos = await DC.Set<DSLongevityInfo>().Where(q => q.ReceiptInfoID == exsitReceipt.ID).OrderBy(q => q.Sum).ToListAsync();
                regResultVM.MemorialInfos = await DC.Set<DSMemorialInfo>().Where(q => q.ReceiptInfoID == exsitReceipt.ID).OrderBy(q => q.Sum).ToListAsync();

                regResultVM.Message = "收據號碼已存在";

                return regResultVM;
            }

            List<DSRegSubmittedVM> submittedList = new();

            foreach (var f in form)
            {
                if (Guid.TryParse(f.Key, out Guid id))
                {
                    if (int.TryParse(f.Value, out int count))
                    {
                        DSRegSubmittedVM vm = new()
                        {
                            DonationProjectID = id,
                            Count = count
                        };

                        submittedList.Add(vm);
                    }
                }
            }

            if (submittedList.Count <= 0)
            {
                regResultVM.Message = "功德項目不能全部為空";
                return regResultVM;
            }

            var newDonorList = new List<DSDonorInfo>();
            var newLongevityList = new List<DSLongevityInfo>();
            var newMemorialList = new List<DSMemorialInfo>();

            Guid dserviceID = Guid.Parse(form["DSProjectID"]);
            DServiceProject dsProject = DC.Set<DServiceProject>().Find(dserviceID); // 查詢法會項目

            List<DSDonationProject> calculateUsedNumberDonationProjectList = new();

            // 計算個數
            foreach (var submittedItem in submittedList)
            {
                var queryDonationProject = DC.Set<DSDonationProject>().Find(submittedItem.DonationProjectID); // 根據功德ID 查詢 功德項目

                int usedNumber = queryDonationProject.UsedNumber; // 已使用數目

                queryDonationProject.UsedNumber += submittedItem.Count;
                calculateUsedNumberDonationProjectList.Add(queryDonationProject);

                // 根據功德項目分類 計算每種個數, 寫入各自的 功德表(DSDonorInfo,DSLongevityInfo,DSMemorialInfo)
                switch (queryDonationProject.DonationCategory)
                {
                    case DSProjectSelectHelper.DonationCategory.功德主:
                        for (int i = 1; i <= submittedItem.Count; i++)
                        {
                            string serial = dsProject.SerialCode + queryDonationProject.SerialCode + (usedNumber + i).ToString().PadLeft(3,'0');
                            DSDonorInfo info = new()
                            {
                                SerialCode = serial,
                                Sum = queryDonationProject.Sum,
                                CreateBy = LoginUserInfo.Name,
                                CreateTime = DateTime.Now
                            };
                            newDonorList.Add(info);
                        }
                        break;

                    case DSProjectSelectHelper.DonationCategory.延生位:
                        for (int i = 1; i <= submittedItem.Count; i++)
                        {
                            string serial = dsProject.SerialCode + queryDonationProject.SerialCode + (usedNumber + i).ToString().PadLeft(3, '0');
                            DSLongevityInfo info = new()
                            {
                                SerialCode = serial,
                                Sum = queryDonationProject.Sum,
                                CreateBy = LoginUserInfo.Name,
                                CreateTime = DateTime.Now
                            };
                            newLongevityList.Add(info);
                        }
                        break;

                    case DSProjectSelectHelper.DonationCategory.附薦位:
                        for (int i = 1; i <= submittedItem.Count; i++)
                        {
                            string serial = dsProject.SerialCode + queryDonationProject.SerialCode + (usedNumber + i).ToString().PadLeft(3, '0');
                            DSMemorialInfo info = new()
                            {
                                SerialCode = serial,
                                Sum = queryDonationProject.Sum,
                                CreateBy = LoginUserInfo.Name,
                                CreateTime = DateTime.Now
                            };
                            newMemorialList.Add(info);
                        }
                        break;

                    default:
                        break;
                }
            }

            // 更新數據庫
            DSReceiptInfo newReceipt;

            using var transaction = DC.BeginTransaction();
            try
            {
                // 添加新收據
                lock (DbTableLocker.T_Receipt)
                {
                    DSReceiptInfo receiptInfo = new()
                    {
                        CreateTime = DateTime.Now,
                        CreateBy = LoginUserInfo.Name,
                        ReceiptNumber = receiptNumber,
                        DSProjectName = dsProject.ProjectName,
                        ReceiptDate = DateTime.Now.Date,
                    };

                    DC.AddEntity(receiptInfo);
                    DC.SaveChanges();

                    newReceipt = DC.Set<DSReceiptInfo>().Where(r => r.ReceiptNumber == receiptNumber).FirstOrDefault();
                    if (newReceipt == null)
                    {
                        regResultVM.Message = "新加收據後, 查詢收據是Null.";
                        return regResultVM;
                    }

                    // 添加新功德主
                    newDonorList.ForEach(info =>
                    {
                        info.ReceiptInfoID = newReceipt.ID;
                        DC.AddEntity(info);
                    });

                    // 添加新延生
                    newLongevityList.ForEach(info =>
                    {
                        info.ReceiptInfoID = newReceipt.ID;
                        DC.AddEntity(info);
                    });

                    // 添加新附薦
                    newMemorialList.ForEach(info =>
                    {
                        info.ReceiptInfoID = newReceipt.ID;
                        DC.AddEntity(info);
                    });

                    // 功德項目更新已使用數
                    calculateUsedNumberDonationProjectList.ForEach(donation =>
                    {
                        DC.UpdateEntity(donation);
                    });

                    DC.SaveChanges();

                    transaction.Commit();                   
                }               
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                regResultVM.Message = ex.Message;               
                return regResultVM;
            }

            regResultVM.DonorInfos = await DC.Set<DSDonorInfo>().Where(q => q.ReceiptInfoID == newReceipt.ID).OrderBy(q => q.Sum).ToListAsync();
            regResultVM.LongevityInfos = await DC.Set<DSLongevityInfo>().Where(q => q.ReceiptInfoID == newReceipt.ID).OrderBy(q => q.Sum).ToListAsync();
            regResultVM.MemorialInfos = await DC.Set<DSMemorialInfo>().Where(q => q.ReceiptInfoID == newReceipt.ID).OrderBy(q => q.Sum).ToListAsync();

            regResultVM.Message = "登記成功.";

            return regResultVM;
        }
        #endregion

    }
}
