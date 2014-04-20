using System;

namespace KMMobile.X3D
{
    /// <summary>
    /// An RGB colour
    /// </summary>
    /// <typeparam name="T">Numeric type</typeparam>
    public struct X3DColourRGB<T>
    {
        public T R, G, B;
    }

    /// <summary>
    /// An RGBA colour
    /// </summary>
    /// <typeparam name="T">Numeric type</typeparam>
    public struct X3DColourRGBA<T>
    {
        public T R, G, B, A;
    }

    /// <summary>
    /// A 2D vector
    /// </summary>
    /// <typeparam name="T">Numeric type</typeparam>
    public struct X3DVector2<T>
    {
        public T X, Y;
    }

    /// <summary>
    /// A 3D vector
    /// </summary>
    /// <typeparam name="T">Numeric type</typeparam>
    public struct X3DVector3<T>
    {
        public T X, Y, Z;
    }

    /// <summary>
    /// A 4D vector
    /// </summary>
    /// <typeparam name="T">Numeric type</typeparam>
    public struct X3DVector4F<T>
    {
        public T X, Y, Z, W;
    }

    /// <summary>
    /// A quaternion (4D vector)
    /// </summary>
    /// <typeparam name="T">Numeric type</typeparam>
    public struct X3DQuaternion<T>
    {
        public T X, Y, Z, W;
    }

    /// <summary>
    /// A matrix
    /// </summary>
    /// <typeparam name="TY">Numeric type</typeparam>
    public struct X3DMatrix<TY>
    {
        public TY AX, AY, AZ;
        public TY BX, BY, BZ;
        public TY CX, CY, CZ;
        public X3DVector3<TY> T;
    }

    public struct X3DBox<TY>
    {
        public X3DVector3<TY> V1;
        public X3DVector3<TY> V2;
    }

    public enum X3DViewNavigation : uint
    {
        Static,
        Walk,
        Fly,
        Examine
    }

    public enum X3DViewMode : uint
    {
        Perspective,
        Front,
        Top,
        Side,
        Folding,
        FlatFront,
        FlatBack,
        Camera1,
        Camera2,
        Camera3,
        Camera4,
        Camera5,
        Camera6,
        Camera7,
        Camera8,
        Camera9,
        Camera10,
        Camera11,
        Camera12,
        Camera13,
        Camera14,
        Camera15,
        Camera16,
        Camera17,
        Camera18,
        Camera19,
        Camera20
    }

    public enum X3DAlignPlane : uint
    {
        /// <summary>
        /// No plane
        /// </summary>
        None,
        /// <summary>
        /// Top plane
        /// </summary>
        XZ,
        /// <summary>
        /// Front plane
        /// </summary>
        XY,
        /// <summary>
        /// Side plane
        /// </summary>
        ZY,
        /// <summary>
        /// X - only plane
        /// </summary>
        X,
        /// <summary>
        /// Y - only plane
        /// </summary>
        Y,
        /// <summary>
        /// Z - only plane
        /// </summary>
        Z,
        /// <summary>
        /// Screen plane
        /// </summary>
        Screen
    }
    public enum X3DManipulationType : uint
    {
        None,
        Move,
        Rotate,
        Scale
    }

    public enum X3DShadeMode : uint
    {
        Inherit,
        Flat,
        Smooth
    }

    public enum X3DFillMode : uint
    {
        Inherit,
        Points,
        Wireframe,
        HiddenLine,
        Solid,
        Textured
    }

    public enum X3DOutlineMode : uint
    {
        Inherit,
        None,
        Black,
        White,
        Colour
    }

    public enum X3DSplineType : uint
    {
        /// <summary>
        /// Object follows straight lines
        /// </summary>
        Straight = 0,
        /// <summary>
        /// Object follows a hermite spline
        /// </summary>
        Hermite,
        /// <summary>
        /// Object follows a catmull rom spline
        /// </summary>
        CatMullRom,
        /// <summary>
        /// Object follows a bezier spline
        /// </summary>
        Bezier
    }

    /// <summary>
    /// A view point
    /// </summary>
    /// <typeparam name="T">Numeric type</typeparam>
    public struct X3DViewpoint<T>
    {
        public X3DViewNavigation ViewNavigation;
        public X3DMatrix<T> StaticMatrix;
        public X3DMatrix<T> ExamineMatrix;
        public X3DMatrix<T> FlyMatrix;
        public X3DMatrix<T> WalkMatrix;
        public X3DViewMode ViewMode;
        public T Zoom;
        public X3DAlignPlane AlignPlane;
        public X3DManipulationType ManipulationType;
        public bool Selected;
        public X3DVector3<T> SplineTangent;
        public object TreeState;
        public X3DSplineType SplineType;
        public X3DBox<T> SelectionBox;
        public string SelectionName;
        public bool Show;
        public X3DFillMode FillMode;
        public X3DShadeMode ShadeMode;
        public X3DOutlineMode OutlineMode;
        public bool Root;
        public X3DVector3<T> SplineOffset;
        public X3DCameraAnimation<T> CameraAnimation;
    }

