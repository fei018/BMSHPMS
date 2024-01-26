using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.Info_Longevity_delVMs
{
    public partial class Info_Longevity_delListVM : BasePagedListVM<Info_Longevity_del_View, Info_Longevity_delSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeStandardAction("Info_Longevity_del", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Longevity_del", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Longevity_del", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Longevity_del", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Longevity_del", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Longevity_del", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Longevity_del", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Longevity_del", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
            };
        }


        protected override IEnumerable<IGridColumn<Info_Longevity_del_View>> InitGridHeader()
        {
            return new List<GridColumn<Info_Longevity_del_View>>{
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Sum),
                this.MakeGridHeader(x => x.SerialCode),
                this.MakeGridHeader(x => x.DSRemark),
                this.MakeGridHeader(x => x.ContactName_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Info_Longevity_del_View> GetSearchQuery()
        {
            var query = DC.Set<Info_Longevity_del>()
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckEqual(Searcher.Sum, x=>x.Sum)
                .CheckContain(Searcher.SerialCode, x=>x.SerialCode)
                .CheckContain(Searcher.DSRemark, x=>x.DSRemark)
                .Select(x => new Info_Longevity_del_View
                {
				    ID = x.ID,
                    Name = x.Name,
                    Sum = x.Sum,
                    SerialCode = x.SerialCode,
                    DSRemark = x.DSRemark,
                    ContactName_view = x.Receipt_del.ContactName,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Info_Longevity_del_View : Info_Longevity_del{
        [Display(Name = "聯絡人姓名")]
        public String ContactName_view { get; set; }

    }
}
