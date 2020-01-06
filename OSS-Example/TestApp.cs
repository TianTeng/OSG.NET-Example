using System;
using System.Reflection;
using OSS_Example.Cmds;
using ZFGK.Addins;
using ZFGK.DevExprss.Utility;
using ZfPlatform.DevExpressGUI;

namespace OSS_Example
{
    public class TestApp : IExtendApp
    {
        public bool StartUp(ExtendAppData data)
        {
            var mainRibbonForm = data.MainForm as MainRibbonForm;

            var addinService = AddinManager.AddinService;
            addinService.RegisterCmd<FirstCmd>();
            mainRibbonForm.ribbonControl.Page("第一个APP").Group("测试")
                .ItemLinks.NewButton("第一个命令", btn => btn.Tag = "First");
            return true;
        }
    }
}
