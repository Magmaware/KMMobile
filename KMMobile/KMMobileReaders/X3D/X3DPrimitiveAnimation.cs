using System.Collections.Generic;
using System.Xml;

namespace KMMobile.X3D
{
    public class X3DPrimitiveAnimation<TY> : X3DKeyFrameAnimation<TY, X3DPrimitiveGlue<TY>>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
            foreach (XmlElement frame in element.SelectNodes(X3DTOK.X3DTOK_FRAME))
            {
                var glue = new X3DPrimitiveGlue<TY>() { Matrix = reader.GetMatrix<TY>(frame, X3DTOK.X3DTOK_MATRIX), SplineTangent = reader.GetVector3<TY>(frame, X3DTOK.X3DTOK_TANGENT) };
                KeyFrames[X3DBaseReader.Get<int>(frame, X3DTOK.X3DTOK_KEY)] = glue;
            }
        }
    }
}
