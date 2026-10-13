using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication1.Views;

/// <summary>
/// A single row in the <see cref="RVTreeView"/>: check box, icon and name.
/// The DataContext is the RVTreeViewModel for that node.
/// </summary>
public partial class RVTreeRow : UserControl
{
    public RVTreeRow()
    {
        InitializeComponent();
    }
}
