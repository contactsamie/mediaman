using System.Collections.Generic;
using System.ComponentModel;
using muyou.Lib;

namespace WinFormHelper.Winform
{
    public class WinformModel
    {
        private Winform.WinFormControlSet WinFormControlSet { set; get; }

        private WorkingSessionManager SessionManager { set; get; }

        private BackgroundWorker BackgroundWorkerThatRunsTheDownload { set; get; }

        public WinformModel(WorkingSessionManager sessionManager, Winform.WinFormControlSet winFormControlSet)
        {
            SessionManager = sessionManager;
            WinFormControlSet = new Winform.WinFormControlSet
            {
                DataGridViewToDisplayTheDownloads = winFormControlSet.DataGridViewToDisplayTheDownloads
            };
            BackgroundWorkerThatRunsTheDownload = new BackgroundWorker();
            BackgroundWorkerThatRunsTheDownload.WorkerSupportsCancellation = true;
            BackgroundWorkerThatRunsTheDownload.WorkerReportsProgress = true;

            BackgroundWorkerThatRunsTheDownload.DoWork += BackgroundWorkerThatRunsTheDownload_DoWork;
            BackgroundWorkerThatRunsTheDownload.RunWorkerCompleted += BackgroundWorkerThatRunsTheDownload_RunWorkerCompleted;
            BackgroundWorkerThatRunsTheDownload.ProgressChanged += BackgroundWorkerThatRunsTheDownload_ProgressChanged;

            SessionManager.LoadSession();
            UpdateGrid();
        }

        private void BackgroundWorkerThatRunsTheDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateGrid(e.UserState.ToString());
        }

        private void BackgroundWorkerThatRunsTheDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (SessionManager.WorkingSession.DownloadList != null)
            {
                UpdateGrid("Completed");
            }
        }

        private void BackgroundWorkerThatRunsTheDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            ((WorkingSessionManager)e.Argument).Downloader.ExecuteAllDownloads((ex, message) =>
            {
                BackgroundWorkerThatRunsTheDownload.ReportProgress(100, message + " : " + (ex == null ? "" : ex.Message));
            }, (p, message) =>
            {
                BackgroundWorkerThatRunsTheDownload.ReportProgress(p, message);
            });
        }

        public void RunDownloads()
        {
            BackgroundWorkerThatRunsTheDownload.RunWorkerAsync(SessionManager);
        }

        public void CreateDownload(string url)
        {
            SessionManager.WorkingSession.DownloadList.Add(SessionManager.CreateNewDownload(url));
            UpdateGrid("queued");
        }

        private void UpdateGrid(string message = null)
        {
            var l = new List<object>();

            SessionManager.WorkingSession.DownloadList.ForEach(x =>
            {
                var obj = new
                {
                    DownLoadState = x.DownLoadState,
                    DownloadUrl = x.DownloadUrl,
                    message = x.DownloadStatus,
                    FileSize = x.FileInformation.size + " " + x.FileInformation.sizeUnit,
                    Location = x.BuildLocalDownloadFileName()
                };

                l.Add(obj);
            });

            WinFormControlSet.DataGridViewToDisplayTheDownloads.DataSource = null;
            WinFormControlSet.DataGridViewToDisplayTheDownloads.DataSource = l;

            SessionManager.SaveSession();
        }
    }
}