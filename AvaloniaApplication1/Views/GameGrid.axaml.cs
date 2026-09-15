using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Models;
using System.ComponentModel;
using System.Diagnostics;

namespace AvaloniaApplication1.Views;

public partial class GameGridView : UserControl
{
    /// <summary>
    /// True when Shift was held for the pointer press that is about to
    /// open the context menu.
    /// </summary>
    private bool _shiftOnPress;

    public GameGridView()
    {
        InitializeComponent();

        // Tunnelling so we see the press before the ContextMenu reacts to it.
        AddHandler(PointerPressedEvent, OnPointerPressedPreview, RoutingStrategies.Tunnel);
    }

    private void OnPointerPressedPreview(object? sender, PointerPressedEventArgs e)
    {
        _shiftOnPress = e.KeyModifiers.HasFlag(KeyModifiers.Shift);
    }

    private void ContextMenu_Opening(object? sender, CancelEventArgs e)
    {
        // Right-click alone does nothing; Shift + right-click opens the menu.
        if (!_shiftOnPress)
        {
            e.Cancel = true;
            return;
        }
    }

    /// <summary>
    /// The row that was right-clicked, or null if the click was not on a row.
    /// </summary>
    private DatNode? SelectedNode =>
        this.FindControl<DataGrid>("GamesGrid")?.SelectedItem as DatNode;

    private void Scan1_Click(object? sender, RoutedEventArgs e)
    {
        RunScan(1);
    }

    /// <summary>
    /// Runs a scan against the row that was right-clicked. Does nothing when
    /// the context menu was opened away from a row.
    /// </summary>
    private void RunScan(int level)
    {
        DatNode? node = SelectedNode;
        if (node is null)
        {
            return;
        }

        // TODO: replace with the real scan once the scanning service exists.
        Debug.WriteLine(
            $"Scan{level} requested for '{node.gName}' ({node.gDescription}), dated {node.gDate:yyyy-MM-dd}.");
    }

    private void Scan2_Click(object? sender, RoutedEventArgs e)
    {
    }

    private void Scan3_Click(object? sender, RoutedEventArgs e)
    {
    }

    private void OpenDir_Click(object? sender, RoutedEventArgs e)
    {
    }
}