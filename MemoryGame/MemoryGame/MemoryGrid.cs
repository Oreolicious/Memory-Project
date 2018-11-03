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
using System.Media;
using System.IO;

namespace MemoryGame
{
    public class MemoryGrid
    {
        private Grid grid;
        private int rows, cols;
        private ImageSource[] images = new ImageSource[16]; //array images
        private int previouscard;
        private int cardsselected;
        private int speler;
        private int[] score = new int[2];
        private string[] namen = new string[2];
        private bool win = false;
        private int GeradenKaartjes;
        private int Streak;
        internal Action OnUpdate;

        /// <summary>
        /// Roept de MemoryGrid class aan en zorgt ervoor dat het speelveld gaat werken
        /// </summary>
        /// <param name="grid">De naam van de grid waar het speelveld in word aangemaakt</param>
        /// <param name="cols">Het aantal columns in de grid</param>
        /// <param name="rows">Het aantal rows in de grid</param>
        public MemoryGrid(Grid grid, int cols, int rows, string naam_1, string naam_2)
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
            win = false;
            speler = 1;
            score[0] = 0;
            score[1] = 0;
            namen[0] = naam_1;
            namen[1] = naam_2;
            Console.WriteLine("Name: " + namen[0] + namen[1]);
            GeradenKaartjes = 0;
            Streak = 0;
        }
        /// <summary>
        /// Maakt de grid rows en columns aan
        /// </summary>
        /// <param name="cols">Het aantal columns in het speelveld</param>
        /// <param name="rows">Het aantal rows in het speelveld</param>
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
        /// <summary>
        /// Deze methode positioneerd alle kaarten op het speelveld
        /// </summary>
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
                backgroundImage.MouseDown += new MouseButtonEventHandler(CardClickAsync);  //wanneer er word geklikt gaat hij over naar de methode CardClick
            }
        }
        /// <summary>
        /// Deze methode word aangeroepen wanneer er op een kaart word geklikt en handeld een cardclick af
        /// </summary>
        private async void CardClickAsync(object sender, MouseButtonEventArgs e)
        {
            Image card = (Image)sender;
            if (cardsselected > 1) //zorgt dat de methode stopt als meer dan 2 kaarten worden geklikt
            {
                return;
            }

            if (card.Source.ToString().Contains("Card.png"))  //laat alleen een kaartselectie toe wanneer de kaart nog niet is omgedraait
            {
                cardsselected++;
            }
            else
            {
                return;
            }

            ImageSource front = images[(int)card.Tag]; //pakt de tag voor de imagesource en stopt het in een variabele
            card.Source = front;  //verandert de source van de geklikte kaart naar het toegewezen plaatje 

            if (cardsselected == 2) //Springt de loop in zodra 2 kaartjes zijn geselecteerd
            {
                bool geraden = false;
                if (previouscard != (int)card.Tag)
                {
                    if (card.Source.ToString() == images[previouscard].ToString()) //Kijkt of de 2 geselecteerde kaarten gelijk zijn aan eklaar
                    {
                        score[speler - 1] += 50 * (Streak + 1)+100; //kent score aan de speler toe die het paar juist raad
                        GeradenKaartjes += 1;
                        geraden = true; //maakt het variabele geraden true zodat de speler die het geraden heeft nog een keer mag
                        Streak++;
                    }
                    else
                    {
                        await Task.Delay(1000); //delay zodat de tweede kaart niet gelijk van het scherm verdwijnt
                        ImageSource back = new BitmapImage(new Uri("assets/Card.png", UriKind.Relative)); //pakt de source van de achterkant van de kaart
                        card.Source = back; //draait huidige kaart terug

                        Image previousImage = (Image)grid.Children[previouscard]; //draait de vorige kaart terug
                        previousImage.Source = back;
                        Streak = 0;
                    }
                }
                cardsselected = 0;
                if (geraden == false) //zorgt ervoor dat de speler gewisseld wordt
                {
                    speler = speler == 1 ? 2 : 1;
                }

                IsDone();

                if (win)
                {
                    var file = new Uri("../../highscore.txt", UriKind.Relative);
                    List<string> lines = File.ReadAllLines(file.ToString()).ToList();

                    for (int j = 0; j < 2; j++)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            string[] entry = lines[i].Split(',');

                            if (score[j] > Int32.Parse(entry[1]))
                            {
                                lines.Insert(i, namen[j] + "," + score[j].ToString());
                                break;
                            }
                        }
                    }
                    File.WriteAllLines(file.ToString(), lines.Take(3));
                }

                OnUpdate(); //Update de currentplayer en score van beide spelers

            }
            previouscard = (int)card.Tag;
        }
        /// <summary>
        /// Methode om de huidige speler te achterhalen
        /// </summary>
        /// <returns>Huidige speler nummer 1 of 2</returns>
        public int GetCurPlayer()
        {
            return this.speler;
        }
        /// <summary>
        /// Methode om de score van een speler op te vragen
        /// </summary>
        /// <param name="player">int die 0 is voor speler 1 en 1 is voor speler 2</param>
        /// <returns>score van opgevraagde speler</returns>
        public int GetPlayerScore(int player)
        {
            return this.score[player];
        }
        /// <summary>
        /// Methode om te kunnen zien of het spel is afgelopen
        /// </summary>
        /// <returns>true or false</returns>
        public bool IsDone()
        {
            if (GeradenKaartjes == 8)
            {
                win = true;
            }
            return this.win;
        }
    }
}