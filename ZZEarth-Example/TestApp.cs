using System;
using System.Reflection;
using ZFGK.AddinServices;
using ZFGK.DvExprss.Utility;
using ZfPlatform.DevExpressGUI;
using ZZEarth_Example.Cmds;

namespace ZZEarth_Example
{
    public class TestApp : IExtendApp
    {
        public bool StartUp()
        {
            var mainRibbonForm = MainRibbonForm.Instance;
            mainRibbonForm.RegisterCmd(Assembly.GetExecutingAssembly());

            mainRibbonForm.ribbonControl.Page("第一个APP").Group("测试")
                .ItemLinks.NewButton("第一个命令", btn => btn.SetTag("first"));
            return true;
        }
    }
}
