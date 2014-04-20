using System.Xml;

namespace KMMobile.X3D
{
    public class X3DCurveWallDeformationAnimation<TY> : X3DKeyFrameAnimation<TY, X3DCurveWallDeformation<TY>>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
            foreach (XmlElement frame in element.SelectNodes(X3DTOK.X3DTOK_FRAME))
            {
                var def = new X3DCurveWallDeformation<TY>() { Bones = reader.GetParts<TY>(frame, X3DTOK.X3DTOK_BONES) };
                KeyFrames[X3DBaseReader.Get<int>(frame, X3DTOK.X3DTOK_KEY)] = def;
            }
        }
    }
}
