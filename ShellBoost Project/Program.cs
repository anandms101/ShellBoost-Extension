using ShellBoost.Core;
using ShellBoost.Core.Utilities;
using System;

namespace ShellBoost_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            Console.WriteLine("ShellBoost Structure ");
            Console.WriteLine();
            Console.WriteLine("Press a key:");
            Console.WriteLine();
            Console.WriteLine("   '1' Register the native proxy, run this sample, and unregister on exit.");
            Console.WriteLine("   '2' Register the native proxy.");
            Console.WriteLine("   '3' Run this sample (the native proxy will need to be registered somehow for Explorer to display something).");
            Console.WriteLine("   '4' Unregister the native proxy.");
            Console.WriteLine("   '5' Restart Windows Explorer.");
            Console.WriteLine("   '6' Register Custom Properties for this sample" );
            Console.WriteLine("   '7' Unregister Custom Properties for this sample." + (DiagnosticsInformation.GetTokenElevationType() == TokenElevationType.Full ? string.Empty : " You need to restart as admin."));
            Console.WriteLine();
            Console.WriteLine("   Any other key will exit.");
            Console.WriteLine();

            do
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                    return;

                switch (key.KeyChar)
                {
                    case '1':
                        ShellFolderServer.RegisterNativeDll(RegistrationMode.User);
                        Console.WriteLine("Registered");
                        Run();
                        ShellFolderServer.UnregisterNativeDll(RegistrationMode.User);
                        Console.WriteLine("Unregistered");
                        return;

                    case '2':
                        ShellFolderServer.RegisterNativeDll(RegistrationMode.User);
                        Console.WriteLine("Registered");
                        break;

                    case '3':
                        Run();
                        return;

                    case '4':
                        ShellFolderServer.UnregisterNativeDll(RegistrationMode.User);
                        Console.WriteLine("Unregistered");
                        break;

                    case '5':
                        var rm = new RestartManager();
                        rm.RestartExplorerProcesses((state) =>
                        {
                            Console.WriteLine("Explorer was stopped. Press any key to restart it ...");
                            Console.ReadKey(true);
                        }, false, out Exception error);

                        if (error != null)
                        {
                            Console.WriteLine("An error has occurred in restart manager: " + error);
                        }
                        break;
                    case '6':
                        try
                        {
                            CustomPropertyHandler propertyObj = new CustomPropertyHandler("LocalFolder.propdesc");
                            propertyObj.schemaPropertyRegister();
                            Console.WriteLine("Properties are registered.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Properties cannot be registered: " + e.Message);
                        }
                        break;

                    case '7':
                        try
                        {
                            CustomPropertyHandler propertyObj = new CustomPropertyHandler("LocalFolder.propdesc");
                            propertyObj.schemaPropertyUnRegister();
                            Console.WriteLine("Properties are unregistered.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Properties cannot be unregistered: " + e.Message);
                        }
                        break;
                }
            }
            while (true);
        }
        static void Run()
        {
            using (var server = new DisplayItemServer())
            {
                var config = new ShellFolderConfiguration();
                server.Start(config);
                Console.WriteLine("Started. Press ESC to stop.");
                while (Console.ReadKey(true).Key != ConsoleKey.Escape)
                {
                }
                Console.WriteLine("Stopped");
            }
        }
    }
}
