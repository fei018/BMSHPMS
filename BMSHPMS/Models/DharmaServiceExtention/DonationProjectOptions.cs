using System.Collections.Generic;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Models.DharmaServiceExtention
{
    public class DonationProjectOptions
    {
        public static class Category
        {
            public const string 功德主 = "功德主";
            public const string 延生位 = "延生位";
            public const string 附薦位 = "附薦位";
        }

        public static List<ComboSelectListItem> GetCategoryComboSelectItems()
        {
            List<ComboSelectListItem> list = new()
            {
                new ComboSelectListItem { Text = "功德主", Value = "功德主" },
                new ComboSelectListItem { Text ="延生位", Value = "延生位" },
                new ComboSelectListItem { Text = "附薦位",Value = "附薦位" }
            };

            return list;
        }
    }
}
