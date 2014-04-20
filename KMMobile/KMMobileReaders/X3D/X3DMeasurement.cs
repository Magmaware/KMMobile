using System.Collections.Generic;
using System.Xml;

namespace KMMobile.X3D
{
    public class X3DMeasurement<TY>
    {
        public List<X3DShape<TY>> Shapes = new List<X3DShape<TY>>();

        public void Create(XmlElement element, X3DBaseReader reader)
        {
            foreach (XmlElement sref in element.SelectNodes(X3DTOK.X3DTOK_SSHAPEREF))
            {
                int sr = X3DBaseReader.Get<int>(sref, X3DTOK.X3DTOK_SREF);
                Shapes.Add((reader as X3DFormatReader<TY>).Shapes[sr]);
            }
        }
    }

    public class X3DBoundingMeasurement<TY> : X3DMeasurement<TY>
    {
    }

    public class X3DCentreOfGravityMeasurement<TY> : X3DMeasurement<TY>
    {
    }

    public class X3DPointToPointMeasurement<TY> : X3DMeasurement<TY>
    {
    }
}
