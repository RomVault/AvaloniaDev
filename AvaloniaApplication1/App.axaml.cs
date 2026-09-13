using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.ViewModels;
using AvaloniaApplication1.Views;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaApplication1
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Keep the app alive while we swap the splash screen for the main window.
                desktop.ShutdownMode = ShutdownMode.OnExplicitShutdown;

                var splashViewModel = new SplashViewModel();
                var splash = new SplashWindow { DataContext = splashViewModel };

                desktop.MainWindow = splash;
                splash.Show();

                _ = StartupAsync(desktop, splash, splashViewModel);
            }

            base.OnFrameworkInitializationCompleted();
        }


        /// <summary>
        /// Performs the expensive start-up work while the splash screen is visible.
        /// Runs on a background thread, so do not touch UI objects here.
        /// </summary>
        private static async Task LoadAsync(IProgress<string> progress, CancellationToken cancellationToken = default)
        {
            progress.Report("Loading DAT files...");
            await Task.Run(() => Thread.Sleep(100), cancellationToken);

            //TopDat.Name = "First DAT";
            //TopDat.Description = "Loaded at startup";

            progress.Report("Scanning ROMs...");
            await Task.Run(() => Thread.Sleep(100), cancellationToken);

            //TopDat1.Name = "Second DAT";
            //TopDat1.Description = "Loaded at startup";

            progress.Report("Ready.");
        }

        private static async Task StartupAsync(
            IClassicDesktopStyleApplicationLifetime desktop,
            Window splash,
            SplashViewModel splashViewModel)
        {
            var viewModel = new MainWindowViewModel();

            try
            {
                var progress = new Progress<string>(message => splashViewModel.Status = message);
                await viewModel.LoadAsync(progress);
                await LoadAsync(progress);
            }
            catch (Exception ex)
            {
                splashViewModel.Status = "Startup failed: " + ex.Message;
                return;
            }

            var mainWindow = new MainWindow { DataContext = viewModel };

            desktop.MainWindow = mainWindow;
            desktop.ShutdownMode = ShutdownMode.OnMainWindowClose;

            mainWindow.Show();
            splash.Close();
        }
    }
}