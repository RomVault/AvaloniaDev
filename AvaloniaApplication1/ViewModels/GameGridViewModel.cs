using AvaloniaApplication1.Models;
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

        /// <summary>
        /// Raises the <see cref="RowSelected"/> event for the given node.
        /// </summary>
        public void OnRowSelected(RvFile gChild)
        {
            RowSelected?.Invoke(this, gChild);
        }

        public GameGridViewModel()
        {
        }

        public void Set(RvFile tDir)
        {
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
            }

            // Single reset notification: the DataGrid updates once, after the
            // whole collection is built, instead of re-rendering per row.
            Games.Reset(loaded);
        }
    }
}
