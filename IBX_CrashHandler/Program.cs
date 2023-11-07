using Topshelf;

namespace IBX_CrashHandler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // HostFactory.Run() will run the entire time the service is running
            TopshelfExitCode exitCode = HostFactory.Run(hostConfigurator => 
            {
                hostConfigurator.Service<CrashHandlerService>(s =>
                {
                    s.ConstructUsing(heartbeat => new CrashHandlerService());
                    s.WhenStarted(heartbeat => heartbeat.Start());
                    s.WhenStopped(heartbeat => heartbeat.Stop());
                });

                hostConfigurator.RunAsLocalSystem();

                hostConfigurator.SetInstanceName("IBXService");
                hostConfigurator.SetDisplayName("IBX Crash Handler");
                hostConfigurator.SetDescription("This service is designed to monitor games developed by IBX (Nathan Schmitt) and watch for game crashes. If a crash is detected this service will handle it gracefully.");
            });

            // Capture exit code
            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}