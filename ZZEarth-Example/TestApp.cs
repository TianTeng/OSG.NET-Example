using System;
using ZFGK.AddinServices;
using ZFGK.DvExprss.Utility;
using ZfPlatform.DevExpressGUI;

namespace ZZEarth_Example
{
    public class TestApp : IExtendApp
    {
        public bool StartUp()
        {
            var mainRibbonForm = MainRibbonForm.Instance;
            mainRibbonForm.RegisterCmd<TestCmd>("test");
            mainRibbonForm.ribbonControl.Page("第一个APP").Group("测试")
                .ItemLinks.NewButton("第一个命令", btn => btn.SetTag("test"));
            return true;
        }
    }
}
