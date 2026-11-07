using AvaloniaApplication1.Models;
using RomVaultCore;
using RomVaultCore.RvDB;
using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.ViewModels
{
    /// <summary>
    /// Details of a row double-click: the row's file and which button was used.
    /// </summary>
    public sealed class RowDoubleClickEventArgs : EventArgs
    {
        public RowDoubleClickEventArgs(RvFile file, bool isRightButton)
        {
            File = file;
            IsRightButton = isRightButton;
        }

        public RvFile File { get; }

        /// <summary>True for a right double-click, false for a left double-click.</summary>
        public bool IsRightButton { get; }

        public bool IsLeftButton => !IsRightButton;
    }

    public partial class GameGridViewModel : ViewModelBase
    {
        public BulkObservableCollection<GameNode> Games { get; } = new();

        [ObservableProperty]
        public partial bool HasDescription { get; set; }

        /// <summary>
        /// Row currently selected in the grid (two-way bound to DataGrid.SelectedItem).
        /// </summary>
        [ObservableProperty]
        public partial GameNode? SelectedGame { get; set; }

        partial void OnSelectedGameChanged(GameNode? value)
        {
            if (value is not null)
            {
                OnRowSelected(value.gFile);
            }
        }

        /// <summary>
        /// Runs a scan against the selected row. Parameter is the scan level (1-3).
        /// </summary>
        [RelayCommand]
        private void Scan(string? level)
        {
            GameNode? node = SelectedGame;
            if (node is null)
            {
                return;
            }

            // TODO: replace with the real scan once the scanning service exists.
            System.Diagnostics.Debug.WriteLine(
                $"Scan{level} requested for '{node.gName}' ({node.gDescription}), dated {node.gDate:yyyy-MM-dd}.");
        }

        [RelayCommand]
        private void OpenDir()
        {
            // TODO: open the selected game's directory.
        }

        public event EventHandler<RvFile>? RowSelected;


        public static int[] _gameGridColumnXPositions;


        public void OnRowSelected(RvFile gChild)
        {
            RowSelected?.Invoke(this, gChild);
        }

        public event EventHandler<RowDoubleClickEventArgs>? RowDoubleClicked;

        public void OnRowDoubleClicked(RvFile gChild, bool isRightButton)
        {
            RowDoubleClicked?.Invoke(this, new RowDoubleClickEventArgs(gChild, isRightButton));
        }

        public GameGridViewModel()
        {
            _gameGridColumnXPositions = new int[(int)RepStatus.EndValue];
        }



        public void Set(RvFile tDir)
        {
            bool hasDescription = false;

            _gameGridColumnXPositions = new int[(int)RepStatus.EndValue];
            int childCount = tDir.ChildCount;
            var loaded = new List<GameNode>(childCount);
            for (int i = 0; i < childCount; i++)
            {
                RvFile tChildDir = tDir.Child(i);
                if (!tChildDir.IsDirectory)
                {
                    continue;
                }
                loaded.Add(new GameNode(tChildDir));

                if (!hasDescription && tChildDir.Game != null)
                {
                    string description = tChildDir.Game.GetData(RvGame.GameData.Description);
                    if (description != null && description != "¤")
                        hasDescription = true;
                }
                ReportStatus tDirStat = tChildDir.DirStatus;

                int columnIndex = 0;
                for (int l = 0; l < RepairStatus.DisplayOrder.Length; l++)
                {
                    if (l >= 13)
                    {
                        columnIndex = l;
                    }

                    if (tDirStat.Get(RepairStatus.DisplayOrder[l]) <= 0)
                    {
                        continue;
                    }

                    int len = DigitLength(tDirStat.Get(RepairStatus.DisplayOrder[l])) * 8 + 26;
                    if (len > _gameGridColumnXPositions[columnIndex])
                    {
                        _gameGridColumnXPositions[columnIndex] = len;
                    }

                    columnIndex++;
                }
            }

            int t = 0;
            for (int l = 0; l < (int)RepStatus.EndValue; l++)
            {
                int colWidth = _gameGridColumnXPositions[l];
                _gameGridColumnXPositions[l] = t;
                t += colWidth;
            }

            HasDescription = hasDescription;

            Games.Reset(loaded);
        }



        private static int DigitLength(int number)
        {
            int textNumber = number;
            int len = 0;

            while (textNumber > 0)
            {
                textNumber /= 10;
                len++;
            }
            return len;
        }
    }
}
