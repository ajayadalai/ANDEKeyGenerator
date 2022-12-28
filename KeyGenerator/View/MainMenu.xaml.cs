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
using Domain;
using TestApplication.View.Common;
using ViewModel;

namespace TestApplication.View
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {

        KeyGenerationViewModel vm = null;

        public MainMenu()
        {
            InitializeComponent();
           // vm = ViewModelMain.Instance.CurrentView as KeyGenerationViewModel;           
        }

      

        private void AboutClick(object sender, RoutedEventArgs e)
        {
           // var aboutWindow = About.Display();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.HorizontalOffset = -140;
            (sender as Button).ContextMenu.VerticalOffset = -5;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        //private void btnCencel_Click(object sender, RoutedEventArgs e)
        //{
        //    Application.Current.MainWindow.Close();
        //}
    }
}
