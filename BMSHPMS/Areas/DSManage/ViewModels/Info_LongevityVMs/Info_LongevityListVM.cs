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
using System.Linq.Dynamic.Core;


namespace BMSHPMS.DSManage.ViewModels.Info_LongevityVMs
{
    public partial class Info_LongevityListVM : BasePagedListVM<Info_Longevity_View, Info_LongevitySearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 1000,dialogHeight:600),
                this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800, dialogHeight : 600),
                this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800, dialogHeight : 400),               
                //this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800),
                //this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("Info_Longevity", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
                this.MakeAction("Info_Longevity","ExportExcelTemplate","牌位Excel","牌位Excel", GridActionParameterTypesEnum.MultiIds,"DSManage",dialogWidth:800,dialogHeight:600),
                
            };
        }


        protected override IEnumerable<IGridColumn<Info_Longevity_View>> InitGridHeader()
        {
            return new List<GridColumn<Info_Longevity_View>>{
                this.MakeGridHeader(x => x.ReceiptDate,width:110).SetSort(),
                this.MakeGridHeader(x => x.DharmaServiceFullName,width:150),
                this.MakeGridHeader(x => x.ReceiptNumber,width:120).SetSort(),
                this.MakeGridHeader(x => x.SerialCode, width : 110).SetSort(),
                this.MakeGridHeader(x => x.Sum,width:100).SetSort().SetShowTotal(),
                this.MakeGridHeader(x => x.Name).SetSort(),                             
                this.MakeGridHeader(x => x.DSRemark),               
                this.MakeGridHeaderAction(width: 200),
            };
        }

        public override IOrderedQueryable<Info_Longevity_View> GetSearchQuery()
        {
            var serial = new ListVMHelper().GetQuerySerialCodes(Searcher.SerialCode, Searcher.SerialCodeEnd);

            var query = DC.Set<Info_Longevity>()
                        .AsNoTracking()
                        .CheckContain(Searcher.Name, x => x.Name)
                        .CheckEqual(Searcher.Sum, x => x.Sum)
                        .CheckContain(Searcher.DSRemark, x => x.DSRemark)
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

            var query1 = query.Select(x => new Info_Longevity_View
            {
                ID = x.ID,
                Name = x.Name,
                Sum = x.Sum,
                SerialCode = x.SerialCode,
                DSRemark = x.DSRemark,
                ReceiptNumber = x.Receipt.ReceiptNumber,
                ReceiptDate = x.Receipt.ReceiptDate.Value,
                DharmaServiceFullName = x.Receipt.DharmaServiceFullName,
                UpdateTime = x.UpdateTime,
                ReceiptID = x.ReceiptID,
            })
            .OrderByDescending(x => x.SerialCode);

            return query1;
        }

        #region public async Task<byte[]> ExportExcel()
        public async Task<byte[]> ExportExcel()
        {
            List<Info_Longevity_View> list;


            if (this.Ids == null || this.Ids.Count <= 0)
            {
                list = await GetSearchQuery().ToListAsync();
            }
            else
            {
                list = await GetSearchQuery().CheckIDs(Ids).ToListAsync();
            }

            if (list == null || list.Count <= 0)
            {
                throw new Exception("查詢數據是空.");
            }

            return await LongevityExcelVM.ExportExcelAsBytes(list);
        }
        #endregion
    }

    public class Info_Longevity_View : Info_Longevity{

        [Display(Name = "收據號碼")]
        public String ReceiptNumber { get; set; }

        [Display(Name = "收據日期")]
        public DateTime ReceiptDate { get; set; }

        [Display(Name = "收據更新日期")]
        public DateTime? ReceiptUpdateTime { get; set; }

        [Display(Name = "法會")]
        public string DharmaServiceFullName { get; set; }
    }
}
