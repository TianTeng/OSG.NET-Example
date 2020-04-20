// ***********************************************************************
// Assembly         : OSS_Example
// Author           : tianteng
// Created          : 04-20-2020
//
// Last Modified By : tianteng
// Last Modified On : 04-20-2020
// ***********************************************************************
// <copyright file="ZfOsgViewForm.cs" company="北京智帆高科科技有限公司">
//     Copyright © 北京智帆高科科技有限公司 2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZFGK.OSG.UI;

namespace OSS_Example.UI
{
    /// <summary>
    /// Class ZfOsgViewForm.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ZfOsgViewForm : Form
    {
        /// <summary>
        /// The zf osg view control
        /// </summary>
        private ZfOsgViewCtrl _zfOsgViewCtrl = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="ZfOsgViewForm"/> class.
        /// </summary>
        public ZfOsgViewForm()
        {
            InitializeComponent();

            _zfOsgViewCtrl = new ZfOsgViewCtrl();
            _zfOsgViewCtrl.Dock = DockStyle.Fill;
            this.Controls.Add(_zfOsgViewCtrl);
        }

        /// <summary>
        /// Handles the Load event of the ZfOsgViewForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ZfOsgViewForm_Load(object sender, EventArgs e)
        {
            _zfOsgViewCtrl.OsgObj.AddAxes();
            _zfOsgViewCtrl.OsgObj.AddBackground();
            _zfOsgViewCtrl.OsgObj.RunOSG();
        }
    }
}
