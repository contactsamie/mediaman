using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace muyou.Lib.Winform
{
    public abstract   class AbstractControl
    {
        public string Text { set; get; }
    }
    public abstract class AbstractGrid
    {
        public object DataSource { set; get; }
    }
}
