using BMSHPMS.Helper;
using BMSHPMS.Models.DharmaService;
using BMSHPMS.Models.DharmaServiceExtention;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSReception.ViewModels.DSReceiptAppendVMs
{
    public class DSReceiptAppendVM : BaseVM
    {
        public AppendInfoVM AppendInfo { get; set; }

        public string Message { get; set; }

        public List<AppendResultVM> AppendResultList { get; set; }

        public List<Opt_DharmaService> GetDharmaServiceList()
        {
            return DC.Set<Opt_DharmaService>().OrderBy(x => x.SerialCode).ToList();
        }

        public void InitAppendInfo(string serviceID)
        {
            var ds = DC.Set<Opt_DharmaService>().CheckID(serviceID).Single();

            AppendInfo = new AppendInfoVM
            {
                DharmaServiceID = serviceID,
                DonationProjectCategoryList = DonationProjectOptions.GetCategoryComboSelectItems(),
                DharmaServiceName = ds.ServiceName,
            };

            foreach (var item in AppendInfo.DonationProjectCategoryList)
            {
                item.Value = $"{item.Value}|{serviceID}";
            }
        }

        /// <summary>
        /// 獲取 功德金額 選項
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ComboSelectListItem> GetDonationSum(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new List<ComboSelectListItem>();
            }

            var ids = id.Split('|');
            string donationCategory = ids[0];
            string serviceID = ids[1];

            var list = DC.Set<Opt_DonationProject>().AsNoTracking()
                                                    .CheckID(serviceID, x => x.DharmaServiceID)
                                                    .CheckEqual(donationCategory, x => x.DonationCategory)
                                                    .OrderBy(x => x.Sum)
                                                    .Select(x => new ComboSelectListItem { Text = x.Sum.ToString(), Value = x.ID.ToString() })
                                                    .ToList();
            return list;
        }

        public void DoAppend(AppendInfoVM info)
        {
            if (!info.DonationProjectCount.HasValue || info.DonationProjectCount.Value <= 0)
            {
                MSD.AddModelError("DonationProjectCount", "功德數目不能小於等於0");
                return;
            }

            // 檢查 收據號碼
            var receipt = DC.Set<Info_Receipt>().CheckEqual(info.ReceiptNumber, x => x.ReceiptNumber).SingleOrDefault();
            if (receipt == null)
            {
                MSD.AddModelError("ReceiptNumber", "收據號碼:" + info.ReceiptNumber + " 不存在數據庫.");
                return;
            }

            if (info.DharmaServiceName.ToLower() != receipt.DharmaServiceName.ToLower())
            {
                MSD.AddModelError("DharmaServiceName", "所選法會與收據登記法會不相符");
                return;
            }

            var donation = DC.Set<Opt_DonationProject>()
                                 .CheckID(info.DonationProjectID, x => x.ID)
                                 .SingleOrDefault();

            if (donation == null)
            {
                MSD.AddModelError("DonationProjectID", "功德項目ID is null: " + info.DonationProjectID);
                return;
            }

            var serialList = new List<string>();

            lock (DbTableLocker.DSDonationTransactionEvent)
            {
                using var dctrans = DC.BeginTransaction();
                try
                {
                    for (int i = 0; i < info.DonationProjectCount.Value; i++)
                    {
                        int nextNumber = donation.UsedNumber + 1;
                        donation.UsedNumber = nextNumber;

                        DC.UpdateProperty(donation, x => x.UsedNumber);
                        DC.SaveChanges();

                        string serial = donation.SerialCode + nextNumber.ToString().PadLeft(4, '0');
                        serialList.Add(serial);

                        switch (donation.DonationCategory)
                        {
                            case DonationProjectOptions.Category.功德主:
                                Info_Donor donor = new()
                                {
                                    ReceiptID = receipt.ID,
                                    SerialCode = serial,
                                    Sum = donation.Sum,
                                    CreateBy = LoginUserInfo.Name,
                                    CreateTime = DateTime.Now,
                                    UpdateBy = LoginUserInfo.Name,
                                    UpdateTime = DateTime.Now,
                                    DonationProjectId = donation.ID,
                                    DProjectSerial = donation.SerialCode,
                                    DProjectSerialNumber = nextNumber,
                                };
                                DC.AddEntity(donor);
                                DC.SaveChanges();
                                break;

                            case DonationProjectOptions.Category.延生位:
                                Info_Longevity longevity = new()
                                {
                                    ReceiptID = receipt.ID,
                                    SerialCode = serial,
                                    Sum = donation.Sum,
                                    CreateBy = LoginUserInfo.Name,
                                    CreateTime = DateTime.Now,
                                    UpdateBy = LoginUserInfo.Name,
                                    UpdateTime = DateTime.Now,
                                    DonationProjectId = donation.ID,
                                    DProjectSerial = donation.SerialCode,
                                    DProjectSerialNumber = nextNumber,
                                };
                                DC.AddEntity(longevity);
                                DC.SaveChanges();
                                break;

                            case DonationProjectOptions.Category.附薦位:
                                Info_Memorial memorial = new()
                                {
                                    ReceiptID = receipt.ID,
                                    SerialCode = serial,
                                    Sum = donation.Sum,
                                    CreateBy = LoginUserInfo.Name,
                                    CreateTime = DateTime.Now,
                                    UpdateBy = LoginUserInfo.Name,
                                    UpdateTime = DateTime.Now,
                                    DonationProjectId = donation.ID,
                                    DProjectSerial = donation.SerialCode,
                                    DProjectSerialNumber = nextNumber,
                                };
                                DC.AddEntity(memorial);
                                DC.SaveChanges();
                                break;

                            default:
                                break;
                        }
                    }

                    if (receipt.DharmaServiceId == null && !string.IsNullOrEmpty(info.DharmaServiceID))
                    {
                        try
                        {
                            receipt.DharmaServiceId = Guid.Parse(info.DharmaServiceID);
                            DC.UpdateProperty(receipt, x => x.DharmaServiceId);
                        }
                        catch (Exception)
                        {
                        }
                    }

                    dctrans.Commit();

                    AppendResultList = new List<AppendResultVM>();
                    foreach (var serial in serialList)
                    {
                        var vm = new AppendResultVM()
                        {
                            SerialCode = serial,
                        };
                        AppendResultList.Add(vm);
                    }

                    Message = donation.DonationCategory + ": $" + donation.Sum;
                }
                catch (Exception)
                {
                    dctrans.Rollback();
                    throw;
                }
            }
        }


    }
}
