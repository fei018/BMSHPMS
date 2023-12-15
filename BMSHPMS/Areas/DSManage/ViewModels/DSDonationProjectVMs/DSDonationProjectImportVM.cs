using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using BMSHPMS.Models.DharmaService;


namespace BMSHPMS.DSManage.ViewModels.DSDonationProjectVMs
{
    public partial class DSDonationProjectTemplateVM : BaseTemplateVM
    {
        [Display(Name = "金額")]
        public ExcelPropety Sum_Excel = ExcelPropety.CreateProperty<DSDonationProject>(x => x.Sum);
        [Display(Name = "金額代碼")]
        public ExcelPropety SumCode_Excel = ExcelPropety.CreateProperty<DSDonationProject>(x => x.SerialCode);
        [Display(Name = "功德類別")]
        public ExcelPropety DonationCategory_Excel = ExcelPropety.CreateProperty<DSDonationProject>(x => x.DonationCategory);
        [Display(Name = "已使用數")]
        public ExcelPropety UsedNumber_Excel = ExcelPropety.CreateProperty<DSDonationProject>(x => x.UsedNumber);
        [Display(Name = "法會項目")]
        public ExcelPropety DServiceProj_Excel = ExcelPropety.CreateProperty<DSDonationProject>(x => x.DServiceProjID);

	    protected override void InitVM()
        {
            DServiceProj_Excel.DataType = ColumnDataType.ComboBox;
            DServiceProj_Excel.ListItems = DC.Set<DServiceProject>().GetSelectListItems(Wtm, y => y.ProjectName);
        }

    }

    public class DSDonationProjectImportVM : BaseImportVM<DSDonationProjectTemplateVM, DSDonationProject>
    {

    }

}
