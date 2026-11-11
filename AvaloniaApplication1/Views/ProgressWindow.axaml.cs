using Avalonia.Controls;
using AvaloniaApplication1.ViewModels;
using System;

namespace AvaloniaApplication1;

public partial class ProgressWindow : Window
{
    public ProgressWindow()
    {
        InitializeComponent();
    }

    private bool _allowClose;

    /// <summary>
    /// Called by Avalonia once the window has been shown.
    /// </summary>
    protected override void OnOpened(EventArgs e)
    {
        base.OnOpened(e);

        if (DataContext is ProgressWindowViewModel vm)
        {
            vm.CloseRequested += (_, _) =>
            {
                _allowClose = true;
                Close();
            };
            vm.OnWindowOpened();
        }
    }

    /// <summary>
    /// Blocks user-initiated closes (e.g. Alt+F4); only the view model's
    /// CloseRequested (job finished) is allowed to close the window.
    /// </summary>
    protected override void OnClosing(WindowClosingEventArgs e)
    {
        if (!_allowClose && !e.IsProgrammatic)
        {
            e.Cancel = true;
        }

        base.OnClosing(e);
    }
}