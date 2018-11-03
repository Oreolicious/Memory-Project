using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace MemoryGame
{
    public class MemoryTimer
    {
        private static System.Timers.Timer aTimer;
        private int Tijd;
        public int SpelTimer { get; set; }
        public string SpeelveldTijd { get; set; }
        internal Action OnTimerUpdate;
        public MemoryTimer(int Tijd)
        {
            this.Tijd = Tijd;//tijd is in secondes
            SetTimer();
            SpelTimer = Tijd;
        }

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(1000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            SpeelveldTijd = "";
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}", e.SignalTime);
            if((SpelTimer / 60 ) < 10)
            {
                SpeelveldTijd += "0" + (SpelTimer / 60).ToString();
            }
            else
            {
                SpeelveldTijd += (SpelTimer / 60).ToString();
            }
            SpeelveldTijd += ":";
            if ((SpelTimer % 60) < 10)
            {
                SpeelveldTijd += "0" + (SpelTimer % 60).ToString();
            }
            else
            {
                SpeelveldTijd += (SpelTimer % 60).ToString();
            }
            OnTimerUpdate();
            Console.WriteLine(SpeelveldTijd);
            SpelTimer -= 1;
        }

        public void StopTimer()
        {
            aTimer.Dispose();
        }
    }
}
