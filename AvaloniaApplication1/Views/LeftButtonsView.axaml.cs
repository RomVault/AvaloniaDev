using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using AvaloniaApplication1.ViewModels;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.Views;

public partial class LeftButtonsView : UserControl
{
    public LeftButtonsView()
    {
        InitializeComponent();

        // Button only reports left clicks (without modifier info) via Command, so read the
        // raw release instead. handledEventsToo because Button marks left clicks as handled.
        foreach (Button button in new[] { bUpdate, bScan, bFindFix, bFix })
        {
            button.AddHandler(PointerReleasedEvent, Button_PointerReleased, RoutingStrategies.Bubble, handledEventsToo: true);
        }
    }

    /// <summary>
    /// Maps a button to its view-model command.
    /// </summary>
    private IRelayCommand<ButtonClickInfo>? CommandFor(Button button, LeftButtonsViewModel vm)
    {
        if (button == bUpdate) return vm.UpdateDatsCommand;
        if (button == bScan) return vm.ScanRomsCommand;
        if (button == bFindFix) return vm.FindFixesCommand;
        if (button == bFix) return vm.FixFilesCommand;
        return null;
    }

    private void Button_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (sender is not Button button || DataContext is not LeftButtonsViewModel vm)
        {
            return;
        }

        bool isRight;
        if (e.InitialPressMouseButton == MouseButton.Left)
        {
            isRight = false;
        }
        else if (e.InitialPressMouseButton == MouseButton.Right)
        {
            isRight = true;
        }
        else
        {
            return;
        }

        // Only count it if the pointer was released over the button.
        if (!new Rect(button.Bounds.Size).Contains(e.GetPosition(button)))
        {
            return;
        }

        var click = new ButtonClickInfo(isRight, e.KeyModifiers.HasFlag(KeyModifiers.Shift));
        IRelayCommand<ButtonClickInfo>? command = CommandFor(button, vm);

        if (command is not null && command.CanExecute(click))
        {
            command.Execute(click);
            e.Handled = true;
        }
    }
}