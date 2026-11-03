
using CommunityToolkit.Mvvm.ComponentModel;
using RomVaultCore.RvDB;
using RVIO;

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


        public void Set(RvFile tGame)
        {
            Name = tGame.Name;
            if (tGame.Game==null)
            {
                Description = "";
                Manufacturer = "";
                return;
            }
            
            string desc = tGame.Game.GetData(RvGame.GameData.Description);
            if (desc == "¤") desc = Path.GetFileNameWithoutExtension(tGame.Name);
            Description = desc;

            Manufacturer = tGame.Game.GetData(RvGame.GameData.Manufacturer);
            CloneOf = tGame.Game.GetData(RvGame.GameData.CloneOf);
            RomOf = tGame.Game.GetData(RvGame.GameData.RomOf);
            Year = tGame.Game.GetData(RvGame.GameData.Year);

        }
    }
}
