using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISkinContract
{
    public interface ISkin
    {
       DataGridView DataGridViewOfDownloadList { set; get; }

        ToolStripTextBox DownloadUrlEntryControl { set; get; }
       ToolStripMenuItem CreateDownloadAction { set; get; }
        ToolStripMenuItem RunDownloadAction { set; get; }
    }
}
