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
    /// Interaction logic for Win_scherm.xaml
    /// </summary>
    public partial class Win_scherm : Page
    {
        public string winnaar { get; set; } //variabele met de naam van de winnaar

        public Win_scherm()
        {
            InitializeComponent();
            Loaded += Winscherm_Loaded;
        }

        private void Winscherm_Loaded(object sender, RoutedEventArgs e)
        {
            Winnaar.Text = winnaar; //laat de naam van de winnaar zien (draw in het geval van een gelijkspel)
        }

        private void NieuwSpelButtonGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Invoer_scherm());
        }

        private void HighscoreButtonGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Highscore_scherm());
        }

        private void HoofdmenuButtonGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Homepage());
        }
    }
}
