using Avalonia.Media.Imaging;
using AvaloniaApplication1.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AvaloniaApplication1.ViewModels
{
    public partial class TreeNodeViewModel : ViewModelBase
    {
        public TreeNodeViewModel(string name, bool isChecked = false, string? imagePath = null)
        {
            Name = name;
            IsChecked = isChecked;

            if (imagePath is not null)
            {
                Icon = ImageLoader.Load(imagePath);
            }
        }

        [ObservableProperty]
        public partial string Name { get; set; }

        [ObservableProperty]
        public partial bool IsChecked { get; set; }

        /// <summary>
        /// Image shown to the left of the check box.
        /// </summary>
        [ObservableProperty]
        public partial Bitmap? Icon { get; set; }

        public ObservableCollection<TreeNodeViewModel> Children { get; } = new();
    }
}
