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
        private List<Downloads> DownloadList = new List<Downloads>();

        private void Form1_Load(object sender, EventArgs e)
        {
            if (_sessionManager.WorkingSession.DownloadList != null)
            {
                dataGridView1.DataSource = _sessionManager.WorkingSession.DownloadList;
            }
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
            toolStripStatusLabel1.Text = e.UserState.ToString();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadList.Add(_sessionManager.CreateNewDownload("http://upload.wikimedia.org/wikipedia/commons/9/9d/Plymouth_Rock,_Plymouth,_MA,_jjron_03.05.2012.jpg"));
            DownloadList.Add(_sessionManager.CreateNewDownload(""));
            _sessionManager.WorkingSession.DownloadList.AddRange(DownloadList);

            backgroundWorker1.RunWorkerAsync(_sessionManager);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //if (_sessionManager.WorkingSession.DownloadList != null)
            //{
                
            //    dataGridView1.DataSource = _sessionManager.WorkingSession.DownloadList;
            //}
        }
    }
}