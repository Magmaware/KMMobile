using System.Collections.Generic;
using System.Xml;

namespace KMMobile.X3D
{
    /// <summary>
    /// The list of types of primitives
    /// </summary>
    public enum X3DPrimitiveType : uint
    {
        LineLoop,
        LineStrip,
        Lines,
        Points,
        Polygon,
        Quads,
        QuadStrip,
        Triangles,
        TriangleFan,
        TriangleStrip,
        Line,
        Rectangle,
        Box,
        Pyramid,
        Cylinder,
        Disk,
        PartialDisk,
        Sphere,
        HermiteSpline,
        CatMullRomSpline,
        BezierSpline,
        BezierSurface,
        NurbsCurve,
        NurbsCurveArray,
        NurbsTrim,
        NurbsTrimArray,
        PwlCurve,
        PwlTrim,
        NurbsSurface,
        ComplexNurbsSurface,
        ExtSphere,
        ExtCone,
        ExtCube,
        ExtTorus,
        ExtDodecahedron,
        ExtTeapot,
        ExtOctahedron,
        ExtTetrahedron,
        ExtIcosahedron,
        Mesh,
        Decal,
        Callout,
        TrackerRectangle,
        SelectionBox,
        Text,
        Note,
        FloatingNote,
        PolyCylinder,
        PolyCone,
        PathExtrusion,
        TwistExtrusion,
        SuperExtrusion,
        SpiralExtrusion,
        Lathe,
        Helicoid,
        Toroid,
        ScrewExtrusion,
        ManualParticleSystem,
        Smoke,
        BitmapExplosion,
        Bubbles,
        Snow,
        Grid,
        Axes,
        GrabHandle,
        Artwork,
        ArtworkMarker,
        Arrow,
        Dimension,
        Rotator,
        LockPoint,
        Light,
        Crease,
        PrimitiveAnimation,
        ShapeAnimation,
        LightAnimation,
        JointedSystemAnimation,
        VertexAnimation,
        MateriaAnimation,
        PolyCurve,
        ClosedPolyCurve,
        DeformationGrabBag,
        CameraSwitchingAnimation,
        CameraAnimation,
        SoundAnimation,
        ScriptAnimation,
        CurveWallDeformationAnimation
    }

    /// <summary>
    /// Base class for all primitives
    /// </summary>
    /// <typeparam name="TY">The numeric type of the primitive</typeparam>
    public abstract class X3DPrimitive<TY>
    {
        /// <summary>
        /// The ID of the primitive
        /// </summary>
        public int ID;

        /// <summary>
        /// The type of the primitive
        /// </summary>
        public X3DPrimitiveType PType;

        /// <summary>
        /// Overridden in all derived classes to load the element from its attributes
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="reader">The reader</param>
        public abstract void Create(XmlElement element, X3DBaseReader reader);

