using Topshelf;

namespace IBX_CrashHandler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // HostFactory.Run() will run the entire time the service is running
            TopshelfExitCode exitCode = HostFactory.Run(x => 
            {
                x.Service<Heartbeat>(s =>
                {
                    s.ConstructUsing(heartbeat => new Heartbeat());
                    s.WhenStarted(heartbeat => heartbeat.Start());
                    s.WhenStopped(heartbeat => heartbeat.Stop());
                });

                x.RunAsLocalSystem();

                x.SetInstanceName("IBXService");
                x.SetDisplayName("IBX Crash Handler");
                x.SetDescription("This service is designed to monitor games developed by IBX (Nathan Schmitt) and watch for game crashes. If a crash is detected this service will handle it gracefully.");
            });

            // Capture exit code
            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}