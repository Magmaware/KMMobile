using System.Xml;

namespace KMMobile.X3D
{
    public class X3DLightAnimation<TY> : X3DKeyFrameAnimation<TY, X3DLightGlue<TY>>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
            foreach (XmlElement frame in element.SelectNodes(X3DTOK.X3DTOK_FRAME))
            {
                var glue = new X3DLightGlue<TY>() { Matrix = reader.GetMatrix<TY>(frame, X3DTOK.X3DTOK_MATRIX), Target = reader.GetVector3<TY>(frame, X3DTOK.X3DTOK_TARGET), SplineTangent = reader.GetVector3<TY>(frame, X3DTOK.X3DTOK_TANGENT) };
                KeyFrames[X3DBaseReader.Get<int>(frame, X3DTOK.X3DTOK_KEY)] = glue;
            }
        }
    }
}
