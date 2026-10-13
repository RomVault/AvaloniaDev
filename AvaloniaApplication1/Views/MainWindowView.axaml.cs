using Avalonia.Controls;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1.Views
{
    public partial class MainWindowView : Window
    {
        private MainWindowViewModel? ViewModel => DataContext as MainWindowViewModel;

        public MainWindowView()
        {
            InitializeComponent();
        }
    }
}