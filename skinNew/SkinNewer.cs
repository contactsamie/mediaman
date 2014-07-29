using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using ISkinContract;

namespace skinNew
{
    public partial class SkinNewer: UserControl, ISkin
    {
      
        public SkinNewer()
        {
            InitializeComponent();
            DataGridViewOfDownloadList =new DataGridView();
            CreateDownloadAction = editToolStripMenuItem;
            RunDownloadAction = helpToolStripMenuItem;
            DownloadUrlEntryControl = toolStripTextBox1;
            DataGridViewOfDownloadList.DataSourceChanged += dg_DataSourceChanged;
        
        }

        void dg_DataSourceChanged(object sender, EventArgs e)
        {
            var l = (List<object>)DataGridViewOfDownloadList.DataSource;
            dataGridView1.SetObjects(l);
          //  dataGridView1.DataBindings.Add("DownLoadState", l, "DownLoadState");
        }

        public DataGridView DataGridViewOfDownloadList { set; get; }

        public ToolStripTextBox DownloadUrlEntryControl { set; get; }

        public ToolStripMenuItem CreateDownloadAction { set; get; }

        public ToolStripMenuItem RunDownloadAction { set; get; }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void DefaultSkin_Load(object sender, EventArgs e)
        {
           
        }
    }
}
