using muyou.Lib;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace myyou
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _sessionManager.LoadSession();
        }

        private readonly WorkingSessionManager _sessionManager = (new WorkingSessionManagerFactory()).CreateWorkingSessionManager();

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ((WorkingSessionManager)e.Argument).Downloader.ExecuteAllDownloads((ex, message) =>
                 {
                     backgroundWorker1.ReportProgress(100, message + " : " + (ex == null ? "" : ex.Message));
                 }, (p, message) =>
                 {
                     backgroundWorker1.ReportProgress(p, message);
                 });
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            UpdateGrid(e.UserState.ToString());
        }

        public void RunDownloads()
        {
            backgroundWorker1.RunWorkerAsync(_sessionManager);
        }

        public void CreateDownload(string url)
        {
            _sessionManager.WorkingSession.DownloadList.Add(_sessionManager.CreateNewDownload(url));
            UpdateGrid("queued");
        }

        private void UpdateGrid(string message = null)
        {
            var l = new List<object>();

            _sessionManager.WorkingSession.DownloadList.ForEach(x =>
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

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = l;

            _sessionManager.SaveSession();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (_sessionManager.WorkingSession.DownloadList != null)
            {
                UpdateGrid("Completed");
            }
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDownload(toolStripTextBox1.Text);
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunDownloads();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            toolStripTextBox1.Text = e.Url.ToString();
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            toolStripStatusLabel1.Text = e.Url.ToString();
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            toolStripStatusLabel1.Text = e.Url.ToString();
        }

        private void prevToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(toolStripTextBox1.Text);
        }

        private void nextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void nextToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }
    }
}