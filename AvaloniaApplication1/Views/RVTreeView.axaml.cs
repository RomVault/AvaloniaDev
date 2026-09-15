using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using AvaloniaApplication1.ViewModels;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

namespace AvaloniaApplication1.Views;

public partial class RVTreeView : UserControl
{
    public static readonly StyledProperty<IEnumerable?> NodesProperty =
        AvaloniaProperty.Register<RVTreeView, IEnumerable?>(nameof(Nodes));

    /// <summary>
    /// The tree nodes to display. Bind this to a collection of TreeNodeViewModel.
    /// </summary>
    public IEnumerable? Nodes
    {
        get => GetValue(NodesProperty);
        set => SetValue(NodesProperty, value);
    }

    /// <summary>
    /// The node under the pointer when the context menu was requested.
    /// Right-clicking does not change TreeView.SelectedItem, so we capture
    /// the row ourselves on the press that opens the menu.
    /// </summary>
    private TreeNodeViewModel? _clickedNode;

    public RVTreeView()
    {
        InitializeComponent();

        // Tunnelling so we see the press before the ContextMenu reacts to it.
        AddHandler(PointerPressedEvent, OnPointerPressedPreview, RoutingStrategies.Tunnel);
    }

    private void OnPointerPressedPreview(object? sender, PointerPressedEventArgs e)
    {
        // Walk up from whatever was hit to the owning row, then take its data item.
        TreeViewItem? row = (e.Source as Visual)?.FindAncestorOfType<TreeViewItem>(includeSelf: true);
        _clickedNode = row?.DataContext as TreeNodeViewModel;
    }

    private void ContextMenu_Opening(object? sender, CancelEventArgs e)
    {
        TreeNodeViewModel? node = _clickedNode;

        // Clicked empty space below the tree: nothing to act on.
        if (node is null)
        {
            e.Cancel = true;
            return;
        }

        bool isBranch = node.Children.Count > 0;

        // Branches get the scan commands; leaves do not.
        miScan1.IsVisible = isBranch;
        miScan2.IsVisible = isBranch;
        miScan3.IsVisible = isBranch;

        // "Open Dir" only makes sense for a leaf (an actual directory entry).
        miOpenDir.IsVisible = !isBranch;

        // Hide the separator unless items exist on both sides of it.
        miSep.IsVisible = isBranch && miOpenDir.IsVisible;
    }

    private void Scan1_Click(object? sender, RoutedEventArgs e)
    {
        RunScan(1);
    }

    private void Scan2_Click(object? sender, RoutedEventArgs e)
    {
        RunScan(2);
    }

    private void Scan3_Click(object? sender, RoutedEventArgs e)
    {
        RunScan(3);
    }

    private void RunScan(int level)
    {
        if (_clickedNode is null)
        {
            return;
        }

        // TODO: replace with the real scan once the scanning service exists.
        Debug.WriteLine($"Scan{level} requested for tree node '{_clickedNode.Name}'.");
    }
}
