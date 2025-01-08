using BMSHPMS.Helper;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSReception.ViewModels.DSOldRegisterVMs
{
    public class DSOldRegisterVM : BaseVM
    {
        #region MyRegion
        public Opt_DharmaService DharmaService { get; set; }

        public OldRegisterSubmitInfo SubmitInfo { get; set; }

        public OldRegDonorListVM DonorListVM { get; set; }

        public OldRegMemorialListVM MemorialListVM { get; set; }

        public OldRegLongevityListVM LongevityListVM { get; set; }
        #endregion

        #region MyRegion
        public Info_Receipt NewReceipt { get; set; }

        public string Message { get; set; }
        #endregion

        protected override void InitVM()
        {
            InitSelectListVM(null);
        }

        public void InitDS(Guid dsID)
        {
            DharmaService = DC.Set<Opt_DharmaService>().AsNoTracking().CheckEqual(dsID, x => x.ID).FirstOrDefault();

            if (DharmaService == null)
            {
                MSD.AddModelError("法會ID為Null", "法會ID為Null.");
                return;
            }
        }

        private void InitSelectListVM(string receiptNumber)
        {
            DonorListVM = new OldRegDonorListVM();
            DonorListVM.Searcher.ReceiptNumber = receiptNumber;
            DonorListVM.CopyContext(this);

            MemorialListVM = new OldRegMemorialListVM();
            MemorialListVM.Searcher.ReceiptNumber = receiptNumber;
            MemorialListVM.CopyContext(this);

            LongevityListVM = new OldRegLongevityListVM();
            LongevityListVM.Searcher.ReceiptNumber = receiptNumber;
            LongevityListVM.CopyContext(this);
        }

        #region IsSameDharmaService
        private bool IsSameDharmaService(Opt_DonationProject donation)
        {
            if (donation.DharmaServiceID.Value == DharmaService.ID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Submitted
        public bool Submitted()
        {
            DharmaService = DC.Set<Opt_DharmaService>().CheckEqual(DharmaService.ID, x => x.ID).FirstOrDefault();

            if (DharmaService == null)
            {
                MSD.AddModelError("法會ID為Null", "法會ID為Null.");
                return false;
            }

            // 检查收据是否存在
            var tmpReceipt = DC.Set<Info_Receipt>().CheckID(SubmitInfo.ReceiptNumber, x => x.ReceiptNumber).FirstOrDefault();
            if (tmpReceipt != null)
            {
                MSD.AddModelError("收據已存在", "收據已存在.");
                return false;
            }

            var newReceipt = new Info_Receipt
            {
                ID = Guid.NewGuid(),
                ReceiptDate = DateTime.Now.Date,
                ReceiptNumber = SubmitInfo.ReceiptNumber,
                ContactName = SubmitInfo.ContactName,
                ContactPhone = SubmitInfo.ContactPhone,
                DharmaServiceId = DharmaService.ID,
                DharmaServiceName = DharmaService.ServiceName,
                DharmaServiceYear = DateTime.Now.Year,
                Sum = 0,
                CreateBy = LoginUserInfo.Name,
                CreateTime = DateTime.Now,
            };

            lock (DbTableLocker.DSDonationTransactionEvent)
            {
                using var transaction = DC.BeginTransaction();

                try
                {
                    DC.AddEntity(newReceipt);
                    DC.SaveChanges();

                    AddNewDonors(ref newReceipt);
                    AddNewLongevitys(ref newReceipt);
                    AddNewMemorials(ref newReceipt);

                    DC.UpdateProperty(newReceipt, x => x.Sum);
                    DC.SaveChanges();

                    // 刪除舊 RollbackInfo
                    DC.Set<Reg_RollbackInfo>().ToList().ForEach(x => DC.DeleteEntity(x));
                    DC.SaveChanges();
                    // 新增 RollbackInfo
                    foreach (var item in _rollbackInfos)
                    {
                        DC.AddEntity(item.Value);
                    }

                    DC.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MSD.AddModelError("Exception", ex.Message);
                    return false;
                }
            }

            NewReceipt = newReceipt;
            InitSelectListVM(newReceipt.ReceiptNumber);
            Message = "登記成功.";
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<Guid, Reg_RollbackInfo> _rollbackInfos = new Dictionary<Guid, Reg_RollbackInfo>();

        #region AddNewDonors
        private void AddNewDonors(ref Info_Receipt receipt)
        {
            if (SubmitInfo.DonorIDListSelect.Count > 0)
            {
                //檢查新舊法會是否相同
                var tmpInfo = DC.Set<Info_Donor>().CheckID(SubmitInfo.DonorIDListSelect[0], x => x.ID).Single();
                var tmpDonation = DC.Set<Opt_DonationProject>().CheckID(tmpInfo.DonationProjectId, x => x.ID).Single();
                if (!IsSameDharmaService(tmpDonation))
                {
                    throw new Exception("功德主新舊法會不相同");
                }

                foreach (var id in SubmitInfo.DonorIDListSelect)
                {
                    var old = DC.Set<Info_Donor>().CheckID(id, x => x.ID).Single();

                    var donation = DC.Set<Opt_DonationProject>().CheckID(old.DonationProjectId, x => x.ID).Single();

                    // 如果功德id存在，則相同的功德不能加入rollbacks，保持 PreUsedNumber 為最開始那一個
                    var rollback = new Reg_RollbackInfo
                    {
                        DonationProjectID = donation.ID,
                        LastReceiptNumber = receipt.ReceiptNumber,
                        PreUsedNumber = donation.UsedNumber,
                    };
                    _rollbackInfos.TryAdd(donation.ID, rollback);

                    ++donation.UsedNumber;

                    DC.UpdateProperty(donation, x => x.UsedNumber);

                    var serial = donation.SerialCode + donation.UsedNumber.ToString().PadLeft(4, '0');

                    var entity = new Info_Donor
                    {
                        ReceiptID = receipt.ID,
                        ID = Guid.NewGuid(),
                        LongevityName = old.LongevityName,
                        DeceasedName_1 = old.DeceasedName_1,
                        DeceasedName_2 = old.DeceasedName_2,
                        DeceasedName_3 = old.DeceasedName_3,
                        BenefactorName = old.BenefactorName,
                        DonationProjectId = old.DonationProjectId,
                        DProjectSerial = donation.SerialCode,
                        Sum = donation.Sum,
                        DProjectSerialNumber = donation.UsedNumber,
                        SerialCode = serial,
                        CreateBy = LoginUserInfo.Name,
                        CreateTime = DateTime.Now,
                        UpdateBy = LoginUserInfo.Name,
                        UpdateTime = DateTime.Now,
                    };

                    DC.AddEntity(entity);

                    //累加收据金额
                    receipt.Sum += entity.Sum;
                }

                DC.SaveChanges();
            }
        }
        #endregion

        #region AddNewMemorials
        private void AddNewMemorials(ref Info_Receipt receipt)
        {
            if (SubmitInfo.MemorialIDListSelect.Count > 0)
            {
                //檢查新舊法會是否相同
                var tmpInfo = DC.Set<Info_Memorial>().CheckID(SubmitInfo.MemorialIDListSelect[0], x => x.ID).Single();
                var tmpDonation = DC.Set<Opt_DonationProject>().CheckID(tmpInfo.DonationProjectId, x => x.ID).Single();
                if (!IsSameDharmaService(tmpDonation))
                {
                    throw new Exception("附薦新舊法會不相同");
                }

                foreach (var id in SubmitInfo.MemorialIDListSelect)
                {
                    var old = DC.Set<Info_Memorial>().CheckID(id, x => x.ID).Single();

                    var donation = DC.Set<Opt_DonationProject>().CheckID(old.DonationProjectId, x => x.ID).Single();

                    // 如果功德id存在，則相同的功德不能加入rollbacks，保持 PreUsedNumber 為最開始那一個
                    var rollback = new Reg_RollbackInfo
                    {
                        DonationProjectID = donation.ID,
                        LastReceiptNumber = receipt.ReceiptNumber,
                        PreUsedNumber = donation.UsedNumber,
                    };
                    _rollbackInfos.TryAdd(donation.ID, rollback);

                    ++donation.UsedNumber;

                    DC.UpdateProperty(donation, x => x.UsedNumber);

                    var serial = donation.SerialCode + donation.UsedNumber.ToString().PadLeft(4, '0');

                    var entity = new Info_Memorial
                    {
                        ReceiptID = receipt.ID,
                        ID = Guid.NewGuid(),
                        DeceasedName_1 = old.DeceasedName_1,
                        DeceasedName_2 = old.DeceasedName_2,
                        DeceasedName_3 = old.DeceasedName_3,
                        BenefactorName = old.BenefactorName,
                        DonationProjectId = old.DonationProjectId,
                        DProjectSerial = donation.SerialCode,
                        Sum = donation.Sum,
                        DProjectSerialNumber = donation.UsedNumber,
                        SerialCode = serial,
                        CreateBy = LoginUserInfo.Name,
                        CreateTime = DateTime.Now,
                        UpdateBy = LoginUserInfo.Name,
                        UpdateTime = DateTime.Now,
                    };

                    DC.AddEntity(entity);

                    //累加收据金额
                    receipt.Sum += entity.Sum;
                }

                DC.SaveChanges();
            }
        }
        #endregion

        #region AddNewLongevitys
        private void AddNewLongevitys(ref Info_Receipt receipt)
        {
            if (SubmitInfo.LongevityIDListSelect.Count > 0)
            {
                //檢查新舊法會是否相同
                var tmpInfo = DC.Set<Info_Longevity>().CheckID(SubmitInfo.LongevityIDListSelect[0], x => x.ID).Single();
                var tmpDonation = DC.Set<Opt_DonationProject>().CheckID(tmpInfo.DonationProjectId, x => x.ID).Single();
                if (!IsSameDharmaService(tmpDonation))
                {
                    throw new Exception("延生新舊法會不相同");
                }

                foreach (var id in SubmitInfo.LongevityIDListSelect)
                {
                    var old = DC.Set<Info_Longevity>().CheckID(id, x => x.ID).Single();

                    var donation = DC.Set<Opt_DonationProject>().CheckID(old.DonationProjectId, x => x.ID).Single();

                    // 如果功德id存在，則相同的功德不能加入rollbacks，保持 PreUsedNumber 為最開始那一個
                    var rollback = new Reg_RollbackInfo
                    {
                        DonationProjectID = donation.ID,
                        LastReceiptNumber = receipt.ReceiptNumber,
                        PreUsedNumber = donation.UsedNumber,
                    };
                    _rollbackInfos.TryAdd(donation.ID, rollback);

                    ++donation.UsedNumber;

                    DC.UpdateProperty(donation, x => x.UsedNumber);

                    var serial = donation.SerialCode + donation.UsedNumber.ToString().PadLeft(4, '0');

                    var entity = new Info_Longevity
                    {
                        ReceiptID = receipt.ID,
                        ID = Guid.NewGuid(),
                        Name = old.Name,
                        DonationProjectId = old.DonationProjectId,
                        DProjectSerial = donation.SerialCode,
                        Sum = donation.Sum,
                        DProjectSerialNumber = donation.UsedNumber,
                        SerialCode = serial,
                        CreateBy = LoginUserInfo.Name,
                        CreateTime = DateTime.Now,
                        UpdateBy = LoginUserInfo.Name,
                        UpdateTime = DateTime.Now,
                    };

                    DC.AddEntity(entity);

                    //累加收据金额
                    receipt.Sum += entity.Sum;
                }

                DC.SaveChanges();
            }
        }
        #endregion

        #endregion
    }
}
