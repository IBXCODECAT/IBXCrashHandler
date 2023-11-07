using System.Diagnostics;
using System.Timers;
using IniParser;
using IniParser.Model;

namespace IBX_CrashHandler
{
    internal class CrashHandlerService
    {
        private const string iniPath = "info.ini";
        private const string logPath = "crash_handler.ibxl";

        private static string targetProcessPath = "Path:to/exe/filename.exe";
        private static string targetProcessFilename = "filename.exe";
        private static string targetProcessName = "filename";

        private readonly System.Timers.Timer _timer;

        private static Process[] targetProcess;

        public CrashHandlerService()
        {
            File.Delete(logPath);

            _timer = new System.Timers.Timer(1000) { AutoReset = true };
            _timer.Elapsed += OnElapse;

            if (File.Exists(iniPath))
            {
                IniData ini = new FileIniDataParser().ReadFile(iniPath);

                foreach(SectionData section in ini.Sections)
                {
                    File.AppendAllText(logPath, $"{DateTime.Now} - Found section \"{section.SectionName}\"\n");

                    if (section.SectionName.Equals("Paths"))
                    {
                        targetProcessPath = section.Keys["exe_path"];

                        File.AppendAllText(logPath, $"{DateTime.Now} - Found target process path {targetProcessPath} in section \"{section.SectionName}\"\n");
                    }
                }
            }
            else
            {
                File.AppendAllText(logPath, $"{DateTime.Now} - Initialization file {iniPath} not found\n");
                ForceStop();
            }

            if(!File.Exists(targetProcessPath))
            {
                File.AppendAllText(logPath, $"{DateTime.Now} - The executable '{targetProcessPath}' does not exist.");
                ForceStop();
            }

            targetProcessFilename = Path.GetFileName(targetProcessPath);
            targetProcessName = targetProcessPath.Substring(0, targetProcessFilename.LastIndexOf("."));

            targetProcess = Process.GetProcessesByName(targetProcessName);
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void ForceStop()
        {
            File.AppendAllText(logPath, $"{DateTime.Now} - Service stopping...\n");
            Environment.Exit(0);
        }

        private void OnElapse(object? sender, ElapsedEventArgs e)
        {
            if(targetProcess.Length > 0)
            {

            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
