using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSDonorInfoVMs
{
    public partial class DSDonorInfoListVM : BasePagedListVM<DSDonorInfo_View, DSDonorInfoSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeStandardAction("DSDonorInfo", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800),
                this.MakeStandardAction("DSDonorInfo", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800,dialogHeight:400),            
                this.MakeStandardAction("DSDonorInfo", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800,dialogHeight:400),
                this.MakeStandardAction("DSDonorInfo", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800,dialogHeight:400),
                //this.MakeStandardAction("DSDonorInfo", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("DSDonorInfo", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("DSDonorInfo", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("DSDonorInfo", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
            };
        }


        protected override IEnumerable<IGridColumn<DSDonorInfo_View>> InitGridHeader()
        {
            return new List<GridColumn<DSDonorInfo_View>>{
                this.MakeGridHeader(x => x.ReceiptDate_view),
                this.MakeGridHeader(x => x.ReceiptNumber_view),
                this.MakeGridHeader(x => x.SerialCode),
                this.MakeGridHeader(x => x.Sum,width:80),
                this.MakeGridHeader(x => x.LongevityName),
                this.MakeGridHeader(x => x.DeceasedName),
                this.MakeGridHeader(x => x.BenefactorName),                              
                this.MakeGridHeader(x => x.DSRemark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DSDonorInfo_View> GetSearchQuery()
        {
            var query = DC.Set<DSDonorInfo>()
                .CheckContain(Searcher.LongevityName, x=>x.LongevityName)
                .CheckContain(Searcher.DeceasedName, x=>x.DeceasedName)
                .CheckContain(Searcher.BenefactorName, x=>x.BenefactorName)
                .CheckEqual(Searcher.Sum, x=>x.Sum)
                .CheckContain(Searcher.SerialCode, x=>x.SerialCode)
                .CheckContain(Searcher.ReceiptNumber, x => x.ReceiptInfo.ReceiptNumber)
                .Select(x => new DSDonorInfo_View
                {
				    ID = x.ID,
                    LongevityName = x.LongevityName,
                    DeceasedName = x.DeceasedName,
                    BenefactorName = x.BenefactorName,
                    Sum = x.Sum,
                    SerialCode = x.SerialCode,
                    DSRemark = x.DSRemark,
                    ReceiptNumber_view = x.ReceiptInfo.ReceiptNumber,
                    ReceiptDate_view = x.ReceiptInfo.ReceiptDate.Value
                })
                .OrderBy(x => x.ReceiptDate_view);
            return query;
        }

    }

    public class DSDonorInfo_View : DSDonorInfo{
        [Display(Name = "收據號碼")]
        public String ReceiptNumber_view { get; set; }

        [Display(Name = "收據日期")]
        public DateTime ReceiptDate_view { get; set; }

    }
}
