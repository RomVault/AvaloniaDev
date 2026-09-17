using AvaloniaApplication1.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaApplication1.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public DatInfoViewModel TopDat { get; } = new();
        public DatInfoViewModel TopDat1 { get; } = new();

        [ObservableProperty]
        public partial string Greeting { get; set; } = "Tree View";

        [ObservableProperty]
        public partial string Greeting1 { get; set; } = "Rom View";


        public FilterViewModel Filter { get; } = new();

        public GameGridViewModel GameGrid { get; } = new();

        /// <summary>
        /// Root nodes of the check-box tree.
        /// </summary>
        public ObservableCollection<TreeNodeViewModel> Tree { get; } = new();

        public MainWindowViewModel()
        {
            const string icon = "avares://AvaloniaApplication1/Assets/G_Correct.png";

            var arcade = new TreeNodeViewModel("RomRoot", false, icon);
            arcade.Children.Add(new TreeNodeViewModel("Capcom", true, icon));
            arcade.Children.Add(new TreeNodeViewModel("Konami", false, icon));

            var console = new TreeNodeViewModel("ToSort", false, icon);
            console.Children.Add(new TreeNodeViewModel("Nintendo", false, icon));
            console.Children.Add(new TreeNodeViewModel("Sega", true, icon));

            Tree.Add(arcade);
            Tree.Add(console);
        }

        /// <summary>
        /// Performs the expensive start-up work while the splash screen is visible.
        /// Runs on a background thread, so do not touch UI objects here.
        /// </summary>
        public async Task LoadAsync(IProgress<string> progress, CancellationToken cancellationToken = default)
        {
            progress.Report("Loading DAT files...");
            await Task.Run(() => Thread.Sleep(100), cancellationToken);

            TopDat.Name = "First DAT";
            TopDat.Description = "Loaded at startup";

            progress.Report("Scanning ROMs...");
            await Task.Run(() => Thread.Sleep(100), cancellationToken);

            TopDat1.Name = "Second DAT";
            TopDat1.Description = "Loaded at startup";

            GameGrid.Dats.Add(new DatNode("Arcade", "System", DateTime.Now, "avares://AvaloniaApplication1/Assets/G_Correct.png", Avalonia.Media.Color.FromRgb(64, 200, 64)));
            GameGrid.Dats.Add(new DatNode("Capcom", "Manufacturer", DateTime.Now, "avares://AvaloniaApplication1/Assets/G_Correct.png", Avalonia.Media.Color.FromRgb(200, 32, 64)));
            GameGrid.Dats.Add(new DatNode("Konami", "Manufacturer", DateTime.Now));
            GameGrid.Dats.Add(new DatNode("Console", "System", DateTime.Now));
            GameGrid.Dats.Add(new DatNode("Nintendo", "Manufacturer", DateTime.Now));

            progress.Report("Ready.");
        }
    }
}