    public struct X3DAvatar<T>
    {
        /// <summary>
        /// Width of the avatar
        /// </summary>
        public T Width;
        /// <summary>
        /// Height of the avatar
        /// </summary>
        public T Height;
        /// <summary>
        /// Step-height of the avatar
        /// </summary>
        public T StepHeight;
        /// <summary>
        /// Relative speed of the avatar
        /// </summary>
        public T RelativeSpeed;
        /// <summary>
        /// Headlight shown for the avatar
        /// </summary>
        public bool ShowHeadlight;
    }

    /// <summary>
    /// The type of projection we are using
    /// </summary>
    public enum X3DProjectionType : uint
    {
        Ortho,
        Perspective,
        Frustum
    }

    /// <summary>
    /// A viewport projection
    /// </summary>
    /// <typeparam name="TY">The numeric type</typeparam>
    public struct X3DProjection<TY>
    {
        /// <summary>
        /// The projection type
        /// </summary>
        public X3DProjectionType ProjectionType;
        //	If ProjectionType is Ortho
        //		P1=left,P2=right,P3=bottom,P4=top
        //	Otherwise if ProjectionType is Perspective
        //		P1=fovy,P2=aspect,P3=zNear,P4=zFar
        //	Otherwise if ProjectionType is Frustum
        //		P1=left,P2=right,P3=bottom,P4=top,P5=zNear,P6=zFar
        public TY P1, P2, P3, P4, P5, P6;
    }

    /// <summary>
    /// The type of a given light
    /// </summary>
    public enum X3DLightType : uint
    {
        LocalLight,
        InfiniteLight,
        SpotLight
    }

    /// <summary>
    /// The visibility of a light etc.
    /// </summary>
    [Flags]
    public enum X3DVisibility : uint
    {
        Default = 0,
        Pickable = 1,
        Invisible = 2,
        Hotspot = Pickable | Invisible,
        DontIncludeInTree = 4,
        DontGarbageCollect = 8,
        Mask = Hotspot
    }

    /// <summary>
    /// Glue which attaches a light to a scene
    /// </summary>
    /// <typeparam name="TY">The numeric type</typeparam>
    public struct X3DLightGlue<TY>
    {
        /// <summary>
        /// The matrix
        /// </summary>
        public X3DMatrix<TY> Matrix;
        /// <summary>
        /// The original matrix (for flat view)
        /// </summary>
        public X3DMatrix<TY> OriginalMatrix;
        /// <summary>
        /// Target of the light (if applicable)
        /// </summary>
        public X3DVector3<TY> Target;
        /// <summary>
        /// Name information
        /// </summary>
        public int H;
        /// <summary>
        /// The visibility of the light
        /// </summary>
        public X3DVisibility Visibility;
        /// <summary>
        /// Whether the light is selected
        /// </summary>
        public bool Selected;
        /// <summary>
        /// The name of the light instance
        /// </summary>
        public string Name;
        /// <summary>
        /// Icon associated with the light
        /// </summary>
        public int Icon;
        /// <summary>
        /// Selected icon associated with the light
        /// </summary>
        public int IconSelected;
        /// <summary>
        /// The display box of the light
        /// </summary>
        public X3DBox<TY> DisplayBox;
        /// <summary>
        /// The associated animation (if available)
        /// </summary>
        public X3DAnimation<TY> Animation;
        /// <summary>
        /// The spline type
        /// </summary>
        public X3DSplineType SplineType;
        /// <summary>
        /// The spline tangent
        /// </summary>
        public X3DVector3<TY> SplineTangent;
        /// <summary>
        /// User information
        /// </summary>
        public object TreeState;
        /// <summary>
        /// Root selection
        /// </summary>
        public bool Root;
        /// <summary>
        /// Offset for grouping
        /// </summary>
        public X3DVector3<TY> SplineOffset;
    }

