using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZFGK.Addins;
using ZFGK.Utility;

namespace OSS_Example.Cmds
{
    public class FirstCmd : IExtendCommand
    {
        public CmdResult Execute(ExtendCmdData cmdData, ref string message)
        {
            ZfMessageUtil.ShowInfo("Hello World!");
            return CmdResult.Succeed;
        }
    }
}
