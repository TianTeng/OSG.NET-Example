using Osg;
using OsgAnimation;
using ZFGK.Addins;
using ZFGK.OSG.UI;
using ZFGK.OSG.Utility;
using ZFGK.Utility;

namespace OSS_Example.Examples
{
    class SkeletonCmd : IExtendCommand
    {
        public CmdResult Execute(ExtendCmdData data, ref string message)
        {
            var osgViewSimpleForm = data.ViewForm as OsgViewSimpleForm;
            var skelroot = new Skeleton(); // 骨骼动画
            skelroot.setDefaultUpdateCallback();
            var root = new Bone(); // 骨骼
            root.setInvBindMatrixInSkeletonSpace(Matrixd.inverse(Matrixd.translate(-1.0f, 0.0f, 0.0f)));
            root.setName("root");
            var pRootUpdate = new UpdateBone("root"); // 骨骼更新器
            pRootUpdate.getStackedTransforms().push_back(new StackedTranslateElement("translate", new Vec3f(-1.0f, 0.0f, 0.0f)));
            root.setUpdateCallback(pRootUpdate);

            var right0 = new Bone();
            right0.setInvBindMatrixInSkeletonSpace(Matrixd.inverse(Matrixd.translate(0.0f, 0.0f, 0.0f)));
            right0.setName("right0");
            var pRight0Update = new UpdateBone("right0");
            pRight0Update.getStackedTransforms().push_back(new StackedTranslateElement("translate", new Vec3f(1.0f, 0.0f, 0.0f)));
            pRight0Update.getStackedTransforms().push_back(new StackedRotateAxisElement("rotate", new Vec3f(0.0f, 0.0f, 1.0f), 0.0));
            right0.setUpdateCallback(pRight0Update);

            var right1 = new Bone();
            right1.setInvBindMatrixInSkeletonSpace(Matrixd.inverse(Matrixd.translate(1.0f, 0.0f, 0.0f)));
            right1.setName("right1");
            var pRight1Update = new UpdateBone("right1");
            pRight1Update.getStackedTransforms().push_back(new StackedTranslateElement("translate", new Vec3f(1.0f, 0.0f, 0.0f)));
            pRight1Update.getStackedTransforms().push_back(new StackedRotateAxisElement("rotate", new Vec3f(0.0f, 0.0f, 1.0f), 0.0));
            right1.setUpdateCallback(pRight1Update);

            root.addChild(right0);
            right0.addChild(right1);
            skelroot.addChild(root);

            Group scene = new Group();
            BasicAnimationManager manager = new BasicAnimationManager();
            scene.setUpdateCallback(manager);

            Animation anim = new Animation();
            {
                var keys0 = new FloatKeyframeContainer(); // 关键帧容器
                keys0.push_back(new FloatKeyframe(0.0, 0.0f));
                keys0.push_back(new FloatKeyframe(3.0, (float)MathUtil.PI_2));
                keys0.push_back(new FloatKeyframe(6.0, (float)MathUtil.PI_2));
                var sampler = new FloatLinearSampler(); // 线性采样器
                sampler.setKeyframeContainer(keys0);
                var channel = new FloatLinearChannel(sampler); // 通道
                channel.setName("rotate");
                channel.setTargetName("right0");
                anim.addChannel(channel);
            }

            {
                var keys1 = new FloatKeyframeContainer(); // 关键帧容器
                keys1.push_back(new FloatKeyframe(0.0, 0.0f));
                keys1.push_back(new FloatKeyframe(3.0, 0.0f));
                keys1.push_back(new FloatKeyframe(6.0, (float)MathUtil.PI_2));
                var sampler = new FloatLinearSampler(); // 线性采样器
                sampler.setKeyframeContainer(keys1);
                var channel = new FloatLinearChannel(sampler); // 通道
                channel.setName("rotate");
                channel.setTargetName("right1");
                anim.addChannel(channel);
            }
            manager.registerAnimation(anim);
            manager.buildTargetReference();

            // let's start !
            manager.playAnimation(anim);

            // we will use local data from the skeleton
            MatrixTransform rootTransform = new MatrixTransform();
            rootTransform.setMatrix(Matrixf.rotate((float)MathUtil.PI_2, new Vec3f(1.0f, 0.0f, 0.0f)));
            right0.addChild(createAxis());
            right0.setDataVariance(Osg.Object.DataVariance.DYNAMIC);
            right1.addChild(createAxis());
            right1.setDataVariance(Osg.Object.DataVariance.DYNAMIC);
            MatrixTransform trueroot = new MatrixTransform();
            //trueroot.setMatrix(new Matrixf(root.getMatrixInBoneSpace().ptr()));
            trueroot.setMatrix(root.getMatrixInBoneSpace());
            trueroot.addChild(createAxis());
            trueroot.addChild(skelroot);
            trueroot.setDataVariance(Osg.Object.DataVariance.DYNAMIC);
            rootTransform.addChild(trueroot);
            scene.addChild(rootTransform);

            RigGeometry geom = createTesselatedBox(4, 4.0f);
            Geode geode = new Geode();
            geode.Name = "2";
            geode.addDrawable(geom);
            skelroot.addChild(geode);
            Vec3Array src = new Vec3Array(geom.getSourceGeometry().getVertexArray());
            geom.setDataVariance(Osg.Object.DataVariance.DYNAMIC);

            initVertexMap(root, right0, right1, geom, src);

            scene.Name = "1";
            osgViewSimpleForm.OsgViewCtrl.OsgObj.AddOrReplaceModel("Models", scene);
            osgViewSimpleForm.OsgViewCtrl.OsgObj.SetView(ViewMode.ShowAll);

            return CmdResult.Succeed;
        }
        static Geode createAxis()
        {
            Geode geode = (new Geode());
            Geometry geometry = (new Geometry());

            Vec3Array vertices = (new Vec3Array());
            vertices.push_back(new Vec3f(0.0f, 0.0f, 0.0f));
            vertices.push_back(new Vec3f(1.0f, 0.0f, 0.0f));
            vertices.push_back(new Vec3f(0.0f, 0.0f, 0.0f));
            vertices.push_back(new Vec3f(0.0f, 1.0f, 0.0f));
            vertices.push_back(new Vec3f(0.0f, 0.0f, 0.0f));
            vertices.push_back(new Vec3f(0.0f, 0.0f, 1.0f));
            geometry.setVertexArray(vertices);

            Vec4Array colors = (new Vec4Array());
            colors.push_back(new Vec4f(1.0f, 0.0f, 0.0f, 1.0f));
            colors.push_back(new Vec4f(1.0f, 0.0f, 0.0f, 1.0f));
            colors.push_back(new Vec4f(0.0f, 1.0f, 0.0f, 1.0f));
            colors.push_back(new Vec4f(0.0f, 1.0f, 0.0f, 1.0f));
            colors.push_back(new Vec4f(0.0f, 0.0f, 1.0f, 1.0f));
            colors.push_back(new Vec4f(0.0f, 0.0f, 1.0f, 1.0f));
            geometry.setColorArray(colors, Array.Binding.BIND_PER_VERTEX);
            geometry.addPrimitiveSet(new DrawArrays((uint)PrimitiveSet.Mode.LINES, 0, 6));
            geometry.getOrCreateStateSet().setMode(GL.GL_LIGHTING, (uint)false.ToStateAttributeValue());

            geode.addDrawable(geometry);
            return geode;
        }

