using Avalonia.Media.Imaging;
using AvaloniaApplication1.Helper;
using CommunityToolkit.Mvvm.ComponentModel;
using RomVaultCore.RvDB;
using RVUtils;
using System.Collections.ObjectModel;

namespace AvaloniaApplication1.ViewModels
{
    /// <summary>
    /// A single node (row) in the RomVault tree. Bound by <see cref="Views.RVTreeRow"/>.
    /// </summary>
    public partial class RVTreeRowViewModel : ViewModelBase
    {
        public RVTreeRowViewModel(RvFile file, string name, bool isChecked, string? imagePath, RVTreeRowViewModel? parent)
        {
            dTree = file;
            Name = name;
            IsChecked = isChecked;
            Parent = parent;

            if (imagePath is not null)
            {
                Icon = AssetsHelper.GetBitmap(imagePath);
            }
        }

        /// <summary>
        /// The database directory this row represents.
        /// </summary>
        public RvFile dTree { get; }

        /// <summary>
        /// Parent row, or null for top-level rows.
        /// </summary>
        public RVTreeRowViewModel? Parent { get; }

        [ObservableProperty]
        public partial string Name { get; set; }

        [ObservableProperty]
        public partial bool IsChecked { get; set; }

        /// <summary>
        /// Whether this row is expanded in the TreeView.
        /// </summary>
        [ObservableProperty]
        public partial bool IsExpanded { get; set; }

        /// <summary>
        /// Image shown to the left of the name.
        /// </summary>
        [ObservableProperty]
        public partial Bitmap? Icon { get; set; }

        public ObservableCollection<string> extraInfo { get; } = new();

        public ObservableCollection<RVTreeRowViewModel> Children { get; } = new();

        /// <summary>
        /// Builds the row for <paramref name="pTree"/> and all of its child rows.
        /// Returns null if the directory is not shown in the tree.
        /// </summary>
        public static RVTreeRowViewModel? Create(RvFile pTree, RVTreeRowViewModel? parent)
        {
            if (pTree.Tree == null)
            {
                return null;
            }

            var row = new RVTreeRowViewModel(
                pTree,
                BuildText(pTree),
                pTree.Tree.Checked == RvTreeRow.TreeSelect.Selected,
                GetIconName(pTree),
                parent);

            for (int i = 0; i < pTree.ChildCount; i++)
            {
                RVTreeRowViewModel? child = Create(pTree.Child(i), row);
                if (child is not null)
                {
                    row.Children.Add(child);
                }
            }

            return row;
        }

        private static string BuildText(RvFile pTree)
        {
            int intMIA = pTree.DirStatus.CountMIA();
            int intFoundMIA = pTree.DirStatus.CountFoundMIA();

            string strMIA = intMIA > 0 ? $" \\ MIA: {intMIA.ToRvString()}" : "";
            string strFoundMIA = intFoundMIA > 0 ? $" \\ Found MIA: {intFoundMIA.ToRvString()}" : "";
            string subtxt = $"( Have: {pTree.DirStatus.CountCorrect().ToRvString()}{strFoundMIA} \\ Missing: {pTree.DirStatus.CountMissing().ToRvString()}{strMIA} )";

            return pTree.Name + " " + subtxt;
        }

        private static string? GetIconName(RvFile pTree)
        {
            int icon = 2;
            if (pTree.DirStatus.HasInToSort())
            {
                icon = 4;
            }
            else if (!pTree.DirStatus.HasCorrect() && pTree.DirStatus.HasMissing())
            {
                icon = 1;
            }
            else if (!pTree.DirStatus.HasMissing() && pTree.DirStatus.HasMIA())
            {
                icon = 5;
            }
            else if (!pTree.DirStatus.HasMissing())
            {
                icon = 3;
            }

            if (pTree.Dat == null && pTree.DirDatCount == 0) // Directory above DAT's in Tree
            {
                return "DirectoryTree" + icon;
            }

            // Directory that contains DAT's, or directories made by a DAT.
            return "Tree" + icon;
        }
    }
}
