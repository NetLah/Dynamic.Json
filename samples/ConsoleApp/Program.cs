using System;
using NetLah.Diagnostics;
using Testing.Dynamic;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var appInfo = ApplicationInfo.Initialize(null);
            Console.WriteLine($"AppTitle: {appInfo.Title}");
            Console.WriteLine($"Version:{appInfo.InformationalVersion} BuildTime:{appInfo.BuildTimestampLocal}; Framework:{appInfo.FrameworkName}");

            var asmAbstracts = new AssemblyInfo(typeof(AssemblyInfo).Assembly);
            Console.WriteLine($"AssemblyTitle: {asmAbstracts.Title}");
            Console.WriteLine($"Version:{asmAbstracts.InformationalVersion} BuildTime:{asmAbstracts.BuildTimestampLocal}; Framework:{asmAbstracts.FrameworkName}");

            var asmConfig = new AssemblyInfo(typeof(DJson).Assembly);
            Console.WriteLine($"AssemblyTitle: {asmConfig.Title}");
            Console.WriteLine($"Version:{asmConfig.InformationalVersion} BuildTime:{asmConfig.BuildTimestampLocal}; Framework:{asmConfig.FrameworkName}");
        }
    }
}
