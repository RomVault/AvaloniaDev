using AvaloniaApplication1.Models;
using System;
using System.Collections.ObjectModel;

namespace AvaloniaApplication1.ViewModels
{
    public partial class GameGridViewModel : ViewModelBase
    {   /// <summary>
        /// Rows shown in the DataGrid.
        /// </summary>
        public ObservableCollection<GameNode> Games { get; } = new();

        public GameGridViewModel()
        {
            Games.Add(new GameNode("Arcade", "System", DateTime.Now, "avares://AvaloniaApplication1/Assets/G_Correct.png", Avalonia.Media.Color.FromRgb(64, 200, 64)));
            Games.Add(new GameNode("Capcom", "Manufacturer", DateTime.Now, "avares://AvaloniaApplication1/Assets/G_Correct.png", Avalonia.Media.Color.FromRgb(200, 32, 64)));
            Games.Add(new GameNode("Konami", "Manufacturer", DateTime.Now));
            Games.Add(new GameNode("Console", "System", DateTime.Now));
        }
    }
}
