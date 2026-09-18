using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.ViewModels;
using System;

namespace AvaloniaApplication1.Views
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel? ViewModel => DataContext as MainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void bUpdate_Click(object? sender, RoutedEventArgs e)
        {
            if (ViewModel is null)
                return;

            ViewModel.TopDat.Description = "Hello";
          //  ViewModel.TopDat1.Description = "Hello 1";

            ViewModel.TopDat.SetValuesFromInput();

            ViewModel.GameGrid.Dats.Add(new DatNode("Hello", "ItWorked", DateTime.Now));
        }
    }
}