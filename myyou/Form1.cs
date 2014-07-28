using muyou.Lib;
using muyou.Lib.Winform;
using System;
using System.Windows.Forms;

namespace myyou
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          
        }

        private  WorkingSessionManager SessionManager { set; get; }

        public WinformModel WinformUtility { set; get; }

        private void Form1_Load(object sender, EventArgs e)
        {
            SessionManager = (new WorkingSessionManagerFactory()).CreateWorkingSessionManager();
            var winFormControlSet = new WinFormControlSet
            {
                UrlEntryTextBoxToCreateDownload = toolStripTextBox1,
                DataGridViewToDisplayTheDownloads =dataGridView1
            };
            WinformUtility = new WinformModel(SessionManager, winFormControlSet);
           
        }


        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WinformUtility.CreateDownload(toolStripTextBox1.Text);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WinformUtility.RunDownloads();
        }
    }
}