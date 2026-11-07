using Avalonia.Media.Imaging;
using AvaloniaApplication1.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using RomVaultCore.RvDB;
using System;
using System.Collections.ObjectModel;
using RVUtils;

namespace AvaloniaApplication1.ViewModels
{
    public partial class RVTreeViewModel : ViewModelBase
    {

        public RVTreeViewModel()
        { }
        public RVTreeViewModel(string name, bool isChecked = false, string? imagePath = null)
        {
            Name = name;
            IsChecked = isChecked;

            if (imagePath is not null)
            {
                Icon = ImageLoader.Load(imagePath);
            }
        }

        [ObservableProperty]
        public partial string Name { get; set; }

        [ObservableProperty]
        public partial bool IsChecked { get; set; }

        public ObservableCollection<string> extraInfo { get; } = new();

        /// <summary>
        /// Image shown to the left of the check box.
        /// </summary>
        [ObservableProperty]
        public partial Bitmap? Icon { get; set; }

        public RvFile dTree;

        /// <summary>
        /// Whether this node is expanded in the TreeView.
        /// </summary>
        [ObservableProperty]
        public partial bool IsExpanded { get; set; }

        /// <summary>
        /// Parent node, or null for top-level nodes.
        /// </summary>
        public RVTreeViewModel? Parent { get; private set; }

        /// <summary>
        /// Currently selected node (only used on the root view model, bound to TreeView.SelectedItem).
        /// </summary>
        [ObservableProperty]
        public partial RVTreeViewModel? SelectedNode { get; set; }

        /// <summary>
        /// Selects the node for <paramref name="file"/>, expanding its ancestors so it is visible.
        /// Returns false if the file is not in the tree.
        /// </summary>
        public bool Select(RvFile file)
        {
            RVTreeViewModel? node = Find(file);
            if (node is null)
            {
                return false;
            }

            for (RVTreeViewModel? p = node.Parent; p is not null; p = p.Parent)
            {
                p.IsExpanded = true;
            }

            SelectedNode = node;
            return true;
        }

        private RVTreeViewModel? Find(RvFile file)
        {
            foreach (RVTreeViewModel child in Children)
            {
                if (ReferenceEquals(child.dTree, file))
                {
                    return child;
                }

                RVTreeViewModel? found = child.Find(file);
                if (found is not null)
                {
                    return found;
                }
            }

            return null;
        }


        public ObservableCollection<RVTreeViewModel> Children { get; } = new();

        public void Add(RVTreeViewModel branch)
        {
            Children.Add(branch);
        }

        /// <summary>
        /// Raised when a tree row is selected in the view. The argument is the
        /// <see cref="RVTreeViewModel"/> node that was selected.
        /// </summary>
        public event EventHandler<RVTreeViewModel>? RowSelected;

        /// <summary>
        /// Raises the <see cref="RowSelected"/> event for the given node.
        /// </summary>
        public void OnRowSelected(RVTreeViewModel node)
        {
            RowSelected?.Invoke(this, node);
        }

        public void Setup(ref RvFile dirTree, bool KeepSelected = false)
        {
            int treeCount = dirTree.ChildCount;

            if (treeCount >= 1)
            {
                for (int i = 0; i < treeCount; i++)
                {
                    SetupTree(dirTree.Child(i));
                }
            }
        }

        private void SetupTree(RvFile pTree)
        {
            if (pTree.Tree == null)
                return;


            int intMIA = pTree.DirStatus.CountMIA();
            int intFoundMIA = pTree.DirStatus.CountFoundMIA();

            string strMIA = intMIA > 0 ? $" \\ MIA: {intMIA.ToRvString()}" : "";
            string strFoundMIA = intFoundMIA > 0 ? $" \\ Found MIA: {intFoundMIA.ToRvString()}" : "";
            string subtxt = $"( Have: {pTree.DirStatus.CountCorrect().ToRvString()}{strFoundMIA} \\ Missing: {pTree.DirStatus.CountMissing().ToRvString()}{strMIA} )";
            string thistxt = pTree.Name;


            thistxt += " " + subtxt;
            var t = new RVTreeViewModel(thistxt, pTree.Tree.Checked == RvTreeRow.TreeSelect.Selected, null);
            t.dTree = pTree;
            t.Parent = dTree is null ? null : this;
            Children.Add(t);
            t.Setup(ref pTree);
        }
    }
}
