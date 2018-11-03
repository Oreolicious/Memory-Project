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
            Naam1 = naam_1;
            Naam2 = naam_2;
            InitializeComponent();
            grid = new MemoryGrid(GameGrid, NR_OF_COLS, NR_OF_ROWS, Naam1, Naam2);
            grid.OnUpdate += OnUpdate;
            PauseMenu.OnUnpause += OnUnpause;
            timer = new MemoryTimer(180);
            timer.OnTimerUpdate += OnTimerUpdate;
            OnUpdate();
            Loaded += MyWindow_Loaded;
        }
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            NameInit();
        }

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
            Speler1score.Text = grid.GetPlayerScore(0).ToString();
            Speler2score.Text = grid.GetPlayerScore(1).ToString(); ;
            if (grid.IsDone())
            {
                await Task.Delay(500);
                timer.StopTimer();
                this.NavigationService.Navigate(new Win_scherm());
            }
        }
        public void OnTimerUpdate()
        {
            Console.WriteLine("OnTimerUpdate() invoked");
            this.Dispatcher.Invoke(() => //uitzoeken
            {
                this.Tijd.Text = timer.SpeelveldTijd;
                //if statement plaatsen timer.speelveldtijd == 00:00 dan terug na winscherm // zonder delay?
            });
        }
        public void NameInit()
        {
            Speler1naam.Text = Naam1 + ":";
            Speler2naam.Text = Naam2 + ":";
        }

        private void Pauzebutton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(PauseMenu);
            this.Dispatcher.Invoke(() => 
            {
                PauseTijd = timer.SpelTimer;
            });
            timer.StopTimer();
        }

        private void Terugbutton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Homepage());
            timer.StopTimer();
        }

        private void OnUnpause()
        {
            timer = new MemoryTimer(PauseTijd);
            timer.OnTimerUpdate += OnTimerUpdate;
        }
    }
}
