// ***********************************************************************
// Assembly         : OSS_Example
// Author           : tianteng
// Created          : 01-06-2020
//
// Last Modified By : tianteng
// Last Modified On : 03-26-2020
// ***********************************************************************
// <copyright file="TestApp.cs" company="北京智帆高科科技有限公司">
//     Copyright © 北京智帆高科科技有限公司 2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using OSS_Example.Cmds;
using OSS_Example.Cmds.Base;
using OSS_Example.Examples;
using ZFGK.Addins;
using ZFGK.DevExprss.Utility;
using ZfPlatform.DevExpressGUI;

namespace OSS_Example
{
    /// <summary>
    /// Class TestApp.
    /// Implements the <see cref="ZFGK.Addins.IExtendApp" />
    /// </summary>
    /// <seealso cref="ZFGK.Addins.IExtendApp" />
    public class TestApp : IExtendApp
    {
        /// <summary>
        /// Starts up.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool StartUp(ExtendAppData data)
        {
            var mainRibbonForm = data.MainForm as MainRibbonForm;

            var addinService = AddinManager.AddinService;
            // 注册命令
            addinService.RegisterCmd<FirstCmd>();
            addinService.RegisterCmd<LaodModelCmd>();
            // 绑定菜单
            mainRibbonForm.ribbonControl.Page("第一个APP").Group("测试")
                .ItemLinks.NewButton("第一个命令", btn => btn.Tag = "First");
            mainRibbonForm.ribbonControl.Page("第一个APP").Group("模型")
                .ItemLinks.NewButton("载入模型", btn => btn.Tag = "LaodModel");

            addinService.RegisterCmd<SequenceCmd>();
            addinService.RegisterCmd<AnimationCmd>();
            addinService.RegisterCmd<LaodModelCmd>();
            mainRibbonForm.ribbonControl.Page("例子").Group("动画").ItemLinks
                .NewButton("队列", btn => btn.Tag = "Sequence")
                .NewButton("关键帧", btn => btn.Tag = "AnimationCmd")
                .NewButton("骨骼", btn => btn.Tag = "SkeletonCmd");
            return true;
        }
    }
}
