using System.Xml;

namespace KMMobile.X3D
{
    public abstract class X3DDeformation<T>
    {
        public abstract void Create(XmlElement element, X3DBaseReader reader);
    }

    public class X3DFreeform222Deformation<T> : X3DDeformation<T>
    {
        public T[] ControlPoints;
        public X3DBox<T> Box;

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Box.V1 = reader.GetVector3<T>(element, X3DTOK.X3DTOK_BOX1);
            Box.V2 = reader.GetVector3<T>(element, X3DTOK.X3DTOK_BOX2);
            ControlPoints = reader.GetParts<T>(element, X3DTOK.X3DTOK_CONTROLPOINTS);
        }
    }

    public class X3DFreeform333Deformation<T> : X3DDeformation<T>
    {
        public T[] ControlPoints;
        public X3DBox<T> Box;

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Box.V1 = reader.GetVector3<T>(element, X3DTOK.X3DTOK_BOX1);
            Box.V2 = reader.GetVector3<T>(element, X3DTOK.X3DTOK_BOX2);
            ControlPoints = reader.GetParts<T>(element, X3DTOK.X3DTOK_CONTROLPOINTS);
        }
    }

    public class X3DFreeform444Deformation<T> : X3DDeformation<T>
    {
        public T[] ControlPoints;
        public X3DBox<T> Box;

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Box.V1 = reader.GetVector3<T>(element, X3DTOK.X3DTOK_BOX1);
            Box.V2 = reader.GetVector3<T>(element, X3DTOK.X3DTOK_BOX2);
            ControlPoints = reader.GetParts<T>(element, X3DTOK.X3DTOK_CONTROLPOINTS);
        }
    }

    public class X3DBendDeformation<T> : X3DDeformation<T>
    {
        public X3DAlignPlane Plane;
        public T Angle;
        public X3DBox<T> Box;
        public T Radius;
        public X3DVector3<T> Point1;
        public X3DVector3<T> Point2;
        public X3DVector3<T> Point3;
        public X3DVector3<T> Point4;
        public T Radial;
        public bool Angular;
        public int BendPosition;

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Plane = X3DBaseReader.GetEnum<X3DAlignPlane>(element, X3DTOK.X3DTOK_PLANE);
            Angle = X3DBaseReader.Get<T>(element, X3DTOK.X3DTOK_ANGLE);
            Box.V1 = reader.GetVector3<T>(element, X3DTOK.X3DTOK_BOX1);
            Box.V2 = reader.GetVector3<T>(element, X3DTOK.X3DTOK_BOX2);
            Radius = X3DBaseReader.Get<T>(element, X3DTOK.X3DTOK_RADIUS);
            Point1 = reader.GetVector3<T>(element, X3DTOK.X3DTOK_POINT1);
            Point2 = reader.GetVector3<T>(element, X3DTOK.X3DTOK_POINT2);
            Point3 = reader.GetVector3<T>(element, X3DTOK.X3DTOK_POINT3);
            Point4 = reader.GetVector3<T>(element, X3DTOK.X3DTOK_POINT4);
            Radial = X3DBaseReader.Get<T>(element, X3DTOK.X3DTOK_RADIAL);
            Angular = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_ANGULAR);
            BendPosition = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_POSITION);
        }
    }

    public class X3DCurveWallDeformation<T> : X3DDeformation<T>
    {
        public int Quality;
        public X3DVector2<T> Extents;
        public X3DVector2<T> Extents2;
        public X3DVector2<T> Centre;
        public T[] Bones;
        public X3DMatrix<T>[] BoneMatrices;
        public T Delta;
        public X3DVector3<T>[] CurvePts;
        public X3DVector3<T>[] DistCurvePts;
        public X3DVertexData<T> Contour;
        public X3DJointedShapeGlue<T> Glue;
        public X3DVector3<T> GlobalOffset;
        public X3DAnimation<T> Animation;

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Quality = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_QUALITY);
            Bones = reader.GetParts<T>(element, X3DTOK.X3DTOK_BONES);
            var anim = element.SelectSingleNode(X3DTOK.X3DTOK_ANIMATION) as XmlElement;
            if (null != anim)
            {
                var _anim = new X3DCurveWallDeformationAnimation<T>();
                _anim.Create(anim, reader);
                Animation = _anim;
            }
        }
    }
}
