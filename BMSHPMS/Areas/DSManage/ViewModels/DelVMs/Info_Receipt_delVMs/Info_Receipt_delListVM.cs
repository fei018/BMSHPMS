using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_Receipt_delVMs
{
    public partial class Info_Receipt_delListVM : BasePagedListVM<Info_Receipt_del_View, Info_Receipt_delSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeStandardAction("Info_Receipt_del", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Receipt_del", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Receipt_del", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Receipt_del", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Receipt_del", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Receipt_del", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Receipt_del", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Receipt_del", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
            };
        }


        protected override IEnumerable<IGridColumn<Info_Receipt_del_View>> InitGridHeader()
        {
            return new List<GridColumn<Info_Receipt_del_View>>{
                this.MakeGridHeader(x => x.ReceiptNumber),
                this.MakeGridHeader(x => x.ReceiptDate),
                this.MakeGridHeader(x => x.DharmaServiceYear),
                this.MakeGridHeader(x => x.DharmaServiceName),
                this.MakeGridHeader(x => x.ReceiptOwn),
                this.MakeGridHeader(x => x.ContactName),
                this.MakeGridHeader(x => x.ContactPhone),
                this.MakeGridHeader(x => x.Sum),
                this.MakeGridHeader(x => x.DSRemark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Info_Receipt_del_View> GetSearchQuery()
        {
            var query = DC.Set<Info_Receipt_del>()
                .CheckContain(Searcher.ReceiptNumber, x=>x.ReceiptNumber)
                .CheckBetween(Searcher.ReceiptDate?.GetStartTime(), Searcher.ReceiptDate?.GetEndTime(), x => x.ReceiptDate, includeMax: false)
                .CheckEqual(Searcher.DharmaServiceYear, x=>x.DharmaServiceYear)
                .CheckContain(Searcher.DharmaServiceName, x=>x.DharmaServiceName)
                .CheckContain(Searcher.ReceiptOwn, x=>x.ReceiptOwn)
                .CheckContain(Searcher.ContactName, x=>x.ContactName)
                .CheckContain(Searcher.ContactPhone, x=>x.ContactPhone)
                .CheckEqual(Searcher.Sum, x=>x.Sum)
                .Select(x => new Info_Receipt_del_View
                {
				    ID = x.ID,
                    ReceiptNumber = x.ReceiptNumber,
                    ReceiptDate = x.ReceiptDate,
                    DharmaServiceYear = x.DharmaServiceYear,
                    DharmaServiceName = x.DharmaServiceName,
                    ReceiptOwn = x.ReceiptOwn,
                    ContactName = x.ContactName,
                    ContactPhone = x.ContactPhone,
                    Sum = x.Sum,
                    DSRemark = x.DSRemark,
                })
                .OrderByDescending(x => x.ReceiptDate);
            return query;
        }

    }

    public class Info_Receipt_del_View : Info_Receipt_del{

    }
}