        /// <summary>
        /// Virtual constructor -- creates a primitive from an XML element
        /// </summary>
        /// <param name="element">The XML element</param>
        /// <param name="reader">The reader used to parse the XML</param>
        /// <returns>The primitive</returns>
        public static X3DPrimitive<TY> Construct(XmlElement element, X3DBaseReader reader)
        {
            var type = X3DBaseReader.GetEnum<X3DPrimitiveType>(element, X3DTOK.X3DTOK_TYPEID);
            X3DPrimitive<TY> res = null;
            switch (type)
            {
                case X3DPrimitiveType.LineLoop:
                    res = new X3DLineLoop<TY>();
                    break;
                case X3DPrimitiveType.LineStrip:
                    res = new X3DLineStrip<TY>();
                    break;
                case X3DPrimitiveType.Lines:
                    res = new X3DLines<TY>();
                    break;
                case X3DPrimitiveType.Points:
                    res = new X3DPoints<TY>();
                    break;
                case X3DPrimitiveType.Polygon:
                    res = new X3DPolygon<TY>();
                    break;
                case X3DPrimitiveType.Quads:
                    res = new X3DQuads<TY>();
                    break;
                case X3DPrimitiveType.QuadStrip:
                    res = new X3DQuadStrip<TY>();
                    break;
                case X3DPrimitiveType.Triangles:
                    res = new X3DTriangles<TY>();
                    break;
                case X3DPrimitiveType.TriangleFan:
                    res = new X3DTriangleFan<TY>();
                    break;
                case X3DPrimitiveType.TriangleStrip:
                    res = new X3DTriangleStrip<TY>();
                    break;
                case X3DPrimitiveType.Grid:
                    res = new X3DGrid<TY>();
                    break;
                case X3DPrimitiveType.Axes:
                    res = new X3DAxes<TY>();
                    break;
                case X3DPrimitiveType.HermiteSpline:
                    res = new X3DHermiteSpline<TY>();
                    break;
                case X3DPrimitiveType.CatMullRomSpline:
                    res = new X3DCatMullRomSpline<TY>();
                    break;
                case X3DPrimitiveType.BezierSpline:
                    res = new X3DBezierSpline<TY>();
                    break;
                case X3DPrimitiveType.BezierSurface:
                    res = new X3DBezierSurface<TY>();
                    break;
                case X3DPrimitiveType.NurbsCurve:
                    res = new X3DNurbsCurve<TY>();
                    break;
                case X3DPrimitiveType.NurbsTrim:
                    res = new X3DNurbsTrim<TY>();
                    break;
                case X3DPrimitiveType.NurbsCurveArray:
                    res = new X3DNurbsCurveArray<TY>();
                    break;
                case X3DPrimitiveType.NurbsTrimArray:
                    res = new X3DNurbsTrimArray<TY>();
                    break;
                case X3DPrimitiveType.PwlCurve:
                    res = new X3DPwlCurve<TY>();
                    break;
                case X3DPrimitiveType.PwlTrim:
                    res = new X3DPwlTrim<TY>();
                    break;
                case X3DPrimitiveType.NurbsSurface:
                    res = new X3DNurbsSurface<TY>();
                    break;
                case X3DPrimitiveType.ComplexNurbsSurface:
                    res = new X3DComplexNurbsSurface<TY>();
                    break;
                case X3DPrimitiveType.Mesh:
                    res = new X3DMesh<TY>();
                    break;
                case X3DPrimitiveType.Line:
                    res = new X3DLine<TY>();
                    break;
                case X3DPrimitiveType.Rectangle:
                    res = new X3DRectangle<TY>();
                    break;
                case X3DPrimitiveType.Box:
                    res = new X3DBoxPrim<TY>();
                    break;
                case X3DPrimitiveType.Pyramid:
                    res = new X3DPyramid<TY>();
                    break;
                case X3DPrimitiveType.Cylinder:
                    res = new X3DCylinder<TY>();
                    break;
                case X3DPrimitiveType.Disk:
                    res = new X3DDisk<TY>();
                    break;
                case X3DPrimitiveType.PartialDisk:
                    res = new X3DPartialDisk<TY>();
                    break;
                case X3DPrimitiveType.Sphere:
                    res = new X3DSphere<TY>();
                    break;
                case X3DPrimitiveType.ExtSphere:
                    res = new X3DExtSphere<TY>();
                    break;
                case X3DPrimitiveType.ExtCone:
                    res = new X3DExtCone<TY>();
                    break;
                case X3DPrimitiveType.ExtCube:
                    res = new X3DExtCube<TY>();
                    break;
                case X3DPrimitiveType.ExtTorus:
                    res = new X3DExtTorus<TY>();
                    break;
                case X3DPrimitiveType.ExtDodecahedron:
                    res = new X3DExtDodecahedron<TY>();
                    break;
                case X3DPrimitiveType.ExtTeapot:
                    res = new X3DExtTeapot<TY>();
                    break;
                case X3DPrimitiveType.ExtOctahedron:
                    res = new X3DExtOctahedron<TY>();
                    break;
                case X3DPrimitiveType.ExtTetrahedron:
                    res = new X3DExtTetrahedron<TY>();
                    break;
                case X3DPrimitiveType.ExtIcosahedron:
                    res = new X3DExtIcosahedron<TY>();
                    break;
                case X3DPrimitiveType.TrackerRectangle:
                    res = new X3DTrackerRectangle<TY>();
                    break;
                case X3DPrimitiveType.SelectionBox:
                    res = new X3DSelectionBox<TY>();
                    break;
                case X3DPrimitiveType.Crease:
                    res = new X3DCrease<TY>();
                    break;
                case X3DPrimitiveType.Text:
                    res = new X3DText<TY>();
                    break;
                case X3DPrimitiveType.Note:
                    res = new X3DNote<TY>();
                    break;
                case X3DPrimitiveType.FloatingNote:
                    res = new X3DFloatingNote<TY>();
                    break;
                case X3DPrimitiveType.Arrow:
                    res = new X3DArrow<TY>();
                    break;
                case X3DPrimitiveType.Dimension:
                    res = new X3DDimension<TY>();
                    break;
                case X3DPrimitiveType.Rotator:
                    res = new X3DRotator<TY>();
                    break;
                case X3DPrimitiveType.LockPoint:
                    res = new X3DLockPoint<TY>();
                    break;
                case X3DPrimitiveType.PolyCylinder:
                    res = new X3DPolyCylinder<TY>();
                    break;
                case X3DPrimitiveType.PolyCone:
                    res = new X3DPolyCone<TY>();
                    break;
                case X3DPrimitiveType.PathExtrusion:
                    res = new X3DPathExtrusion<TY>();
                    break;
                case X3DPrimitiveType.TwistExtrusion:
                    res = new X3DTwistExtrusion<TY>();
                    break;
                case X3DPrimitiveType.SuperExtrusion:
                    res = new X3DSuperExtrusion<TY>();
                    break;
                case X3DPrimitiveType.SpiralExtrusion:
                    res = new X3DSpiralExtrusion<TY>();
                    break;
                case X3DPrimitiveType.Lathe:
                    res = new X3DLathe<TY>();
                    break;
                case X3DPrimitiveType.Helicoid:
                    res = new X3DHelicoid<TY>();
                    break;
                case X3DPrimitiveType.Toroid:
                    res = new X3DToroid<TY>();
                    break;
                case X3DPrimitiveType.ScrewExtrusion:
                    res = new X3DScrewExtrusion<TY>();
                    break;
                case X3DPrimitiveType.Decal:
                    res = new X3DDecal<TY>();
                    break;
                case X3DPrimitiveType.GrabHandle:
                    res = new X3DGrabHandle<TY>();
                    break;
                case X3DPrimitiveType.Artwork:
                    res = new X3DArtwork<TY>();
                    break;
                case X3DPrimitiveType.ArtworkMarker:
                    res = new X3DArtworkMarker<TY>();
                    break;
                case X3DPrimitiveType.Callout:
                    res = new X3DCallout<TY>();
                    break;
                case X3DPrimitiveType.ManualParticleSystem:
                    res = new X3DManualParticleSystem<TY>();
                    break;
                case X3DPrimitiveType.Smoke:
                    res = new X3DSmoke<TY>();
                    break;
                case X3DPrimitiveType.BitmapExplosion:
                    res = new X3DBitmapExplosion<TY>();
                    break;
                case X3DPrimitiveType.Bubbles:
                    res = new X3DBubbles<TY>();
                    break;
                case X3DPrimitiveType.Snow:
                    res = new X3DSnow<TY>();
                    break;
                case X3DPrimitiveType.PolyCurve:
                    res = new X3DPolyCurve<TY>();
                    break;
                case X3DPrimitiveType.ClosedPolyCurve:
                    res = new X3DClosedPolyCurve<TY>();
                    break;
                case X3DPrimitiveType.DeformationGrabBag:
                    res = new X3DDeformationGrabBag<TY>();
                    break;
            }
            if (null != res)
            {
                res.PType = type;
                res.ID = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_REF);
                res.Create(element, reader);
            }
            return res;
        }
    }

    /// <summary>
    /// Base class for all primitives with vertices
    /// </summary>
    /// <typeparam name="TY">The numeric type</typeparam>
    public class X3DPrimitiveVertices<TY> : X3DPrimitive<TY>
    {
        /// <summary>
        /// The vertex data
        /// </summary>
        public X3DVertexData<TY> VertexData = new X3DVertexData<TY>();

        /// <summary>
        /// Create this primitive from XML
        /// </summary>
        /// <param name="element"></param>
        /// <param name="reader"></param>
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            var vertices = element.SelectSingleNode(X3DTOK.X3DTOK_PVVERTICES) as XmlElement;
            VertexData.NumVertices = X3DBaseReader.Get<int>(vertices, X3DTOK.X3DTOK_PVS);
            VertexData.RenderSort = X3DBaseReader.GetEnum<X3DRenderSort>(vertices, X3DTOK.X3DTOK_PVST);
            if (X3DBaseReader.Exists(vertices, X3DTOK.X3DTOK_PVE))
                VertexData.Edges = reader.GetParts<bool>(vertices, X3DTOK.X3DTOK_PVE);
            if (X3DBaseReader.Exists(vertices, X3DTOK.X3DTOK_PVC))
                VertexData.EdgeColours = reader.GetParts<TY>(vertices, X3DTOK.X3DTOK_PVC);
            VertexData.VertexData = reader.GetParts<TY>(vertices, X3DTOK.X3DTOK_PVD);
            if (X3DBaseReader.Exists(vertices, X3DTOK.X3DTOK_PVTC))
                VertexData.TextureCoords = reader.GetParts<TY>(vertices, X3DTOK.X3DTOK_PVTC);
            if (X3DBaseReader.Exists(vertices, X3DTOK.X3DTOK_TEXMAPTYPE))
            {
                VertexData.TextureMap.T = X3DBaseReader.GetEnum<X3DTextureMapType>(vertices, X3DTOK.X3DTOK_TEXMAPTYPE);
                VertexData.TextureMap.U1 = X3DBaseReader.Get<TY>(vertices, X3DTOK.X3DTOK_U1);
                VertexData.TextureMap.V1 = X3DBaseReader.Get<TY>(vertices, X3DTOK.X3DTOK_V1);
                VertexData.TextureMap.U2 = X3DBaseReader.Get<TY>(vertices, X3DTOK.X3DTOK_U2);
                VertexData.TextureMap.V2 = X3DBaseReader.Get<TY>(vertices, X3DTOK.X3DTOK_V2);
            }
            else
            {
                VertexData.TextureMap.T = X3DTextureMapType.None;
            }
        }
    }

    public class X3DLineLoop<TY> : X3DPrimitiveVertices<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DLineStrip<TY> : X3DPrimitiveVertices<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DLines<TY> : X3DPrimitiveVertices<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DPoints<TY> : X3DPrimitiveVertices<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DPolygon<TY> : X3DPrimitiveVertices<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DQuads<TY> : X3DPrimitiveVertices<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DQuadStrip<TY> : X3DPrimitiveVertices<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DTriangles<TY> : X3DPrimitiveVertices<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DTriangleFan<TY> : X3DPrimitiveVertices<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DTriangleStrip<TY> : X3DPrimitiveVertices<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DGrid<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  No nothing
        }
    }

    public class X3DAxes<TY> : X3DPrimitive<TY>
    {
        public X3DAxesData<TY> Data = new X3DAxesData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.Radius = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_R);
            Data.X = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_X);
            Data.Y = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_Y);
            Data.Z = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_Z);
            Data.ColourX = reader.GetColourRGB<TY>(element, X3DTOK.X3DTOK_CX);
            Data.ColourY = reader.GetColourRGB<TY>(element, X3DTOK.X3DTOK_CY);
            Data.ColourZ = reader.GetColourRGB<TY>(element, X3DTOK.X3DTOK_CZ);
            Data.ShowLetters = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_SHOWLETTERS);
        }
    }

    public class X3DHermiteSpline<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    public class X3DCatMullRomSpline<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    public class X3DBezierSpline<TY> : X3DPrimitiveVertices<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    public class X3DBezierSurface<TY> : X3DPrimitiveVertices<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DNurbsSurface<TY> : X3DPrimitiveVertices<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DNurbsTrim<TY> : X3DPrimitiveVertices<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DNurbsCurve<TY> : X3DPrimitiveVertices<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DNurbsCurveArray<TY> : X3DPrimitive<TY>
    {
        List<X3DNurbsCurve<TY>> Curves = new List<X3DNurbsCurve<TY>>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            foreach (XmlElement curve in element.SelectNodes(X3DTOK.X3DTOK_CURVE))
            {
                var _curve = new X3DNurbsCurve<TY>();
                _curve.Create(curve, reader);
                Curves.Add(_curve);
            }
        }
    }

    public class X3DNurbsTrimArray<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    public class X3DPwlCurve<TY> : X3DPrimitiveVertices<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DPwlTrim<TY> : X3DPrimitiveVertices<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DComplexNurbsSurface<TY> : X3DPrimitiveVertices<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DMesh<TY> : X3DPrimitiveVertices<TY>
    {
        public X3DMeshData Data = new X3DMeshData();
        public bool Wall;

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
            var mesh = element.SelectSingleNode(X3DTOK.X3DTOK_MESH) as XmlElement;
            Data.MeshFaceType = X3DBaseReader.GetEnum<X3DMeshFaceType>(mesh, X3DTOK.X3DTOK_MMF);
            Data.IndexCount = X3DBaseReader.Get<int>(mesh, X3DTOK.X3DTOK_MIC);
            Data.Indices = reader.GetParts<int>(mesh, X3DTOK.X3DTOK_MI);
            Data.SimpleMesh = Data.Indices.Length == 0;
            Wall = X3DBaseReader.Get<bool>(mesh, X3DTOK.X3DTOK_WALL);
        }
    }

    public class X3DLine<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    public class X3DRectangle<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    public class X3DBoxPrim<TY> : X3DPrimitive<TY>
    {
        public X3DBox<TY> Data = new X3DBox<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.V1 = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_BXV1);
            Data.V2 = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_BXV2);
        }
    }

    public class X3DPyramid<TY> : X3DPrimitive<TY>
    {
        public X3DPyramidData<TY> Data = new X3DPyramidData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.BR = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PYBR);
            Data.HT = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PYHT);
            Data.SL = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_PYSL);
            Data.ST = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_PYST);
        }
    }

    public class X3DCylinder<TY> : X3DPrimitive<TY>
    {
        public X3DCylinderData<TY> Data = new X3DCylinderData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.BR = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_CYBR);
            Data.TR = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_CYTR);
            Data.HT = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_CYHT);
            Data.SL = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_CYSL);
            Data.ST = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_CYST);
        }
    }

    public class X3DDisk<TY> : X3DPrimitive<TY>
    {
        public X3DDiskData<TY> Data = new X3DDiskData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.IR = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_DKIR);
            Data.OR = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_DKOR);
            Data.SL = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_DKSL);
            Data.LP = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_DKLP);
        }
    }

    public class X3DPartialDisk<TY> : X3DPrimitive<TY>
    {
        public X3DPartialDiskData<TY> Data = new X3DPartialDiskData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.IR = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PDIR);
            Data.OR = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PDOR);
            Data.SL = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_PDSL);
            Data.LP = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_PDLP);
            Data.SA = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PDSA);
            Data.SP = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PDSP);
        }
    }

    public class X3DSphere<TY> : X3DPrimitive<TY>
    {
        public X3DSphereData<TY> Data = new X3DSphereData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.RA = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_SPRA);
            Data.SL = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_SPSL);
            Data.ST = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_SPST);
        }
    }

    public class X3DExtSphere<TY> : X3DPrimitive<TY>
    {
        public X3DExtSphereData<TY> Data = new X3DExtSphereData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.RA = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ESRA);
            Data.SL = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_ESSL);
            Data.ST = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_ESST);
        }
    }

    public class X3DExtCone<TY> : X3DPrimitive<TY>
    {
        public X3DExtConeData<TY> Data = new X3DExtConeData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.BR = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ECBR);
            Data.HT = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ECHT);
            Data.SL = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_ECSL);
            Data.ST = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_ECST);
        }
    }

    public class X3DExtCube<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    public class X3DExtTorus<TY> : X3DPrimitive<TY>
    {
        public X3DExtTorusData<TY> Data = new X3DExtTorusData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.IR = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ETIR);
            Data.OR = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ETOR);
            Data.SD = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_ETSD);
            Data.RG = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_ETRG);
        }
    }

    public class X3DExtDodecahedron<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    public class X3DExtTeapot<TY> : X3DPrimitive<TY>
    {
        public X3DExtTeapotData<TY> Data = new X3DExtTeapotData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.SZ = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ETSZ);
        }
    }

    public class X3DExtOctahedron<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    public class X3DExtTetrahedron<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    public class X3DExtIcosahedron<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    public class X3DTrackerRectangle<TY> : X3DPrimitive<TY>
    {
        public X3DTrackerRectangleData<TY> Data = new X3DTrackerRectangleData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.P1 = reader.GetVector2<TY>(element, X3DTOK.X3DTOK_P1);
            Data.P2 = reader.GetVector2<TY>(element, X3DTOK.X3DTOK_P2);
        }
    }

    public class X3DSelectionBox<TY> : X3DPrimitive<TY>
    {
        public X3DBox<TY> Data = new X3DBox<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.V1 = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_P1);
            Data.V2 = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_P2);
        }
    }

    public class X3DCrease<TY> : X3DPrimitive<TY>
    {
        public X3DCreaseData<TY> Data = new X3DCreaseData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.ShapeGlue = reader.GetShapeGlue<TY>(element, X3DTOK.X3DTOK_CREASE, X3DShapeGlueType.JointedShapeGlue);
            Data.JointData.JointOffset = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_CREASE + X3DTOK.X3DTOKCOM_JO);
            Data.JointData.JointAxis = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_CREASE + X3DTOK.X3DTOKCOM_JA);
            Data.JointData.JointAngle = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_CREASE + X3DTOK.X3DTOKCOM_JG);
            Data.JointData.JointAxisLength = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_CREASE + X3DTOK.X3DTOKCOM_JL);
            Data.JointData.JointDisplayed = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_CREASE + X3DTOK.X3DTOKCOM_JD);
            Data.JointData.TexCoord = reader.GetParts<TY>(element, X3DTOK.X3DTOK_CREASE + X3DTOK.X3DTOKCOM_JT);
            Data.JointData.RenderJointAxis = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_CREASE + X3DTOK.X3DTOKCOM_JRA);
            Data.Index = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_CREASE + X3DTOK.X3DTOKCOM_INDEX);
        }
    }

    public class X3DText<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    public class X3DNote<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    public class X3DFloatingNote<TY> : X3DPrimitive<TY>
    {
        public X3DFloatingNoteData<TY> Data = new X3DFloatingNoteData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.NoteId = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_NOTEID);
            Data.Direction = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_NOTEDIRECTION);
            Data.Radius = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_NOTERADIUS);
            Data.CentreOffset = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_NOTECENTREOFFSET);
            Data.CX = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_NOTECX);
            Data.CY = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_NOTECY);
            Data.StartFrame = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_NOTESTARTFRAME);
            Data.CurrentFrame = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_NOTECURRENTFRAME);
            Data.EndFrame = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_NOTEENDFRAME);
        }
    }

    public class X3DArrow<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    public class X3DDimension<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    public class X3DRotator<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    public class X3DLockPoint<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    /// <summary>
    /// Base class for all GLE-based primitives
    /// </summary>
    /// <typeparam name="TY">The numeric type</typeparam>
    public abstract class X3DGLEPrimitive<TY> : X3DPrimitive<TY>
    {
    }

    public class X3DPolyCylinder<TY> : X3DGLEPrimitive<TY>
    {
        public X3DPolyCylinderData<TY> Data = new X3DPolyCylinderData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.JointStyle = X3DBaseReader.GetEnum<X3DJointStyle>(element, X3DTOK.X3DTOK_EJS);
            Data.NormalStyle = X3DBaseReader.GetEnum<X3DNormalStyle>(element, X3DTOK.X3DTOK_ENS);
            Data.TextureStyle = X3DBaseReader.GetEnum<X3DTextureStyle>(element, X3DTOK.X3DTOK_ETS);
            Data.Capped = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_ECP);
            Data.NumSides = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_ENU);
            Data.Points = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_EPS);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_EPA))
                Data.PointArray = reader.GetParts<TY>(element, X3DTOK.X3DTOK_EPA);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ECA))
                Data.ColourArray = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ECA);
            Data.Radius = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ERA);
        }
    }

    public class X3DPolyCone<TY> : X3DGLEPrimitive<TY>
    {
        public X3DPolyConeData<TY> Data = new X3DPolyConeData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.JointStyle = X3DBaseReader.GetEnum<X3DJointStyle>(element, X3DTOK.X3DTOK_EJS);
            Data.NormalStyle = X3DBaseReader.GetEnum<X3DNormalStyle>(element, X3DTOK.X3DTOK_ENS);
            Data.TextureStyle = X3DBaseReader.GetEnum<X3DTextureStyle>(element, X3DTOK.X3DTOK_ETS);
            Data.Capped = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_ECP);
            Data.NumSides = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_ENU);
            Data.Points = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_EPS);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_EPA))
                Data.PointArray = reader.GetParts<TY>(element, X3DTOK.X3DTOK_EPA);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ECA))
                Data.ColourArray = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ECA);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ERA))
                Data.RadiusArray = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ERA);
        }
    }

    public class X3DPathExtrusion<TY> : X3DGLEPrimitive<TY>
    {
        public X3DPathExtrusionData<TY> Data = new X3DPathExtrusionData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.JointStyle = X3DBaseReader.GetEnum<X3DJointStyle>(element, X3DTOK.X3DTOK_EJS);
            Data.NormalStyle = X3DBaseReader.GetEnum<X3DNormalStyle>(element, X3DTOK.X3DTOK_ENS);
            Data.TextureStyle = X3DBaseReader.GetEnum<X3DTextureStyle>(element, X3DTOK.X3DTOK_ETS);
            Data.Capped = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_ECP);
            Data.Closed = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_ECL);
            Data.ContourPoints = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_ECTP);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ECT))
                Data.Contour = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ECT);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ECN))
                Data.ContourNormals = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ECN);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_EUP))
                Data.Up = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_EUP);
            Data.PolylinePoints = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_EPLP);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_EPL))
                Data.PolylineVertices = reader.GetParts<TY>(element, X3DTOK.X3DTOK_EPL);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ECA))
                Data.ColourArray = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ECA);
        }
    }

    public class X3DTwistExtrusion<TY> : X3DGLEPrimitive<TY>
    {
        public X3DTwistExtrusionData<TY> Data = new X3DTwistExtrusionData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.JointStyle = X3DBaseReader.GetEnum<X3DJointStyle>(element, X3DTOK.X3DTOK_EJS);
            Data.NormalStyle = X3DBaseReader.GetEnum<X3DNormalStyle>(element, X3DTOK.X3DTOK_ENS);
            Data.TextureStyle = X3DBaseReader.GetEnum<X3DTextureStyle>(element, X3DTOK.X3DTOK_ETS);
            Data.Capped = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_ECP);
            Data.Closed = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_ECL);
            Data.ContourPoints = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_ECTP);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ECT))
                Data.Contour = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ECT);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ECN))
                Data.ContourNormals = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ECN);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_EUP))
                Data.Up = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_EUP);
            Data.PolylinePoints = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_EPLP);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_EPL))
                Data.PolylineVertices = reader.GetParts<TY>(element, X3DTOK.X3DTOK_EPL);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ECA))
                Data.ColourArray = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ECA);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ETW))
                Data.TwistArray = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ETW);
        }
    }

    public class X3DSuperExtrusion<TY> : X3DGLEPrimitive<TY>
    {
        public X3DSuperExtrusionData<TY> Data = new X3DSuperExtrusionData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.JointStyle = X3DBaseReader.GetEnum<X3DJointStyle>(element, X3DTOK.X3DTOK_EJS);
            Data.NormalStyle = X3DBaseReader.GetEnum<X3DNormalStyle>(element, X3DTOK.X3DTOK_ENS);
            Data.TextureStyle = X3DBaseReader.GetEnum<X3DTextureStyle>(element, X3DTOK.X3DTOK_ETS);
            Data.Capped = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_ECP);
            Data.Closed = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_ECL);
            Data.ContourPoints = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_ECTP);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ECT))
                Data.Contour = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ECT);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ECN))
                Data.ContourNormals = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ECN);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_EUP))
                Data.Up = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_EUP);
            Data.PolylinePoints = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_EPLP);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_EPL))
                Data.PolylineVertices = reader.GetParts<TY>(element, X3DTOK.X3DTOK_EPL);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ECA))
                Data.ColourArray = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ECA);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ESA))
                Data.ScaleArray = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ESA);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ETA))
                Data.TwistArray = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ETA);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_EXA))
                Data.TransformArray = reader.GetParts<TY>(element, X3DTOK.X3DTOK_EXA);
            Data.NumSides = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_ENU);
        }
    }

    public class X3DSpiralExtrusion<TY> : X3DGLEPrimitive<TY>
    {
        public X3DSpiralExtrusionData<TY> Data = new X3DSpiralExtrusionData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.JointStyle = X3DBaseReader.GetEnum<X3DJointStyle>(element, X3DTOK.X3DTOK_EJS);
            Data.NormalStyle = X3DBaseReader.GetEnum<X3DNormalStyle>(element, X3DTOK.X3DTOK_ENS);
            Data.TextureStyle = X3DBaseReader.GetEnum<X3DTextureStyle>(element, X3DTOK.X3DTOK_ETS);
            Data.Capped = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_ECP);
            Data.Closed = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_ECL);
            Data.ContourPoints = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_ECTP);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ECT))
                Data.Contour = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ECT);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ECN))
                Data.ContourNormals = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ECN);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_EUP))
                Data.Up = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_EUP);
            Data.StartRadius = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ESR);
            Data.DRDTheta = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_EDRT);
            Data.StartZ = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ESZ);
            Data.DZDTheta = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_EDZT);
            Data.StartTransform = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ESX);
            Data.TransformDTheta = reader.GetParts<TY>(element, X3DTOK.X3DTOK_EDFD);
            Data.StartTheta = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_EST);
            Data.SweepTheta = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_EWT);
        }
    }

    public class X3DLathe<TY> : X3DGLEPrimitive<TY>
    {
        public X3DLatheData<TY> Data = new X3DLatheData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.JointStyle = X3DBaseReader.GetEnum<X3DJointStyle>(element, X3DTOK.X3DTOK_EJS);
            Data.NormalStyle = X3DBaseReader.GetEnum<X3DNormalStyle>(element, X3DTOK.X3DTOK_ENS);
            Data.TextureStyle = X3DBaseReader.GetEnum<X3DTextureStyle>(element, X3DTOK.X3DTOK_ETS);
            Data.Capped = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_ECP);
            Data.Closed = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_ECL);
            Data.ContourPoints = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_ECTP);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ECT))
                Data.Contour = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ECT);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ECN))
                Data.ContourNormals = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ECN);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_EUP))
                Data.Up = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_EUP);
            Data.StartRadius = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ESR);
            Data.DRDTheta = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_EDRT);
            Data.StartZ = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ESZ);
            Data.DZDTheta = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_EDZT);
            Data.StartTransform = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ESX);
            Data.DTransformDTheta = reader.GetParts<TY>(element, X3DTOK.X3DTOK_EDFD);
            Data.StartTheta = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_EST);
            Data.SweepTheta = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_EWT);
            Data.NumSides = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_ENU);
        }
    }

    public class X3DHelicoid<TY> : X3DGLEPrimitive<TY>
    {
        public X3DHelicoidData<TY> Data = new X3DHelicoidData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.JointStyle = X3DBaseReader.GetEnum<X3DJointStyle>(element, X3DTOK.X3DTOK_EJS);
            Data.NormalStyle = X3DBaseReader.GetEnum<X3DNormalStyle>(element, X3DTOK.X3DTOK_ENS);
            Data.TextureStyle = X3DBaseReader.GetEnum<X3DTextureStyle>(element, X3DTOK.X3DTOK_ETS);
            Data.Capped = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_ECP);
            Data.RTOROID = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ERT);
            Data.StartRadius = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ESR);
            Data.DRDTheta = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_EDT);
            Data.StartZ = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ESZ);
            Data.DZDTheta = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_EZT);
            Data.StartTransform = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ESX);
            Data.DTransformDTheta = reader.GetParts<TY>(element, X3DTOK.X3DTOK_EXT);
            Data.StartTheta = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_EST);
            Data.SweepTheta = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_EWT);
        }
    }

    public class X3DToroid<TY> : X3DGLEPrimitive<TY>
    {
        public X3DToroidData<TY> Data = new X3DToroidData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.JointStyle = X3DBaseReader.GetEnum<X3DJointStyle>(element, X3DTOK.X3DTOK_EJS);
            Data.NormalStyle = X3DBaseReader.GetEnum<X3DNormalStyle>(element, X3DTOK.X3DTOK_ENS);
            Data.TextureStyle = X3DBaseReader.GetEnum<X3DTextureStyle>(element, X3DTOK.X3DTOK_ETS);
            Data.Capped = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_ECP);
            Data.RTOROID = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ERT);
            Data.StartRadius = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ESR);
            Data.DRDTheta = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_EDT);
            Data.StartZ = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ESZ);
            Data.DZDTheta = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_EZT);
            Data.StartTransform = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ESX);
            Data.DTransformDTheta = reader.GetParts<TY>(element, X3DTOK.X3DTOK_EXT);
            Data.StartTheta = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_EST);
            Data.SweepTheta = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_EWT);
        }
    }

    public class X3DScrewExtrusion<TY> : X3DGLEPrimitive<TY>
    {
        public X3DScrewExtrusionData<TY> Data = new X3DScrewExtrusionData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.JointStyle = X3DBaseReader.GetEnum<X3DJointStyle>(element, X3DTOK.X3DTOK_EJS);
            Data.NormalStyle = X3DBaseReader.GetEnum<X3DNormalStyle>(element, X3DTOK.X3DTOK_ENS);
            Data.TextureStyle = X3DBaseReader.GetEnum<X3DTextureStyle>(element, X3DTOK.X3DTOK_ETS);
            Data.Capped = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_ECP);
            Data.Closed = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_ECL);
            Data.ContourPoints = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_ECTP);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ECT))
                Data.Contour = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ECT);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_ECN))
                Data.ContourNormals = reader.GetParts<TY>(element, X3DTOK.X3DTOK_ECN);
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_EUP))
                Data.Up = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_EUP);
            Data.StartZ = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ESZ);
            Data.EndZ = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_EEZ);
            Data.Twist = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_ETW);
        }
    }

    public class X3DDecal<TY> : X3DPrimitive<TY>
    {
        public X3DDecalData<TY> Data = new X3DDecalData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.Orientation = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_ORIENTATION);
            Data.Lit = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_LIT);
            Data.Size = reader.GetParts<TY>(element, X3DTOK.X3DTOK_SIZE);
        }
    }

    public class X3DGrabHandle<TY> : X3DPrimitive<TY>
    {
        public X3DGrabHandleData<TY> Data = new X3DGrabHandleData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.GrabHandleType = X3DBaseReader.GetEnum<X3DGrabHandleType>(element, X3DTOK.X3DTOK_GHT);
            Data.Size = reader.GetVector2<TY>(element, X3DTOK.X3DTOK_SIZE);
            Data.Offset = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_OFFSET);
        }
    }

    public class X3DArtwork<TY> : X3DPrimitive<TY>
    {
        public X3DArtworkData<TY> Data = new X3DArtworkData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.DrawBorder = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_BORDER);
            Data.Size = reader.GetVector2<TY>(element, X3DTOK.X3DTOK_SIZE);
            Data.Application = X3DBaseReader.GetEnum<X3DArtworkApplication>(element, X3DTOK.X3DTOK_ARTWORKAPPLICATION);
            Data.MirrorX = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_MIRRORX);
            Data.MirrorY = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_MIRRORY);
            Data.Proportional = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_PROPORTIONAL);
        }
    }

    public class X3DArtworkMarker<TY> : X3DPrimitive<TY>
    {
        public X3DArtworkMarkerData<TY> Data = new X3DArtworkMarkerData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.MarkerStyle = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_MARKERSTYLE);
            Data.Size = reader.GetVector2<TY>(element, X3DTOK.X3DTOK_SIZE);
        }
    }

    public class X3DCallout<TY> : X3DPrimitive<TY>
    {
        public X3DCalloutData<TY> Data = new X3DCalloutData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.SZ = reader.GetVector2<TY>(element, X3DTOK.X3DTOK_SIZE);
            Data.P1 = reader.GetVector2<TY>(element, X3DTOK.X3DTOK_P1);
            Data.P2 = reader.GetVector2<TY>(element, X3DTOK.X3DTOK_P2);
        }
    }

    /// <summary>
    /// Base class for all particle systems (based on quads)
    /// </summary>
    /// <typeparam name="TY">The numeric type</typeparam>
    public class X3DParticleSystemPrimitive<TY> : X3DQuads<TY>
    {
        public X3DParticleSystemData<TY> Data = new X3DParticleSystemData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
            Data.ParticleSystemType = X3DBaseReader.GetEnum<X3DParticleSystemType>(element, X3DTOK.X3DTOK_PT);
            Data.ParticleCount = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_PC);
            Data.Positions = reader.GetParts<TY>(element, X3DTOK.X3DTOK_PP);
            Data.OldPositions = reader.GetParts<TY>(element, X3DTOK.X3DTOK_PO);
            Data.Velocities = reader.GetParts<TY>(element, X3DTOK.X3DTOK_PV);
            Data.Colours = reader.GetParts<TY>(element, X3DTOK.X3DTOK_PL);
            Data.Energies = reader.GetParts<int>(element, X3DTOK.X3DTOK_PE);
            Data.Sizes = reader.GetParts<TY>(element, X3DTOK.X3DTOK_PS);
            Data.Min = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_PM);
            Data.Max = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_PX);
            Data.Active = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_PA);
        }
    }

    public class X3DManualParticleSystem<TY> : X3DParticleSystemPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DSmoke<TY> : X3DParticleSystemPrimitive<TY>
    {
        public X3DSmokeData<TY> SmokeData = new X3DSmokeData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
            SmokeData.ScaleSpeed = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PSS);
            SmokeData.EnergySpeed = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PSE);
            SmokeData.WindDistortFactor = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PSF);
            SmokeData.WindDirection = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_PSD);
            SmokeData.KeepSmokeAlive = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_PSA);
        }
    }

    public class X3DBitmapExplosion<TY> : X3DParticleSystemPrimitive<TY>
    {
        public X3DBitmapExplosionData<TY> BitmapExplosionData = new X3DBitmapExplosionData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
            BitmapExplosionData.Size = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PBS);
            BitmapExplosionData.EnergySpeed = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PBE);
            BitmapExplosionData.AnimSpeed = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PBA);
        }
    }

    public class X3DBubbles<TY> : X3DParticleSystemPrimitive<TY>
    {
        public X3DBubblesData<TY> BubblesData = new X3DBubblesData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
            BubblesData.MaxDist = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PBM);
            BubblesData.WindDistortFactor = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PBF);
            BubblesData.WindDirection = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_PBD);
        }
    }

    public class X3DSnow<TY> : X3DParticleSystemPrimitive<TY>
    {
        public X3DSnowData<TY> SnowData = new X3DSnowData<TY>();

        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
            SnowData.Radius = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PNR);
            SnowData.Top = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PNT);
            SnowData.Bottom = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PNB);
            SnowData.Distortion = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PNF);
            SnowData.Speed = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_PNS);
            SnowData.WindDirection = reader.GetVector3<TY>(element, X3DTOK.X3DTOK_PND);
        }
    }

    public class X3DPolyCurve<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    public class X3DClosedPolyCurve<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }

    public class X3DDeformationGrabBag<TY> : X3DPrimitive<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            //  Do nothing
        }
    }
}
