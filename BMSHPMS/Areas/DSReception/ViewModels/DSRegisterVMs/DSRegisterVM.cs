using BMSHPMS.Helper;
using BMSHPMS.Models.DharmaService;
using BMSHPMS.Models.DharmaServiceExtention;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NetBox.Extensions;
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

        public List<Opt_DharmaService> DisplayDharmaServiceList()
        {
            // 獲取登錄用戶的角色Ids
            var loginRoleIds = LoginUserInfo.Roles.Select(x => x.ID).ToList().ConvertAll(x => Convert.ToString(x));

            // 根據登錄用戶角色Ids 獲取 Opt_DServiceRole 表裏的法會Ids
            var dsIds = DC.Set<Opt_DServiceRole>().CheckIDs(loginRoleIds, x => x.FrameworkRoleId)
                                .Select(x => x.DSId)
                                .Distinct()
                                .ToList()
                                .ConvertAll(x => Convert.ToString(x));

            if (dsIds == null || dsIds.Count <= 0)
            {
                DharmaServices = null;                
            }
            else
            {
                // 獲取要顯示的法會
                DharmaServices = DC.Set<Opt_DharmaService>()
                                    .Where(x => x.Enable)
                                    .CheckIDs(dsIds, x => x.ID)
                                    .OrderBy(x => x.SerialCode)
                                    .ToList();
            }

            return DharmaServices;
        }
        #endregion

        #region Register page    

        public string DharmaServiceName { get; set; }

        public List<Opt_DonationProject> DonationProject_Donors { get; set; }

        public List<Opt_DonationProject> DonationProject_Longevitys { get; set; }

        public List<Opt_DonationProject> DonationProject_Memorials { get; set; }

        /// <summary>
        /// 填充顯示登記頁面的數據
        /// </summary>
        /// <param name="dsProjectID"></param>
        public void FillDonationProjectList(Guid dsProjectID)
        {

            var dharmasService = DC.Set<Opt_DharmaService>().Find(dsProjectID);
            DharmaServiceName = dharmasService?.ServiceName;

            DonationProject_Donors = new List<Opt_DonationProject>();
            DonationProject_Longevitys = new List<Opt_DonationProject>();
            DonationProject_Memorials = new List<Opt_DonationProject>();

            var donations = DC.Set<Opt_DonationProject>().Where(d => d.DharmaServiceID == dsProjectID).ToList();
            foreach (var donation in donations)
            {
                if (donation.DonationCategory == DonationProjectOptions.Category.功德主)
                {
                    DonationProject_Donors.Add(donation);
                }

                if (donation.DonationCategory == DonationProjectOptions.Category.延生位)
                {
                    DonationProject_Longevitys.Add(donation);
                }

                if (donation.DonationCategory == DonationProjectOptions.Category.附薦位)
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
            Guid postDharmaServiceID = Guid.Parse(form[nameof(DharmaServiceID)]);   // post form 法會ID
            Opt_DharmaService postDharmaService = DC.Set<Opt_DharmaService>().Find(postDharmaServiceID); // 查詢post法會項目

            // 提交的 收據號碼
            string receiptNumber = form["ReceiptNumber"];
            receiptNumber = receiptNumber?.Trim();

            DSRegResultVM regResultVM = new()
            {
                DharmaServiceID = postDharmaServiceID,
                DharmaServiceName = postDharmaService.ServiceName,
                ReceiptNumber = receiptNumber,
            };

            if (string.IsNullOrWhiteSpace(receiptNumber))
            {
                regResultVM.Message = "收據號碼是Null";
                return regResultVM;
            }

            //收據號碼已存在
            Info_Receipt exsitReceipt = DC.Set<Info_Receipt>()
                                            .Where(r => r.ReceiptNumber.ToUpper() == receiptNumber.ToUpper())
                                            .FirstOrDefault();
            if (exsitReceipt != null)
            {
                regResultVM.Donors = await DC.Set<Info_Donor>().Where(q => q.ReceiptID == exsitReceipt.ID).OrderBy(q => q.Sum).ToListAsync();
                regResultVM.Longevitys = await DC.Set<Info_Longevity>().Where(q => q.ReceiptID == exsitReceipt.ID).OrderBy(q => q.Sum).ToListAsync();
                regResultVM.Memorials = await DC.Set<Info_Memorial>().Where(q => q.ReceiptID == exsitReceipt.ID).OrderBy(q => q.Sum).ToListAsync();
                regResultVM.DharmaServiceName = exsitReceipt.DharmaServiceName;
                regResultVM.Message = "收據號碼已存在";

                return regResultVM;
            }

            List<DSRegSubmittedVM> submittedList = new();

            // 解析 post form 數據
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

            // 檢查提交的 Count 是否 <= 0
            foreach (var item in submittedList)
            {
                if (item.Count <= 0)
                {
                    regResultVM.Message = "功德項目不能小於等於0";
                    return regResultVM;
                }
            }

            var newDonorList = new List<Info_Donor>();
            var newLongevityList = new List<Info_Longevity>();
            var newMemorialList = new List<Info_Memorial>();

            // 用來計數功德使用數
            List<Opt_DonationProject> calculateUsedNumberDonationProjectList = new();

            // 記錄回退信息
            List<Reg_RollbackInfo> rollbackInfos = new List<Reg_RollbackInfo>();

            #region // 計算提交的每種功德的使用個數
            foreach (var submittedItem in submittedList)
            {
                // 如果提交的數目小於等於0， 循環下一個
                if (submittedItem.Count <= 0)
                {
                    continue;
                }

                var queryDonationProject = DC.Set<Opt_DonationProject>().Find(submittedItem.DonationProjectID); // 根據功德ID 查詢 功德項目

                // 功德種類的當前已使用數目
                int usedNumber = queryDonationProject.UsedNumber;

                // rollback 回退表中，記錄功德上一次的已使用數
                rollbackInfos.Add(new Reg_RollbackInfo { PreUsedNumber = usedNumber, DonationProjectID = queryDonationProject.ID, LastReceiptNumber = receiptNumber });

                queryDonationProject.UsedNumber += submittedItem.Count; // 當前使用數 + 提交的功德個數 = 已使用數
                calculateUsedNumberDonationProjectList.Add(queryDonationProject);

                // 根據功德項目分類 計算每種個數, 寫入各自的 功德表(Info_Donor,Info_Longevity,Info_Memorial)
                switch (queryDonationProject.DonationCategory)
                {
                    case DonationProjectOptions.Category.功德主:
                        for (int i = 1; i <= submittedItem.Count; i++)
                        {
                            int nextNumber = usedNumber + i;
                            // 總編號
                            string serial = queryDonationProject.SerialCode + nextNumber.ToString().PadLeft(4, '0');
                            Info_Donor info = new()
                            {
                                SerialCode = serial,
                                Sum = queryDonationProject.Sum,
                                CreateBy = LoginUserInfo.Name,
                                CreateTime = DateTime.Now,
                                UpdateBy = LoginUserInfo.Name,
                                UpdateTime = DateTime.Now,
                                DonationProjectId = queryDonationProject.ID,
                                DProjectSerial = queryDonationProject.SerialCode,
                                DProjectSerialNumber = nextNumber,
                            };
                            newDonorList.Add(info);
                        }
                        break;

                    case DonationProjectOptions.Category.延生位:
                        for (int i = 1; i <= submittedItem.Count; i++)
                        {
                            int nextNumber = usedNumber + i;
                            string serial = queryDonationProject.SerialCode + nextNumber.ToString().PadLeft(4, '0');
                            Info_Longevity info = new()
                            {
                                SerialCode = serial,
                                Sum = queryDonationProject.Sum,
                                CreateBy = LoginUserInfo.Name,
                                CreateTime = DateTime.Now,
                                UpdateBy = LoginUserInfo.Name,
                                UpdateTime = DateTime.Now,
                                DonationProjectId = queryDonationProject.ID,
                                DProjectSerial = queryDonationProject.SerialCode,
                                DProjectSerialNumber = nextNumber,
                            };
                            newLongevityList.Add(info);
                        }
                        break;

                    case DonationProjectOptions.Category.附薦位:
                        for (int i = 1; i <= submittedItem.Count; i++)
                        {
                            int nextNumber = usedNumber + i;
                            string serial = queryDonationProject.SerialCode + nextNumber.ToString().PadLeft(4, '0');
                            Info_Memorial info = new()
                            {
                                SerialCode = serial,
                                Sum = queryDonationProject.Sum,
                                CreateBy = LoginUserInfo.Name,
                                CreateTime = DateTime.Now,
                                UpdateBy = LoginUserInfo.Name,
                                UpdateTime = DateTime.Now,
                                DonationProjectId = queryDonationProject.ID,
                                DProjectSerial = queryDonationProject.SerialCode,
                                DProjectSerialNumber = nextNumber,
                            };
                            newMemorialList.Add(info);
                        }
                        break;

                    default:
                        break;
                }
            }
            #endregion

            #region 更新數據庫
            Info_Receipt newReceipt;

            string contactPhone = null;
            string contactName = null;
            try
            {
                contactPhone = form["ContactPhone"];
                contactPhone = contactPhone?.Trim();

                contactName = form["ContactName"];
                contactName = contactName?.Trim();
            }
            catch (Exception) { }

            lock (DbTableLocker.DSDonationTransactionEvent)
            {
                // 使用事務
                using var transaction = DC.BeginTransaction();
                try
                {
                    // 功德項目更新已使用數
                    calculateUsedNumberDonationProjectList.ForEach(donation =>
                    {
                        DC.UpdateProperty(donation, x => x.UsedNumber);
                    });

                    DC.SaveChanges();

                    int newReceiptSum = 0;

                    // 添加新收據
                    Info_Receipt receiptInfo = new()
                    {
                        CreateTime = DateTime.Now,
                        CreateBy = LoginUserInfo.Name,
                        ReceiptNumber = receiptNumber,
                        DharmaServiceName = postDharmaService.ServiceName,
                        DharmaServiceYear = DateTime.Now.Year,
                        ReceiptDate = DateTime.Now.Date,
                        UpdateBy = LoginUserInfo.Name,
                        UpdateTime = DateTime.Now,
                        ContactPhone = contactPhone,
                        ContactName = contactName,
                        DharmaServiceId = postDharmaServiceID,
                    };

                    DC.AddEntity(receiptInfo);
                    DC.SaveChanges();

                    newReceipt = DC.Set<Info_Receipt>()
                                    .Where(r => r.ReceiptNumber.ToLower() == receiptNumber.ToLower())
                                    .FirstOrDefault();

                    if (newReceipt == null)
                    {
                        regResultVM.Message = "新加收據後, 查詢收據是Null";
                        return regResultVM;
                    }

                    // 添加新功德主
                    newDonorList.ForEach(info =>
                    {
                        info.ReceiptID = newReceipt.ID;
                        DC.AddEntity(info);

                        if (info.Sum.HasValue)
                        {
                            newReceiptSum += info.Sum.Value;
                        }
                    });

                    // 添加新延生
                    newLongevityList.ForEach(info =>
                    {
                        info.ReceiptID = newReceipt.ID;
                        DC.AddEntity(info);

                        if (info.Sum.HasValue)
                        {
                            newReceiptSum += info.Sum.Value;
                        }
                    });

                    // 添加新附薦
                    newMemorialList.ForEach(info =>
                    {
                        info.ReceiptID = newReceipt.ID;
                        DC.AddEntity(info);

                        if (info.Sum.HasValue)
                        {
                            newReceiptSum += info.Sum.Value;
                        }
                    });


                    //更新收據金額
                    newReceipt.Sum = newReceiptSum;
                    DC.UpdateProperty(newReceipt, x => x.Sum);

                    // 刪除舊 RollbackInfo
                    DC.Set<Reg_RollbackInfo>().ToList().ForEach(x => DC.DeleteEntity(x));
                    DC.SaveChanges();
                    // 新增 RollbackInfo
                    rollbackInfos.ForEach(x => DC.AddEntity(x));


                    DC.SaveChanges();

                    // 事務寫入數據庫
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    regResultVM.Message = ex.Message;
                    return regResultVM;
                }
            }

            regResultVM.Donors = await DC.Set<Info_Donor>().Where(q => q.ReceiptID == newReceipt.ID).OrderBy(q => q.Sum).ToListAsync();
            regResultVM.Longevitys = await DC.Set<Info_Longevity>().Where(q => q.ReceiptID == newReceipt.ID).OrderBy(q => q.Sum).ToListAsync();
            regResultVM.Memorials = await DC.Set<Info_Memorial>().Where(q => q.ReceiptID == newReceipt.ID).OrderBy(q => q.Sum).ToListAsync();

            regResultVM.Message = "登記成功";
            regResultVM.Succed = true;

            return regResultVM;
            #endregion
        }
        #endregion

    }
}
