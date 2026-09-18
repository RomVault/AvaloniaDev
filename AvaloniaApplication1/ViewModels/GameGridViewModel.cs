using AvaloniaApplication1.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;

namespace AvaloniaApplication1.ViewModels
{
    public partial class GameGridViewModel : ViewModelBase
    {   /// <summary>
        /// Rows shown in the DataGrid.
        /// </summary>
        public ObservableCollection<DatNode> Dats { get; } = new();

        public GameGridViewModel()
        {

            Dats.Add(new DatNode("Arcade", "System", DateTime.Now, "avares://AvaloniaApplication1/Assets/G_Correct.png", Avalonia.Media.Color.FromRgb(64, 200, 64)));
            Dats.Add(new DatNode("Capcom", "Manufacturer", DateTime.Now, "avares://AvaloniaApplication1/Assets/G_Correct.png", Avalonia.Media.Color.FromRgb(200, 32, 64)));
            Dats.Add(new DatNode("Konami", "Manufacturer", DateTime.Now));
            Dats.Add(new DatNode("Console", "System", DateTime.Now));
            Dats.Add(new DatNode("Nintendo", "Manufacturer", DateTime.Now));

        }
    }
}