    /// <summary>
    /// Information about a light
    /// </summary>
    /// <typeparam name="TY"></typeparam>
    public struct X3DLight<TY>
    {
        /// <summary>
        /// ID of the light
        /// </summary>
        public int Id;
        /// <summary>
        /// Type of the light
        /// </summary>
        public X3DLightType LightType;
        /// <summary>
        /// Ambient colour of the light
        /// </summary>
        public X3DColourRGBA<TY> AmbientColour;
        /// <summary>
        /// Diffuse colour of the light
        /// </summary>
        public X3DColourRGBA<TY> DiffuseColour;
        /// <summary>
        /// Specular colour of the light
        /// </summary>
        public X3DColourRGBA<TY> SpecularColour;
        /// <summary>
        /// Spotlight exponent
        /// </summary>
        public TY Exponent;
        /// <summary>
        /// Spotlight cutoff
        /// </summary>
        public TY Cutoff;
        /// <summary>
        /// Attenuation
        /// </summary>
        public X3DVector3<TY> Attenuation;
        /// <summary>
        /// Scale (presentation)
        /// </summary>
        public TY PresentationScale;
        /// <summary>
        /// Whether the light is on
        /// </summary>
        public bool On;
        /// <summary>
        /// Whether the light is shown
        /// </summary>
        public bool Shown;
        /// <summary>
        /// The glue which attaches this light to the scene (for position and target etc.)
        /// </summary>
        public X3DLightGlue<TY> Glue;
    }

    /// <summary>
    /// The type of material
    /// </summary>
    public enum X3DMaterialType : uint
    {
        Simple,
        Bitmap,
        Movie,
        Show,
        TextureRenderer,
        LiveVideo,
        BGR,
        BGRA,
        Noise,
        Fractal,
        Turbulence,
        HeteroFractal,
        HybridFractal,
        MultiFractal,
        RidgedFractal
    }

    public struct X3DColourMap<T>
    {
        public X3DColourRGB<T>[] C;
    }

    /// <summary>
    /// Fractal definition
    /// </summary>
    public struct X3DFractal<T>
    {
        public X3DColourMap<T> MAP;
        public int SEED;
        public T FRACTALH;
        public T FRACTALLAC;
        public T OFFSET;
        public T GAIN;
        public T THRESHOLD;
    }

    public enum X3DTextureFilter : uint
    {
        Nearest,
        Linear,
        Anisotropic
    }

    public enum X3DTextureMode : uint
    {
        Modulate,
        Decal
    }

    public enum X3DTextureRepeat : uint
    {
        Repeat,
        Clamp
    }

    public enum X3DMaterialPurpose : uint
    {
        Default,
        BumpMap,
        EnvironmentMap,
        Dot3BumpMap
    }

    public enum X3DShadowCasting : uint
    {
        None,
        Soft,
        Crisp
    }

    public struct X3DMaterial<T>
    {
        public X3DColourRGB<T> Emissive;
        public X3DColourRGB<T> Diffuse;
        public X3DColourRGB<T> Specular;
        public T Power;
        public T Opacity;
        public X3DMaterialType MaterialType;
        public X3DFractal<T> Fractal;
        public bool NoMipMap;
        public bool ApplyTransform;
        public X3DMatrix<T> Transform;
        public X3DTextureFilter Filter;
        public X3DTextureMode Mode;
        public X3DTextureRepeat RepeatX, RepeatY;
        public X3DVector4F<T> ColourScale;
        public X3DVector4F<T> ColourBias;
        public string Filename;
        public X3DMaterialPurpose Purpose;
        public int TexCoordIndex;
        public X3DAnimation<T> Animation;
        public T TexScale1;
        public T TexScale2;
        public T Shiny;
        public int BumpMap;
        public X3DShadowCasting Shadow;
    }

    /// <summary>
    /// A view (perspective, front, top, side, etc.)
    /// </summary>
    /// <typeparam name="T">Numeric type</typeparam>
    public class X3DView<T>
    {
        public X3DViewpoint<T> Viewpoint;
        public X3DProjection<T> Projection;
        public X3DAvatar<T> Avatar;
        public bool ShowNotes;
        public bool ShowWatermark;
    }

    /// <summary>
    /// A material (or multi-material)
    /// </summary>
    /// <typeparam name="T">The numeric type</typeparam>
    public class X3DMaterialDef<T>
    {
        public int MaterialCount;
        public X3DMaterial<T>[] Materials;
        public object[] Movies;
        public bool ArtworkOnly;
    }

    /// <summary>
    /// Indicates the sort of date stored in an X3DVertexData
    /// </summary>
    public enum X3DRenderSort : uint
    {
        None,
        Vertices,
        VerticesAndNormals,
        VerticesAndTextureCoords,
        VerticesNormalsAndTextureCoords,
        Vertices4,
        Vertices2,
        VerticesAndTextureCoords2,
        VerticesAndTextureCoords3,
        VerticesNormalsAndTextureCoords2,
        VerticesNormalsAndTextureCoords3,
        VerticesColourAndTextureCoords2
    }

