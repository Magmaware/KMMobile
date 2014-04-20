using System;
using System.Collections.Generic;
using System.Xml;

namespace KMMobile.X3D
{
    /// <summary>
    /// The list of types of shapes
    /// </summary>
    public enum X3DShapeType : uint
    {
        Basic,
        Group,
        Physical,
        Jointed,
        Billboard,
        LodGroup,
        VertexAnimation,
        LayerManager,
        Manipulator,
        ArtworkParent,
        Curved
    }
    /// <summary>
    /// The wall type
    /// </summary>
    public enum X3DWallType
    {
        /// <summary>
        /// Not a wall
        /// </summary>
        None = -1,
        /// <summary>
        /// Has no thickness
        /// </summary>
        NoThickness = 0,
        /// <summary>
        /// A single wall (folds around the centre)
        /// </summary>
        SingleWall = 1,
        /// <summary>
        /// A double wall (folds around a given offset)
        /// </summary>
        DoubleWall = 2,
        /// <summary>
        /// Xanita (folds around the outside)
        /// </summary>
        Xanita = 3
    }

    /// <summary>
    /// Base class for all shapes
    /// </summary>
    /// <typeparam name="TY">The numeric type of the primitive</typeparam>
    public class X3DShape<TY>
    {
        /// <summary>
        /// The ID of the shape
        /// </summary>
        public int ID;

        /// <summary>
        /// A dictionary of sub-shapes
        /// </summary>
        public List<KeyValuePair<X3DShapeGlue<TY>, X3DShape<TY>>> SubShapes = new List<KeyValuePair<X3DShapeGlue<TY>, X3DShape<TY>>>();

        /// <summary>
        /// A dictionary of primitives
        /// </summary>
        public List<KeyValuePair<X3DPrimitiveGlue<TY>, X3DPrimitive<TY>>> Primitives = new List<KeyValuePair<X3DPrimitiveGlue<TY>, X3DPrimitive<TY>>>();

        public X3DShapeType ShapeType;
        public TY InnerThickness;
        public TY OuterThickness;
        public X3DShape<TY> AssociatedArtwork;
        public X3DWallType WallType;
        public bool Foldable;
        public TY SceneScale;
        public X3DOrigami<TY> Origami;
        public X3DVertexData<TY>[] Contours;
        public X3DFloatingNote<TY>[] Notes;
        public X3DDeformation<TY>[] Deformations;

        /// <summary>
        /// Overridden in all derived classes to load the element from its attributes
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="reader">The reader</param>
        public virtual void Create(XmlElement element, X3DBaseReader reader)
        {
            ID = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_SREF);

