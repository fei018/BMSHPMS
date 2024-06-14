using Microsoft.Extensions.DependencyInjection;
using System;
using WalkingTec.Mvvm.Core;

namespace AdminPasswd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("password:");
            string? pass = Console.ReadLine();

            var services = new ServiceCollection();
            services.AddWtmContextForConsole();
            var Provider = services.BuildServiceProvider();

            var wtm = Provider.GetRequiredService<WTMContext>();
            
            var user = wtm.DC.Set<FrameworkUser>().Where(x=>x.ITCode == "001").Single();
            user.Password = Utils.GetMD5String(pass);
            wtm.DC.UpdateProperty(user, x => x.Password);
            wtm.DC.SaveChanges();

            Console.WriteLine("set password done.");
        }
    }
}
