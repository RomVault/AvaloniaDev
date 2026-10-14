
using CommunityToolkit.Mvvm.ComponentModel;
using RomVaultCore.RvDB;
using RVUtils; 

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


        public void Set(RvFile tDir)
        {
            Name = tDir.Name;



            RvDat tDat = null;
            if (tDir.Dat != null)
                tDat = tDir.Dat;
            else if (tDir.DirDatCount == 1)
                tDat = tDir.DirDat(0);

            if (tDat != null)
            {
                if (Name != tDat.GetData(RvDat.DatData.DatName))
                    Name += $":  {tDat.GetData(RvDat.DatData.DatName)}";

                string DatId = tDat.GetData(RvDat.DatData.Id);
                if (!string.IsNullOrWhiteSpace(DatId))
                    Name += $" (ID:{DatId})";


                Description = tDat.GetData(RvDat.DatData.Description);
                Category = tDat.GetData(RvDat.DatData.Category);
                Version = tDat.GetData(RvDat.DatData.Version);
                Author = tDat.GetData(RvDat.DatData.Author);
                Date = tDat.GetData(RvDat.DatData.Date);
                string header = tDat.GetData(RvDat.DatData.Header);
                if (!string.IsNullOrWhiteSpace(header))
                    Name += " (" + header + ")";

            }
            else
            {
                Description = "";
                Category = "";
                Version = "";
                Author = "";
                Date = "";

            }
            
            ROMPath = tDir.FullName;

            ROMsGot = tDir.DirStatus.CountCorrect().ToRvString();
            if (tDir.DirStatus.CountFoundMIA() > 0) { ROMsGot += $"  -  {tDir.DirStatus.CountFoundMIA().ToRvString()} Found MIA"; }
            ROMsMissing = tDir.DirStatus.CountMissing().ToRvString();
            if (tDir.DirStatus.CountMIA() > 0) { ROMsMissing += $"  -  {tDir.DirStatus.CountMIA().ToRvString()} MIA"; }
            ROMsFixable = tDir.DirStatus.CountFixesNeeded().ToRvString();
            ROMsTotal = (tDir.DirStatus.CountUnknown() + tDir.DirStatus.CountInToSort()).ToRvString();
            
        }


    }
}
