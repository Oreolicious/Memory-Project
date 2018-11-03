using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for Speelveld.xaml
    /// </summary>
    
    public partial class Speelveld : Page
    {
        private const int NR_OF_COLS = 8;
        private const int NR_OF_ROWS = 2;
        private string Naam1;
        private string Naam2;
        private int PauseTijd;
        public string TimerTijd { get; set; }
        MemoryGrid grid;
        MemoryTimer timer;
        Pauze_menu PauseMenu = new Pauze_menu();

        public Speelveld(string naam_1, string naam_2)
        {
            Naam1 = naam_1; //Naam 1 van het invoerscherm
            Naam2 = naam_2; //Naam 1 van het invoerscherm
            InitializeComponent();
            grid = new MemoryGrid(GameGrid, NR_OF_COLS, NR_OF_ROWS, Naam1, Naam2); //Maakt een nieuwe instantie van de class MemoryGrid aan
            grid.OnUpdate += OnUpdate; //Maakt de methode onupdate beschikbaar voor de class MemoryGrid
            PauseMenu.OnUnpause += OnUnpause; //Maakt de methode OnUnpause beschikbaar voor de class Pauze_menu
            timer = new MemoryTimer(180); //Maakt een nieuwe timer aan
            timer.OnTimerUpdate += OnTimerUpdate; //Maakt de OnTimerUpdate beschikbaar voor de class MemoryTimer
            OnUpdate(); //Update de eerste speler scores en beurten
            Loaded += MyWindow_Loaded; //Zodra de pagina laadt word de functie MyWindow_Loaded geroepen
        }

        /// <summary>
        /// Zorgt dat beide namen van de spelers zichtbaar worden wanneer de pagina geladen is
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            NameInit(); //Zorgt dat beide namen van de spelers zichtbaar worden
        }

        /// <summary>
        /// Functie die elke Update word geroepen en die de score en beurt van de spelers update (een update voor deze functie is wanneer 2 kaarten geselecteerd zijn)
        /// </summary>
        public async void OnUpdate()
        {
            if (grid.GetCurPlayer() == 1) 
            {
                Spelerbeurt2.Visibility = Visibility.Hidden;
                Spelerbeurt1.Visibility = Visibility.Visible;
            }
            else
            {
                Spelerbeurt1.Visibility = Visibility.Hidden;
                Spelerbeurt2.Visibility = Visibility.Visible;
            }
            Speler1score.Text = grid.GetPlayerScore(0).ToString(); //Update de scores
            Speler2score.Text = grid.GetPlayerScore(1).ToString(); //Update de scores
            if (grid.IsDone()) //Kijkt of het spel voorbij is na elke update
            {
                await Task.Delay(500);
                EndGame();
            }
        }

        /// <summary>
        /// Functie die elke TimerUpdate word geroepen en de timer in het speelveld update (een update voor deze functie is elke seconde)
        /// </summary>
        public void OnTimerUpdate()
        {
            Console.WriteLine("OnTimerUpdate() invoked");
            this.Dispatcher.Invoke(() => //Maakt het mogelijk om informatie uit de Timer thread te bewerken en lezen
            {
                this.Tijd.Text = timer.SpeelveldTijd;
                if (timer.SpeelveldTijd == "00:00") //Eindigt het spel als de timer 00:00 is
                {
                    EndGame();
                }
            });
        }

        /// <summary>
        /// Stopt het spel en laat het winscherm zien
        /// </summary>
        public void EndGame()
        {
            timer.StopTimer(); //stopt de timer
            Win_scherm Winscherm = new Win_scherm();
            if (grid.GetPlayerScore(0) > grid.GetPlayerScore(1)) //Vraagt of de winnaar speler 1 is
            {
                Winscherm.winnaar = Naam1 + " : " + grid.GetPlayerScore(0).ToString();
            }
            else if (grid.GetPlayerScore(0) < grid.GetPlayerScore(1)) //Vraagt of de winnaar speler 1 is
            {
                Winscherm.winnaar = Naam2 + " : " + grid.GetPlayerScore(1).ToString();
            }
            else //Als de score gelijk is komt er geen spelernaam maar "Draw"
            {
                Winscherm.winnaar = "Draw : " + grid.GetPlayerScore(0).ToString();
            }
            this.NavigationService.Navigate(Winscherm); //Navigeerd naar het winscherm
        }

        /// <summary>
        /// Update de namen in het speelveld naar de namen in het invoerscherm
        /// </summary>
        public void NameInit()
        {
            Speler1naam.Text = Naam1 + ":";
            Speler2naam.Text = Naam2 + ":";
        }

        /// <summary>
        /// Stopt de timer en zorgt dat de stand van de timer word opgeslagen. Ook word het pauzemenu in zicht gebracht
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pauzebutton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(PauseMenu);
            this.Dispatcher.Invoke(() => 
            {
                PauseTijd = timer.SpelTimer;
            });
            timer.StopTimer(); //stopt de timer
        }

        private void Terugbutton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Homepage());
            timer.StopTimer(); //stopt de timer
        }

        /// <summary>
        /// Functie die word geroepen wanneer de game weer word hervat
        /// </summary>
        private void OnUnpause()
        {
            timer = new MemoryTimer(PauseTijd); //laat de timer weer verder gaan
            timer.OnTimerUpdate += OnTimerUpdate;
        }
    }
}
