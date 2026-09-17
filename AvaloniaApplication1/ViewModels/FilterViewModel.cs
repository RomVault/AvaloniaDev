
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaApplication1.ViewModels
{
    public partial class FilterViewModel : ViewModelBase
     {
        [ObservableProperty]
        public partial bool bShowComplete { get; set; } = true;
        [ObservableProperty]
        public partial bool bShowPartial { get; set; } = true;

        [ObservableProperty]
        public partial bool bShowEmpty { get; set; } = true;
    }
}
