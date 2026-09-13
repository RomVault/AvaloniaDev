using AvaloniaApplication1.Models;
using CommunityToolkit.Mvvm.ComponentModel;
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
            // Image paths can be "avares://" resources (anything under Assets\)
            // or absolute paths on disk.
            //Dats.Add(new DatNode("Arcade", "System", 0,"avares://AvaloniaApplication1/Assets/G_Correct.png"));

            //Dats.Add(new DatNode("Capcom", "Manufacturer", 120,null));

            //Dats.Add(new DatNode("Konami", "Manufacturer", 98,null));
        }
    }
}
