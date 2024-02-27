using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_Donor_delVMs
{
    public partial class Info_Donor_delListVM : BasePagedListVM<Info_Donor_del_View, Info_Donor_delSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeStandardAction("Info_Donor_del", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Donor_del", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Donor_del", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Donor_del", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Donor_del", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Donor_del", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Donor_del", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Donor_del", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
            };
        }


        protected override IEnumerable<IGridColumn<Info_Donor_del_View>> InitGridHeader()
        {
            return new List<GridColumn<Info_Donor_del_View>>{
                this.MakeGridHeader(x => x.LongevityName),
                this.MakeGridHeader(x => x.DeceasedName_1),
                this.MakeGridHeader(x => x.DeceasedName_2),
                this.MakeGridHeader(x => x.DeceasedName_3),
                this.MakeGridHeader(x => x.BenefactorName),
                this.MakeGridHeader(x => x.Sum),
                this.MakeGridHeader(x => x.SerialCode),
                this.MakeGridHeader(x => x.DSRemark),
                this.MakeGridHeader(x => x.ContactName_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Info_Donor_del_View> GetSearchQuery()
        {
            var query = DC.Set<Info_Donor_del>()
                .CheckContain(Searcher.LongevityName, x=>x.LongevityName)
                .CheckContain(Searcher.DeceasedName_1, x=>x.DeceasedName_1)
                .CheckContain(Searcher.DeceasedName_2, x=>x.DeceasedName_2)
                .CheckContain(Searcher.DeceasedName_3, x=>x.DeceasedName_3)
                .CheckContain(Searcher.BenefactorName, x=>x.BenefactorName)
                .CheckEqual(Searcher.Sum, x=>x.Sum)
                .CheckContain(Searcher.SerialCode, x=>x.SerialCode)
                .Select(x => new Info_Donor_del_View
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
                    ContactName_view = x.Receipt_del.ContactName,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Info_Donor_del_View : Info_Donor_del{
        [Display(Name = "聯絡人姓名")]
        public String ContactName_view { get; set; }

    }
}
