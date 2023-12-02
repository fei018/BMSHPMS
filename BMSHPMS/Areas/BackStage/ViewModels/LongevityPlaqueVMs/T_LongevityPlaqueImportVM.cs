using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.Areas.BackStage.ViewModels.T_LongevityPlaqueVMs
{
    public partial class T_LongevityPlaqueTemplateVM : BaseTemplateVM
    {
        [Display(Name = "姓名")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<T_LongevityPlaque>(x => x.Name);
        [Display(Name = "金額")]
        public ExcelPropety Sum_Excel = ExcelPropety.CreateProperty<T_LongevityPlaque>(x => x.Sum);
        [Display(Name = "延生編號")]
        public ExcelPropety Serial_Excel = ExcelPropety.CreateProperty<T_LongevityPlaque>(x => x.Serial);
        [Display(Name = "備註")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<T_LongevityPlaque>(x => x.PRemark);
        public ExcelPropety Receipt_Excel = ExcelPropety.CreateProperty<T_LongevityPlaque>(x => x.ReceiptID);

	    protected override void InitVM()
        {
            Receipt_Excel.DataType = ColumnDataType.ComboBox;
            Receipt_Excel.ListItems = DC.Set<T_Receipt>().GetSelectListItems(Wtm, y => y.ReceiptNumber);
        }

    }

    public class T_LongevityPlaqueImportVM : BaseImportVM<T_LongevityPlaqueTemplateVM, T_LongevityPlaque>
    {

    }

}