        private static RigGeometry createTesselatedBox(int nsplit, float size)
        {
            RigGeometry riggeometry = new RigGeometry();
            var geometry = createTesselatedBox2(nsplit, size);
            riggeometry.setSourceGeometry(geometry);
            return riggeometry;
        }

        private static Geometry createTesselatedBox2(int nsplit, float size)
        {
            Geometry geometry = new Geometry();
            Vec3Array vertices = new Vec3Array();
            Vec3Array colors = new Vec3Array();
            geometry.setVertexArray(vertices);
            geometry.setColorArray(colors, Array.Binding.BIND_PER_VERTEX);

            float step = size / nsplit;
            float s = 0.5f / 4.0f;
            for (int i = 0; i < nsplit; i++)
            {
                float x = -1.0f + i * step;
                //std.cout << x << std.endl;
                vertices.push_back(new Vec3f(x, s, s));
                vertices.push_back(new Vec3f(x, -s, s));
                vertices.push_back(new Vec3f(x, -s, -s));
                vertices.push_back(new Vec3f(x, s, -s));
                Vec3f c = new Vec3f(0.0f, 0.0f, 0.0f);
                c[i % 3] = 1.0f;
                colors.push_back(c);
                colors.push_back(c);
                colors.push_back(c);
                colors.push_back(c);
            }

            UIntArray array = new UIntArray();
            for (uint i = 0; i < nsplit - 1; i++)
            {
                uint base1 = i * 4;
                array.push_back(base1);
                array.push_back(base1 + 1);
                array.push_back(base1 + 4);
                array.push_back(base1 + 1);
                array.push_back(base1 + 5);
                array.push_back(base1 + 4);

                array.push_back(base1 + 3);
                array.push_back(base1);
                array.push_back(base1 + 4);
                array.push_back(base1 + 7);
                array.push_back(base1 + 3);
                array.push_back(base1 + 4);

                array.push_back(base1 + 5);
                array.push_back(base1 + 1);
                array.push_back(base1 + 2);
                array.push_back(base1 + 2);
                array.push_back(base1 + 6);
                array.push_back(base1 + 5);

                array.push_back(base1 + 2);
                array.push_back(base1 + 3);
                array.push_back(base1 + 7);
                array.push_back(base1 + 6);
                array.push_back(base1 + 2);
                array.push_back(base1 + 7);
            }

            geometry.addPrimitiveSet(new DrawElementsUInt((uint)PrimitiveSet.Mode.TRIANGLES, array.size(), array.frontPtr()));
            geometry.getOrCreateStateSet().setMode(GL.GL_LIGHTING, (uint)false.ToStateAttributeValue());
            geometry.setUseDisplayList(false);
            return geometry;
        }


        private static void initVertexMap(Bone b0,
            Bone b1,
            Bone b2,
            RigGeometry geom,
            Vec3Array array)
        {
            VertexInfluenceMap vim = new VertexInfluenceMap();
            vim[b0.getName()].setName(b0.getName());
            vim[b1.getName()].setName(b1.getName());
            vim[b2.getName()].setName(b2.getName());
            for (uint i = 0; i < array.size(); i++)
            {
                float val = array[i][0];
                //std.cout << val << std.endl;
                if (val >= -1.0f && val <= 0.0f)
                    vim[b0.getName()].push_back(/*new VertexIndexWeight(*/(int)i, 1.0f/*)*/);
                else if (val > 0.0f && val <= 1.0f)
                    vim[b1.getName()].push_back(/*new VertexIndexWeight(*/(int)i, 1.0f/*)*/);
                else if (val > 1.0f)
                    vim[b2.getName()].push_back(/*new VertexIndexWeight(*/(int)i, 1.0f/*)*/);
            }

            geom.setInfluenceMap(vim);
        }
    }
}
