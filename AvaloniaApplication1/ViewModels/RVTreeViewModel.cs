using CommunityToolkit.Mvvm.ComponentModel;
using RomVaultCore.RvDB;
using System;
using System.Collections.ObjectModel;

namespace AvaloniaApplication1.ViewModels
{
    /// <summary>
    /// View model for the whole tree (<see cref="Views.RVTreeView"/>).
    /// Owns the top-level rows, the selection and the RowSelected event.
    /// Each node is an <see cref="RVTreeRowViewModel"/>.
    /// </summary>
    public partial class RVTreeViewModel : ViewModelBase
    {
        /// <summary>
        /// Top-level rows (bound to TreeView.ItemsSource).
        /// </summary>
        public ObservableCollection<RVTreeRowViewModel> Children { get; } = new();

        /// <summary>
        /// Currently selected row (bound to TreeView.SelectedItem).
        /// </summary>
        [ObservableProperty]
        public partial RVTreeRowViewModel? SelectedNode { get; set; }

        /// <summary>
        /// Raised when a tree row is selected in the view.
        /// </summary>
        public event EventHandler<RVTreeRowViewModel>? RowSelected;

        public void OnRowSelected(RVTreeRowViewModel node)
        {
            RowSelected?.Invoke(this, node);
        }

        /// <summary>
        /// Rebuilds the tree from <paramref name="dirTree"/> (normally DB.DirRoot).
        /// </summary>
        public void Setup(ref RvFile dirTree, bool KeepSelected = false)
        {
            RvFile? previous = KeepSelected ? SelectedNode?.dTree : null;

            Children.Clear();
            for (int i = 0; i < dirTree.ChildCount; i++)
            {
                RVTreeRowViewModel? row = RVTreeRowViewModel.Create(dirTree.Child(i), null);
                if (row is not null)
                {
                    Children.Add(row);
                }
            }

            if (previous is not null)
            {
                Select(previous);
            }
        }

        /// <summary>
        /// Selects the row for <paramref name="file"/>, expanding its ancestors so it is visible.
        /// Returns false if the file is not in the tree.
        /// </summary>
        public bool Select(RvFile file)
        {
            RVTreeRowViewModel? node = Find(Children, file);
            if (node is null)
            {
                return false;
            }

            for (RVTreeRowViewModel? p = node.Parent; p is not null; p = p.Parent)
            {
                p.IsExpanded = true;
            }

            SelectedNode = node;
            return true;
        }

        private static RVTreeRowViewModel? Find(ObservableCollection<RVTreeRowViewModel> rows, RvFile file)
        {
            foreach (RVTreeRowViewModel row in rows)
            {
                if (ReferenceEquals(row.dTree, file))
                {
                    return row;
                }

                RVTreeRowViewModel? found = Find(row.Children, file);
                if (found is not null)
                {
                    return found;
                }
            }

            return null;
        }
    }
}
