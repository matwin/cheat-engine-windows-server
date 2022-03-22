using System;
using System.Security.Principal;
using CEServerWindows;

namespace CEServerApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            CheatEngineServer server = new CEServerWindows.CheatEngineServer();

            Console.WriteLine("Server is listing on port {0}", server.Port);
            if (!HasAdminRights())
            {
                Console.WriteLine("Please run this program with admin rights to allow memory access to other programs.");
            }

            server.StartAsync().Wait();
        }

        private static bool HasAdminRights()
        {
            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return isElevated;
        }
    }
}