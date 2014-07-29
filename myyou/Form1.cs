using ISkinContract;
using muyou.Lib;

using skin;
using System;
using System.Windows.Forms;
using WinFormHelper.Winform;

namespace myyou
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Skin = new DefaultSkin();
        }

        private ISkin Skin { set; get; }

        private WorkingSessionManager SessionManager { set; get; }

        public WinformModel WinformUtility { set; get; }

        private void Form1_Load(object sender, EventArgs e)
        {
            Skin.CreateDownloadAction.Click += editToolStripMenuItem_Click;
            Skin.RunDownloadAction.Click += helpToolStripMenuItem_Click;
            var skinControl = (Control) Skin;

            Controls.Add(skinControl);
            skinControl.Dock = DockStyle.Fill;


            SessionManager = (new WorkingSessionManagerFactory()).CreateWorkingSessionManager();
            var winFormControlSet = new WinFormControlSet
            {
                DataGridViewToDisplayTheDownloads = Skin.DataGridViewOfDownloadList
            };
            WinformUtility = new WinformModel(SessionManager, winFormControlSet);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WinformUtility.CreateDownload(Skin.DownloadUrlEntryControl.Text);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WinformUtility.RunDownloads();
        }
    }
}