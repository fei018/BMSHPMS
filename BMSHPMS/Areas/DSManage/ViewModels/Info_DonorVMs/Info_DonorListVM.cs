using BMSHPMS.DSManage.ViewModels.Common;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;


namespace BMSHPMS.DSManage.ViewModels.Info_DonorVMs
{
    public partial class Info_DonorListVM : BasePagedListVM<Info_Donor_View, Info_DonorSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 1000),
                this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800,dialogHeight:600),
                this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800,dialogHeight:500),
                this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800,dialogHeight:500),
                //this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Donor", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
                this.MakeAction("Info_Donor","ExportExcelTemplate","牌位Excel","牌位Excel", GridActionParameterTypesEnum.MultiIds,"DSManage",dialogWidth:800,dialogHeight:600),
            };
        }


        protected override IEnumerable<IGridColumn<Info_Donor_View>> InitGridHeader()
        {
            return new List<GridColumn<Info_Donor_View>>{
                this.MakeGridHeader(x => x.ReceiptDate,width:110).SetSort(),
                this.MakeGridHeader(x => x.DharmaServiceFullName,width:150),
                this.MakeGridHeader(x => x.ReceiptNumber,width:120).SetSort(),
                this.MakeGridHeader(x => x.SerialCode,width:120).SetSort(),
                this.MakeGridHeader(x => x.Sum,width:100).SetSort().SetShowTotal(),
                this.MakeGridHeader(x => x.LongevityName, width : 120).SetSort(),
                this.MakeGridHeader(x => x.DeceasedName_1, width : 170).SetSort(),
                this.MakeGridHeader(x => x.DeceasedName_2, width : 170).SetSort(),
                this.MakeGridHeader(x => x.DeceasedName_3, width : 170).SetSort(),
                this.MakeGridHeader(x => x.BenefactorName, width : 170).SetSort(),
                this.MakeGridHeader(x => x.DSRemark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Info_Donor_View> GetSearchQuery()
        {
            var query = DC.Set<Info_Donor>()
                .AsNoTracking()
                .CheckContain(Searcher.LongevityName, x => x.LongevityName)
                .CheckContain(Searcher.DeceasedName, x => x.DeceasedName_1)
                .CheckContain(Searcher.DeceasedName, x => x.DeceasedName_2)
                .CheckContain(Searcher.DeceasedName, x => x.DeceasedName_3)
                .CheckContain(Searcher.BenefactorName, x => x.BenefactorName)
                .CheckContain(Searcher.DSRemark, x => x.DSRemark)
                .CheckEqual(Searcher.Sum, x => x.Sum)
                .CheckContain(Searcher.ReceiptNumber, x => x.Receipt.ReceiptNumber)
                .CheckBetween(Searcher.ReceiptDate?.GetStartTime(), Searcher.ReceiptDate?.GetEndTime(), x => x.Receipt.ReceiptDate, includeMax: false)
                .CheckEqual(Searcher.DharmaServiceName, x => x.Receipt.DharmaServiceName)
                .CheckEqual(Searcher.DharmaServiceYear, x => x.Receipt.DharmaServiceYear);

            // serials
            var serials = new ListVMHelper().GetQuerySerialCodes(Searcher.SerialCode, Searcher.SerialCodeEnd);
            if (serials.Count == 1)
            {
                query = query.CheckContain(serials.ElementAt(0), x => x.SerialCode);
            }
            else
            {
                query = query.CheckContain(serials, x => x.SerialCode);
            }

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
                ReceiptNumber = x.Receipt.ReceiptNumber,
                ReceiptDate = x.Receipt.ReceiptDate.Value,
                ReceiptUpdateTime = x.Receipt.UpdateTime.Value,
                DharmaServiceFullName = x.Receipt.DharmaServiceFullName,
                UpdateTime = x.UpdateTime,
            })
            .OrderByDescending(x => x.ReceiptDate)
            .ThenBy(x => x.SerialCode);

            return query1;
        }

        #region public async Task<byte[]> ExportExcel()
        public async Task<byte[]> ExportExcel()
        {
            List<Info_Donor_View> list;


            if (this.Ids == null || this.Ids.Count <= 0)
            {
                list = await GetSearchQuery().ToListAsync();
            }
            else
            {
                list = await GetSearchQuery().CheckIDs(Ids).OrderBy(x => x.ID).ToListAsync();
            }

            if (list == null || list.Count <= 0)
            {
                throw new Exception("查詢數據是空.");
            }

            return await DonorExcelVM.ExportExcelAsBytes(list);
        }
        #endregion

    }

    public class Info_Donor_View : Info_Donor
    {
        [Display(Name = "收據號碼")]
        public String ReceiptNumber { get; set; }

        [Display(Name = "收據日期")]
        public DateTime ReceiptDate { get; set; }

        [Display(Name = "更新日期")]
        public DateTime ReceiptUpdateTime { get; set; }

        [Display(Name = "法會")]
        public string DharmaServiceFullName { get; set; }

    }
}
