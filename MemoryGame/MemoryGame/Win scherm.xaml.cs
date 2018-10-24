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
    public partial class Win_scherm : Page
    {
        public Win_scherm()
        {
            InitializeComponent();
        }

        private void NieuwSpelButtonGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Speelveld());
        }

        private void HighscoreButtonGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //do here something
        }

        private void HoofdmenuButtonGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Homepage());
        }
    }
}
