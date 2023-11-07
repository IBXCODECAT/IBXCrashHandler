using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace IBX_CrashHandler
{
    internal class Heartbeat
    {
        private readonly System.Timers.Timer _timer;

        public Heartbeat()
        {
            _timer = new System.Timers.Timer(1000) { AutoReset = true };
            _timer.Elapsed += OnElapse;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void OnElapse(object? sender, ElapsedEventArgs e)
        {
            string[] lines = new string[] { DateTime.Now.ToString() };

            File.AppendAllLines("heartbeat.txt", lines);
        }
    }
}
