using BMSHPMS.DSManage.ViewModels.Common;
using BMSHPMS.DSManage.ViewModels.Info_LongevityVMs;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSReception.ViewModels.DSOldRegisterVMs
{
    public class OldRegLongevityListVM : BasePagedListVM<Info_Longevity_View, OldRegSearcher>
    {
        protected override IEnumerable<IGridColumn<Info_Longevity_View>> InitGridHeader()
        {
            return new List<GridColumn<Info_Longevity_View>>{
                this.MakeGridHeader(x => x.DharmaServiceFullName, width : 150),
                this.MakeGridHeader(x => x.ReceiptNumber, width : 150),
                this.MakeGridHeader(x => x.SerialCode, width : 110),
                this.MakeGridHeader(x => x.Sum,width:80),
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.DSRemark),
                //this.MakeGridHeaderAction(width: 200),
            };
        }

        public override IOrderedQueryable<Info_Longevity_View> GetBatchQuery()
        {
            return GetSearchQuery();
        }

        public override IOrderedQueryable<Info_Longevity_View> GetSearchQuery()
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

            var query = DC.Set<Info_Longevity>().AsNoTracking().CheckEqual(receipt.ID, x => x.ReceiptID);

            var query1 = query.Select(x => new Info_Longevity_View
            {
                ID = x.ID,
                Name = x.Name,
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
