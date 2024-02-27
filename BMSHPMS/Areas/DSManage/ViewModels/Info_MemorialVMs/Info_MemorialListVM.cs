using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DharmaService;
using BMSHPMS.DSManage.ViewModels.Common;


namespace BMSHPMS.DSManage.ViewModels.Info_MemorialVMs
{
    public partial class Info_MemorialListVM : BasePagedListVM<Info_Memorial_View, Info_MemorialSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeStandardAction("Info_Memorial", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Memorial", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800,dialogHeight:500),
                this.MakeStandardAction("Info_Memorial", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800,dialogHeight:500),
                this.MakeStandardAction("Info_Memorial", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800,dialogHeight:500),              
                //this.MakeStandardAction("Info_Memorial", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Memorial", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Memorial", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Memorial", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
            };
        }


        protected override IEnumerable<IGridColumn<Info_Memorial_View>> InitGridHeader()
        {
            return new List<GridColumn<Info_Memorial_View>>{
                this.MakeGridHeader(x => x.ReceiptDate_view,width:110).SetSort(),
                this.MakeGridHeader(x => x.DharmaServiceFullName,width:150).SetSort(),
                this.MakeGridHeader(x => x.ReceiptNumber_view,width:120).SetSort(),
                this.MakeGridHeader(x => x.SerialCode,width:110).SetSort(),
                this.MakeGridHeader(x => x.Sum,width:80).SetSort(),
                this.MakeGridHeader(x => x.DeceasedName_1,width:170).SetSort(),
                this.MakeGridHeader(x => x.DeceasedName_2,width:170).SetSort(),
                this.MakeGridHeader(x => x.DeceasedName_3,width:170).SetSort(),
                this.MakeGridHeader(x => x.BenefactorName).SetSort(),
                this.MakeGridHeader(x => x.DSRemark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Info_Memorial_View> GetSearchQuery()
        {
            var serial = new ListVMHelper().GetQuerySerialCodes(Searcher.SerialCode, Searcher.SerialCodeEnd);

            var query = DC.Set<Info_Memorial>()
                .CheckContain(serial, x => x.SerialCode)
                .CheckContain(Searcher.BenefactorName, x => x.BenefactorName)
                .CheckContain(Searcher.DeceasedName, x => x.DeceasedName_1)
                .CheckContain(Searcher.DeceasedName, x => x.DeceasedName_2)
                .CheckContain(Searcher.DeceasedName, x => x.DeceasedName_3)
                .CheckEqual(Searcher.Sum, x => x.Sum)
                .CheckContain(Searcher.ReceiptNumber, x => x.Receipt.ReceiptNumber)
                .CheckBetween(Searcher.ReceiptDate?.GetStartTime(), Searcher.ReceiptDate?.GetEndTime(), x => x.Receipt.ReceiptDate)
                .CheckEqual(Searcher.DharmaServiceName, x => x.Receipt.DharmaServiceName)
                .CheckEqual(Searcher.DharmaServiceYear, x => x.Receipt.DharmaServiceYear);

            var query1 = query.Select(x => new Info_Memorial_View
            {
                ID = x.ID,
                SerialCode = x.SerialCode,
                BenefactorName = x.BenefactorName,
                DeceasedName_1 = x.DeceasedName_1,
                DeceasedName_2 = x.DeceasedName_2,
                DeceasedName_3 = x.DeceasedName_3,
                Sum = x.Sum,
                DSRemark = x.DSRemark,
                ReceiptNumber_view = x.Receipt.ReceiptNumber,
                ReceiptDate_view = x.Receipt.ReceiptDate.Value,
                ReceiptUpdateTime_view = x.Receipt.UpdateTime.Value,
                DharmaServiceFullName = x.Receipt.DharmaServiceFullName,
            })
                .OrderByDescending(x => x.ReceiptUpdateTime_view);

            return query1;
        }

    }

    public class Info_Memorial_View : Info_Memorial
    {
        [Display(Name = "收據號碼")]
        public String ReceiptNumber_view { get; set; }

        [Display(Name = "收據日期")]
        public DateTime ReceiptDate_view { get; set; }

        [Display(Name = "收據更新日期")]
        public DateTime ReceiptUpdateTime_view { get; set; }

        [Display(Name = "法會")]
        public string DharmaServiceFullName { get; set; }
    }
}
