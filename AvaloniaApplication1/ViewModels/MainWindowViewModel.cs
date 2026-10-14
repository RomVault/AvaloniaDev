using AvaloniaApplication1.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using RomVaultCore;
using RomVaultCore.RvDB;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;


namespace AvaloniaApplication1.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public TopMenuViewModel TopMenu { get; } = new();
        public LeftButtonsViewModel LeftButtons { get; } = new();

        public DatInfoViewModel TopDat { get; } = new();
        public GameInfoViewModel TopGame { get; } = new();
        public FilterViewModel Filter { get; } = new();

        public RVTreeViewModel Tree { get; } = new();

        public GameGridViewModel GameGrid { get; } = new();
        public RomGridViewModel RomGrid { get; } = new();


        /// <summary>
        /// Root nodes of the check-box tree.
        /// </summary>


        public MainWindowViewModel()
        {

        }
        private ThreadWorker _thWrk;
        /// <summary>
        /// Performs the expensive start-up work while the splash screen is visible.
        /// Runs on a background thread, so do not touch UI objects here.
        /// </summary>
        public async Task LoadAsync(IProgress<string> progress, CancellationToken cancellationToken = default)
        {
            progress.Report("Loading Settings...");
            await Task.Run(() =>
            {
                Settings.checkdirs();
                Settings.rvSettings = new Settings();
                Settings.rvSettings = Settings.SetDefaults(out string errorReadingSettings);
            }, cancellationToken);

            progress.Report("Loading Cache...");



            await Task.Run(() =>
            {
                _thWrk = new ThreadWorker(StartUpCode) { wReport = BgwProgressChanged, wFinal = BgwRunWorkerCompleted };
                StartUpCode(_thWrk);
            }, cancellationToken);

            progress.Report("Ready.");

            Tree.Setup(ref DB.DirRoot);


            Tree.RowSelected += OnTreeRowSelected;
        }

        private void OnTreeRowSelected(object? sender, RVTreeViewModel e)
        {
            TopDat.Set(e.dTree);
        }

        private static void StartUpCode(ThreadWorker thWrk)
        {
            RepairStatus.InitStatusCheck();
            DB.Read(thWrk);
        }

        private void BgwProgressChanged(object e)
        {
        }

        private void BgwRunWorkerCompleted()
        {
        }
    }
}
