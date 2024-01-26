using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_DonorVMs
{
    public partial class Info_DonorListVM : BasePagedListVM<Info_Donor_View, Info_DonorSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800,dialogHeight:500),            
                this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800,dialogHeight:500),
                this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800,dialogHeight:500),
                //this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
            };
        }


        protected override IEnumerable<IGridColumn<Info_Donor_View>> InitGridHeader()
        {
            return new List<GridColumn<Info_Donor_View>>{
                this.MakeGridHeader(x => x.ReceiptDate_view,width:150),
                this.MakeGridHeader(x => x.ReceiptNumber_view,width:150),
                this.MakeGridHeader(x => x.SerialCode,width:150),
                this.MakeGridHeader(x => x.Sum,width:100),
                this.MakeGridHeader(x => x.LongevityName, width : 100),
                this.MakeGridHeader(x => x.DeceasedName_1,width:180),
                this.MakeGridHeader(x => x.DeceasedName_2,width:180),
                this.MakeGridHeader(x => x.DeceasedName_3,width:180),
                this.MakeGridHeader(x => x.BenefactorName, width : 100),                              
                this.MakeGridHeader(x => x.DSRemark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Info_Donor_View> GetSearchQuery()
        {
            var query = DC.Set<Info_Donor>()
                .CheckContain(Searcher.LongevityName, x=>x.LongevityName)
                .CheckContain(Searcher.DeceasedName, x=>x.DeceasedName_1)
                .CheckContain(Searcher.DeceasedName, x => x.DeceasedName_2)
                .CheckContain(Searcher.DeceasedName, x => x.DeceasedName_3)
                .CheckContain(Searcher.BenefactorName, x=>x.BenefactorName)
                .CheckEqual(Searcher.Sum, x=>x.Sum)
                .CheckContain(Searcher.SerialCode, x=>x.SerialCode)
                .CheckContain(Searcher.ReceiptNumber, x => x.Receipt.ReceiptNumber)
                .Select(x => new Info_Donor_View
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
                    ReceiptNumber_view = x.Receipt.ReceiptNumber,
                    ReceiptDate_view = x.Receipt.ReceiptDate.Value,
                    ReceiptUpdateTime_view = x.Receipt.UpdateTime.Value,
                })
                .OrderByDescending(x => x.ReceiptUpdateTime_view);
            return query;
        }

    }

    public class Info_Donor_View : Info_Donor{
        [Display(Name = "收據號碼")]
        public String ReceiptNumber_view { get; set; }

        [Display(Name = "收據日期")]
        public DateTime ReceiptDate_view { get; set; }

        [Display(Name = "更新日期")]
        public DateTime ReceiptUpdateTime_view { get; set; }

    }
}
