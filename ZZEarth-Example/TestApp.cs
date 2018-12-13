using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zf3DPlatform.osgEarthGUI;
using ZFGK.AddinServices;

namespace ZZEarth_Example
{
    public class TestApp : IExtendApp
    {
        public bool StartUp()
        {
            var mainForm = OsgEarthForm.GetInstance();
            mainForm.RibbonControl.Page("第一个APP").Group("测试").ItemLinks
                .NewButton("第一个命令", btn => btn.SetTag(typeof(TestCmd)));
            return true;
        }
    }
}
