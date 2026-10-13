using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace AvaloniaApplication1.ViewModels
{
    public partial class LeftButtonsViewModel : ViewModelBase
    {
        /// <summary>
        /// Set to false while a long running operation is in progress, so the
        /// buttons grey out and swap to their "_Disabled" images.
        /// </summary>
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(UpdateDatsCommand))]
        [NotifyCanExecuteChangedFor(nameof(ScanRomsCommand))]
        [NotifyCanExecuteChangedFor(nameof(FindFixesCommand))]
        [NotifyCanExecuteChangedFor(nameof(FixFilesCommand))]
        public partial bool IsIdle { get; set; } = true;

        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void UpdateDats()
        {
            // TODO: replace with the real DAT update.
            Debug.WriteLine("Update DATs requested.");
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
        private void FixFiles()
        {
            // TODO: replace with the real fix operation.
            Debug.WriteLine("Fix Files requested.");
        }
    }
}
