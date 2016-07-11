using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Iris.Utils
{
    public class Timer
    {
        public bool Enabled { get; set; } = false;
        public int Interval { get; set; }

        public delegate void TimerTickEventHandler();

        public event TimerTickEventHandler TimerTick;

        protected virtual void OnTimerTick()
        {
            TimerTick?.Invoke();
        }

        public async Task Run()
        {
            var watcher = Stopwatch.StartNew();

            while (Enabled)
            {
                if (watcher.Elapsed.Ticks%Interval != 0) continue;
                await Task.Yield();
                OnTimerTick();
            }
        }
    }
}
