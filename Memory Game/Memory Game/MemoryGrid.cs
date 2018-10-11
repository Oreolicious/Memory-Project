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

        public MemoryGrid(Grid grid, int cols, int rows)

        {
            this.grid = grid;
            this.cols = cols;
            this.rows = rows;
            InitializeGameGrid(cols, rows); //grid aanroepen
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

        private void AddImages ()
        {
            List<ImageSource> images = GetImagesList();
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < cols; column++)
                {
                    Image backgroundImage = new Image();
                    backgroundImage.Source = new BitmapImage(new Uri("Plaatjes/1.png", UriKind.Relative));
                    backgroundImage.Tag = images.First();
                    images.RemoveAt(0);

                    backgroundImage.MouseDown += new MouseButtonEventHandler(CardClick); //deze code zorgt ervoor dat je op de kaart kunt klikken               
                    Grid.SetColumn(backgroundImage, column);
                    Grid.SetRow(backgroundImage, row);
                    grid.Children.Add(backgroundImage);

                }

            }
        }

        private void CardClick(object sender, MouseButtonEventArgs e)
        {
            Image card = (Image)sender;
            ImageSource front = (ImageSource)card.Tag;
            card.Source = front;
        }

        private List<ImageSource> GetImagesList()
        {
            List<ImageSource> images = new List<ImageSource>();
            for (int i = 0; i < 16; i++)
            {
                int imageNr = i % 8 + 1;
                ImageSource source = new BitmapImage(new Uri("plaatjes/" + imageNr + ".png", UriKind.Relative)); //haalt alle plaatjes uit de map
                images.Add(source);
            }
            //TODO randomize volgorde
            return images;
        }

        //opmerking betreft kaartjes: ik heb twee kaartjes uit kinderversie gepakt om te testen, gezien ik deze op mijn computer had. 
    }
}
