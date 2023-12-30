using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DharmaService;
using NetBox.Extensions;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using BMSHPMS.Helper;
using BMSHPMS.DSManage.ViewModels.ExcelVMs;



namespace BMSHPMS.DSManage.ViewModels.Info_ReceiptVMs
{
    public partial class Info_ReceiptListVM : BasePagedListVM<Info_Receipt_View, Info_ReceiptSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800,dialogHeight:400),
                this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800, dialogHeight : 400),
                this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800, dialogHeight : 600),
                this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800, dialogHeight : 400),               
                //this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Receipt", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
                //this.MakeAction("Info_Receipt","Test","test","test title", GridActionParameterTypesEnum.MultiIds,"DSManage").SetOnClickScript("test"),
            };
        }


        protected override IEnumerable<IGridColumn<Info_Receipt_View>> InitGridHeader()
        {
            return new List<GridColumn<Info_Receipt_View>>{
                this.MakeGridHeader(x => x.ReceiptDate,width:150).SetSort(),
                this.MakeGridHeader(x => x.ReceiptNumber,width:150).SetSort(),
                this.MakeGridHeader(x => x.Sum,width:150).SetSort(),
                this.MakeGridHeader(x => x.DharmaServiceName),
                this.MakeGridHeader(x => x.ReceiptOwn),
                this.MakeGridHeader(x => x.ContactName),
                this.MakeGridHeader(x => x.ContactPhone),
                this.MakeGridHeader(x => x.DSRemark),
                this.MakeGridHeaderAction(width: 220)
            };
        }

        public override IOrderedQueryable<Info_Receipt_View> GetSearchQuery()
        {
            var query = DC.Set<Info_Receipt>()
                .CheckContain(Searcher.ReceiptNumber, x => x.ReceiptNumber)
                .CheckContain(Searcher.ReceiptOwn, x => x.ReceiptOwn)
                .CheckContain(Searcher.ContactName, x => x.ContactName)
                .CheckContain(Searcher.ContactPhone, x => x.ContactPhone)
                .CheckEqual(Searcher.Sum, x => x.Sum)
                .CheckEqual(Searcher.DharmaServiceName, x => x.DharmaServiceName)
                .Where(x => x.IsValid)
                //.CheckBetween(Searcher.ReceiptDate?.GetStartTime(), Searcher.ReceiptDate?.GetStartTime(), x => x.ReceiptDate /*includeMax: false*/)
                .Select(x => new Info_Receipt_View
                {
                    ID = x.ID,
                    ReceiptNumber = x.ReceiptNumber,
                    ReceiptOwn = x.ReceiptOwn,
                    ContactName = x.ContactName,
                    ContactPhone = x.ContactPhone,
                    Sum = x.Sum,
                    DSRemark = x.DSRemark,
                    ReceiptDate = x.ReceiptDate,
                    DharmaServiceName = x.DharmaServiceName,
                    UpdateTime = x.UpdateTime,
                })
                .OrderByDescending(x => x.UpdateTime)
                .ThenByDescending(x => x.ReceiptNumber);

            if (Searcher.ReceiptDate.HasValue)
            {
                query = query.Where(x => DateTime.Compare(Searcher.ReceiptDate.Value.Date, x.ReceiptDate.Value.Date) == 0)
                             .OrderByDescending(x => x.UpdateTime);
            }

            return query;
        }

        public async Task<byte[]> ExportExcel()
        {
            List<Info_Receipt> receiptList;

            if (this.Ids == null || this.Ids.Count <= 0)
            {
                receiptList = await GetSearchQuery().ToListAsync<Info_Receipt>();                            
            }
            else
            {
                receiptList = DC.Set<Info_Receipt>().CheckIDs(this.Ids).ToList();
            }

            if (receiptList == null || receiptList.Count <= 0)
            {
                return null;
            }

            foreach (var r in receiptList)
            {
                r.Info_Donors = DC.Set<Info_Donor>().Where(x => x.ReceiptID == r.ID).ToList();
                r.Info_Longevitys = DC.Set<Info_Longevity>().Where(x => x.ReceiptID == r.ID).ToList();
                r.Info_Memorials = DC.Set<Info_Memorial>().Where(x => x.ReceiptID == r.ID).ToList();
            }

            return await ReceiptExcelVM.ExportExcelAsBytes(receiptList);
        }
    }


    public class Info_Receipt_View : Info_Receipt
    {

    }
}
