using AvaloniaApplication1.Models;
using RomVaultCore;
using RomVaultCore.RvDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AvaloniaApplication1.ViewModels
{
    public partial class GameGridViewModel : ViewModelBase
    {
        /// <summary>
        /// Rows shown in the DataGrid.
        /// </summary>
        public BulkObservableCollection<GameNode> Games { get; } = new();

        /// <summary>
        /// Raised when a grid row is selected, passing back the selected
        /// <see cref="GameNode"/>.
        /// </summary>
        public event EventHandler<RvFile>? RowSelected;


        public static int[] _gameGridColumnXPositions;

        /// <summary>
        /// Raises the <see cref="RowSelected"/> event for the given node.
        /// </summary>
        public void OnRowSelected(RvFile gChild)
        {
            RowSelected?.Invoke(this, gChild);
        }

        public GameGridViewModel()
        {
            _gameGridColumnXPositions = new int[(int)RepStatus.EndValue];
        }

        public void Set(RvFile tDir)
        {
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

            // Single reset notification: the DataGrid updates once, after the
            // whole collection is built, instead of re-rendering per row.
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
