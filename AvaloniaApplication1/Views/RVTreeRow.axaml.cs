using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

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
