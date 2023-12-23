using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSMemorialInfoVMs
{
    public partial class DSMemorialInfoListVM : BasePagedListVM<DSMemorialInfo_View, DSMemorialInfoSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeStandardAction("DSMemorialInfo", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800),
                this.MakeStandardAction("DSMemorialInfo", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800,dialogHeight:400),
                this.MakeStandardAction("DSMemorialInfo", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800,dialogHeight:400),
                this.MakeStandardAction("DSMemorialInfo", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800,dialogHeight:400),              
                //this.MakeStandardAction("DSMemorialInfo", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("DSMemorialInfo", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("DSMemorialInfo", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("DSMemorialInfo", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
            };
        }


        protected override IEnumerable<IGridColumn<DSMemorialInfo_View>> InitGridHeader()
        {
            return new List<GridColumn<DSMemorialInfo_View>>{
                this.MakeGridHeader(x => x.ReceiptDate_view,width:120),
                this.MakeGridHeader(x => x.ReceiptNumber_view,width:120),
                this.MakeGridHeader(x => x.SerialCode,width:120),
                this.MakeGridHeader(x => x.Sum,width:80),
                this.MakeGridHeader(x => x.DeceasedName),
                this.MakeGridHeader(x => x.BenefactorName),                         
                this.MakeGridHeader(x => x.DSRemark),               
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DSMemorialInfo_View> GetSearchQuery()
        {
            var query = DC.Set<DSMemorialInfo>()
                .CheckContain(Searcher.SerialCode, x=>x.SerialCode)
                .CheckContain(Searcher.BenefactorName, x=>x.BenefactorName)
                .CheckContain(Searcher.DeceasedName, x=>x.DeceasedName)
                .CheckEqual(Searcher.Sum, x=>x.Sum)
                .CheckContain(Searcher.ReceiptNumber, x => x.ReceiptInfo.ReceiptNumber)
                .Select(x => new DSMemorialInfo_View
                {
				    ID = x.ID,
                    SerialCode = x.SerialCode,
                    BenefactorName = x.BenefactorName,
                    DeceasedName = x.DeceasedName,
                    Sum = x.Sum,
                    DSRemark = x.DSRemark,
                    ReceiptNumber_view = x.ReceiptInfo.ReceiptNumber,
                    ReceiptDate_view = x.ReceiptInfo.ReceiptDate.Value
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class DSMemorialInfo_View : DSMemorialInfo{
        [Display(Name = "收據號碼")]
        public String ReceiptNumber_view { get; set; }

        [Display(Name = "收據日期")]
        public DateTime ReceiptDate_view { get; set; }

    }
}