    public enum X3DTextureMapType : uint
    {
        Sphere,
        LinearXY,
        LinearZY,
        LinearXYZ,
        Cubic,
        Cylindrical,
        None
    }

    public struct X3DTextureMapData<TY>
    {
        public X3DTextureMapType T;
        public TY U1, U2;
        public TY V1, V2;
    }

    public struct X3DVertexData<TY>
    {
        public int NumVertices;
        public X3DRenderSort RenderSort;
        public TY[] VertexData;
        public bool[] Edges;
        public TY[] EdgeColours;
        public TY[] TextureCoords;
        public X3DTextureMapData<TY> TextureMap;
    }

    public enum X3DMeshFaceType : uint
    {
        Points,
        LineStrip,
        LineLoop,
        Lines,
        TriangleStrip,
        TriangleFan,
        Triangles,
        QuadStrip,
        Quads,
        Polygon
    }

    public struct X3DMeshData
    {
        public int IndexCount;
        public X3DMeshFaceType MeshFaceType;
        public int[] Indices;
        public bool SimpleMesh;
    }

    public struct X3DBezierSplineData
    {
        public int VP;
    }

    public struct X3DHermiteSplineData<TY>
    {
        public int VP;
        public int NP;
        public X3DVector3<TY>[] CP;
        public bool CYCLIC;
        public X3DVector3<TY>[] TAN;
    }

    public struct X3DCatMullRomSplineData<TY>
    {
        public int VP;
        public int NP;
        public X3DVector3<TY>[] CP;
        public bool CYCLIC;
    }

    public struct X3DBezierSurfaceData<TY>
    {
        public int OS;
        public int OT;
        public int DIVS;
        public Colour C00, C0N, CN0, CNN;
        public bool TEXTURED;
        public X3DVector2<TY> T00, T0N, TN0, TNN;
        public bool LIGHTMAP;
        public X3DVector2<TY> L00, L0N, LN0, LNN;
        public uint SHADER;
        public bool NOTESS;
    }

    public struct X3DNurbsCurveData<TY>
    {
        public int UKNOTCOUNT;
        public TY[] UKNOTS;
        public int USTRIDE;
        public int UORDER;
    }

    public struct X3DNurbsSurfaceData<TY>
    {
        public int UKNOTCOUNT;
        public TY[] UKNOTS;
        public int VKNOTCOUNT;
        public TY[] VKNOTS;
        public int USTRIDE;
        public int VSTRIDE;
        public int UORDER;
        public int VORDER;
    }

    public struct X3DPyramidData<TY>
    {
        public TY BR;
        public TY HT;
        public int SL;
        public int ST;
    }

    public struct X3DLineData<TY>
    {
        public X3DVector3<TY> V1;
        public X3DVector3<TY> V2;
    }

    public struct X3DRectangleData<TY>
    {
        public X3DVector3<TY> V1;
        public X3DVector3<TY> V2;
        public X3DVector3<TY> V3;
        public X3DVector3<TY> V4;
    }

    public struct X3DCylinderData<TY>
    {
        public TY BR;
        public TY TR;
        public TY HT;
        public int SL;
        public int ST;
        public bool BODY;
        public bool CAP;
        public TY START;
        public TY END;
        public X3DVector2<TY>[] TEXCO;
    }

    public struct X3DDiskData<TY>
    {
        public TY IR;
        public TY OR;
        public int SL;
        public int LP;
    }

    public struct X3DPartialDiskData<TY>
    {
        public TY IR;
        public TY OR;
        public int SL;
        public int LP;
        public TY SA;
        public TY SP;
    }

    public struct X3DSphereData<TY>
    {
        public TY RA;
        public int SL;
        public int ST;
    }

    public struct X3DExtSphereData<TY>
    {
        public TY RA;
        public int SL;
        public int ST;
    }

    public struct X3DExtConeData<TY>
    {
        public TY BR;
        public TY HT;
        public int SL;
        public int ST;
    }

    public struct X3DExtCubeData<TY>
    {
        public TY WIDTH;
        public TY HEIGHT;
        public TY DEPTH;
    }

    public struct X3DExtTorusData<TY>
    {
        public TY IR;
        public TY OR;
        public int SD;
        public int RG;
    }

    public struct X3DExtTeapotData<TY>
    {
        public TY SZ;
    }

    public struct X3DDecalData<TY>
    {
        public bool Orientation;
        public bool Lit;
        public TY[] Size;
    }

    public enum X3DGrabHandleType : uint
    {
        Size,
        Fold
    }

    public struct X3DGrabHandleData<TY>
    {
        public int GRABHANDLEID;
        public X3DGrabHandleType GrabHandleType;
        public X3DVector2<TY> Size;
        public X3DVector3<TY> Offset;
    }

