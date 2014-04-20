using KMMobile.ZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WaveEngine.Common.Math;

namespace KMMobile.X3D
{
    /// <summary>
    /// This class knows how to import a KSN or KOB file and create a model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class X3DReader<T>
    {
        /// <summary>
        /// The GLE engine for extrusions and lathes etc
        /// </summary>
        private readonly IGLE<T> gle;

        /// <summary>
        /// Function to create an image from binary data
        /// </summary>
        private Func<byte[], bool, bool, X3DMobileImage> createImageFromBinary;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="createImageFromBinary">A function which can convert compressed binary to an image</param>
        /// <param name="gle">The GLE engine</param>
        public X3DReader(Func<byte[], bool, bool, X3DMobileImage> createImageFromBinary = null, IGLE<T> gle = null)
        {
            this.gle = gle;
            this.createImageFromBinary = createImageFromBinary ?? X3DMobileImage.ImageFromBinary;
        }

        /// <summary>
        /// Stores the next index to append on the name the mesh
        /// </summary>
        private int meshLabelCounter;

        /// <summary>
        /// Reads a world from a KSN or KOB file
        /// </summary>
        /// <param name="filename">The filename</param>
        /// <param name="optimizations">The model optimizations</param>
        /// <returns>The world</returns>
        public World Read(string filename, string assetName, UnitDimension importDimensions, bool skipPopUps = false)
        {
            materialMap.Clear();
            var world = new World() { Name = assetName, KSN = true };
            ZipFile zipFile = null;
            FileStream fs = null;
            try
            {
                meshLabelCounter = 0;
                fs = File.OpenRead(filename);//, FileMode.Open, FileAccess.Read, FileShare.Delete);
                zipFile = new ZipFile(fs);
                var reader = new X3DFormatReader<T>(zipFile, createImageFromBinary);
                world.Thumbnail = reader.Thumbnail;
                world.Preview = reader.Preview;
                var glueAndShape = new KeyValuePair<X3DShapeGlue<T>, X3DShape<T>>(null, reader.Shapes.Last().Value);
                var visitedNodes = new List<X3DShape<T>>();
                world.Children.Add(CreateNode(world, reader, glueAndShape, visitedNodes));
                var dimensionScale = 1.0f;
                switch (importDimensions)
                {
                    case UnitDimension.Millimetres:
                        dimensionScale = 0.001f;
                        break;

                    case UnitDimension.Centimetres:
                        dimensionScale = 0.01f;
                        break;

                    case UnitDimension.Metres:
                        dimensionScale = 1.0f;
                        break;

                    case UnitDimension.Inches:
                        dimensionScale = 0.0254f;
                        break;

                    case UnitDimension.Feet:
                        dimensionScale = 0.3048f;
                        break;
                }
                world.Scale = 1.0f / Convert.ToSingle(reader.SceneScale) * dimensionScale;
            }
            catch (Exception ex)
            {
                throw new Exception(filename + " - " + ex.Message);
            }
            finally
            {
                if (fs != null)
                {
                    if (zipFile != null)
                    {
                        zipFile.IsStreamOwner = true;
                        zipFile.Close();
                    }
                    fs.Close();
                    fs.Dispose();
                }
            }
            return world;
        }

        /// <summary>
        /// Creates a node through the reader
        /// </summary>
        /// <param name="world">The world</param>
        /// <param name="reader">The X3DReader we are using to construct the model</param>
        /// <param name="glueAndShape">The X3D shape we are using to create the node</param>
        private SceneNode CreateNode(World world, X3DFormatReader<T> reader, KeyValuePair<X3DShapeGlue<T>, X3DShape<T>> glueAndShape, List<X3DShape<T>> visitedNodes)
        {
            visitedNodes.Add(glueAndShape.Value);
            SceneNode node = null;

            if ((null != glueAndShape.Key) && (glueAndShape.Key.ShapeGlueType == X3DShapeGlueType.JointedShapeGlue))
                node = new JointedSceneNode();
            else
                node = new MeshSceneNode();

            if (null != glueAndShape.Key)
                node.Transform = glueAndShape.Key.Matrix.ToTransform();
            else
                node.Transform = Matrix.Identity;

            node.Scale = (null != glueAndShape.Key) ?
                Convert.ToSingle(glueAndShape.Key.Scale) : 1.0f;         

            if (node is MeshSceneNode)
            {
                var meshNode = node as MeshSceneNode;
                meshNode.Mesh = new Mesh() { Name = MeshName(world.Name, meshLabelCounter) };
                int surfaceLabelCounter = 0;
                foreach (var primitive in glueAndShape.Value.Primitives)
                {
                    CreateSurface(
                        world,
                        meshNode,
                        reader,
                        primitive,
                        meshLabelCounter,
                        surfaceLabelCounter,
                        glueAndShape.Value.WallType,
                        glueAndShape.Value.InnerThickness,
                        glueAndShape.Value.OuterThickness
                    );
                    surfaceLabelCounter++;
                }

                meshNode.Name = "panel" + meshLabelCounter;
                meshNode.wallType = (WallType)glueAndShape.Value.WallType;
                meshLabelCounter++;
                meshNode.Mesh.EvaluateBounds();
                if (node is JointedSceneNode & meshNode.Mesh.Surfaces.Count > 0)
                {
                    var jointNode = node as JointedSceneNode;
                    var jointGlue = glueAndShape.Key as X3DJointedShapeGlue<T>;
                    jointNode.Joint = new Hinge
                    {
                        Angle = Convert.ToSingle(jointGlue.JointData.JointAngle),
                        HingeAxis = new Vector3(
                            Convert.ToSingle(jointGlue.JointData.JointAxis.X),
                            Convert.ToSingle(jointGlue.JointData.JointAxis.Y),
                            Convert.ToSingle(jointGlue.JointData.JointAxis.Z)),
                        HingeOffset = new Vector3(
                            Convert.ToSingle(jointGlue.JointData.JointOffset.X), 
                            Convert.ToSingle(jointGlue.JointData.JointOffset.Y),
                            Convert.ToSingle(jointGlue.JointData.JointOffset.Z)),
                        WallType = (WallType)glueAndShape.Value.WallType,
                        OuterThickness = Convert.ToSingle(glueAndShape.Value.OuterThickness) * Convert.ToSingle(glueAndShape.Value.SceneScale) * meshNode.Mesh.Surfaces[0].Scale2,
                        InnerThickness = Convert.ToSingle(glueAndShape.Value.InnerThickness) * Convert.ToSingle(glueAndShape.Value.SceneScale) * meshNode.Mesh.Surfaces[0].Scale2,
                        TexCoord =
                            jointGlue.JointData.TexCoord.Length == 4 ?
                                new Vector4(
                                    Convert.ToSingle(jointGlue.JointData.TexCoord[0]),
                                    Convert.ToSingle(jointGlue.JointData.TexCoord[1]),
                                    Convert.ToSingle(jointGlue.JointData.TexCoord[2]),
                                    Convert.ToSingle(jointGlue.JointData.TexCoord[3])) :
                                Vector4.Zero
                    };
                    var radius = Convert.ToSingle(jointGlue.JointData.JointAxisLength) * meshNode.Mesh.Surfaces[0].Scale2 * 0.5f;
                    jointNode.Joint.Cylinders.Add(new CylinderDef(jointNode.Joint.HingeOffset + radius * jointNode.Joint.HingeAxis,
                        jointNode.Joint.HingeOffset + radius * -jointNode.Joint.HingeAxis));
                }
            }
            else
            {
                node.Name = "dummy" + meshLabelCounter;
                meshLabelCounter++;
            }

            //  Visit all the sub-shapes of this shape
            foreach (var child in glueAndShape.Value.SubShapes)
            {
                if (!visitedNodes.Contains(child.Value))
                {
                    //Grouping
                    if (null != child.Value)
                    {
                        if (child.Value.ShapeType == X3DShapeType.Group)
                        {
                            world.Grouped = true;
                            node.Group = true;

                            var subnodeGroup = CreateNode(world, reader, child, visitedNodes);
                            if (null != subnodeGroup)
                            {
                                var groupNode = new DummySceneNode();
                                groupNode.Group = true;
                                world.Grouped = true;
                                node.Children.Add(groupNode);
                                groupNode.Children.Add(subnodeGroup);
                            }
                            continue;
                        }
                    }
                    var subnode = CreateNode(world, reader, child, visitedNodes);
                    if (null != subnode)
                        node.Children.Add(subnode);
                }
            }

            return node;
        }

        /// <summary>
        /// Yields the material name from the material id
        /// </summary>
        /// <param name="materialId">The material id</param>
        /// <returns>The material name</returns>
        private static string MaterialName(string name, int materialId)
        {
            return string.Format("{0}{1}", name, materialId);
        }

        /// <summary>
        /// Yields the mesh name from the mesh id
        /// </summary>
        /// <param name="meshId">The mesh id</param>
        /// <returns>The mesh name</returns>
        private static string MeshName(string name, int meshId)
        {
            return string.Format("{0}{1}", name, meshId);
        }

        /// <summary>
        /// Yields the surface name from the surface id
        /// </summary>
        /// <param name="meshId">The mesh id</param>
        /// <param name="surfaceId">The surface id</param>
        /// <returns>The surface name</returns>
        private static string SurfaceName(int meshId, int surfaceId)
        {
            return string.Format("surface{0}_{1}", meshId, surfaceId);
        }

        /// <summary>
        /// This method extracts what it can from an X3D material
        /// </summary>
        /// <param name="materialId">The name of the material</param>
        /// <param name="originalMaterialDef">The X3D material</param>
        /// <returns>The resultant material</returns>
        private static Material MaterialFromX3DMaterial(string materialId, X3DMaterialDef<T> originalMaterialDef)
        {
            if (originalMaterialDef.MaterialCount >= 1)
            {
                var originalMaterial = originalMaterialDef.Materials[0];
                var newMaterial = new Material
                {
                    Name = materialId,
                    DiffuseTexture = originalMaterial.Filename,
                    SpecularScale = Convert.ToSingle(originalMaterial.Power) / 20.0f,
                    RefractionScale = Convert.ToSingle(originalMaterial.Shiny),
                    DiffuseColour = new Colour(
                        Convert.ToSingle(originalMaterial.Diffuse.R),
                        Convert.ToSingle(originalMaterial.Diffuse.G),
                        Convert.ToSingle(originalMaterial.Diffuse.B),
                        1.0f),
                    EmissiveColour = new Colour(
                        Convert.ToSingle(originalMaterial.Emissive.R),
                        Convert.ToSingle(originalMaterial.Emissive.G),
                        Convert.ToSingle(originalMaterial.Emissive.B),
                        1.0f),
                    SpecularColour = new Colour(
                        Convert.ToSingle(originalMaterial.Specular.R),
                        Convert.ToSingle(originalMaterial.Specular.G),
                        Convert.ToSingle(originalMaterial.Specular.B),
                        1.0f),
                    Opacity = Convert.ToSingle(originalMaterial.Opacity),
                    Shininess = Convert.ToSingle(originalMaterial.Shiny),
                    BaseTransform = new Vector4
                    {
                        X = (Convert.ToSingle(originalMaterial.TexScale1) == 0) ?
                        1 : Convert.ToSingle(originalMaterial.TexScale1),
                        Y = (Convert.ToSingle(originalMaterial.TexScale2) == 0) ?
                        1 : Convert.ToSingle(originalMaterial.TexScale2),
                        Z = 0.0f,
                        W = 0.0f
                    }
                };

                return newMaterial;
            }
            return null;
        }

        /// <summary>
        /// The material map
        /// </summary>
        private readonly Dictionary<int, Material> materialMap = new Dictionary<int, Material>();

        /// <summary>
        /// Creates a surface and adds it to this node
        /// </summary>
        /// <param name="world">The world</param>
        /// <param name="node">The node to add the surface to</param>
        /// <param name="reader">The reader</param>
        /// <param name="glueAndPrimitive">The glue primitive</param>
        /// <param name="meshId">The id of the current mesh</param>
        /// <param name="surfaceId">The id of this surface</param>
        /// <param name="surfaceCount">The number of surfaces</param>
        /// <param name="wallType">The wall type</param>
        /// <param name="innerThickness">The inner thickness</param>
        /// <param name="outerThickness">The outer thickness</param>
        private void CreateSurface(
            World world,
            MeshSceneNode node,
            X3DFormatReader<T> reader,
            KeyValuePair<X3DPrimitiveGlue<T>,
            X3DPrimitive<T>> glueAndPrimitive,
            int meshId, int surfaceId,
            X3DWallType wallType,
            T innerThickness,
            T outerThickness)
        {
            X3DMaterialDef<T> material;
            Material _material;
            if (reader.Materials.TryGetValue(glueAndPrimitive.Key.I, out material))
            {
                if (materialMap.TryGetValue(glueAndPrimitive.Key.I, out _material))
                {
                }
                else
                {
                    _material = MaterialFromX3DMaterial(MaterialName(world.Name, glueAndPrimitive.Key.I), material);
                    world.Materials.Add(_material);
                    materialMap[glueAndPrimitive.Key.I] = _material;
                }
            }
            else
            {
                _material = new Material() { Name = MaterialName(world.Name, glueAndPrimitive.Key.I) };
                world.Materials.Add(_material);
                materialMap[glueAndPrimitive.Key.I] = _material;
            }

            var scale =
                Convert.ToSingle(glueAndPrimitive.Key.S) == 0.0f ? 1.0f :
                Convert.ToSingle(glueAndPrimitive.Key.S);
            var scale2 = //sceneScale;
                Convert.ToSingle(glueAndPrimitive.Key.Scale) == 0.0f ? 1.0f :
                Convert.ToSingle(glueAndPrimitive.Key.Scale);

            var surface = new Surface
            {
                Name = SurfaceName(meshId, surfaceId),
                Scale = scale,
                Scale2 = scale2,
                Matrix = glueAndPrimitive.Key.Matrix.ToTransform(),
                Material = _material,
                CullMode = glueAndPrimitive.Key.Culling == X3DCullHint.None ? CullMode.None : glueAndPrimitive.Key.Culling == X3DCullHint.CW ? CullMode.Clockwise : CullMode.Anticlockwise
            };
            switch (wallType)
            {
                case X3DWallType.None:
                    surface.FaceType = FaceType.Other;
                    surface.Visible = true;
                    break;

                case X3DWallType.NoThickness:
                    surface.CullMode = CullMode.None;
                    surface.Thickness = 0.0f;
                    surface.ThicknessRatio = 0.0f;
                    surface.Visible = (0 == surfaceId);
                    surface.FaceType = (0 == surfaceId) ?
                    FaceType.Front : (1 == surfaceId) ?
                    FaceType.Back : FaceType.Edge;
                    break;

                case X3DWallType.SingleWall:
                    surface.Thickness = Convert.ToSingle(innerThickness) + Convert.ToSingle(outerThickness);
                    surface.ThicknessRatio = 0.5f;
                    surface.Visible = true;
                    surface.FaceType = (0 == surfaceId) ?
                    FaceType.Front : (1 == surfaceId) ?
                    FaceType.Back : FaceType.Edge;
                    break;

                case X3DWallType.DoubleWall:
                    surface.Thickness = Convert.ToSingle(innerThickness) + Convert.ToSingle(outerThickness);
                    surface.ThicknessRatio = Convert.ToSingle(outerThickness) / surface.Thickness;
                    surface.Visible = true;
                    surface.FaceType = (0 == surfaceId) ?
                    FaceType.Front : (1 == surfaceId) ?
                    FaceType.Back : FaceType.Edge;
                    break;

                case X3DWallType.Xanita:
                    surface.Thickness = Convert.ToSingle(innerThickness) + Convert.ToSingle(outerThickness);
                    surface.ThicknessRatio = 1.0f;
                    surface.Visible = true;
                    surface.FaceType = (0 == surfaceId) ?
                    FaceType.Front : (1 == surfaceId) ?
                    FaceType.Back : FaceType.Edge;
                    break;
            }

            try
            {
                switch (glueAndPrimitive.Value.PType)
                {
                    case X3DPrimitiveType.Mesh:
                        CreateMesh(surface, glueAndPrimitive.Value as X3DMesh<T>);
                        break;

                    case X3DPrimitiveType.PolyCylinder:
                        if (null != gle)
                            gle.PolyCylinder(surface, (glueAndPrimitive.Value as X3DPolyCylinder<T>).Data);
                        break;

                    case X3DPrimitiveType.PolyCone:
                        if (null != gle)
                            gle.PolyCone(surface, (glueAndPrimitive.Value as X3DPolyCone<T>).Data);
                        break;

                    case X3DPrimitiveType.PathExtrusion:
                        if (null != gle)
                            gle.PathExtrusion(surface, (glueAndPrimitive.Value as X3DPathExtrusion<T>).Data);
                        break;

                    case X3DPrimitiveType.TwistExtrusion:
                        if (null != gle)
                            gle.TwistExtrusion(surface, (glueAndPrimitive.Value as X3DTwistExtrusion<T>).Data);
                        break;

                    case X3DPrimitiveType.SuperExtrusion:
                        if (null != gle)
                            gle.SuperExtrusion(surface, (glueAndPrimitive.Value as X3DSuperExtrusion<T>).Data);
                        break;

                    case X3DPrimitiveType.SpiralExtrusion:
                        if (null != gle)
                            gle.SpiralExtrusion(surface, (glueAndPrimitive.Value as X3DSpiralExtrusion<T>).Data);
                        break;

                    case X3DPrimitiveType.Lathe:
                        if (null != gle)
                            gle.Lathe(surface, (glueAndPrimitive.Value as X3DLathe<T>).Data);
                        break;

                    case X3DPrimitiveType.Helicoid:
                        if (null != gle)
                            gle.Helicoid(surface, (glueAndPrimitive.Value as X3DHelicoid<T>).Data);
                        break;

                    case X3DPrimitiveType.Toroid:
                        if (null != gle)
                            gle.Toroid(surface, (glueAndPrimitive.Value as X3DToroid<T>).Data);
                        break;

                    case X3DPrimitiveType.ScrewExtrusion:
                        if (null != gle)
                            gle.ScrewExtrusion(surface, (glueAndPrimitive.Value as X3DScrewExtrusion<T>).Data);
                        break;

                    default:
                        break;
                }

            }
            catch
            {
                throw new Exception(glueAndPrimitive.Value.PType.ToString() + " creation error occurred");
            }

            node.Mesh.Surfaces.Add(surface);
        }

        /// <summary>
        /// Creates a surface from a mesh
        /// </summary>
        /// <param name="surface">The surface</param>
        /// <param name="mesh">The mesh to create the surface from</param>
        private static void CreateMesh(Surface surface, X3DMesh<T> mesh)
        {
            if (null == mesh)
                return;
            var indexData = mesh.Data;
            if ((null == indexData.Indices) ||
            (indexData.Indices.Length == 0))
                for (var index = 0u; index < indexData.IndexCount; index++)
                    surface.Indices.Add(index);
            else
            {
                var a = indexData.Indices.Select(i => (uint)i);
                surface.Indices.AddRange(a);
            }

            switch (mesh.VertexData.RenderSort)
            {
                case X3DRenderSort.Vertices:
                    {
                        for (var v = 0; v < mesh.VertexData.NumVertices; v++)
                        {
                            surface.Vertices.Add(
                                new Vector3(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 3)]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 3) + 1]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 3) + 2])));
                        }
                    }
                    break;

                case X3DRenderSort.Vertices2:
                    {
                        for (var v = 0; v < mesh.VertexData.NumVertices; v++)
                        {
                            surface.Vertices.Add(
                                new Vector3(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 2)]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 2) + 1]),
                                    0.0f));
                        }
                    }
                    break;

                case X3DRenderSort.Vertices4:
                    {
                        for (var v = 0; v < mesh.VertexData.NumVertices; v++)
                        {
                            surface.Vertices.Add(
                                new Vector3(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 3)]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 3) + 1]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 3) + 2])));
                        }
                    }
                    break;

                case X3DRenderSort.VerticesAndNormals:
                    {
                        for (var v = 0; v < mesh.VertexData.NumVertices; v++)
                        {
                            surface.Vertices.Add(
                                new Vector3(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 6)]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 6) + 1]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 6) + 2])));
                            surface.Normals.Add(
                                new Vector3(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 6) + 3]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 6) + 4]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 6) + 5])));
                        }
                    }
                    break;

                case X3DRenderSort.VerticesAndTextureCoords:
                    {
                        for (var v = 0; v < mesh.VertexData.NumVertices; v++)
                        {
                            surface.Vertices.Add(
                                new Vector3(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 5)]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 5) + 1]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 5) + 2])));
                            surface.TexCoords0.Add(
                                new Vector2(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 5) + 3]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 5) + 4])));
                        }
                    }
                    break;

                case X3DRenderSort.VerticesAndTextureCoords2:
                    {
                        for (var v = 0; v < mesh.VertexData.NumVertices; v++)
                        {
                            surface.Vertices.Add(
                                new Vector3(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 7)]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 7) + 1]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 7) + 2])));
                            surface.TexCoords0.Add(
                                new Vector2(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 7) + 3]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 7) + 4])));
                            surface.TexCoords1.Add(
                                new Vector2(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 7) + 5]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 7) + 6])));
                        }
                    }
                    break;

                case X3DRenderSort.VerticesAndTextureCoords3:
                    {
                        for (var v = 0; v < mesh.VertexData.NumVertices; v++)
                        {
                            surface.Vertices.Add(
                                new Vector3(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 9)]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 9) + 1]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 9) + 2])));
                            surface.TexCoords0.Add(
                                new Vector2(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 9) + 3]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 9) + 4])));
                            surface.TexCoords1.Add(
                                new Vector2(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 9) + 5]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 9) + 6])));
                            //  Texcoords (2) ignored
                        }
                    }
                    break;

                case X3DRenderSort.VerticesColourAndTextureCoords2:
                    {
                        for (var v = 0; v < mesh.VertexData.NumVertices; v++)
                        {
                        }
                    }
                    break;

                case X3DRenderSort.VerticesNormalsAndTextureCoords:
                    {
                        for (var v = 0; v < mesh.VertexData.NumVertices; v++)
                        {
                            surface.Vertices.Add(
                                new Vector3(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 8)]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 8) + 1]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 8) + 2])));
                            surface.Normals.Add(
                                new Vector3(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 8) + 3]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 8) + 4]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 8) + 5])));
                            surface.TexCoords0.Add(
                                new Vector2(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 8) + 6]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 8) + 7])));
                        }
                    }
                    break;

                case X3DRenderSort.VerticesNormalsAndTextureCoords2:
                    {
                        for (var v = 0; v < mesh.VertexData.NumVertices; v++)
                        {
                            surface.Vertices.Add(
                                new Vector3(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 10)]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 10) + 1]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 10) + 2])));
                            surface.Normals.Add(
                                new Vector3(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 10) + 3]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 10) + 4]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 10) + 5])));
                            surface.TexCoords0.Add(
                                new Vector2(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 10) + 6]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 10) + 7])));
                            surface.TexCoords1.Add(
                                new Vector2(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 10) + 8]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 10) + 9])));
                        }
                    }
                    break;

                case X3DRenderSort.VerticesNormalsAndTextureCoords3:
                    {
                        for (var v = 0; v < mesh.VertexData.NumVertices; v++)
                        {
                            surface.Vertices.Add(
                                new Vector3(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 12)]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 12) + 1]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 12) + 2])));
                            surface.Normals.Add(
                                new Vector3(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 12) + 3]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 12) + 4]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 12) + 5])));
                            surface.TexCoords0.Add(
                                new Vector2(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 12) + 6]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 12) + 7])));
                            surface.TexCoords1.Add(
                                new Vector2(
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 12) + 8]),
                                    Convert.ToSingle(mesh.VertexData.VertexData[(v * 12) + 9])));
                            //  Texcoords (2) ignored
                        }
                    }
                    break;

                default:
                    break;
            }

            // cullmode modifications
            if (surface.CullMode == CullMode.Clockwise)
            {
                surface.Indices.Reverse();
            }
            if (surface.CullMode == CullMode.None)
            {
                var indices = new List<uint>(surface.Indices);
                indices.Reverse();
                for (int i = 0; i < indices.Count; i++)
                    indices[i] += (uint)indices.Count;
                surface.Indices.AddRange(indices);

                var vertices = new List<Vector3>(surface.Vertices);
                surface.Vertices.AddRange(vertices);

                var texCoords = new List<Vector2>(surface.TexCoords0);
                surface.TexCoords0.AddRange(texCoords);

                var normals = new List<Vector3>(surface.Normals);
                for (int i = 0; i < normals.Count; i++)
                    normals[i] = -normals[i];
                surface.Normals.AddRange(normals);
            }
        }
    }
}
