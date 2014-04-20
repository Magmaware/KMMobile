using System.Xml;

namespace KMMobile.X3D
{
    public class X3DJointedSystemAnimation<TY> : X3DKeyFrameAnimation<TY, X3DJointedShapeGlue<TY>>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
            foreach (XmlElement frame in element.SelectNodes(X3DTOK.X3DTOK_FRAME))
            {
                var glue = new X3DJointedShapeGlue<TY>();
                glue.JointData.JointAngle = X3DBaseReader.Get<TY>(frame, X3DTOK.X3DTOK_ANGLE);
                KeyFrames[X3DBaseReader.Get<int>(frame, X3DTOK.X3DTOK_KEY)] = glue;
            }
        }
    }
}
