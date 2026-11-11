using Avalonia.Controls;
using Avalonia.Input;

namespace AvaloniaApplication1.Views;

/// <summary>
/// A single row in the <see cref="RVTreeView"/>: check box, icon and name.
/// The DataContext is the RVTreeRowViewModel for that node.
/// </summary>
public partial class RVTreeRow : UserControl
{
    public RVTreeRow()
    {
        InitializeComponent();

        // Tunnel so we see the left press before the CheckBox handles it.
        RowCheck.AddHandler(PointerPressedEvent, RowCheck_PreviewPointerPressed, Avalonia.Interactivity.RoutingStrategies.Tunnel);
    }

    /// <summary>
    /// Left click on a locked (null) check box goes straight to true.
    /// Handling the press stops the CheckBox from also toggling on release.
    /// </summary>
    private void RowCheck_PreviewPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (!RowCheck.IsEnabled)
            return;

        if (e.GetCurrentPoint(RowCheck).Properties.IsLeftButtonPressed
            && DataContext is AvaloniaApplication1.ViewModels.RVTreeRowViewModel row
            && row.IsChecked == null)
        {
            row.IsChecked = true;
            e.Handled = true;
        }
    }

    /// <summary>
    /// Right mouse button on the check box sets the row to the null (Locked) state.
    /// Left button is handled by the CheckBox itself (true/false toggle).
    /// </summary>
    private void CheckBox_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (sender is not CheckBox cb || !cb.IsEnabled)
            return;

        if (e.GetCurrentPoint(cb).Properties.IsRightButtonPressed
            && DataContext is AvaloniaApplication1.ViewModels.RVTreeRowViewModel row)
        {
            row.IsChecked = null;
            e.Handled = true;
        }
    }

    /// <summary>
    /// Stops the tree's context menu opening when right-clicking the check box.
    /// </summary>
    private void CheckBox_ContextRequested(object? sender, ContextRequestedEventArgs e)
    {
        e.Handled = true;
    }
}
