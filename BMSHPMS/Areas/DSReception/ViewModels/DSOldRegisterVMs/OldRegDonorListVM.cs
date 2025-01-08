using BMSHPMS.DSManage.ViewModels.Info_DonorVMs;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSReception.ViewModels.DSOldRegisterVMs
{
    public class OldRegDonorListVM : BasePagedListVM<Info_Donor_View, OldRegSearcher>
    {

        protected override IEnumerable<IGridColumn<Info_Donor_View>> InitGridHeader()
        {
            return new List<GridColumn<Info_Donor_View>>{
                this.MakeGridHeader(x => x.DharmaServiceFullName, width : 150),
                this.MakeGridHeader(x => x.ReceiptNumber, width : 150),
                this.MakeGridHeader(x => x.SerialCode,width:120),
                this.MakeGridHeader(x => x.Sum,width:80),
                this.MakeGridHeader(x => x.LongevityName, width : 150),
                this.MakeGridHeader(x => x.DeceasedName_1, width : 250),
                this.MakeGridHeader(x => x.DeceasedName_2, width : 250),
                this.MakeGridHeader(x => x.DeceasedName_3, width : 250),
                this.MakeGridHeader(x => x.BenefactorName, width : 200),
                this.MakeGridHeader(x => x.DSRemark),
                //this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Info_Donor_View> GetBatchQuery()
        {
            return GetSearchQuery();
        }

        public override IOrderedQueryable<Info_Donor_View> GetSearchQuery()
        {
            if (string.IsNullOrWhiteSpace(Searcher.ReceiptNumber))
            {
                return null;
            }

            var receipt = DC.Set<Info_Receipt>().AsNoTracking().CheckEqual(Searcher.ReceiptNumber, x => x.ReceiptNumber).FirstOrDefault();

            if (receipt == null)
            {
                return null;
            }

            var query = DC.Set<Info_Donor>().AsNoTracking().CheckEqual(receipt.ID, x => x.ReceiptID);

            var query1 = query.Select(x => new Info_Donor_View
            {
                ID = x.ID,
                LongevityName = x.LongevityName,
                DeceasedName_1 = x.DeceasedName_1,
                DeceasedName_2 = x.DeceasedName_2,
                DeceasedName_3 = x.DeceasedName_3,
                BenefactorName = x.BenefactorName,
                Sum = x.Sum,
                SerialCode = x.SerialCode,
                DSRemark = x.DSRemark,
                DharmaServiceFullName = receipt.DharmaServiceFullName,
                ReceiptNumber = receipt.ReceiptNumber,
            })
            .OrderBy(x => x.SerialCode);

            return query1;
        }
    }
}
