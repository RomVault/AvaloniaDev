using Avalonia.Media.Imaging;
using AvaloniaApplication1.Helper;
using RomVaultCore.RvDB;
using RVUtils;
using System.Collections.ObjectModel;

namespace AvaloniaApplication1.ViewModels
{
    public partial class RVTreeRowViewModel : ViewModelBase
    {

        public RvFile dTree { get; }

        public RVTreeRowViewModel? Parent { get; }


        public ObservableCollection<string> extraInfo { get; } = new();

        public ObservableCollection<RVTreeRowViewModel> Children { get; } = new();

        private RVTreeRowViewModel(RvFile file, RVTreeRowViewModel? parent)
        {
            dTree = file;
            Parent = parent;
        }


        public static RVTreeRowViewModel? Create(RvFile pTree, RVTreeRowViewModel? parent)
        {
            if (pTree.Tree == null)
                return null;

            var row = new RVTreeRowViewModel(pTree, parent);

            for (int i = 0; i < pTree.ChildCount; i++)
            {
                RVTreeRowViewModel? child = Create(pTree.Child(i), row);
                if (child is not null)
                    row.Children.Add(child);
            }

            return row;
        }



        public string Name { get { return BuildText(); } }

        public bool? IsChecked
        {
            get
            {
                if (dTree.Tree.Checked == RvTreeRow.TreeSelect.Locked) return null;
                return dTree.Tree.Checked == RvTreeRow.TreeSelect.Selected;
            }
            set
            {
                if (IsChecked == value)
                    return;


                RvTreeRow.OpenStream();
                RvTreeRow.TreeSelect nSelection;
                if (value == null) nSelection = RvTreeRow.TreeSelect.Locked;
                else if (value == true) nSelection = RvTreeRow.TreeSelect.Selected;
                else nSelection = RvTreeRow.TreeSelect.UnSelected;

                SetCheckedRecurse(nSelection);
                RvTreeRow.CloseStream();

            }
        }

        private void SetCheckedRecurse(RvTreeRow.TreeSelect nSelection)
        {
            dTree.Tree.SetChecked(nSelection, false);
            OnPropertyChanged(nameof(IsChecked));

            //if (shiftPressed)
            //    return;
            foreach (RVTreeRowViewModel child in Children)
                child.SetCheckedRecurse(nSelection);
        }

        public bool IsExpanded
        {
            get => dTree.Tree.TreeExpanded;
            set
            {
                if (IsExpanded == value)
                    return;
                dTree.Tree.SetTreeExpanded(value, false);
                OnPropertyChanged();
            }
        }

        public Bitmap? Icon
        {
            get { return AssetsHelper.GetBitmap(GetIconName()); }
        }


        private string BuildText()
        {
            int intMIA = dTree.DirStatus.CountMIA();
            int intFoundMIA = dTree.DirStatus.CountFoundMIA();

            string strMIA = intMIA > 0 ? $" \\ MIA: {intMIA.ToRvString()}" : "";
            string strFoundMIA = intFoundMIA > 0 ? $" \\ Found MIA: {intFoundMIA.ToRvString()}" : "";
            string subtxt = $"( Have: {dTree.DirStatus.CountCorrect().ToRvString()}{strFoundMIA} \\ Missing: {dTree.DirStatus.CountMissing().ToRvString()}{strMIA} )";

            return dTree.Name + " " + subtxt;
        }

        private string? GetIconName()
        {
            int icon = 2;
            if (dTree.DirStatus.HasInToSort())
                icon = 4;
            else if (!dTree.DirStatus.HasCorrect() && dTree.DirStatus.HasMissing())
                icon = 1;
            else if (!dTree.DirStatus.HasMissing() && dTree.DirStatus.HasMIA())
                icon = 5;
            else if (!dTree.DirStatus.HasMissing())
                icon = 3;

            if (dTree.Dat == null && dTree.DirDatCount == 0) // Directory above DAT's in Tree
                return "DirectoryTree" + icon;

            // Directory that contains DAT's, or directories made by a DAT.
            return "Tree" + icon;
        }
    }
}
