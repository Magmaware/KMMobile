using System.Xml;

namespace KMMobile.X3D
{
    public class X3DShapeAnimation<TY> : X3DKeyFrameAnimation<TY, X3DShapeGlue<TY>>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
            foreach (XmlElement frame in element.SelectNodes(X3DTOK.X3DTOK_FRAME))
            {
                var glue = new X3DShapeGlue<TY>() { Matrix = reader.GetMatrix<TY>(frame, X3DTOK.X3DTOK_MATRIX), Scale = X3DBaseReader.Get<TY>(frame, X3DTOK.X3DTOK_SCALE), SplineTangent = reader.GetVector3<TY>(frame, X3DTOK.X3DTOK_TANGENT) };
                KeyFrames[X3DBaseReader.Get<int>(frame, X3DTOK.X3DTOK_KEY)] = glue;
            }
        }
    }
}
