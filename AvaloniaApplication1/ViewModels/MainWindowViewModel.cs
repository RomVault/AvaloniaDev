using Avalonia.Controls;
using AvaloniaApplication1.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using RomVaultCore;
using RomVaultCore.ReadDat;
using RomVaultCore.RvDB;
using RomVaultCore.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.DataContracts;
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


        [ObservableProperty]
        public bool working = false;

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
            GameGrid.RowSelected += OnGameGridSelected;

            GameGrid.RowDoubleClicked += OnGameGridDoubleCheck;

            LeftButtons.UpdateDatsClicked += OnUpdateDatsClicked;
            LeftButtons.ScanRomsClicked += OnScanRomsClicked;
            LeftButtons.FindFixesClicked += OnFindFixesClicked;
            LeftButtons.FixFilesClicked += OnFixFilesClicked;
        }

        private void OnUpdateDatsClicked(object? sender, ButtonClickInfo e)
        {
            RootDirsCreate.CheckDatRoot();
            if (e.IsRightClick)
            {
                //ExtHelper.DatVaultRightClick();
                return;
            }
            else if (e.IsShift)
            {
                DatUpdate.InvalidateAllDATs(DB.DirRoot.Child(0), @"DatRoot\");
            }
            Start();
            UpdateDats();
            Finish();
        }

        private void OnScanRomsClicked(object? sender, ButtonClickInfo e)
        {
            // TODO: replace with the real ROM scan.
            System.Diagnostics.Debug.WriteLine($"Scan ROMs requested (right={e.IsRightClick}, shift={e.IsShift}).");
        }

        private void OnFindFixesClicked(object? sender, ButtonClickInfo e)
        {
            // TODO: replace with the real fix search.
            System.Diagnostics.Debug.WriteLine($"Find Fixes requested (right={e.IsRightClick}, shift={e.IsShift}).");
        }

        private void OnFixFilesClicked(object? sender, ButtonClickInfo e)
        {
            // TODO: replace with the real fix operation.
            System.Diagnostics.Debug.WriteLine($"Fix Files requested (right={e.IsRightClick}, shift={e.IsShift}).");
        }

        private void OnGameGridDoubleCheck(object? sender, RowDoubleClickEventArgs e)
        {
            RvFile target = e.IsLeftButton ? e.File : e.File.Parent?.Parent;

            if (target == null)
                return;
            if (target.Game != null)
                return;
            // Selecting the tree node raises Tree.RowSelected, which updates TopDat and GameGrid.
            if (!Tree.Select(target))
            {
                TopDat.Set(target);
                GameGrid.Set(target);
            }
        }

        private void OnGameGridSelected(object? sender, RvFile e)
        {
            TopGame.Set(e);
            RomGrid.Set(e);
        }

        private void OnTreeRowSelected(object? sender, RVTreeRowViewModel e)
        {
            TopDat.Set(e.dTree);
            GameGrid.Set(e.dTree);
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


        private void Start()
        {
            Working = true;
        }

        private void Finish()
        {
            Working = false;
        }

        public void UpdateDats()
        {
            // incase the selected tree item(DAT) is removed from the tree in the updated we need to build a parent list and traverse up it until we find a parent item still in the tree.

            // build a list of the selected item in the Tree view and all the items up the parent list from there back to the root.
            /*
            RvFile selected = ctrRvTree.Selected;
            List<RvFile> parents = new List<RvFile>();
            while (selected != null)
            {
                parents.Add(selected);
                selected = selected.Parent;
            }
            */

            ProgressWindowViewModel progress = new ProgressWindowViewModel();
            // update the dats
            ProgressWindow window = new ProgressWindow { DataContext = progress };

            progress.Initialize("Scanning Dats", DatUpdate.UpdateDat, null);

            //progress.HideCancelButton();
            Window? owner = (Avalonia.Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            if (owner is not null)
            {
                progress.HideCancelButton();
                window.ShowDialog(owner);
            }
            else
            {
                window.Show();
            }

            /*
            // rebuild the tree
            ctrRvTree.Setup(ref DB.DirRoot);

            // if the rvFile.Parent is null it have been removed from the tree so remove it from the list.
            // set up until we find a rvFile with a parent.
            while (parents.Count > 1 && parents[0].Parent == null)
                parents.RemoveAt(0);

            // did we find a parent
            if (parents.Count > 0)
                selected = parents[0];
            else
                selected = null;

            // update the selected tree item, and the game grid view.
            ctrRvTree.SetSelected(selected);
            DatSetSelected(selected);
        */
        }

    }
}
