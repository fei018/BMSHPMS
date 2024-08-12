using BMSHPMS.DSManage.ViewModels.Info_DonorVMs;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSManage.ViewModels.Info_ReceiptVMs
{
    public class FillData_DonorListVM : BasePagedListVM<Info_Donor_View, ReceiptListVMSearcher>
    {
        public ReceiptPageMode PageMode { get; set; }

        protected override List<GridAction> InitGridAction()
        {
            List<GridAction> actions;

            switch (PageMode)
            {
                case ReceiptPageMode.Detials:
                    actions = new List<GridAction>()
                    {
                        this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800, dialogHeight: 500)
                    };
                    break;

                case ReceiptPageMode.FillData:
                    actions = new List<GridAction>
                    {
                        this.MakeAction("Info_Donor","EditFill","修改","修改", GridActionParameterTypesEnum.SingleId,"DSManage",dialogWidth: 800,dialogHeight:600).SetShowInRow().SetHideOnToolBar(),
                    };
                    break;

                default:
                    actions = new List<GridAction>
                    {
                        this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800),
                        this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800,dialogHeight:600),
                        this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800,dialogHeight:500),
                        this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800,dialogHeight:500),
                    };
                    break;
            }

            return actions;
        }

        protected override IEnumerable<IGridColumn<Info_Donor_View>> InitGridHeader()
        {
            return new List<GridColumn<Info_Donor_View>>{
                this.MakeGridHeader(x => x.SerialCode,width:120),
                this.MakeGridHeader(x => x.Sum,width:80),
                this.MakeGridHeader(x => x.LongevityName, width : 150),
                this.MakeGridHeader(x => x.DeceasedName_1, width : 250),
                this.MakeGridHeader(x => x.DeceasedName_2, width : 250),
                this.MakeGridHeader(x => x.DeceasedName_3, width : 250),
                this.MakeGridHeader(x => x.BenefactorName, width : 200),
                this.MakeGridHeader(x => x.DSRemark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Info_Donor_View> GetSearchQuery()
        {
            var query = DC.Set<Info_Donor>().AsNoTracking().CheckEqual(Searcher.ReceiptID, x => x.ReceiptID);

            var query1 = query.Select(x => new Info_Donor_View
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
            })
            .OrderBy(x => x.SerialCode);

            return query1;
        }
    }
}
