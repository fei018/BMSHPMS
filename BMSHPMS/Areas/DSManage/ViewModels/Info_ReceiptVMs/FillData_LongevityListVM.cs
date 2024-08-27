using BMSHPMS.DSManage.ViewModels.Info_LongevityVMs;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSManage.ViewModels.Info_ReceiptVMs
{
    public class FillData_LongevityListVM : BasePagedListVM<Info_Longevity_View, FillDataSearcher>
    {
        public ReceiptPageMode PageMode { get; set; }

        protected override List<GridAction> InitGridAction()
        {
            switch (PageMode)
            {
                case ReceiptPageMode.Detials:
                    return new List<GridAction>
                    {
                        this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800),

                    };

                case ReceiptPageMode.FillData:
                    return new List<GridAction>
                    {
                        this.MakeAction("Info_Longevity","EditFill","修改","修改", GridActionParameterTypesEnum.SingleId,"DSManage",dialogWidth: 800,dialogHeight:600).SetShowInRow().SetHideOnToolBar(),
                        this.MakeAction("Info_Longevity","DeleteFill","刪除","刪除", GridActionParameterTypesEnum.SingleId,"DSManage",dialogWidth: 800,dialogHeight:600).SetShowInRow().SetHideOnToolBar(),
                    };

                default:
                    return new List<GridAction>
                    {
                        //this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800),
                        //this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800, dialogHeight : 600),
                        //this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800),
                        //this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800, dialogHeight : 400),
                    };
            }
        }


        protected override IEnumerable<IGridColumn<Info_Longevity_View>> InitGridHeader()
        {
            return new List<GridColumn<Info_Longevity_View>>{
                this.MakeGridHeader(x => x.SerialCode, width : 110),
                this.MakeGridHeader(x => x.Sum,width:80),
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.DSRemark),
                this.MakeGridHeaderAction(width: 200),
            };
        }

        public override IOrderedQueryable<Info_Longevity_View> GetSearchQuery()
        {
            var query = DC.Set<Info_Longevity>()
                        .AsNoTracking()
                        .CheckEqual(Searcher.ReceiptID, x => x.ReceiptID);

            var query1 = query.Select(x => new Info_Longevity_View
            {
                ID = x.ID,
                Name = x.Name,
                Sum = x.Sum,
                SerialCode = x.SerialCode,
                DSRemark = x.DSRemark,
            })
            .OrderBy(x => x.SerialCode);

            return query1;
        }
    }
}
