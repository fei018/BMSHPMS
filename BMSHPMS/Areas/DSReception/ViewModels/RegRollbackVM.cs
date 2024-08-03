using BMSHPMS.Helper;
using BMSHPMS.Models.DharmaService;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSReception.ViewModels
{
    public class RegRollbackVM : BaseVM
    {
        public string Error { get; set; }

        public string Msg { get; set; }

        public bool Succed { get; set; } = false;

        /// <summary>
        /// 提交上來的收據號碼
        /// </summary>
        [Required(ErrorMessage = "收據號碼必填")]
        [Display(Name = "收據號碼")]
        public string ReceiptNumber { get; set; }

        /// <summary>
        /// 最近一次登記收據號
        /// </summary>
        public string LastReceiptNumber { get; set; }

        public void Rollback()
        {
            lock (DbTableLocker.DSDonationTransactionEvent)
            {
                using var transaction = DC.BeginTransaction();
                try
                {
                    var rollbackList = DC.Set<Reg_RollbackInfo>().ToList();
                    if (rollbackList == null || rollbackList.Count <= 0)
                    {
                        Error = "Reg_RollbackInfo 查詢數據庫為空.";
                        return;
                    }

                    LastReceiptNumber = rollbackList[0].LastReceiptNumber;
                    if (LastReceiptNumber != ReceiptNumber) // 對比最近記錄的一次收據號碼 和 提交上來收據號碼
                    {
                        Error = "輸入的收據號碼不是最近一次登記";
                        return;
                    }

                    // 刪除最近一次的收據記錄
                    var del = DC.Set<Info_Receipt>().Where(x => x.ReceiptNumber == ReceiptNumber).FirstOrDefault();
                    if (del == null)
                    {
                        Error = "輸入的收據號碼不存在, 可能已撤銷.";
                        return;
                    }
                    DC.DeleteEntity(del);
                    DC.SaveChanges();

                    // 更新 功德使用數 退回 上一次記錄
                    foreach (var item in rollbackList)
                    {
                        var donation = DC.Set<Opt_DonationProject>().Find(item.DonationProjectID);
                        donation.UsedNumber = item.PreUsedNumber.Value;
                        DC.UpdateProperty(donation, x => x.UsedNumber);
                    }

                    DC.SaveChanges();

                    transaction.Commit();

                    Msg = "功德登記撤銷成功.";
                    Succed = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Error = ex.Message;
                }
            }
        }
    }
}
