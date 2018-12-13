using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using Zf3DPlatform.osgEarthGUI;
using ZfPlatform.GUI;
using ZFGK.AddinServices;
using ZFGK.WinForms.DvExprss.Utility;

namespace ZZEarth_Example
{
    public class TestApp : IExtendApp
    {
        public bool StartUp()
        {
            var mainRibbonForm = MainRibbonForm.GetInstance();
            mainRibbonForm.Load += delegate(object sender, EventArgs args)
            {
                var osgEarthForm = mainRibbonForm.MdiChildren.Where(d => d is OsgEarthForm).Select(d => d as OsgEarthForm).First();
                osgEarthForm.ribbonControl.Page("第一个APP").Group("测试").ItemLinks.NewButton("第一个命令", btn => btn.SetTag(typeof(TestCmd)));

                // 必须重新合并Ribbbon才能有效
                mainRibbonForm.ribbonControl.UnMergeRibbon();
                mainRibbonForm.ribbonControl.MergeRibbon(osgEarthForm.ribbonControl);
            };
            return true;
        }
    }
}
