
using System.Windows.Forms;
using ZFGK.AddinServices;

namespace ZZEarth_Example
{
    class TestCmd : IExtendCommand
    {
        public CmdResult Execute(ExtendCmdData commandData, ref string message)
        {
            MessageBox.Show("Hello World!");
            return CmdResult.Succeed;
        }
    }
}
