using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Memory_Game
{
    public class MemoryGrid
    {
        private Grid grid;
        private int rows, cols;
        private ImageSource[] images = new ImageSource[16]; //array images
        private int previouscard;
        private int cardsselected;


        public MemoryGrid(Grid grid, int cols, int rows)
        {
            this.grid = grid;
            this.cols = cols;
            this.rows = rows;
            this.previouscard = 0;
            this.cardsselected = 0;

            for (int i = 0; i < 16; i++)  //loop om alle kaartjes aan te roepen (werkt alleen nog maar bij 16 kaarten) 
            {
                int imageNr = i % 8 + 1;
                ImageSource source = new BitmapImage(new Uri("assets/" + imageNr + ".png", UriKind.Relative));
                images[i] = source;
            }
            InitializeGameGrid(cols, rows);  //grid aanroepen
            AddImages();

        }

        private void InitializeGameGrid(int cols, int rows) //grid aanmaken en vullen
        {
            for (int i = 0; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < cols; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        private void AddImages()
        {
            List<int> positions = new List<int>();
            Random rnd = new Random();

            for (int column = 0; column < cols; column++)  //loop voor alle mogelijke posities op de grid (max range cols = 1-9 & max range rows = 1-9)
            {
                for (int row = 0; row < rows; row++)
                {
                    positions.Add(Convert.ToInt32(Convert.ToString(row + 1) + Convert.ToString(column + 1)));
                }
            }

            for (int i = 0; i < rows * cols; i++)
            {
                
                    Image backgroundImage = new Image();
                    backgroundImage.Source = new BitmapImage(new Uri("assets/Card.png", UriKind.Relative)); //default kaartje
                    backgroundImage.Tag = i;
                    int randompos = rnd.Next(positions.Count);  //pakt een random item uit de lijst van mogelijke posities
                    int randomposres = positions[randompos];
                    int randomrow = randomposres / 10 - 1;  //pakt het eerste getal voor rows
                    int randomcol = randomposres % 10 - 1;  //pakt het tweede getal voor cols
                    positions.RemoveAt(randompos);  //haalt de positie uit de mogelijke posities omdat hij nu bezet is
                    Grid.SetColumn(backgroundImage, randomcol);
                    Grid.SetRow(backgroundImage, randomrow);
                    grid.Children.Add(backgroundImage);
                    backgroundImage.MouseDown += new MouseButtonEventHandler(CardClick);  //wanneer er word geklikt gaat hij over naar de methode CardClick
                
            }
        }

        private void CardClick(object sender, MouseButtonEventArgs e)
        {
            cardsselected++;
            Image card = (Image)sender;

            ImageSource front = images[(int)card.Tag]; //pakt de tag voor de imagesource en stopt het in een variabele
            card.Source = front;  //verandert de source van de geklikte kaart naar het toegewezen plaatje

            if (cardsselected == 2) 
            {
                if(previouscard != (int)card.Tag)
                {
                    if(card.Source.ToString() == images[previouscard].ToString())
                    {
                        Console.WriteLine("Setje gevonden!");
                    }
                    else
                    {
                        Console.WriteLine(card.Source.ToString()); // voor nu (om te zien wat de tweede kaart is)
                        ImageSource back = new BitmapImage(new Uri("assets/Card.png", UriKind.Relative)); //pakt de source van de achterkant van de kaart

                        card.Source = back; //draait huidige kaart terug

                        Image previousImage = (Image)grid.Children[previouscard]; // draait de vorige kaart terug
                        previousImage.Source = back;
                    }
                }

                cardsselected = 0;
            }
         
            previouscard = (int)card.Tag;
        }

    }
}