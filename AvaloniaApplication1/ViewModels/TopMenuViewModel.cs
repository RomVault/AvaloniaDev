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
        [NotifyCanExecuteChangedFor(nameof(UpdateAllCommand))]
        [NotifyCanExecuteChangedFor(nameof(RefreshAllCommand))]
        [NotifyCanExecuteChangedFor(nameof(ScanRomsCommand))]
        [NotifyCanExecuteChangedFor(nameof(FindFixesCommand))]
        [NotifyCanExecuteChangedFor(nameof(FixRomsCommand))]
        [NotifyCanExecuteChangedFor(nameof(AddToSortCommand))]
        public partial bool IsIdle { get; set; } = true;

        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void UpdateAll()
        {
            // TODO: replace with the real DAT update.
            Debug.WriteLine("Update All requested.");
        }

        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void RefreshAll()
        {
            // TODO: replace with the real DAT refresh.
            Debug.WriteLine("Refresh All requested.");
        }

        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void ScanRoms()
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
        private void AddToSort()
        {
            // TODO: replace with the real ToSort directory add.
            Debug.WriteLine("Add ToSort requested.");
        }

        [RelayCommand]
        private void Reports()
        {
            // TODO: replace with the real report display.
            Debug.WriteLine("Reports requested.");
        }

        [RelayCommand]
        private void Settings()
        {
            // TODO: replace with the real settings dialog.
            Debug.WriteLine("Settings requested.");
        }

        [RelayCommand]
        private void Help()
        {
            // TODO: replace with the real help display.
            Debug.WriteLine("Help requested.");
        }
    }
}
