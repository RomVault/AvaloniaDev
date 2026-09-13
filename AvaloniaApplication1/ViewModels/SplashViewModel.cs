using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaApplication1.ViewModels
{
    public partial class SplashViewModel : ViewModelBase
    {
        [ObservableProperty]
        public partial string Title { get; set; } = "AvaloniaApplication1";

        [ObservableProperty]
        public partial string Status { get; set; } = "Starting up...";
    }
}
