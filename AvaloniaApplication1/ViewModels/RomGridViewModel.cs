using AvaloniaApplication1.Models;
using System;
using System.Collections.ObjectModel;

namespace AvaloniaApplication1.ViewModels
{
    public partial class RomGridViewModel : ViewModelBase
    {
        /// <summary>
        /// Rows shown in the DataGrid.
        /// </summary>
        public ObservableCollection<RomNode> Roms { get; } = new();

        public RomGridViewModel()
        {
            Roms.Add(new RomNode("110dance.bin", "avares://AvaloniaApplication1/Assets/G_Correct.png")
            {
                rSize = 1048576,
                rCRC32 = "1a2b3c4d",
                rSHA1 = "da39a3ee5e6b4b0d3255bfef95601890afd80709",
                rStatus = "Correct",
                rFileModDate = DateTime.Now,
                rZipIndex = 0,
                rInstanceCount = 1
            });

            Roms.Add(new RomNode("110dance.nvram")
            {
                rSize = 8192,
                rCRC32 = "5e6f7a8b",
                rStatus = "Missing",
                rZipIndex = 1,
                rInstanceCount = 1
            });
        }
    }
}
