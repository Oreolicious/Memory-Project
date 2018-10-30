﻿using System;
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
    public partial class Kinderspeelveld : Page
    {
        private const int NR_OF_COLS = 5;
        private const int NR_OF_ROWS = 2;
        public string Naam1 { get; set; }
        public string Naam2 { get; set; }
        MemoryGridKinderen grid;
        public Kinderspeelveld()
        {
            Invoer_scherm invoer = new Invoer_scherm();
            InitializeComponent();
            grid = new MemoryGridKinderen(GameGrid, NR_OF_COLS, NR_OF_ROWS);
        }
        private void Terugbutton_MouseDown(object sender, MouseEventArgs e)
        {
            this.NavigationService.Navigate(new Homepage());
        }
    }
}