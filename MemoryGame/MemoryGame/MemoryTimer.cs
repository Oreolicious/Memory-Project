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
            this.Tijd = Tijd; //tijd in seconden
            SetTimer();
            SpelTimer = Tijd;
        }

        /// <summary>
        /// Maakt een nieuwe timer aan die na elke seconde opnieuw begint
        /// </summary>
        private void SetTimer()
        {
            aTimer = new System.Timers.Timer(1000); //een timer van 1 seconde
            aTimer.Elapsed += OnTimedEvent; //elke keer als de timer afloopt wordt deze methode aangeroepen
            aTimer.AutoReset = true; //zorgt ervoor dat elke seconde de timer opnieuw begint
            aTimer.Enabled = true; //zorgt ervoor dat de OnTimedEvent mag worden aangeroepen
        }

        /// <summary>
        /// Een methode die elke seconde word aangeroepen om de timer van het speelveld aan te passen
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            SpeelveldTijd = "";
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}", e.SignalTime);
            if((SpelTimer / 60 ) < 10) //int deling voor het verkrijgen van minuten uit seconden
            {
                SpeelveldTijd += "0" + (SpelTimer / 60).ToString();
            }
            else
            {
                SpeelveldTijd += (SpelTimer / 60).ToString();
            }
            SpeelveldTijd += ":";
            if ((SpelTimer % 60) < 10) //Pakt de losse secondes die niet bij de minuten horen
            {
                SpeelveldTijd += "0" + (SpelTimer % 60).ToString();
            }
            else
            {
                SpeelveldTijd += (SpelTimer % 60).ToString();
            }
            OnTimerUpdate(); //update de timer in speelveld
            Console.WriteLine(SpeelveldTijd);
            SpelTimer -= 1;
        }

        /// <summary>
        /// Stopt de timer
        /// </summary>
        public void StopTimer()
        {
            aTimer.Dispose(); 
        }
    }
}
