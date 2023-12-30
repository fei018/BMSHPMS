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

            string val = Utils.GetMD5String(pass);
            Console.WriteLine(val);
        }
    }
}
