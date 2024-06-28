using BMSHPMS.Models.CommonDService;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;


namespace BMSHPMS.CommonManage.ViewModels.CommonReceiptVMs
{
    public partial class CommonReceiptVM : BaseCRUDVM<CommonReceipt>
    {
        public string DonationCreateUrl { get; set; }

        public List<ComboSelectListItem> DonationCategorySelectItems { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        public string[] DonationCategoryArray { get; set; }

        public CommonReceiptVM()
        {
        }

        protected override void InitVM()
        {
            DonationCategorySelectItems = CommonDonateCategoryHelper.SelectItems();
        }

        public override void DoAdd()
        {
            if (DonationCategoryArray.Length == 1)
            {
                Entity.DonationCategory = DonationCategoryArray[0];
            }
            else
            {
                Entity.DonationCategory = string.Join(',',DonationCategoryArray);
            }

            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }


        public DonationListVM_AnnualDabei AnnualDabeiListVM { get; set; }

        public DonationListVM_AnnualLight AnnualLightListVM { get; set; }

        public void InitDonationListVM()
        {
            //switch (Entity.DonationCategory)
            //{
            //    case CommonDonateCategoryEnum.全年大悲法會:
            //        AnnualDabeiListVM = new DonationListVM_AnnualDabei
            //        {
            //            Searcher = new DonationListVMSearcher { ReceiptId = Entity.ID }
            //        };
            //        AnnualDabeiListVM.CopyContext(this);
            //        break;

            //    case CommonDonateCategoryEnum.全年光明燈:
            //        AnnualLightListVM = new DonationListVM_AnnualLight
            //        {
            //            Searcher = new DonationListVMSearcher { ReceiptId = Entity.ID }
            //        };
            //        AnnualLightListVM.CopyContext(this);
            //        break;

            //    default:
            //        break;
            //}
        }

        public void InitDetails()
        {
            //switch (Entity.DonationCategory)
            //{
            //    case CommonDonateCategoryEnum.全年大悲法會:
            //        DonationCreateUrl = $"/CommonManage/AnnualDabeiInfo/Create?receiptId={Entity.ID}";
            //        break;

            //    case CommonDonateCategoryEnum.全年光明燈:
            //        DonationCreateUrl = $"/CommonManage/AnnualLightInfo/Create?receiptId={Entity.ID}";
            //        break;

            //    default:
            //        break;
            //}
        }
    }
}
