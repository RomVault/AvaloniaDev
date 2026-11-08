using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace AvaloniaApplication1.ViewModels
{
    /// <summary>
    /// How a toolbar button was clicked.
    /// </summary>
    /// <param name="IsRightClick">True for a right click, false for a left click.</param>
    /// <param name="IsShift">True when Shift was held during the click.</param>
    public sealed record ButtonClickInfo(bool IsRightClick, bool IsShift);

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

        /// <summary>Raised when Update DATs is clicked.</summary>
        public event EventHandler<ButtonClickInfo>? UpdateDatsClicked;

        /// <summary>Raised when Scan ROMs is clicked.</summary>
        public event EventHandler<ButtonClickInfo>? ScanRomsClicked;

        /// <summary>Raised when Find Fixes is clicked.</summary>
        public event EventHandler<ButtonClickInfo>? FindFixesClicked;

        /// <summary>Raised when Fix Files is clicked.</summary>
        public event EventHandler<ButtonClickInfo>? FixFilesClicked;

        /// <param name="click">Which mouse button was used and whether Shift was held.</param>
        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void UpdateDats(ButtonClickInfo click) => UpdateDatsClicked?.Invoke(this, click);

        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void ScanRoms(ButtonClickInfo click) => ScanRomsClicked?.Invoke(this, click);

        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void FindFixes(ButtonClickInfo click) => FindFixesClicked?.Invoke(this, click);

        [RelayCommand(CanExecute = nameof(IsIdle))]
        private void FixFiles(ButtonClickInfo click) => FixFilesClicked?.Invoke(this, click);
    }
}
