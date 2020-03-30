using System;
using Osg;
using OsgAnimation;
using ZFGK.Addins;
using ZFGK.OSG.UI;

namespace OSS_Example.Examples
{
    public class AnimationCmd : IExtendCommand
    {
        public CmdResult Execute(ExtendCmdData data, ref string message)
        {
            var osgViewSimpleForm = data.ViewForm as OsgViewSimpleForm;

            var geode = new Geode();
            geode.addDrawable(new ShapeDrawable(new Box(new Vec3f(0, 0, 0), 0.5f)));
            var trans = new MatrixTransform();
            trans.Name = "AnimatedNode";
            trans.setDataVariance(Osg.Object.DataVariance.DYNAMIC);
            trans.setMatrix(Matrixf.identity());
            trans.addChild(geode);

            var root = new Group();
            root.addChild(trans);

            var grp = new Group() { Name = "1" };
            grp.addChild(root);

            var updatecb = new UpdateMatrixTransform("AnimatedCallback");
            updatecb.getStackedTransforms().push_back(new StackedTranslateElement("position"));
            updatecb.getStackedTransforms().push_back(new StackedRotateAxisElement("euler", new Vec3f(1, 0, 0), 0));
            trans.setUpdateCallback(updatecb);

            var mng = new BasicAnimationManager();
            grp.setUpdateCallback(mng);

            var channelAnimation1 = new Vec3LinearChannel();
            //name of the AnimationUpdateCallback
            channelAnimation1.setTargetName("AnimatedCallback");
            //name of the StackedElementTransform for position modification
            channelAnimation1.setName("position");
            //Create keyframes for (in this case linear) interpolation of a Vec3
            var keyframeContainer = channelAnimation1.getOrCreateSampler().getOrCreateKeyframeContainer();
            keyframeContainer.push_back(new Vec3Keyframe(0, new Vec3f(0, 0, 0)));
            keyframeContainer.push_back(new Vec3Keyframe(2, new Vec3f(1, 1, 0)));
            var anim1 = new Animation();
            anim1.addChannel(channelAnimation1);
            anim1.setPlayMode(Animation.PlayMode.PPONG);

            //define the channel for interpolation of a float angle value
            var channelAnimation2 = new FloatLinearChannel();
            //name of the AnimationUpdateCallback
            channelAnimation2.setTargetName("AnimatedCallback");
            //name of the StackedElementTransform for position modification
            channelAnimation2.setName("euler");
            //Create keyframes for (in this case linear) interpolation of a Vec3
            var keyframeContainer2 = channelAnimation2.getOrCreateSampler().getOrCreateKeyframeContainer();
            keyframeContainer2.push_back(new FloatKeyframe(0, 0));
            keyframeContainer2.push_back(new FloatKeyframe(1.5, (float)(2 * Math.PI)));
            var anim2 = new Animation();
            anim2.addChannel(channelAnimation2);
            anim2.setPlayMode(Animation.PlayMode.LOOP);


            // We register all animation inside the scheduler
            mng.registerAnimation(anim1);
            mng.registerAnimation(anim2);

            //start the animation
            mng.playAnimation(anim1);
            mng.playAnimation(anim2);

            osgViewSimpleForm.OsgViewCtrl.OsgObj.AddOrReplaceModel("Models", grp);
            osgViewSimpleForm.OsgViewCtrl.OsgObj.SetView(ViewMode.ShowAll);

            return CmdResult.Succeed;
        }
    }
}
