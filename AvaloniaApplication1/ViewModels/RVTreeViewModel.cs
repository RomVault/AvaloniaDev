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
            Children.Add(t);
            t.Setup(ref pTree);
        }
    }
}