            ShapeType = X3DBaseReader.GetEnum<X3DShapeType>(element, X3DTOK.X3DTOK_TYPEID);
            InnerThickness = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_STHICKNESS);
            OuterThickness = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_STHICKNESSRATIO);

            int assoc = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_ASSOCARTWORK);
            if (assoc > 0)
                AssociatedArtwork = (reader as X3DFormatReader<TY>).Artwork[assoc];
            var wall = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_WALL);
            if (wall)
            {
                var wallType = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_WALLTYPE);
                switch (wallType)
                {
                    case 0:
                        WallType = X3DWallType.NoThickness;
                        break;
                    case 1:
                        WallType = X3DWallType.SingleWall;
                        break;
                    case 2:
                        WallType = X3DWallType.DoubleWall;
                        break;
                    case 3:
                        WallType = X3DWallType.Xanita;
                        break;
                }
            }
            else
            {
                WallType = X3DWallType.None;
            }
            Foldable = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_FOLDABLE);
            SceneScale = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_SCENESCALE);
            if (0.0 == (double)Convert.ChangeType(SceneScale, typeof(double)))
                SceneScale = (TY)Convert.ChangeType(1.0, typeof(TY));

            var primitives = element.SelectSingleNode(X3DTOK.X3DTOK_SPRIMITIVEREFS) as XmlElement;
            if (null != primitives)
            {
                foreach (XmlElement primitive in primitives.SelectNodes(X3DTOK.X3DTOK_SPRIMITIVEREF))
                {
                    var pref = X3DBaseReader.Get<int>(primitive, X3DTOK.X3DTOK_SREF);
                    var primitiveglue = primitive.SelectSingleNode(X3DTOK.X3DTOK_SGLUE) as XmlElement;
                    if (null != primitiveglue)
                    {
                        var glue = reader.GetPrimitiveGlue<TY>(primitiveglue, X3DTOK.X3DTOK_SGLUE);
                        var animation = primitiveglue.SelectSingleNode(X3DTOK.X3DTOK_ANIMATION) as XmlElement;
                        if (null != animation)
                        {
                            var anim = new X3DPrimitiveAnimation<TY>();
                            anim.Create(animation, reader);
                            glue.Animation = anim;
                        }
                        Primitives.Add(
                        new KeyValuePair<X3DPrimitiveGlue<TY>, X3DPrimitive<TY>>(
                        glue, (reader as X3DFormatReader<TY>).Primitives[pref]));
                    }
                }
            }

            var shapes = element.SelectSingleNode(X3DTOK.X3DTOK_SSHAPEREFS) as XmlElement;
            if (null != shapes)
            {
                foreach (XmlElement shape in shapes.SelectNodes(X3DTOK.X3DTOK_SSHAPEREF))
                {
                    var sref = X3DBaseReader.Get<int>(shape, X3DTOK.X3DTOK_SREF);
                    var _shape = (reader as X3DFormatReader<TY>).Shapes[sref];
                    var shapeglue = shape.SelectSingleNode(X3DTOK.X3DTOK_SGLUE) as XmlElement;
                    if (null != shapeglue)
                    {
                        switch (ShapeType)
                        {
                            case X3DShapeType.Basic:
                            case X3DShapeType.Group:
                                {
                                    var glue =
                                    reader.GetShapeGlue<TY>(shapeglue, X3DTOK.X3DTOK_SGLUE, X3DShapeGlueType.ShapeGlue);
                                    var animation =
                                    shapeglue.SelectSingleNode(X3DTOK.X3DTOK_ANIMATION) as XmlElement;
                                    if (null != animation)
                                    {
                                        var anim = new X3DShapeAnimation<TY>();
                                        anim.Create(animation, reader);
                                        glue.Animation = anim;
                                    }
                                    //  TODO: Deformations
                                    SubShapes.Add(
                                    new KeyValuePair<X3DShapeGlue<TY>, X3DShape<TY>>(
                                    glue, _shape));
                                    break;
                                }
                            case X3DShapeType.VertexAnimation:
                            case X3DShapeType.Billboard:
                            case X3DShapeType.ArtworkParent:
                                {
                                    var glue =
                                    reader.GetShapeGlue<TY>(shapeglue, X3DTOK.X3DTOK_SGLUE, X3DShapeGlueType.ShapeGlue);
                                    SubShapes.Add(
                                    new KeyValuePair<X3DShapeGlue<TY>, X3DShape<TY>>(
                                    glue, _shape));
                                    break;
                                }
                            case X3DShapeType.Physical:
                                {
                                    var glue =
                                    reader.GetShapeGlue<TY>(shapeglue, X3DTOK.X3DTOK_SGLUE, X3DShapeGlueType.PhysicalShapeGlue);
                                    SubShapes.Add(
                                    new KeyValuePair<X3DShapeGlue<TY>, X3DShape<TY>>(
                                    glue, _shape));
                                    break;
                                }
                            case X3DShapeType.Jointed:
                                {
                                    var glue =
                                    reader.GetShapeGlue<TY>(shapeglue, X3DTOK.X3DTOK_SGLUE, X3DShapeGlueType.JointedShapeGlue);
                                    var animation =
                                    shapeglue.SelectSingleNode(X3DTOK.X3DTOK_ANIMATION) as XmlElement;
                                    if (null != animation)
                                    {
                                        var anim = new X3DJointedSystemAnimation<TY>();
                                        anim.Create(animation, reader);
                                        glue.Animation = anim;
                                    }
                                    //  TODO: Curve wall deformations
                                    SubShapes.Add(
                                    new KeyValuePair<X3DShapeGlue<TY>, X3DShape<TY>>(
                                    glue, _shape));
                                    break;
                                }
                            case X3DShapeType.Curved:
                                {
                                    var glue =
                                    reader.GetShapeGlue<TY>(shapeglue, X3DTOK.X3DTOK_SGLUE, X3DShapeGlueType.JointedShapeGlue);
                                    SubShapes.Add(
                                    new KeyValuePair<X3DShapeGlue<TY>, X3DShape<TY>>(
                                    glue, _shape));
                                    break;
                                }
                            case X3DShapeType.LodGroup:
                                {
                                    var glue =
                                    reader.GetShapeGlue<TY>(shapeglue, X3DTOK.X3DTOK_SGLUE, X3DShapeGlueType.LodGlue);
                                    SubShapes.Add(
                                    new KeyValuePair<X3DShapeGlue<TY>, X3DShape<TY>>(
                                    glue, _shape));
                                    break;
                                }
                            case X3DShapeType.LayerManager:
                                {
                                    var glue =
                                    reader.GetShapeGlue<TY>(shapeglue, X3DTOK.X3DTOK_SGLUE, X3DShapeGlueType.LayerGlue);
                                    SubShapes.Add(
                                    new KeyValuePair<X3DShapeGlue<TY>, X3DShape<TY>>(
                                    glue, _shape));
                                    break;
                                }
                        }
                    }
                    else
                    {
                        SubShapes.Add(
                        new KeyValuePair<X3DShapeGlue<TY>, X3DShape<TY>>(
                        null, _shape));
                    }
                }
            }

            var contours = element.SelectSingleNode(X3DTOK.X3DTOK_CONTOURS) as XmlElement;
            if (null != contours)
            {
                var _contours = contours.SelectNodes(X3DTOK.X3DTOK_CONTOUR);
                Contours = new X3DVertexData<TY>[_contours.Count];
                for (int i = 0; i < _contours.Count; i++)
                {
                    var vd = new X3DVertexData<TY>() { NumVertices = X3DBaseReader.Get<int>(_contours[i] as XmlElement, X3DTOK.X3DTOK_PVS), RenderSort = X3DBaseReader.GetEnum<X3DRenderSort>(_contours[i] as XmlElement, X3DTOK.X3DTOK_PVST) };
                    if (X3DBaseReader.Exists(_contours[i] as XmlElement, X3DTOK.X3DTOK_PVE))
                        vd.Edges = reader.GetParts<bool>(_contours[i] as XmlElement, X3DTOK.X3DTOK_PVE);
                    if (X3DBaseReader.Exists(_contours[i] as XmlElement, X3DTOK.X3DTOK_PVC))
                        vd.EdgeColours = reader.GetParts<TY>(_contours[i] as XmlElement, X3DTOK.X3DTOK_PVC);
                    vd.VertexData = reader.GetParts<TY>(_contours[i] as XmlElement, X3DTOK.X3DTOK_PVD);
                    Contours[i] = vd;
                }
            }

            var notebag = element.SelectSingleNode(X3DTOK.X3DTOK_NOTEBAG) as XmlElement;
            if (null != notebag)
            {
                var notes = notebag.SelectNodes(X3DTOK.X3DTOK_NOTE);
                Notes = new X3DFloatingNote<TY>[notes.Count];
                for (int i = 0; i < notes.Count; i++)
                {
                    var note = new X3DFloatingNote<TY>();
                    note.Create(notes[i] as XmlElement, reader);
                    Notes[i] = note;
                }
            }

            var deformations = element.SelectSingleNode(X3DTOK.X3DTOK_DEFORMATIONS) as XmlElement;
            if (null != deformations)
            {
                var deforms = deformations.ChildNodes;
                Deformations = new X3DDeformation<TY>[deforms.Count];
                for (int i = 0; i < deforms.Count; i++)
                {
                    X3DDeformation<TY> deformation = null;
                    switch (deforms[i].Name)
                    {
                        case X3DTOK.X3DTOK_FREEFORM222DEFORMATION:
                            deformation = new X3DFreeform222Deformation<TY>();
                            break;
                        case X3DTOK.X3DTOK_FREEFORM333DEFORMATION:
                            deformation = new X3DFreeform333Deformation<TY>();
                            break;
                        case X3DTOK.X3DTOK_FREEFORM444DEFORMATION:
                            deformation = new X3DFreeform444Deformation<TY>();
                            break;
                        case X3DTOK.X3DTOK_BENDDEFORMATION:
                            deformation = new X3DBendDeformation<TY>();
                            break;
                        case X3DTOK.X3DTOK_CURVEWALLDEFORMATION:
                            deformation = new X3DCurveWallDeformation<TY>();
                            break;
                        default:
                            break;
                    }
                    if (null != deformation)
                    {
                        deformation.Create(deforms[i] as XmlElement, reader);
                        Deformations[i] = deformation;
                    }
                }
            }
        }

        /// <summary>
        /// Creates the origami for this shape
        /// </summary>
        /// <param name="element">The element which may contain origami info</param>
        /// <param name="reader">The reader</param>
        public void CreateOrigami(XmlElement element, X3DBaseReader reader)
        {
            var origami = element.SelectSingleNode(X3DTOK.X3DTOK_ORIGAMI) as XmlElement;
            if (null != origami)
            {
                Origami = new X3DOrigami<TY>();
                Origami.Create(origami, reader);
            }
        }

        /// <summary>
        /// Virtual constructor -- creates a shape from an XML element
        /// </summary>
        /// <param name="element">The XML element</param>
        /// <param name="reader">The reader used to parse the XML</param>
        /// <returns>The shape</returns>
        public static X3DShape<TY> Construct(XmlElement element, X3DBaseReader reader)
        {
            var st = X3DBaseReader.GetEnum<X3DShapeType>(element, X3DTOK.X3DTOK_TYPEID);
            X3DShape<TY> res = null;
            switch (st)
            {
                case X3DShapeType.Basic:
                    res = new X3DShape<TY>();
                    break;
                case X3DShapeType.Group:
                    res = new X3DGroup<TY>();
                    break;
                case X3DShapeType.ArtworkParent:
                    res = new X3DArtworkParent<TY>();
                    break;
                case X3DShapeType.Billboard:
                    res = new X3DBillboard<TY>();
                    break;
                case X3DShapeType.Physical:
                    res = new X3DPhysicalSystem<TY>();
                    break;
                case X3DShapeType.Jointed:
                    res = new X3DJointedSystem<TY>();
                    break;
                case X3DShapeType.Curved:
                    res = new X3DCurvedSystem<TY>();
                    break;
                case X3DShapeType.LodGroup:
                    res = new X3DLodGroup<TY>();
                    break;
                case X3DShapeType.VertexAnimation:
                    res = new X3DVertexAnimation<TY>();
                    break;
                case X3DShapeType.LayerManager:
                    res = new X3DLayerManager<TY>();
                    break;
                case X3DShapeType.Manipulator:
                    res = new X3DManipulator<TY>();
                    break;
            }
            if (null != res)
            {
                res.ID = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_REF);
                res.Create(element, reader);
            }
            return res;
        }

        /// <summary>
        /// Find the glue which connects this parent and the given child
        /// </summary>
        /// <param name="child">The child</param>
        /// <returns>The shape glue</returns>
        public X3DShapeGlue<TY> FindGlue(X3DShape<TY> child)
        {
            foreach (var subshape in SubShapes)
                if (subshape.Value == child)
                    return subshape.Key;
            return null;
        }
    }

    public class X3DGroup<TY> : X3DShape<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DArtworkParent<TY> : X3DShape<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DBillboard<TY> : X3DShape<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DPhysicalSystem<TY> : X3DShape<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DJointedSystem<TY> : X3DShape<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DCurvedSystem<TY> : X3DShape<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DLodGroup<TY> : X3DShape<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DVertexAnimation<TY> : X3DShape<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DLayerManager<TY> : X3DShape<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }

    public class X3DManipulator<TY> : X3DShape<TY>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
        }
    }
}