    public enum X3DArtworkApplication : uint
    {
        FrontAndBack = 0,
        Front,
        Back
    }

    public enum X3DArtworkQuality : uint
    {
        Inherit,
        Maximum,
        VeryHight,
        High,
        Medium,
        Low
    }

    public struct X3DArtworkData<TY>
    {
        public bool DrawBorder;
        public X3DVector2<TY> Size;
        public X3DArtworkApplication Application;

        public bool MirrorX, MirrorY;
        public bool Proportional;
    }

    public struct X3DArtworkMarkerData<TY>
    {
        public int MarkerStyle;
        public X3DVector2<TY> Size;
    }

    public struct X3DCalloutData<TY>
    {
        public X3DVector2<TY> SZ;
        public X3DVector2<TY> P1, P2;
    }

    public struct X3DTrackerRectangleData<TY>
    {
        public X3DVector2<TY> P1, P2;
    }

    public enum X3DWatermarkPosition : uint
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
        Centre
    }

    [Flags]
    public enum X3DFontFaceFlags : uint
    {
        Bold = 0x1,
        Italic = 0x2
    }

    [Flags]
    public enum X3DFontRenderingFlags : uint
    {
        Centred = 0x1,
        TwoSided = 0x2,
        Filtered = 0x4
    }

    public struct X3DFontData<TY>
    {
        public string FontFace;
        public int Height;
        public X3DFontFaceFlags FontFaceFlags;
        public X3DFontRenderingFlags FontRenderingFlags;
        public X3DVector2<TY> SCALE;
    }

    public struct X3DTextData<TY>
    {
        public X3DFontData<TY> Font;
        public string Text;
        public Colour Colour;
        public TY Opacity;
    }

    public struct X3DWatermarkData<TY>
    {
        public bool IsText;
        public X3DMaterial<TY> Material;
        public X3DMobileImage Texture;
        public object Movie;
        public X3DTextData<TY> Text;
        public X3DWatermarkPosition WatermarkPosition;
        public int X, Y;
    }

    public struct X3DNoteData<TY>
    {
        public X3DTextData<TY> T;
        public TY X, Y, CX, CY;
        public bool DrawBorder;
    }

    public struct X3DFloatingNoteData<TY>
    {
        public int NoteId;
        public X3DVector3<TY> Direction;
        public TY Radius;
        public X3DVector3<TY> CentreOffset;
        public int X, Y, CX, CY;
        public int StartFrame;
        public int CurrentFrame;
        public int EndFrame;
    }

    public enum X3DArrowhead : uint
    {
        None,
        Point,
        Ball,
        Diamond
    }

    public struct X3DArrowData<TY>
    {
        public X3DVector3<TY> Offset;
        public TY Thickness;
        public X3DColourRGBA<TY> Colour;
        public X3DVector3<TY> Axis;
        public X3DVector3<TY> Up;
        public TY L1, L2;
        public X3DArrowhead A1, A2;
    }

    public struct X3DDimensionData<TY>
    {
        public X3DArrowData<TY> Arrow;
        public X3DTextData<TY> Text;
    }

    public struct X3DRotatorData<TY>
    {
        public TY Offset;
        public X3DAlignPlane Plane;
        public X3DColourRGBA<TY> Colour;
        public TY Radius;
    }

    public struct X3DLockPointData<TY>
    {
        public bool Absolute;
        public X3DVector3<TY> Point;
    }

    public struct X3DAxesData<TY>
    {
        public TY Radius;
        public bool X, Y, Z;
        public X3DColourRGB<TY> ColourX, ColourY, ColourZ;
        public bool ShowLetters;
    }

    public enum X3DShapeGlueType : uint
    {
        Null = 0,
        ShapeGlue = (Null + 1),
        PhysicalShapeGlue = (ShapeGlue + 1),
        JointedShapeGlue = (PhysicalShapeGlue + 1),
        LodGlue = (JointedShapeGlue + 1),
        LayerGlue = (LodGlue + 1),
        Last = (LayerGlue + 1)
    }

    public class X3DShapeGlue<T>
    {
        public X3DShapeGlueType ShapeGlueType;
        public X3DMatrix<T> Matrix;
        public X3DMatrix<T> OriginalMatrix;
        public T Scale;
        public int H;
        public X3DVisibility Visibility;
        public bool Selected;
        public string Name;
        public int Icon;
        public int IconSelected;
        public X3DAnimation<T> Animation;
        public X3DVector3<T> ActualPos;
        public X3DSplineType SplineType;
        public X3DVector3<T> SplineTangent;
        public object TreeState;
        public bool Root;
        public X3DVector3<T> SplineOffset;
        public int BoxTick;
        public X3DMatrix<T> ActualMatrix;
    }

    public enum X3DPhysicalJointKind : uint
    {
        World = 0,
        Spherical = (World + 1),
        PointToPath = (Spherical + 1),
        PointToSurface = (PointToPath + 1)
    }

    public struct X3DPhysicsDataConfig<TY>
    {
        public X3DMatrix<TY> Transform;
        public X3DVector3<TY> LinearVelocity;
        public X3DVector3<TY> AngularVelocity;
        public X3DVector3<TY> Force;
        public X3DVector3<TY> AppliedForce;
        public X3DVector3<TY> Torque;
        public X3DVector3<TY> AppliedTorque;
        public X3DQuaternion<TY> Orientation;
    }

    public struct X3DPhysicsData<TY>
    {
        public TY Mass;
        public TY OneOverMass;
        public X3DVector3<TY> OriginalLocation;
        public TY[] InertiaTensor;
        public TY[] InertiaTensorInverse;
        public bool IsAffectedByGravity;
        public bool RespondsToForces;
        public TY LinearDamping;
        public TY AngularDamping;
        public TY PhysicsScale;
        public int ActiveConfig;
        public X3DPhysicsDataConfig<TY> Config1;
        public X3DPhysicsDataConfig<TY> Config2;
    }

    public struct X3DPhysicalJoint<TY>
    {
        public X3DPhysicalJointKind Kind;
        public X3DVector3<TY> Location;
        public TY AssemblyRate;
        public X3DPhysicsData<TY> Object1;
        public X3DPhysicsData<TY> Object2;
        public TY Scale;
    }

    public class X3DPhysicalShapeGlue<TY> : X3DShapeGlue<TY>
    {
        public X3DPhysicsData<TY> PhysicsData;
    }

    public struct X3DJointData<TY>
    {
        public X3DVector3<TY> JointOffset;
        public X3DVector3<TY> JointAxis;
        public TY JointAngle;
        public TY OriginalJointAngle;
        public TY LastJointAngle;
        public TY JointAxisLength;
        public bool JointDisplayed;
        public TY[] TexCoord;
        public TY[] TexCoordPerPanel;
        public X3DVector3<TY> RenderJointAxis;
    }

    public class X3DJointedShapeGlue<TY> : X3DShapeGlue<TY>
    {
        public X3DJointData<TY> JointData;
    }

    public struct X3DCreaseData<TY>
    {
        public X3DShapeGlue<TY> ShapeGlue;
        public X3DJointData<TY> JointData;
        public int Index;
    }

    public class X3DLodGlue<TY> : X3DShapeGlue<TY>
    {
        public TY VisibilityRange;
    }

    public class X3DLayerGlue<TY> : X3DShapeGlue<TY>
    {
    }

    public enum X3DLightingHint : uint
    {
        Default = 0,
        Never = (Default + 1),
        Always = (Never + 1),
        Light = (Always + 1)
    }

    public enum X3DCullHint : uint
    {
        None = 0,
        CW = (None + 1),
        CCW = (CW + 1)
    }

    public struct X3DPrimitiveGlue<TY>
    {
        public X3DMatrix<TY> Matrix;
        public X3DMatrix<TY> OriginalMatrix;
        public TY S;
        public TY Scale;
        public int I;
        public int Reserved;
        public int H;
        public X3DVisibility Visibility;
        public bool Selected;
        public X3DCullHint Culling;
        public X3DLightingHint Lighting;
        public int BoxTick;
        public X3DBox<TY> Box;
        public string Name;
        public int Icon;
        public int IconSelected;
        public X3DAnimation<TY> Animation;
        public X3DSplineType SplineType;
        public X3DVector3<TY> SplineTangent;
        public int ZBias;
        public int Shader;
        public object TreeState;
        public X3DFillMode FillMode;
        public X3DShadeMode ShadeMode;
        public X3DOutlineMode OutlineMode;
        public bool Root;
        public X3DVector3<TY> SplineOffset;
        public X3DArtworkQuality ArtworkQuality;
        public X3DMatrix<TY> ActualMatrix;
        public TY Grammage;
    }

    public enum X3DNormalStyle : uint
    {
        Facet,
        Edge,
        PathEdge
    }

    public enum X3DTextureStyle : uint
    {
        None,
        VertexFlat,
        NormalFlat,
        VertexCyl,
        NormalCyl,
        VertexSph,
        NormalSph,
        ModelVertexFlat,
        ModelNormalFlat,
        ModelVertexCyl,
        ModelNormalCyl,
        ModelVertexSph,
        ModelNormalSph,
        LatheCyl
    }

    public enum X3DJointStyle : uint
    {
        Raw,
        Angle,
        Cut,
        Round
    }

    public struct X3DPolyCylinderData<TY>
    {
        public X3DJointStyle JointStyle;
        public X3DNormalStyle NormalStyle;
        public X3DTextureStyle TextureStyle;
        public bool Capped;
        public int NumSides;
        public int Points;
        public TY[] PointArray;
        public TY[] ColourArray;
        public TY Radius;
    }

    public struct X3DPolyConeData<TY>
    {
        public X3DJointStyle JointStyle;
        public X3DNormalStyle NormalStyle;
        public X3DTextureStyle TextureStyle;
        public bool Capped;
        public int NumSides;
        public int Points;
        public TY[] PointArray;
        public TY[] ColourArray;
        public TY[] RadiusArray;
    }

    public struct X3DPathExtrusionData<TY>
    {
        public X3DJointStyle JointStyle;
        public X3DNormalStyle NormalStyle;
        public X3DTextureStyle TextureStyle;
        public bool Capped;
        public bool Closed;
        public int ContourPoints;
        public TY[] Contour;
        public TY[] ContourNormals;
        public X3DVector3<TY> Up;
        public int PolylinePoints;
        public TY[] PolylineVertices;
        public TY[] ColourArray;
    }

    public struct X3DTwistExtrusionData<TY>
    {
        public X3DJointStyle JointStyle;
        public X3DNormalStyle NormalStyle;
        public X3DTextureStyle TextureStyle;
        public bool Capped;
        public bool Closed;
        public int ContourPoints;
        public TY[] Contour;
        public TY[] ContourNormals;
        public X3DVector3<TY> Up;
        public int PolylinePoints;
        public TY[] PolylineVertices;
        public TY[] ColourArray;
        public TY[] TwistArray;
    }

    public struct X3DSuperExtrusionData<TY>
    {
        public X3DJointStyle JointStyle;
        public X3DNormalStyle NormalStyle;
        public X3DTextureStyle TextureStyle;
        public bool Capped;
        public bool Closed;
        public int ContourPoints;
        public TY[] Contour;
        public TY[] ContourNormals;
        public X3DVector3<TY> Up;
        public int PolylinePoints;
        public TY[] PolylineVertices;
        public TY[] ColourArray;
        public TY[] ScaleArray;
        public TY[] TwistArray;
        public TY[] TransformArray;
        public int NumSides;
    }

    public struct X3DSpiralExtrusionData<TY>
    {
        public X3DJointStyle JointStyle;
        public X3DNormalStyle NormalStyle;
        public X3DTextureStyle TextureStyle;
        public bool Capped;
        public bool Closed;
        public int ContourPoints;
        public TY[] Contour;
        public TY[] ContourNormals;
        public X3DVector3<TY> Up;
        public TY StartRadius;
        public TY DRDTheta;
        public TY StartZ;
        public TY DZDTheta;
        public TY[] StartTransform;
        public TY[] TransformDTheta;
        public TY StartTheta;
        public TY SweepTheta;
    }

    public struct X3DLatheData<TY>
    {
        public X3DJointStyle JointStyle;
        public X3DNormalStyle NormalStyle;
        public X3DTextureStyle TextureStyle;
        public bool Capped;
        public bool Closed;
        public int ContourPoints;
        public TY[] Contour;
        public TY[] ContourNormals;
        public X3DVector3<TY> Up;
        public TY StartRadius;
        public TY DRDTheta;
        public TY StartZ;
        public TY DZDTheta;
        public TY[] StartTransform;
        public TY[] DTransformDTheta;
        public TY StartTheta;
        public TY SweepTheta;
        public int NumSides;
    }

    public struct X3DHelicoidData<TY>
    {
        public X3DJointStyle JointStyle;
        public X3DNormalStyle NormalStyle;
        public X3DTextureStyle TextureStyle;
        public bool Capped;
        public TY RTOROID;
        public TY StartRadius;
        public TY DRDTheta;
        public TY StartZ;
        public TY DZDTheta;
        public TY[] StartTransform;
        public TY[] DTransformDTheta;
        public TY StartTheta;
        public TY SweepTheta;
    }

    public struct X3DToroidData<TY>
    {
        public X3DJointStyle JointStyle;
        public X3DNormalStyle NormalStyle;
        public X3DTextureStyle TextureStyle;
        public bool Capped;
        public TY RTOROID;
        public TY StartRadius;
        public TY DRDTheta;
        public TY StartZ;
        public TY DZDTheta;
        public TY[] StartTransform;
        public TY[] DTransformDTheta;
        public TY StartTheta;
        public TY SweepTheta;
    }

    public struct X3DScrewExtrusionData<TY>
    {
        public X3DJointStyle JointStyle;
        public X3DNormalStyle NormalStyle;
        public X3DTextureStyle TextureStyle;
        public bool Capped;
        public bool Closed;
        public int ContourPoints;
        public TY[] Contour;
        public TY[] ContourNormals;
        public X3DVector3<TY> Up;
        public TY StartZ;
        public TY EndZ;
        public TY Twist;
    }

    public enum X3DParticleSystemType : uint
    {
        Manual = 0,
        Smoke = (Manual + 1),
        BitmapExplosion = (Smoke + 1),
        Bubbles = (BitmapExplosion + 1),
        Snow = (Bubbles + 1)
    }

    public struct X3DParticleSystemData<TY>
    {
        public X3DParticleSystemType ParticleSystemType;
        public int ParticleCount;
        public TY[] Positions;
        public TY[] OldPositions;
        public TY[] Velocities;
        public TY[] Colours;
        public int[] Energies;
        public TY[] Sizes;
        public X3DVector3<TY> Min;
        public X3DVector3<TY> Max;
        public int Active;
    }

    public struct X3DSmokeData<TY>
    {
        public TY ScaleSpeed;
        public TY EnergySpeed;
        public TY WindDistortFactor;
        public X3DVector3<TY> WindDirection;
        public bool KeepSmokeAlive;
    }

    public struct X3DBitmapExplosionData<TY>
    {
        public TY Size;
        public TY EnergySpeed;
        public TY AnimSpeed;
    }

    public struct X3DBubblesData<TY>
    {
        public TY MaxDist;
        public TY WindDistortFactor;
        public X3DVector3<TY> WindDirection;
    }

    public struct X3DSnowData<TY>
    {
        public TY Radius;
        public TY Top;
        public TY Bottom;
        public TY Distortion;
        public TY Speed;
        public X3DVector3<TY> WindDirection;
    }

    public enum X3DSoundType : uint
    {
        CDTrack,
        Sound,
        Music
    }

    public class X3DSound
    {
        public X3DSoundType SoundType;
        public int SoundTrack;
        public string SoundFile;
        public object Sound;
    }

    public enum X3DPanoramaType : uint
    {
        Cubic,
        Spherical,
        Cylindrical
    }

    public struct X3DPanoramaData<TY>
    {
        public X3DPanoramaType PanoType;
        public X3DMaterial<TY> Spherical;
        public X3DMobileImage SphericalTexture;
        public object SphericalMovie;
        public X3DMaterial<TY> Cylindrical;
        public X3DMobileImage CylindricalTexture;
        public object CylindricalMovie;
        public X3DMaterial<TY> Front;
        public X3DMobileImage FrontTexture;
        public object FrontMovie;
        public X3DMaterial<TY> Back;
        public X3DMobileImage BackTexture;
        public object BackMovie;
        public X3DMaterial<TY> Left;
        public X3DMobileImage LeftTexture;
        public object LeftMovie;
        public X3DMaterial<TY> Right;
        public X3DMobileImage RightTexture;
        public object RightMovie;
        public X3DMaterial<TY> Top;
        public X3DMobileImage TopTexture;
        public object TopMovie;
        public X3DMaterial<TY> Bottom;
        public X3DMobileImage BottomTexture;
        public object BottomMovie;
    }

    public enum X3DBackgroundMode : uint
    {
        Tiled,
        Stretched,
        Centred
    }

    public struct X3DBackgroundData<TY>
    {
        public X3DMaterial<TY> Material;
        public X3DMobileImage Image;
        public object Movie;
        public X3DBackgroundMode Mode;
    }

    public struct X3DGradientBackgroundData<TY>
    {
        public X3DColourRGB<TY> Colour1, Colour2, Colour3, Colour4;
    }

    public enum X3DFogType : uint
    {
        Linear,
        Exp,
        Exp2
    }

    public struct X3DFogData<TY>
    {
        public bool Enabled;
        public X3DFogType FogType;
        public X3DColourRGBA<TY> FogColour;
        public TY FogStart, FogEnd, FogDensity, FarPlane;
    }

    public enum X3DGroundType : uint
    {
        Square,
        Circular,
        FacetHeightField,
        SmoothHeightField
    }

    public struct X3DGroundData<TY>
    {
        public bool Enabled;
        public int Tiling;
        public X3DGroundType GroundType;
        public TY Size;
        public X3DMaterial<TY> Material;
        public TY Offset;
        public int XS;
        public int YS;
        public bool Reflective;
        public TY Opacity;
        public bool Lit;
        public TY[] HeightMap;
        public X3DMobileImage Image;
        public object Movie;
        public X3DColourRGBA<TY> Colour;
    }
}
