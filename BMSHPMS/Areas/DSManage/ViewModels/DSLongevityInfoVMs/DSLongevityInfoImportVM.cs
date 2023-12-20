using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSLongevityInfoVMs
{
    public partial class DSLongevityInfoTemplateVM : BaseTemplateVM
    {
        [Display(Name = "姓名")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<DSLongevityInfo>(x => x.Name);
        [Display(Name = "金額")]
        public ExcelPropety Sum_Excel = ExcelPropety.CreateProperty<DSLongevityInfo>(x => x.Sum);
        [Display(Name = "延生編號")]
        public ExcelPropety SerialCode_Excel = ExcelPropety.CreateProperty<DSLongevityInfo>(x => x.SerialCode);
        [Display(Name = "備註")]
        public ExcelPropety DSRemark_Excel = ExcelPropety.CreateProperty<DSLongevityInfo>(x => x.DSRemark);
        [Display(Name = "收據")]
        public ExcelPropety ReceiptInfo_Excel = ExcelPropety.CreateProperty<DSLongevityInfo>(x => x.ReceiptInfoID);

	    protected override void InitVM()
        {
            ReceiptInfo_Excel.DataType = ColumnDataType.ComboBox;
            ReceiptInfo_Excel.ListItems = DC.Set<DSReceiptInfo>().GetSelectListItems(Wtm, y => y.ContactName);
        }

    }

    public class DSLongevityInfoImportVM : BaseImportVM<DSLongevityInfoTemplateVM, DSLongevityInfo>
    {

    }

}
