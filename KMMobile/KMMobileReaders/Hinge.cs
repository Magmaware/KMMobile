using KMMobile.X3D;
using System.Collections.Generic;
using WaveEngine.Common.Math;

namespace KMMobile
{
    public struct CylinderDef
    {
        public Vector3 Start;
        public Vector3 End;

        public CylinderDef(Vector3 start, Vector3 end)
        {
            Start = start;
            End = end;
        }
    }

    /// <summary>
    /// Defines a hinge
    /// </summary>
    public class Hinge
    {
        public Hinge()
        {
            Cylinders = new List<CylinderDef>();
        }

        /// <summary>
        /// The offset of the hinge
        /// </summary>
        public Vector3 HingeOffset;
        /// <summary>
        /// The axis of the hinge
        /// </summary>
        public Vector3 HingeAxis;
        /// <summary>
        /// The angle of the hinge
        /// </summary>
        public float Angle;
        /// <summary>
        /// The cylinder length
        /// </summary>
        public List<CylinderDef> Cylinders;
        /// <summary>
        /// The wall type
        /// </summary>
        public WallType WallType;
        /// <summary>
        /// The outer thickness
        /// </summary>
        public float OuterThickness;
        /// <summary>
        /// The inner thickness
        /// </summary>
        public float InnerThickness;
        /// <summary>
        /// The texcoord thickness
        /// </summary>
        public Vector4 TexCoord;
    }
}
