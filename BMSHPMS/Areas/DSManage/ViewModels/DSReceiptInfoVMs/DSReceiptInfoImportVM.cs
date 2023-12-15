using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSReceiptInfoVMs
{
    public partial class DSReceiptInfoTemplateVM : BaseTemplateVM
    {
        [Display(Name = "收據號碼")]
        public ExcelPropety ReceiptNumber_Excel = ExcelPropety.CreateProperty<DSReceiptInfo>(x => x.ReceiptNumber);
        [Display(Name = "收據人姓名")]
        public ExcelPropety ReceiptOwn_Excel = ExcelPropety.CreateProperty<DSReceiptInfo>(x => x.ReceiptOwn);
        [Display(Name = "聯絡人姓名")]
        public ExcelPropety ContactName_Excel = ExcelPropety.CreateProperty<DSReceiptInfo>(x => x.ContactName);
        [Display(Name = "聯絡人電話")]
        public ExcelPropety ContactPhone_Excel = ExcelPropety.CreateProperty<DSReceiptInfo>(x => x.ContactPhone);
        [Display(Name = "金額")]
        public ExcelPropety Sum_Excel = ExcelPropety.CreateProperty<DSReceiptInfo>(x => x.Sum);
        [Display(Name = "備註")]
        public ExcelPropety DSRemark_Excel = ExcelPropety.CreateProperty<DSReceiptInfo>(x => x.DSRemark);
        [Display(Name = "收據日期")]
        public ExcelPropety ReceiptDate_Excel = ExcelPropety.CreateProperty<DSReceiptInfo>(x => x.ReceiptDate);

	    protected override void InitVM()
        {
        }

    }

    public class DSReceiptInfoImportVM : BaseImportVM<DSReceiptInfoTemplateVM, DSReceiptInfo>
    {

    }

}
