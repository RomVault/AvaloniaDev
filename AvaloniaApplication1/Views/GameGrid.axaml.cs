using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Models;
using System.ComponentModel;

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
        }
    }

    /// <summary>
    /// The row that was right-clicked, or null if the click was not on a row.
    /// </summary>
    private DatNode? SelectedNode =>
        this.FindControl<DataGrid>("GamesGrid")?.SelectedItem as DatNode;

    private void Scan1_Click(object? sender, RoutedEventArgs e)
    {
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