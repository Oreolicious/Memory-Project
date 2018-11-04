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
using System.IO;

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for Highscore_scherm.xaml
    /// </summary>
    public partial class Highscore_scherm : Page
    {
        public Highscore_scherm()
        {
            InitializeComponent();

            var file = new Uri("highscore.txt", UriKind.Relative); //Pakt de locatie van de highscore lijst
            string[] lines = File.ReadAllLines(file.ToString()); //Haalt de highscores uit de lijst en stopt die in een C# array

            for (int i = 0; i < 3; i++) //loop om door de highscore lijst te lopen
            {
                string[] entry = lines[i].Split(','); //Split de naam van de score

                switch (i) //Schrijft de lijst in volgorde
                {
                    case 0:
                        score_1.Text = entry[0] + ": " + entry[1];
                        break;

                    case 1:
                        score_2.Text = entry[0] + ": " + entry[1];
                        break;

                    case 2:
                        score_3.Text = entry[0] + ": " + entry[1];
                        break;

                    default:
                        break;
                }
            }
        }

        private void TerugButtonGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Homepage());
        }
    }
}
