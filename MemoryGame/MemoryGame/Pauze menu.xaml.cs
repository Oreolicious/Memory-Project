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
    /// Interaction logic for Pauze_menu.xaml
    /// </summary>
    public partial class Pauze_menu : Page
    {
        internal Action OnUnpause;

        public Pauze_menu()
        {
            InitializeComponent();
        }

        private void SpelHervattenGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.GoBack(); //gaat terug naar het speelveld met alle gegevens nog opgeslagen
            OnUnpause(); //Zorgt ervoor dat de timer weer gaat lopen wanneer de game was gepauzeerd
        }

        private void NaarHoofdmenuGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Homepage());
        }
    }
}
