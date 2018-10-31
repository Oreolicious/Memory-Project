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

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for Speelveld.xaml
    /// </summary>
    
    public partial class Speelveld : Page
    {
        private const int NR_OF_COLS = 8;
        private const int NR_OF_ROWS = 2;
        public string Naam1 { get; set; }
        public string Naam2 { get; set; }
        MemoryGrid grid;
        public Speelveld()
        {
            Invoer_scherm invoer = new Invoer_scherm();
            InitializeComponent();
            grid = new MemoryGrid(GameGrid, NR_OF_COLS, NR_OF_ROWS);
            grid.OnUpdate += OnUpdate;
            OnUpdate();
            Loaded += MyWindow_Loaded;
        }
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            NameInit();
        }

        public void OnUpdate()
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
        }
        public void NameInit()
        {
            Speler1naam.Text = Naam1 + ":";
            Speler2naam.Text = Naam2 + ":";
        }

        private void Pauzebutton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Pauze_menu());
        }

        private void Terugbutton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Homepage());
        }
    }
}
