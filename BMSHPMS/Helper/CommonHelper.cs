using System.Text.RegularExpressions;

namespace BMSHPMS.Helper
{
    public class CommonHelper
    {
        public const string REG_GUID = "^([A-Fa-f0-9]{8}(-[A-Fa-f0-9]{4}){3}-[A-Fa-f0-9]{12})?$";

        public static bool IsGUID(string guid)
        {
            return guid != null && Regex.IsMatch(guid, REG_GUID);
        }
    }
}
