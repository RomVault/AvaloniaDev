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

    /// <summary>
    /// Called by Avalonia once the window has been shown.
    /// </summary>
    protected override void OnOpened(EventArgs e)
    {
        base.OnOpened(e);

        if (DataContext is ProgressWindowViewModel vm)
        {
            vm.CloseRequested += (_, _) => Close();
            vm.OnWindowOpened();
        }
    }
}