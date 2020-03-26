// ***********************************************************************
// Assembly         : OSS_Example
// Author           : 田腾
// Created          : 01-06-2020
//
// Last Modified By : 田腾
// Last Modified On : 01-03-2020
// ***********************************************************************
// <copyright file="FirstCmd.cs" company="北京智帆高科科技有限公司">
//     Copyright © 北京智帆高科科技有限公司 2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using ZFGK.Addins;
using ZFGK.Utility;

namespace OSS_Example.Cmds
{
    /// <summary>
    /// Class FirstCmd.
    /// Implements the <see cref="ZFGK.Addins.IExtendCommand" />
    /// </summary>
    /// <seealso cref="ZFGK.Addins.IExtendCommand" />
    public class FirstCmd : IExtendCommand
    {
        /// <summary>
        /// Executes the specified command data.
        /// </summary>
        /// <param name="cmdData">The command data.</param>
        /// <param name="message">The message.</param>
        /// <returns>CmdResult.</returns>
        public CmdResult Execute(ExtendCmdData cmdData, ref string message)
        {
            ZfMessageUtil.ShowInfo("Hello World!");
            return CmdResult.Succeed;
        }
    }
}
