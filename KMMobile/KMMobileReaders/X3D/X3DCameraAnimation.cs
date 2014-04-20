using System.Collections.Generic;
using System.Xml;

namespace KMMobile.X3D
{
    public class X3DCameraAnimation<TY> : X3DKeyFrameAnimation<TY, X3DViewpoint<TY>>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
            foreach (XmlElement egAnimationFrame in
            element.SelectNodes(X3DTOK.X3DTOK_FRAME))
            {
                var frame = new X3DViewpoint<TY>() { FlyMatrix = reader.GetMatrix<TY>(egAnimationFrame, X3DTOK.X3DTOK_VPCAMERA), SplineTangent = reader.GetVector3<TY>(egAnimationFrame, X3DTOK.X3DTOK_TANGENT) };
                KeyFrames[X3DBaseReader.Get<int>(egAnimationFrame, X3DTOK.X3DTOK_KEY)] = frame;
            }
        }
    }
}
