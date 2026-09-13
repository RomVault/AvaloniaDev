
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaApplication1.ViewModels
{
    public partial class DatInfoViewModel : ViewModelBase
    {
        [ObservableProperty]
        public partial string Name { get; set; } = "Name";

        [ObservableProperty]
        public partial string Description { get; set; } = "Description";

        [ObservableProperty]
        public partial string Category { get; set; } = "Category";

        [ObservableProperty]
        public partial string Version { get; set; } = "Version";

        [ObservableProperty]
        public partial string Author { get; set; } = "Author";

        [ObservableProperty]
        public partial string Date { get; set; } = "Date";

        [ObservableProperty]
        public partial string ROMPath { get; set; } = "ROM Path";

        [ObservableProperty]
        public partial string ROMsGot { get; set; } = "0";

        [ObservableProperty]
        public partial string ROMsFixable { get; set; } = "0";

        [ObservableProperty]
        public partial string ROMsMissing { get; set; } = "0";

        [ObservableProperty]
        public partial string ROMsTotal { get; set; } = "0";


        public void SetValuesFromInput()
        {
            Name = "Hello 1";
            Description = "From RV";
            Category = "Category 1";
        }
    }
}
