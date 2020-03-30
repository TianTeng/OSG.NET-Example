using System;
using Osg;
using ZFGK.Addins;
using ZFGK.OSG.UI;
using ZFGK.WinForms.Utility;

namespace OSS_Example.Examples
{
    class SequenceCmd : IExtendCommand
    {
        public CmdResult Execute(ExtendCmdData data, ref string message)
        {
            var osgViewSimpleForm = data.ViewForm as OsgViewSimpleForm;

            // 读取模型
            string osgFileName;
            if (!DialogUtil.OpenOSG(out osgFileName))
                return CmdResult.Cancel;
            var node = OsgDB._.readNodeFile(osgFileName);

            var sequence = new Sequence();
            for (int i = 0; i < 24 * 2; i++)
            {
                var matrixd = Matrixf.rotate((float)(Math.PI / 12 * 2) * i, new Vec3d(0, 0, 1));
                var matrixTransform = new MatrixTransform(matrixd);
                matrixTransform.addChild(node);
                sequence.addChild(matrixTransform, i);
            }

            //设置帧动画持续的时间
            sequence.setInterval(Sequence.LoopMode.LOOP, 0, -1);
            //设置播放的速度及重复的次数
            sequence.setDuration(1.0f / 24, 10);
            sequence.setLoopMode(Sequence.LoopMode.LOOP);
            sequence.setMode(Sequence.SequenceMode.START);
            sequence.Name = "sequence";
            osgViewSimpleForm.OsgViewCtrl.OsgObj.AddOrReplaceModel("Models", sequence);

            return CmdResult.Succeed;
        }
    }
}
