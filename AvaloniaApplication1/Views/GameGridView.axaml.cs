using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
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

        if (e.ClickCount != 2)
        {
            return;
        }

        PointerUpdateKind kind = e.GetCurrentPoint(this).Properties.PointerUpdateKind;
        bool isRight;
        if (kind == PointerUpdateKind.LeftButtonPressed)
        {
            isRight = false;
        }
        else if (kind == PointerUpdateKind.RightButtonPressed)
        {
            isRight = true;
        }
        else
        {
            return;
        }

        // Only rows count; ignore double-clicks on headers or empty space.
        DataGridRow? row = (e.Source as Visual)?.FindAncestorOfType<DataGridRow>(includeSelf: true);
        if (row?.DataContext is not GameNode node)
        {
            return;
        }

        if (DataContext is ViewModels.GameGridViewModel vm)
        {
            vm.OnRowDoubleClicked(node.gFile, isRight);
        }
    }

    private void ContextMenu_Opening(object? sender, CancelEventArgs e)
    {
        // Right-click alone does nothing; Shift + right-click opens the menu.
        if (!_shiftOnPress)
        {
            e.Cancel = true;
        }
    }
}