using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSDonorInfoVMs
{
    public partial class DSDonorInfoTemplateVM : BaseTemplateVM
    {
        [Display(Name = "延生位姓名")]
        public ExcelPropety LongevityName_Excel = ExcelPropety.CreateProperty<DSDonorInfo>(x => x.LongevityName);
        [Display(Name = "附薦宗親名及稱呼")]
        public ExcelPropety DeceasedName_Excel = ExcelPropety.CreateProperty<DSDonorInfo>(x => x.DeceasedName);
        [Display(Name = "陽居姓名")]
        public ExcelPropety BenefactorName_Excel = ExcelPropety.CreateProperty<DSDonorInfo>(x => x.BenefactorName);
        [Display(Name = "金額")]
        public ExcelPropety Sum_Excel = ExcelPropety.CreateProperty<DSDonorInfo>(x => x.Sum);
        [Display(Name = "功德主編號")]
        public ExcelPropety SerialCode_Excel = ExcelPropety.CreateProperty<DSDonorInfo>(x => x.SerialCode);
        [Display(Name = "備註")]
        public ExcelPropety DSRemark_Excel = ExcelPropety.CreateProperty<DSDonorInfo>(x => x.DSRemark);
        [Display(Name = "收據")]
        public ExcelPropety ReceiptInfo_Excel = ExcelPropety.CreateProperty<DSDonorInfo>(x => x.ReceiptInfoID);

	    protected override void InitVM()
        {
            ReceiptInfo_Excel.DataType = ColumnDataType.ComboBox;
            ReceiptInfo_Excel.ListItems = DC.Set<DSReceiptInfo>().GetSelectListItems(Wtm, y => y.ContactName);
        }

    }

    public class DSDonorInfoImportVM : BaseImportVM<DSDonorInfoTemplateVM, DSDonorInfo>
    {

    }

}
