using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Helper
{
    public static class WtmContextExtension
    {
        public static DataContext GetDataContext(this WTMContext context)
        {
            return context.DC as DataContext;
        }
    }
}
