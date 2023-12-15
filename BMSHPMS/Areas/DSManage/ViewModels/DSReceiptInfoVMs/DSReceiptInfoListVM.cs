﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSReceiptInfoVMs
{
    public partial class DSReceiptInfoListVM : BasePagedListVM<DSReceiptInfo_View, DSReceiptInfoSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("DSReceiptInfo", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"DSManage", dialogWidth: 800),
                this.MakeStandardAction("DSReceiptInfo", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("DSReceiptInfo", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("DSReceiptInfo", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("DSReceiptInfo", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("DSReceiptInfo", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("DSReceiptInfo", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "DSManage", dialogWidth: 800),
                this.MakeStandardAction("DSReceiptInfo", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "DSManage"),
            };
        }


        protected override IEnumerable<IGridColumn<DSReceiptInfo_View>> InitGridHeader()
        {
            return new List<GridColumn<DSReceiptInfo_View>>{
                this.MakeGridHeader(x => x.ReceiptNumber),
                this.MakeGridHeader(x => x.ReceiptOwn),
                this.MakeGridHeader(x => x.ContactName),
                this.MakeGridHeader(x => x.ContactPhone),
                this.MakeGridHeader(x => x.Sum),
                this.MakeGridHeader(x => x.DSRemark),
                this.MakeGridHeader(x => x.ReceiptDate),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DSReceiptInfo_View> GetSearchQuery()
        {
            var query = DC.Set<DSReceiptInfo>()
                .CheckContain(Searcher.ReceiptNumber, x=>x.ReceiptNumber)
                .CheckContain(Searcher.ReceiptOwn, x=>x.ReceiptOwn)
                .CheckContain(Searcher.ContactName, x=>x.ContactName)
                .CheckContain(Searcher.ContactPhone, x=>x.ContactPhone)
                .CheckEqual(Searcher.Sum, x=>x.Sum)
                .CheckBetween(Searcher.ReceiptDate?.GetStartTime(), Searcher.ReceiptDate?.GetEndTime(), x => x.ReceiptDate, includeMax: false)
                .Select(x => new DSReceiptInfo_View
                {
				    ID = x.ID,
                    ReceiptNumber = x.ReceiptNumber,
                    ReceiptOwn = x.ReceiptOwn,
                    ContactName = x.ContactName,
                    ContactPhone = x.ContactPhone,
                    Sum = x.Sum,
                    DSRemark = x.DSRemark,
                    ReceiptDate = x.ReceiptDate,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class DSReceiptInfo_View : DSReceiptInfo{

    }
}