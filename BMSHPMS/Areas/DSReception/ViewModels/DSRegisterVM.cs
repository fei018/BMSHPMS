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
        public List<Opt_DharmaService> DharmaServices { get; set; }

        public Guid DharmaServiceID { get; set; }

        public void FillDharmaServiceList()
        {
            DharmaServices = DC.Set<Opt_DharmaService>().OrderBy(x => x.SerialCode).ToList();
        }
        #endregion

        #region Register page       
        public List<Opt_DonationProject> DonationProject_Donors { get; set; }

        public List<Opt_DonationProject> DonationProject_Longevitys { get; set; }

        public List<Opt_DonationProject> DonationProject_Memorials { get; set; }

        public void FillDonationProjectList(Guid dsProjectID)
        {
            DonationProject_Donors = new List<Opt_DonationProject>();
            DonationProject_Longevitys = new List<Opt_DonationProject>();
            DonationProject_Memorials = new List<Opt_DonationProject>();

            var donations = DC.Set<Opt_DonationProject>().Where(d => d.DharmaServiceID == dsProjectID).ToList();
            foreach (var donation in donations)
            {
                if (donation.DonationCategory == DharmaServiceSelectHelper.DonationCategory.功德主)
                {
                    DonationProject_Donors.Add(donation);                    
                }

                if (donation.DonationCategory == DharmaServiceSelectHelper.DonationCategory.延生位)
                {
                    DonationProject_Longevitys.Add(donation);                    
                }

                if (donation.DonationCategory == DharmaServiceSelectHelper.DonationCategory.附薦位)
                {
                    DonationProject_Memorials.Add(donation);                   
                }
            }

            DonationProject_Donors?.Sort((x, y) => x.Sum.Value.CompareTo(y.Sum));
            DonationProject_Longevitys?.Sort((x, y) => x.Sum.Value.CompareTo(y.Sum));
            DonationProject_Memorials?.Sort((x, y) => x.Sum.Value.CompareTo(y.Sum));
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
            Info_Receipt exsitReceipt = DC.Set<Info_Receipt>().Where(r => r.ReceiptNumber.Equals(receiptNumber)).FirstOrDefault();
            if (exsitReceipt != null)
            {
                regResultVM.DonorInfos = await   DC.Set<Info_Donor>().Where(q => q.ReceiptID == exsitReceipt.ID).OrderBy(q => q.Sum).ToListAsync();
                regResultVM.LongevityInfos = await DC.Set<Info_Longevity>().Where(q => q.ReceiptID == exsitReceipt.ID).OrderBy(q => q.Sum).ToListAsync();
                regResultVM.MemorialInfos = await DC.Set<Info_Memorial>().Where(q => q.ReceiptID == exsitReceipt.ID).OrderBy(q => q.Sum).ToListAsync();
                regResultVM.ReceiptNumber = receiptNumber;
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

            var newDonorList = new List<Info_Donor>();
            var newLongevityList = new List<Info_Longevity>();
            var newMemorialList = new List<Info_Memorial>();

            Guid dserviceID = Guid.Parse(form[nameof(DharmaServiceID)]);
            Opt_DharmaService dharmaService = DC.Set<Opt_DharmaService>().Find(dserviceID); // 查詢法會項目

            List<Opt_DonationProject> calculateUsedNumberDonationProjectList = new();

            // 計算個數
            foreach (var submittedItem in submittedList)
            {
                var queryDonationProject = DC.Set<Opt_DonationProject>().Find(submittedItem.DonationProjectID); // 根據功德ID 查詢 功德項目

                int usedNumber = queryDonationProject.UsedNumber; // 已使用數目

                queryDonationProject.UsedNumber += submittedItem.Count;
                calculateUsedNumberDonationProjectList.Add(queryDonationProject);

                // 根據功德項目分類 計算每種個數, 寫入各自的 功德表(DSDonorInfo,DSLongevityInfo,DSMemorialInfo)
                switch (queryDonationProject.DonationCategory)
                {
                    case DharmaServiceSelectHelper.DonationCategory.功德主:
                        for (int i = 1; i <= submittedItem.Count; i++)
                        {
                            string serial = dharmaService.SerialCode + queryDonationProject.SerialCode + (usedNumber + i).ToString().PadLeft(3,'0');
                            Info_Donor info = new()
                            {
                                SerialCode = serial,
                                Sum = queryDonationProject.Sum,
                                CreateBy = LoginUserInfo.Name,
                                CreateTime = DateTime.Now,
                                UpdateBy = LoginUserInfo.Name,
                                UpdateTime = DateTime.Now,
                                IsDataValid = true,
                            };
                            newDonorList.Add(info);
                        }
                        break;

                    case DharmaServiceSelectHelper.DonationCategory.延生位:
                        for (int i = 1; i <= submittedItem.Count; i++)
                        {
                            string serial = dharmaService.SerialCode + queryDonationProject.SerialCode + (usedNumber + i).ToString().PadLeft(3, '0');
                            Info_Longevity info = new()
                            {
                                SerialCode = serial,
                                Sum = queryDonationProject.Sum,
                                CreateBy = LoginUserInfo.Name,
                                CreateTime = DateTime.Now,
                                UpdateBy = LoginUserInfo.Name,
                                UpdateTime = DateTime.Now,
                                IsDataValid = true,
                            };
                            newLongevityList.Add(info);
                        }
                        break;

                    case DharmaServiceSelectHelper.DonationCategory.附薦位:
                        for (int i = 1; i <= submittedItem.Count; i++)
                        {
                            string serial = dharmaService.SerialCode + queryDonationProject.SerialCode + (usedNumber + i).ToString().PadLeft(3, '0');
                            Info_Memorial info = new()
                            {
                                SerialCode = serial,
                                Sum = queryDonationProject.Sum,
                                CreateBy = LoginUserInfo.Name,
                                CreateTime = DateTime.Now,
                                UpdateBy = LoginUserInfo.Name,
                                UpdateTime = DateTime.Now,
                                IsDataValid = true,
                            };
                            newMemorialList.Add(info);
                        }
                        break;

                    default:
                        break;
                }
            }

            // 更新數據庫
            Info_Receipt newReceipt;

            using var transaction = DC.BeginTransaction();
            try
            {
                // 添加新收據
                lock (DbTableLocker.T_Receipt)
                {
                    Info_Receipt receiptInfo = new()
                    {
                        CreateTime = DateTime.Now,
                        CreateBy = LoginUserInfo.Name,
                        ReceiptNumber = receiptNumber,
                        DharmaServiceName = dharmaService.ServiceName,
                        DharmaServiceYear = DateTime.Now.Year,
                        ReceiptDate = DateTime.Now.Date,
                        UpdateBy = LoginUserInfo.Name,
                        UpdateTime = DateTime.Now,
                        IsDataValid = true,
                    };

                    DC.AddEntity(receiptInfo);
                    DC.SaveChanges();

                    newReceipt = DC.Set<Info_Receipt>().Where(r => r.ReceiptNumber == receiptNumber).FirstOrDefault();
                    if (newReceipt == null)
                    {
                        regResultVM.Message = "新加收據後, 查詢收據是Null.";
                        return regResultVM;
                    }

                    // 添加新功德主
                    newDonorList.ForEach(info =>
                    {
                        info.ReceiptID = newReceipt.ID;
                        DC.AddEntity(info);
                    });

                    // 添加新延生
                    newLongevityList.ForEach(info =>
                    {
                        info.ReceiptID = newReceipt.ID;
                        DC.AddEntity(info);
                    });

                    // 添加新附薦
                    newMemorialList.ForEach(info =>
                    {
                        info.ReceiptID = newReceipt.ID;
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

            regResultVM.DonorInfos = await DC.Set<Info_Donor>().Where(q => q.ReceiptID == newReceipt.ID).OrderBy(q => q.Sum).ToListAsync();
            regResultVM.LongevityInfos = await DC.Set<Info_Longevity>().Where(q => q.ReceiptID == newReceipt.ID).OrderBy(q => q.Sum).ToListAsync();
            regResultVM.MemorialInfos = await DC.Set<Info_Memorial>().Where(q => q.ReceiptID == newReceipt.ID).OrderBy(q => q.Sum).ToListAsync();
            regResultVM.ReceiptNumber = receiptNumber;
            regResultVM.Message = "登記成功.";

            return regResultVM;
        }
        #endregion

    }
}
