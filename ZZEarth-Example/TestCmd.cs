using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZFGK.AddinServices;

namespace ZZEarth_Example
{
    class TestCmd : IExtendCommand
    {
        public CmdResult Execute(ExtendCommandData commandData, ref string message)
        {
            MessageBox.Show("Hello World!");
            return CmdResult.Succeed;
        }
    }
}
