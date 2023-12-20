using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSLongevityInfoVMs
{
    public partial class DSLongevityInfoListVM : BasePagedListVM<DSLongevityInfo_View, DSLongevityInfoSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeStandardAction("DSLongevityInfo", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800),
                this.MakeStandardAction("DSLongevityInfo", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800, dialogHeight : 400),
                this.MakeStandardAction("DSLongevityInfo", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("DSLongevityInfo", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800, dialogHeight : 400),               
                //this.MakeStandardAction("DSLongevityInfo", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("DSLongevityInfo", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("DSLongevityInfo", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("DSLongevityInfo", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
            };
        }


        protected override IEnumerable<IGridColumn<DSLongevityInfo_View>> InitGridHeader()
        {
            return new List<GridColumn<DSLongevityInfo_View>>{
                this.MakeGridHeader(x => x.ReceiptDate_view),
                this.MakeGridHeader(x => x.ReceiptNumber_view),               
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Sum),
                this.MakeGridHeader(x => x.SerialCode),
                this.MakeGridHeader(x => x.DSRemark),               
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DSLongevityInfo_View> GetSearchQuery()
        {
            var query = DC.Set<DSLongevityInfo>()
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckEqual(Searcher.Sum, x=>x.Sum)
                .CheckContain(Searcher.SerialCode, x=>x.SerialCode)
                .CheckContain(Searcher.ReceiptNumber, x=>x.ReceiptInfo.ReceiptNumber)
                .Select(x => new DSLongevityInfo_View
                {
				    ID = x.ID,
                    Name = x.Name,
                    Sum = x.Sum,
                    SerialCode = x.SerialCode,
                    DSRemark = x.DSRemark,
                    ReceiptNumber_view = x.ReceiptInfo.ReceiptNumber,
                    ReceiptDate_view = x.ReceiptInfo.ReceiptDate.Value
                })
                .OrderBy(x => x.ReceiptDate_view);

            //if (Searcher.ReceiptDate.HasValue)
            //{
            //    query = query.Where(q => DateTime.Compare(q.ReceiptDate_view.Date, Searcher.ReceiptDate.Value.Date) == 0).OrderBy(q => q.ReceiptDate_view);
            //}

            return query;
        }

    }

    public class DSLongevityInfo_View : DSLongevityInfo{

        [Display(Name = "收據號碼")]
        public String ReceiptNumber_view { get; set; }

        [Display(Name = "收據日期")]
        public DateTime ReceiptDate_view { get; set; }
    }
}
