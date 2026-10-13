using AvaloniaApplication1.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace AvaloniaApplication1.ViewModels
{
    public partial class TopMenuViewModel : ViewModelBase
    {
        /// <summary>
        /// Set to false while a long running operation is in progress, so the
        /// menu items grey out.
        /// </summary>
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(DATVaultUpdateCommand))]
        [NotifyCanExecuteChangedFor(nameof(UpdateNewDATsCommand))]
        [NotifyCanExecuteChangedFor(nameof(RefreshAllDATsCommand))]
        [NotifyCanExecuteChangedFor(nameof(UpdateMIACommand))]
        [NotifyCanExecuteChangedFor(nameof(ScanRomsLevel1Command))]
        [NotifyCanExecuteChangedFor(nameof(ScanRomsLevel2Command))]
        [NotifyCanExecuteChangedFor(nameof(ScanRomsLevel3Command))]
        [NotifyCanExecuteChangedFor(nameof(FindFixesCommand))]
        [NotifyCanExecuteChangedFor(nameof(FixRomsCommand))]
        [NotifyCanExecuteChangedFor(nameof(FixDatReportCommand))]
        [NotifyCanExecuteChangedFor(nameof(FullReportCommand))]
        [NotifyCanExecuteChangedFor(nameof(FixReportCommand))]
        [NotifyCanExecuteChangedFor(nameof(AddToSortCommand))]
        public partial bool IsIdle { get; set; } = true;

        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void DATVaultUpdate()
        {
            // TODO: replace with the real DAT update.
            Debug.WriteLine("DATVault Update requested.");
        }

        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void UpdateNewDATs()
        {
            // TODO: replace with the real DAT update.
            Debug.WriteLine("Update New DATs requested.");
        }

        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void RefreshAllDATs()
        {
            // TODO: replace with the real DAT update.
            Debug.WriteLine("Refresh All DATs requested.");
        }

        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void UpdateMIA()
        {
            // TODO: replace with the real DAT update.
            Debug.WriteLine("Update MIA requested.");
        }





        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void ScanRomsLevel1()
        {
            // TODO: replace with the real ROM scan.
            Debug.WriteLine("Scan ROMs requested.");
        }
        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void ScanRomsLevel2()
        {
            // TODO: replace with the real ROM scan.
            Debug.WriteLine("Scan ROMs requested.");
        }
        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void ScanRomsLevel3()
        {
            // TODO: replace with the real ROM scan.
            Debug.WriteLine("Scan ROMs requested.");
        }

        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void FindFixes()
        {
            // TODO: replace with the real fix search.
            Debug.WriteLine("Find Fixes requested.");
        }

        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void FixRoms()
        {
            // TODO: replace with the real fix operation.
            Debug.WriteLine("Fix ROMs requested.");
        }

        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void FixDatReport()
        {
            // TODO: replace with the real fix operation.
            Debug.WriteLine("Fix Dat Report requested.");
        }

        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void FullReport()
        {
            // TODO: replace with the real fix operation.
            Debug.WriteLine("Full Report requested.");
        }
        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void FixReport()
        {
            // TODO: replace with the real fix operation.
            Debug.WriteLine("Fix Report requested.");
        }



        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void AddToSort()
        {
            // TODO: replace with the real ToSort directory add.
            Debug.WriteLine("Add ToSort requested.");
        }
        [RelayCommand]
        private void Settings()
        {
            // TODO: replace with the real settings dialog.
            Debug.WriteLine("Settings requested.");
        }

        private IconKeyView? _iconKeyView;

        [RelayCommand]
        private void IconKey()
        {
            if (_iconKeyView == null)
            {
                _iconKeyView = new IconKeyView();
                _iconKeyView.Closed += (s, e) => _iconKeyView = null;
                _iconKeyView.Show();
            }
            else
            {
                _iconKeyView.Activate();
            }
        }


    }
}
