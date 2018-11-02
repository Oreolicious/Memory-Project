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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Invoer_scherm : Page
    {
        public Invoer_scherm()
        {
            InitializeComponent();
        }

        private void TerugButtonGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Homepage());
        }

        private void StartButtonGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Speelveld Speel = new Speelveld();
            this.NavigationService.Navigate(Speel);
            if (Speler1naam.Text == "")
            {
                Speel.Naam1 = "Speler 1";
            }
            else
            {
                Speel.Naam1 = Speler1naam.Text;
            }
            if (Speler2naam.Text == "")
            {
                Speel.Naam2 = "Speler 2";
            }
            else
            {
                Speel.Naam2 = Speler2naam.Text;
            }
        }
    }
}
