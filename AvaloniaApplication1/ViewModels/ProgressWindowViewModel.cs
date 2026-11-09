using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RomVaultCore;
using System.Reflection;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace AvaloniaApplication1.ViewModels
{
    public partial class ProgressWindowViewModel : ViewModelBase
    {
        public delegate void Finished();

        [ObservableProperty]
        public string title = "";

        [ObservableProperty]
        public string line1 = "";

        [ObservableProperty]
        public string line1Progress = "";

        [ObservableProperty]
        public string line2 = "";

        [ObservableProperty]
        public string line3 = "";

        [ObservableProperty]
        public int progress1Max = 100;

        [ObservableProperty]
        public int progress1Value = 0;

        [ObservableProperty]
        public int progress2Max = 100;

        [ObservableProperty]
        public int progress2Value = 0;

        [RelayCommand]
        private void Cancel()
        {
        }


        private string _titleRoot;

        private bool _errorOpen = false;

        private ThreadWorker _thWrk;
        private Finished _funcFinished;

        public void Initialize(string titleRoot, WorkerStart function, Finished funcFinished)
        {
            _titleRoot = titleRoot;
            Title = titleRoot;
            _funcFinished = funcFinished;
            _thWrk = new ThreadWorker(function);
        }

        public void OnWindowOpened()
        {
            _thWrk.wReport = BgwProgressChanged;
            _thWrk.wFinal = BgwRunWorkerCompleted;
            _thWrk.StartAsync();
        }
        public void HideCancelButton()
        {
            //  cancelButton.Text = "Close";
            //  cancelButton.Enabled = false;
        }

        private void BgwProgressChanged(object obj)
        {
            /*
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => BgwProgressChanged(obj)));
                return;
            }
            */

            if (obj is int e)
            {
                if (e >= 0 && e <= Progress1Max)
                {
                    Progress1Value = e;
                }
                UpdateStatusText();
                return;
            }

            if (obj is bgwText bgwT)
            {
                Line1 = bgwT.Text;
                //if (ShowTimeLog)
                //    TimeLogShow(bgwT.Text);
                return;
            }
            if (obj is bgwSetRange bgwSr)
            {
                Progress1Max = bgwSr.MaxVal >= 0 ? bgwSr.MaxVal : 0;
                Progress1Value = 0;
                UpdateStatusText();
                return;
            }


            if (obj is bgwText2 bgwT2)
            {
                Line2 = bgwT2.Text;
                return;
            }

            if (obj is bgwValue2 bgwV2)
            {
                if (bgwV2.Value >= 0 && bgwV2.Value <= Progress2Max)
                {
                    Progress2Value = bgwV2.Value;
                }
                UpdateStatusText2();
                return;
            }

            if (obj is bgwSetRange2 bgwSr2)
            {
                Progress2Max = bgwSr2.MaxVal >= 0 ? bgwSr2.MaxVal : 0;
                Progress2Value = 0;
                UpdateStatusText2();
                return;
            }
            if (obj is bgwRange2Visible bgwR2V)
            {
                //label2.Visible = bgwR2V.Visible;
                //progressBar2.Visible = bgwR2V.Visible;
                //lbl2Prog.Visible = bgwR2V.Visible;
                return;
            }


            if (obj is bgwText3 bgwT3)
            {
                line3 = bgwT3.Text;
                return;
            }

            if (obj is bgwTextError bgwTW)
            {
                _errorOpen = true;
                Line1 = bgwTW.Text;
                //if (ShowTimeLog)
                //    TimeLogShow(bgwTW.Text);
                return;
            }

            if (obj is bgwShowError bgwSE)
            {
                /*
                if (!_errorOpen)
                {
                    _errorOpen = true;
                    ClientSize = new Size(511, 292);
                    MinimumSize = new Size(511, 292);
                }

                ErrorGrid.Rows.Add();
                int row = ErrorGrid.Rows.Count - 1;

                ErrorGrid.Rows[row].Cells["CError"].Value = bgwSE.error;
                ErrorGrid.Rows[row].Cells["CError"].Style.ForeColor = Color.FromArgb(255, 0, 0);

                ErrorGrid.Rows[row].Cells["CErrorFile"].Value = bgwSE.filename;
                ErrorGrid.Rows[row].Cells["CErrorFile"].Style.ForeColor = Color.FromArgb(255, 0, 0);

                RVPlayer.PlaySound("audio\\error.wav");

                if (row >= 0)
                {
                    ErrorGrid.FirstDisplayedScrollingRowIndex = row;
                }
                */
            }
        }


        private void UpdateStatusText()
        {
            int range = progress1Max;
            int percent = range > 0 ? progress1Value * 100 / range : 0;

            Title = $"{_titleRoot} - {percent}% complete";
        }

        private void UpdateStatusText2()
        {
            Line1Progress = progress1Max > 0 ? $"{progress1Value}/{progress1Max}" : "";
        }

        private void BgwRunWorkerCompleted()
        {
            /*
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(BgwRunWorkerCompleted));
                return;
            }
            RVPlayer.PlaySound("audio\\complete.wav");
            

            if (_errorOpen)
            {
                cancelButton.Visible = true;
                cancelButton.Text = "Close";
                cancelButton.Enabled = true;
                _bDone = true;
            }
            else
            {
                long tElapsed = (long)sw.Elapsed.TotalMilliseconds;
                if (tElapsed > 2000)
                    tElapsed = 2000;
                tElapsed = 2500 - tElapsed;

                tm = new System.Threading.Timer(trmClose, null, tElapsed, Timeout.Infinite);
            }
*/
            _funcFinished?.Invoke();
            //_parentForm.Show();

            // wFinal is called on the worker thread; close on the UI thread.
            Avalonia.Threading.Dispatcher.UIThread.Post(() => CloseRequested?.Invoke(this, System.EventArgs.Empty));
        }

        /// <summary>
        /// Raised when the window should close. The view subscribes and calls Close().
        /// </summary>
        public event System.EventHandler? CloseRequested;


    }
}
