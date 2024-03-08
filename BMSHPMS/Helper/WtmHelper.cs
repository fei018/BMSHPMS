using Microsoft.Extensions.DependencyInjection;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS.Helper
{
    public class WtmHelper
    {
        public WTMContext GetWtmContext()
        {
            var services = new ServiceCollection();
            ServiceProvider Provider = services.BuildServiceProvider();
            var wtm = Provider.GetRequiredService<WTMContext>();
            return wtm;
        }
    }
}
