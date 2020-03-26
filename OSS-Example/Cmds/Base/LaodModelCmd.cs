// ***********************************************************************
// Assembly         : OSS_Example
// Author           : tianteng
// Created          : 03-26-2020
//
// Last Modified By : tianteng
// Last Modified On : 03-26-2020
// ***********************************************************************
// <copyright file="LaodModelCmd.cs" company="北京智帆高科科技有限公司">
//     Copyright © 北京智帆高科科技有限公司 2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.IO;
using System.Windows.Forms;
using ZFGK.Addins;
using ZFGK.OSG.UI;
using ZFGK.OSG.Utility;
using ZFGK.Utility;
using ZFGK.WinForms.Utility;

namespace OSS_Example.Cmds.Base
{
    /// <summary>
    /// Class LaodModelCmd.
    /// Implements the <see cref="ZFGK.Addins.IExtendCommand" />
    /// </summary>
    /// <seealso cref="ZFGK.Addins.IExtendCommand" />
    public class LaodModelCmd : IExtendCommand
    {
        /// <summary>
        /// Executes the specified command data.
        /// </summary>
        /// <param name="cmdData">The command data.</param>
        /// <param name="message">The message.</param>
        /// <returns>CmdResult.</returns>
        public CmdResult Execute(ExtendCmdData cmdData, ref string message)
        {
            var mainForm = cmdData.MainForm as Form;
            var viewForm = cmdData.ViewForm as IViewForm;
            if (viewForm == null)
                return CmdResult.Cancel;
            var osgView = viewForm.View as ZfOsgViewCtrl;
            var osgObj = osgView.OsgObj;

            string[] osgFileNames;
            if (!DialogUtil.OpenOSG(out osgFileNames))
            {
                return CmdResult.Cancel;
            }

            foreach (var osgFileName in osgFileNames)
            {
                var node = OsgDB._.readNodeFile(osgFileName); // 读取模型
                if (node.IsValid())
                {
                    // 使用[分组]-[模型]，的方式组织模型树，模型Name为Key，即如果添加相同名称的模型，原来的模型会删除
                    node.Name = Path.GetFileNameWithoutExtension(osgFileName);
                    osgObj.AddOrReplaceModel("Models", node);
                }
                else
                {
                    ZfMessageUtil.ShowError(string.Format("打开文件\"{0}\"失败！", osgFileName));
                }
            }

            return CmdResult.Succeed;
        }
    }
}
