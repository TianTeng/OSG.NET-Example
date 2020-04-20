// ***********************************************************************
// Assembly         : OSS_Example
// Author           : tianteng
// Created          : 04-20-2020
//
// Last Modified By : tianteng
// Last Modified On : 04-20-2020
// ***********************************************************************
// <copyright file="ZfEarthTestForm.cs" company="北京智帆高科科技有限公司">
//     Copyright © 北京智帆高科科技有限公司 2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Windows.Forms;
using OsgEarth;
using Osg;
using ZFGK.osgEarth.UI;

namespace OSS_Example.UI
{
    /// <summary>
    /// Class ZfEarthTestForm.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ZfEarthTestForm : Form
    {
        /// <summary>
        /// The zf osg earth view control
        /// </summary>
        private ZfOsgEarthViewCtrl _zfOsgEarthViewCtrl = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="ZfEarthTestForm"/> class.
        /// </summary>
        public ZfEarthTestForm()
        {
            InitializeComponent();

            var node = OsgDB._.readNodeFile(
                    @"D:\SDK_LIB\vc10\osgEarth2.10.2\tests\gdal_tiff.earth");
            var mapNode = MapNode.findMapNode(node);

            _zfOsgEarthViewCtrl = new ZfOsgEarthViewCtrl(mapNode);
            _zfOsgEarthViewCtrl.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(_zfOsgEarthViewCtrl);
        }

        /// <summary>
        /// Handles the Load event of the ZfEarthTestForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ZfEarthTestForm_Load(object sender, EventArgs e)
        {
            _zfOsgEarthViewCtrl.OsgObj.RunOSG();
        }

        /// <summary>
        /// Handles the Click event of the testToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vec3d mapPt;
            if (_zfOsgEarthViewCtrl.ActionUtility.PickEarthPoint("选择点", out mapPt))
            {
                MessageBox.Show(mapPt.ToString());
            }
        }
    }
}
