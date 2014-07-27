using muyou.Lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
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
            this.UseWaitCursor = false;
            webBrowser1.UseWaitCursor = false;
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

        private void ParseLine(string line)
        {
            Regex r = new Regex("([ \\t{}():;])");
            String[] tokens = r.Split(line);
            foreach (string token in tokens)
            {
                // Set the tokens default color and font.
                richTextBox1.SelectionColor = Color.Black;
                richTextBox1.SelectionFont = new Font("Courier New", 10, FontStyle.Regular);
                // Check whether the token is a keyword.
                String[] keywords = { "<DIV", ">", "/" };
                for (int i = 0; i < keywords.Length; i++)
                {
                    if (keywords[i] == token)
                    {
                        // Apply alternative color and font to highlight keyword.
                        richTextBox1.SelectionColor = Color.Blue;
                        richTextBox1.SelectionFont = new Font("Courier New", 10, FontStyle.Bold);
                        break;
                    }
                }
                richTextBox1.SelectedText = token;
            }
            richTextBox1.SelectedText = "\n";
        }

        public bool hasRun = false;

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            KickUpdate = false;
            toolStripTextBox1.Text = e.Url.ToString();
            //var doc = webBrowser1.Document;  // This gives you the browser contents
            //if (doc == null) return;
            //var content =(((mshtml.HTMLDocument)(doc.DomDocument)).documentElement).innerText;
            //richTextBox1.Text = content;

            if (hasRun) return;
            hasRun = true;
            if (webBrowser1.Document != null)
            {
                var doc = webBrowser1.Document.DomDocument as mshtml.HTMLDocument;
                if (doc != null)
                {
                    var html = doc.documentElement.outerHTML;
                    richTextBox1.Text = html;
                }
            }
            //var inputLanguage = richTextBox1.Text;
            //Regex r = new Regex("\\n");
            //String[] lines = r.Split(inputLanguage);
            //foreach (string l in lines)
            //{
            //    ParseLine(l);
            //}
            KickUpdate = true;
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            toolStripStatusLabel1.Text = e.Url.ToString();
        }

        public bool KickUpdate = true;

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

        public int duration = 1;
        public void UpdateHtmlText()
        {
            var secon = (DateTime.Now-lastStrike ).TotalSeconds;
            if ((secon > duration) && secon < 8)
            {

               // System.IO.File.WriteAllText(@"C:\Users\dom\Desktop\Shield\Shield Theme\index.html", richTextBox1.Text);

               webBrowser1.DocumentText = richTextBox1.Text;
                backgroundWorker2.RunWorkerAsync(webBrowser1);
            }
            else
{
    timer1.Interval = 100 + (duration*1000);
    timer1.Enabled = true;

}
            lastStrike = DateTime.Now;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!KickUpdate) return;

            UpdateHtmlText();
        }

        public DateTime lastStrike = DateTime.Now;

        private void timer1_Tick(object sender, EventArgs e)
        { timer1.Enabled = false;
            UpdateHtmlText();
        }

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
           
          //  ((WebBrowser)e.Argument).Refresh();
           // var w = ((WebBrowser) e.Argument);
           // w.DocumentText = richTextBox1.Text;

        }


    }
}