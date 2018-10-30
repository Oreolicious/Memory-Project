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
    /// Interaction logic for Homepage.xaml
    /// </summary>
    public partial class Homepage : Page
    {
        public Homepage()
        {
            InitializeComponent();
        }
        private void Volwassenbutton_MouseDown(object sender, MouseEventArgs e)
        {
            this.NavigationService.Navigate(new Invoer_scherm());
        }
        private void Kinderbutton_MouseDown(object sender, MouseEventArgs e)
        {
            this.NavigationService.Navigate(new Kinderspeelveld());
        }

        private void Highscorebutton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Highscore_scherm());
        }
    }
}
