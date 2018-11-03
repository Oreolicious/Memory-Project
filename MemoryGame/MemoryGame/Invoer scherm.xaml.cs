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

        /// <summary>
        /// Methode die het onmogelijk maakt om niet alphabetische characters in te vullen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Spelernaam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.Z) //Vraagt of het alphabetisch is
            {
            }
            else
            {
                e.Handled = true; //Andere characters worden onderschept en worden niet toegelaten
            }
        }

        /// <summary>
        /// Methode om de variabelen Naam1 en Naam2 van invoerscherm mee te nemen naar het speelveld
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButtonGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string naam_1, naam_2;

            if (Speler1naam.Text == "")
            {
                naam_1 = "Speler 1"; //maakt de naam speler 1 als de invoer niks is
            }
            else
            {
                naam_1 = Speler1naam.Text;
            }
            if (Speler2naam.Text == "")
            {
                naam_2 = "Speler 2"; //maakt de naam speler 1 als de invoer niks is
            }
            else
            {
                naam_2 = Speler2naam.Text;
            }

            Speelveld Speel = new Speelveld(naam_1, naam_2);
            this.NavigationService.Navigate(Speel); //Navigeerd naar het speelveld
        }
    }
}
