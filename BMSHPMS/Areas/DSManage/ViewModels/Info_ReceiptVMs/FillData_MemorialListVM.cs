using BMSHPMS.DSManage.ViewModels.Info_MemorialVMs;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSManage.ViewModels.Info_ReceiptVMs
{
    public class FillData_MemorialListVM : BasePagedListVM<Info_Memorial_View, ReceiptListVMSearcher>
    {
        public ReceiptPageMode PageMode { get; set; }

        protected override List<GridAction> InitGridAction()
        {
            switch (PageMode)
            {
                case ReceiptPageMode.Detials:
                    return new List<GridAction>
                    {
                        this.MakeStandardAction("Info_Memorial", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800,dialogHeight:500),
                    };

                case ReceiptPageMode.FillData:
                    return new List<GridAction>
                    {
                        this.MakeAction("Info_Memorial","EditFill","修改","修改", GridActionParameterTypesEnum.SingleId,"DSManage",dialogWidth: 800,dialogHeight:600).SetShowInRow().SetHideOnToolBar(),
                    };

                default:
                    return new List<GridAction>
                    {
                        this.MakeStandardAction("Info_Memorial", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800),
                        this.MakeStandardAction("Info_Memorial", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800,dialogHeight:600),
                        this.MakeStandardAction("Info_Memorial", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800,dialogHeight:500),
                        this.MakeStandardAction("Info_Memorial", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800,dialogHeight:500),
                    };
            }

        }

        protected override IEnumerable<IGridColumn<Info_Memorial_View>> InitGridHeader()
        {
            return new List<GridColumn<Info_Memorial_View>>{
                this.MakeGridHeader(x => x.SerialCode,width:110),
                this.MakeGridHeader(x => x.Sum,width:80),
                this.MakeGridHeader(x => x.DeceasedName_1,width:250),
                this.MakeGridHeader(x => x.DeceasedName_2,width:250),
                this.MakeGridHeader(x => x.DeceasedName_3,width:250),
                this.MakeGridHeader(x => x.BenefactorName,width:250),
                this.MakeGridHeader(x => x.DSRemark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Info_Memorial_View> GetSearchQuery()
        {
            var query = DC.Set<Info_Memorial>()
                .AsNoTracking()
                .CheckEqual(Searcher.ReceiptID, x => x.ReceiptID);

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
            })
            .OrderBy(x => x.SerialCode);

            return query1;
        }
    }
}
