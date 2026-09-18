
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaApplication1.ViewModels
{
    public partial class GameInfoViewModel : ViewModelBase
    {
        [ObservableProperty]
        public partial string Name { get; set; } = "Name";

        [ObservableProperty]
        public partial string Description { get; set; } = "Description";

        [ObservableProperty]
        public partial string Manufacturer { get; set; } = "<unknown>";

        [ObservableProperty]
        public partial string CloneOf { get; set; } = "";

        [ObservableProperty]
        public partial string RomOf { get; set; } = "";

        [ObservableProperty]
        public partial string Category { get; set; } = "";

        [ObservableProperty]
        public partial string SerialGameId { get; set; } = "";

        [ObservableProperty]
        public partial string Year { get; set; } = "";

        [ObservableProperty]
        public partial string Version { get; set; } = "";
    }
}
