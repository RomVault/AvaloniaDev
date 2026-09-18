using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Models;
using System.ComponentModel;
using System.Diagnostics;

namespace AvaloniaApplication1.Views;

public partial class RomGridView : UserControl
{
    /// <summary>
    /// True when Shift was held for the pointer press that is about to
    /// open the context menu.
    /// </summary>
    private bool _shiftOnPress;

    public RomGridView()
    {
        InitializeComponent();

        // Tunnelling so we see the press before the ContextMenu reacts to it.
        AddHandler(PointerPressedEvent, OnPointerPressedPreview, RoutingStrategies.Tunnel);
    }

    private void OnPointerPressedPreview(object? sender, PointerPressedEventArgs e)
    {
        _shiftOnPress = e.KeyModifiers.HasFlag(KeyModifiers.Shift);
    }

    /// <summary>
    /// The row that was right-clicked, or null if the click was not on a row.
    /// </summary>
    private RomNode? SelectedNode =>
        this.FindControl<DataGrid>("RomsGrid")?.SelectedItem as RomNode;

    private void ContextMenu_Opening(object? sender, CancelEventArgs e)
    {
        // Right-click alone does nothing; Shift + right-click opens the menu.
        if (!_shiftOnPress)
        {
            e.Cancel = true;
            return;
        }

        if (SelectedNode is null)
        {
            e.Cancel = true;
        }
    }

    private void OpenDir_Click(object? sender, RoutedEventArgs e)
    {
        RomNode? node = SelectedNode;
        if (node is null)
        {
            return;
        }

        // TODO: replace with the real directory open once the path is available.
        Debug.WriteLine($"Open Dir requested for ROM '{node.rRom}'.");
    }
}
