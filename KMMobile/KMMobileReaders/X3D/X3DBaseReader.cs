using KMMobile.ZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace KMMobile.X3D
{
    /// <summary>
    /// This class is able to open and interpret KSN and KOB files
    /// </summary>
    public class X3DBaseReader
    {
        /// <summary>
        /// The zip file the reader is talking to
        /// </summary>
        private readonly ZipFile zipFile;

        /// <summary>
        /// This function knows how to create an image from binary data
        /// </summary>
        protected Func<byte[], bool, bool, X3DMobileImage> createImageFromBinary;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="zipFile">The zip file</param>
        public X3DBaseReader(ZipFile zipFile, Func<byte[], bool, bool, X3DMobileImage> createImageFromBinary = null)
        {
            this.zipFile = zipFile;
            this.createImageFromBinary = createImageFromBinary ?? X3DMobileImage.ImageFromBinary;
        }

        /// <summary>
        /// Reads a text stream, interpreting whether the stream is Unicode or
        /// ASCII
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <returns>The contents of the stream</returns>
        public static string ReadTextStream(Stream stream)
        {
            using (var ms = new MemoryStream())
            {
                int c = 0;
                while (-1 != c)
                {
                    c = stream.ReadByte();
                    if (-1 != c)
                        ms.WriteByte((byte)c);
                }
                ms.Seek(0, SeekOrigin.Begin);
                if (ms.GetBuffer()[0] == '\0' ||
                ms.GetBuffer()[1] == '\0')
                    using (var sr = new StreamReader(ms, Encoding.Unicode))
                        return sr.ReadToEnd();
                else
                    using (var sr = new StreamReader(ms, Encoding.ASCII))
                        return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// Yields the raw XML from the KSN or KOB file
        /// </summary>
        public XmlDocument RawXml
        {
            get
            {
                var doc = new XmlDocument();
                using (var archive = zipFile.GetInputStream(
                zipFile.GetEntry("archive.xml")))
                {
                    try
                    {
                        doc.LoadXml(ReadTextStream(archive));
                    }
                    catch
                    {
                    }
                }
                return doc;
            }
        }

        /// <summary>
        /// Yields the thumbnail from the KSN or KOB file
        /// </summary>
        public X3DMobileImage Thumbnail
        {
            get
            {
                return GetTexture("___snapshot.xdb", false);
            }
        }

        /// <summary>
        /// Yields the preview from the KSN or KOB file
        /// </summary>
        public X3DMobileImage Preview
        {
            get
            {
                return GetTexture("___preview.xdb", false);
            }
        }

        /// <summary>
        /// Yields a collection of data sections from the KSN or KOB file
        /// (these are used from the RawXml via attributes of the form arc:dataNNN.dat)
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> DataSections
        {
            get
            {
                foreach (ZipEntry ze in zipFile)
                    if (ze.Name.EndsWith(".dat"))
                        yield return new KeyValuePair<string, string>(ze.Name, GetDataSection(ze.Name));
            }
        }

        /// <summary>
        /// Yields a collection of all images from the KSN or KOB file
        /// </summary>
        public IEnumerable<KeyValuePair<string, X3DMobileImage>> Textures
        {
            get
            {
                foreach (ZipEntry ze in zipFile)
                    if (ze.Name.EndsWith(".xdz") || ze.Name.EndsWith(".xdb"))
                        yield return new KeyValuePair<string, X3DMobileImage>(ze.Name, GetTexture(ze.Name, true));
            }
        }

        /// <summary>
        /// Yields the text from a particular data section name
        /// </summary>
        /// <param name="datName">The dat name</param>
        /// <returns></returns>
        public string GetDataSection(string datName)
        {
            using (var archiveStream = zipFile.GetInputStream(zipFile.GetEntry(datName)))
            {
                return ReadTextStream(archiveStream);
            }
        }

        /// <summary>
        /// Yields a movie or sound etc. from the KSN or KOB file
        /// </summary>
        /// <param name="The raw file name">The raw file name</param>
        /// <returns>The movie or sound etc.</returns>
        public object GetRawFile(string rawFileName)
        {
            if (File.Exists(rawFileName))
            {
                return File.ReadAllBytes(rawFileName);
            }
            else
            {
                if (zipFile.FindEntry(rawFileName, true) != -1)
                {
                    using (var s = zipFile.GetInputStream(zipFile.GetEntry(rawFileName)))
                    {
                        using (var ms = new MemoryStream())
                        {
                            var res = -1;
                            do
                            {
                                res = s.ReadByte();
                                if (res != -1)
                                    ms.WriteByte((byte)res);
                            }
                            while (res != -1);
                            return ms.GetBuffer();
                        }
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Yields a particular texture from the KSN or KOB file
        /// </summary>
        /// <param name="imageName">The name of the image</param>
        /// <returns>The image, loaded from the ZIP file</returns>
        public X3DMobileImage GetTexture(string imageName, bool flip)
        {
            if (zipFile.FindEntry(imageName, true) != -1)
            {
                using (var s = zipFile.GetInputStream(zipFile.GetEntry(imageName)))
                {
                    using (var ms = new MemoryStream())
                    {
                        var res = -1;
                        do
                        {
                            res = s.ReadByte();
                            if (res != -1)
                                ms.WriteByte((byte)res);
                        }
                        while (res != -1);
                        return createImageFromBinary(
                            ms.GetBuffer(),
                            imageName.EndsWith(".xdz"),
                            flip);
                    }
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Checks whether an attribute exists
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="attribute">The attribute</param>
        /// <returns>True if the attribute exists</returns>
        public static bool Exists(XmlElement element, string attribute)
        {
            return element.HasAttribute(attribute);
        }

        /// <summary>
        /// Gets a value from a given attribute of a given element
        /// </summary>
        /// <typeparam name="T">The type (numeric, bool, etc.)</typeparam>
        /// <param name="element">The element</param>
        /// <param name="attribute">The attribute</param>
        /// <returns>The value</returns>
        public static T Get<T>(XmlElement element, string attribute)
        {
            var str = element.GetAttribute(attribute);
            if (!string.IsNullOrEmpty(str))
            {
                if (typeof(T) == typeof(float))
                    return (T)Convert.ChangeType(float.Parse(str), typeof(T));
                else
                    if (typeof(T) == typeof(double))
                        return (T)Convert.ChangeType(double.Parse(str), typeof(T));
                    else
                        if (typeof(T) == typeof(int))
                            return (T)Convert.ChangeType(int.Parse(str), typeof(T));
                        else
                            if (typeof(T) == typeof(long))
                                return (T)Convert.ChangeType(long.Parse(str), typeof(T));
                            else
                                if (typeof(T) == typeof(bool))
                                    return (T)Convert.ChangeType(int.Parse(str), typeof(T));
                                else
                                    if (typeof(T) == typeof(string))
                                        return (T)Convert.ChangeType(str, typeof(T));
                                    else
                                        return default(T);
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Gets an enum value from a given attribute of a given element
        /// </summary>
        /// <typeparam name="T">The numeric part</typeparam>
        /// <param name="element">The element</param>
        /// <param name="attribute">The attribute</param>
        /// <returns></returns>
        public static T GetEnum<T>(XmlElement element, string attribute)
        {
            var i = uint.Parse(element.GetAttribute(attribute));
            return (T)Convert.ChangeType(i, typeof(uint));
        }

        /// <summary>
        /// Gets a bunch of numeric parts from a given attribute of a given element
        /// </summary>
        /// <typeparam name="T">The numeric type</typeparam>
        /// <param name="element">The element</param>
        /// <param name="attribute">The attribute within the element</param>
        /// <returns>The bunch of numeric parts</returns>
        public T[] GetParts<T>(XmlElement element, string attribute)
        {
            var z = element.GetAttribute(attribute);
            //  Cope with data sections (quite trivial to do!)
            if (z.StartsWith("arc:"))
                z = GetDataSection(z.Substring(4));
            var parts = z.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var list = new List<T>();
            foreach (var part in parts)
            {
                if (typeof(T) == typeof(float))
                    list.Add((T)Convert.ChangeType(float.Parse(part), typeof(T)));
                else
                    if (typeof(T) == typeof(double))
                        list.Add((T)Convert.ChangeType(double.Parse(part), typeof(T)));
                    else
                        if (typeof(T) == typeof(int))
                            list.Add((T)Convert.ChangeType(int.Parse(part), typeof(T)));
                        else
                            if (typeof(T) == typeof(long))
                                list.Add((T)Convert.ChangeType(long.Parse(part), typeof(T)));
                            else
                                if (typeof(T) == typeof(bool))
                                    list.Add((T)Convert.ChangeType(int.Parse(part), typeof(T)));
            }
            return list.ToArray();
        }

        /// <summary>
        /// Gets a matrix from the given attribute of the given element
        /// </summary>
        /// <typeparam name="T">The matrix numeric type</typeparam>
        /// <param name="element">The element</param>
        /// <param name="attribute">The attribute within the element</param>
        /// <returns>The matrix</returns>
        public X3DMatrix<T> GetMatrix<T>(XmlElement element, string attribute)
        {
            var parts = GetParts<T>(element, attribute);
            var matrix = new X3DMatrix<T>() { AX = parts[0], AY = parts[1], AZ = parts[2], BX = parts[3], BY = parts[4], BZ = parts[5], CX = parts[6], CY = parts[7], CZ = parts[8] };
            matrix.T.X = parts[9];
            matrix.T.Y = parts[10];
            matrix.T.Z = parts[11];
            return matrix;
        }

        /// <summary>
        /// Gets a 2-d vector from the given attribute of the given element
        /// </summary>
        /// <typeparam name="T">The vector numeric type</typeparam>
        /// <param name="element">The element</param>
        /// <param name="attribute">The attribute within the element</param>
        /// <returns>The vector</returns>
        public X3DVector2<T> GetVector2<T>(XmlElement element, string attribute)
        {
            var parts = GetParts<T>(element, attribute);
            var vec = new X3DVector2<T>() { X = parts[0], Y = parts[1] };
            return vec;
        }

        /// <summary>
        /// Gets a 3-d vector from the given attribute of the given element
        /// </summary>
        /// <typeparam name="T">The vector numeric type</typeparam>
        /// <param name="element">The element</param>
        /// <param name="attribute">The attribute within the element</param>
        /// <returns>The vector</returns>
        public X3DVector3<T> GetVector3<T>(XmlElement element, string attribute)
        {
            var parts = GetParts<T>(element, attribute);
            if (parts.Length == 3)
            {
                var vec = new X3DVector3<T>() { X = parts[0], Y = parts[1], Z = parts[2] };
                return vec;
            }
            else
            {
                return new X3DVector3<T>();
            }
        }

        /// <summary>
        /// Gets a quaternion from the given attribute of the given element
        /// </summary>
        /// <typeparam name="T">The vector numeric type</typeparam>
        /// <param name="element">The element</param>
        /// <param name="attribute">The attribute within the element</param>
        /// <returns>The quaternion</returns>
        public X3DQuaternion<T> GetQuaternion<T>(XmlElement element, string attribute)
        {
            var parts = GetParts<T>(element, attribute);
            var quat = new X3DQuaternion<T>() { X = parts[0], Y = parts[1], Z = parts[2], W = parts[3] };
            return quat;
        }

        /// <summary>
        /// Gets a 4-d vector from the given attribute of the given element
        /// </summary>
        /// <typeparam name="T">The vector numeric type</typeparam>
        /// <param name="element">The element</param>
        /// <param name="attribute">The attribute within the element</param>
        /// <returns>The vector</returns>
        public X3DVector4F<T> GetVector4F<T>(XmlElement element, string attribute)
        {
            var parts = GetParts<T>(element, attribute);
            var vec = new X3DVector4F<T>() { X = parts[0], Y = parts[1], Z = parts[2], W = parts[3] };
            return vec;
        }

        /// <summary>
        /// Gets an RGBA colour from the given attribute of the given element
        /// </summary>
        /// <typeparam name="T">The numeric type</typeparam>
        /// <param name="element">The element</param>
        /// <param name="attribute">The attribute within the element</param>
        /// <returns>The vector</returns>
        public X3DColourRGBA<T> GetColourRGBA<T>(XmlElement element, string attribute)
        {
            var parts = GetParts<T>(element, attribute);
            var colour = new X3DColourRGBA<T>() { R = parts[0], G = parts[1], B = parts[2], A = parts[3] };
            return colour;
        }

        /// <summary>
        /// Gets an RGB colour from the given attribute of the given element
        /// </summary>
        /// <typeparam name="T">The numeric type</typeparam>
        /// <param name="element">The element</param>
        /// <param name="attribute">The attribute within the element</param>
        /// <returns></returns>
        public X3DColourRGB<T> GetColourRGB<T>(XmlElement element, string attribute)
        {
            var parts = GetParts<T>(element, attribute);
            var colour = new X3DColourRGB<T>();
            if (parts.Length == 3)
            {
                colour.R = parts[0];
                colour.G = parts[1];
                colour.B = parts[2];
            }
            return colour;
        }

        /// <summary>
        /// Gets a primitive glue
        /// </summary>
        /// <typeparam name="T">The numeric type</typeparam>
        /// <param name="element">The element</param>
        /// <returns>The primitive glue</returns>
        public X3DPrimitiveGlue<T> GetPrimitiveGlue<T>(XmlElement element, string name)
        {
            var glue = new X3DPrimitiveGlue<T>() { Matrix = GetMatrix<T>(element, name + X3DTOK.X3DTOKCOM_M),
            OriginalMatrix = GetMatrix<T>(element, name + X3DTOK.X3DTOKCOM_ORIGINALM),
            I = Get<int>(element, name + X3DTOK.X3DTOKCOM_MATREF),
            H = Get<int>(element, name + X3DTOK.X3DTOKCOM_H),
            S = Get<T>(element, name + X3DTOK.X3DTOKCOM_SCALE) };
            if (Exists(element, name + X3DTOK.X3DTOKCOM_SCALE2))
                glue.Scale = Get<T>(element, name + X3DTOK.X3DTOKCOM_SCALE2);
            else
                glue.Scale = (T)Convert.ChangeType(1.0, typeof(T));
            glue.Visibility = GetEnum<X3DVisibility>(element, name + X3DTOK.X3DTOKCOM_V);
            glue.Culling = GetEnum<X3DCullHint>(element, name + X3DTOK.X3DTOKCOM_CULLING);
            glue.Lighting = GetEnum<X3DLightingHint>(element, name + X3DTOK.X3DTOKCOM_LIGHTING);
            glue.SplineType = GetEnum<X3DSplineType>(element, name + X3DTOK.X3DTOKCOM_SPLINETYPE);
            glue.SplineOffset = GetVector3<T>(element, name + X3DTOK.X3DTOKCOM_SPLINEOFFSET);
            glue.ZBias = Get<int>(element, name + X3DTOK.X3DTOKCOM_ZBIAS);
            glue.Shader = Get<int>(element, name + X3DTOK.X3DTOKCOM_SHADER);
            glue.FillMode = GetEnum<X3DFillMode>(element, name + X3DTOK.X3DTOKCOM_FILLMODE);
            glue.ShadeMode = GetEnum<X3DShadeMode>(element, name + X3DTOK.X3DTOKCOM_SHADEMODE);
            glue.OutlineMode = GetEnum<X3DOutlineMode>(element, name + X3DTOK.X3DTOKCOM_OUTLINEMODE);
            if (Exists(element, name + X3DTOK.X3DTOKCOM_AWQ))
                glue.ArtworkQuality = GetEnum<X3DArtworkQuality>(element, name + X3DTOK.X3DTOKCOM_AWQ);
            return glue;
        }

        /// <summary>
        /// Gets a shape glue
        /// </summary>
        /// <typeparam name="T">The numeric type</typeparam>
        /// <param name="element">The element</param>
        /// <param name="name">The name</param>
        /// <returns>The shape glue</returns>
        public X3DShapeGlue<T> GetShapeGlue<T>(XmlElement element, string name, X3DShapeGlueType shapeGlueType)
        {
            X3DShapeGlue<T> glue;
            switch (shapeGlueType)
            {
                case X3DShapeGlueType.ShapeGlue:
                    glue = new X3DShapeGlue<T>();
                    break;
                case X3DShapeGlueType.JointedShapeGlue:
                    glue = new X3DJointedShapeGlue<T>();
                    {
                        var gg = glue as X3DJointedShapeGlue<T>;
                        gg.JointData.JointOffset = GetVector3<T>(element, name + X3DTOK.X3DTOKCOM_JO);
                        gg.JointData.JointAxis = GetVector3<T>(element, name + X3DTOK.X3DTOKCOM_JA);
                        gg.JointData.JointAngle = Get<T>(element, name + X3DTOK.X3DTOKCOM_JG);
                        gg.JointData.JointAxisLength = Get<T>(element, name + X3DTOK.X3DTOKCOM_JL);
                        gg.JointData.JointDisplayed = Get<bool>(element, name + X3DTOK.X3DTOKCOM_JD);
                        gg.JointData.TexCoord = GetParts<T>(element, name + X3DTOK.X3DTOKCOM_JT);
                        gg.JointData.TexCoordPerPanel = GetParts<T>(element, name + X3DTOK.X3DTOKCOM_JTPP);
                        gg.JointData.RenderJointAxis = GetVector3<T>(element, name + X3DTOK.X3DTOKCOM_JRA);
                    }
                    break;
                case X3DShapeGlueType.PhysicalShapeGlue:
                    glue = new X3DPhysicalShapeGlue<T>();
                    {
                        var gg = glue as X3DPhysicalShapeGlue<T>;
                        gg.PhysicsData.Mass = Get<T>(element, name + X3DTOK.X3DTOKCOM_MA);
                        gg.PhysicsData.OneOverMass = Get<T>(element, name + X3DTOK.X3DTOKCOM_OM);
                        gg.PhysicsData.OriginalLocation = GetVector3<T>(element, name + X3DTOK.X3DTOKCOM_OL);
                        gg.PhysicsData.InertiaTensor = GetParts<T>(element, name + X3DTOK.X3DTOKCOM_IT);
                        gg.PhysicsData.InertiaTensorInverse = GetParts<T>(element, name + X3DTOK.X3DTOKCOM_ITI);
                        gg.PhysicsData.IsAffectedByGravity = Get<bool>(element, name + X3DTOK.X3DTOKCOM_GR);
                        gg.PhysicsData.RespondsToForces = Get<bool>(element, name + X3DTOK.X3DTOKCOM_RE);
                        gg.PhysicsData.LinearDamping = Get<T>(element, name + X3DTOK.X3DTOKCOM_LD);
                        gg.PhysicsData.AngularDamping = Get<T>(element, name + X3DTOK.X3DTOKCOM_AD);
                        gg.PhysicsData.PhysicsScale = Get<T>(element, name + X3DTOK.X3DTOKCOM_PS);
                        gg.PhysicsData.ActiveConfig = Get<int>(element, name + X3DTOK.X3DTOKCOM_AC);
                        gg.PhysicsData.Config1 = new X3DPhysicsDataConfig<T>();
                        gg.PhysicsData.Config1.Transform = GetMatrix<T>(element, name + X3DTOK.X3DTOKCOM_0T);
                        gg.PhysicsData.Config1.LinearVelocity = GetVector3<T>(element, name + X3DTOK.X3DTOKCOM_0LV);
                        gg.PhysicsData.Config1.AngularVelocity = GetVector3<T>(element, name + X3DTOK.X3DTOKCOM_0AV);
                        gg.PhysicsData.Config1.Force = GetVector3<T>(element, name + X3DTOK.X3DTOKCOM_0F);
                        gg.PhysicsData.Config1.AppliedForce = GetVector3<T>(element, name + X3DTOK.X3DTOKCOM_0AF);
                        gg.PhysicsData.Config1.Torque = GetVector3<T>(element, name + X3DTOK.X3DTOKCOM_0TO);
                        gg.PhysicsData.Config1.AppliedForce = GetVector3<T>(element, name + X3DTOK.X3DTOKCOM_0AT);
                        gg.PhysicsData.Config1.Orientation = GetQuaternion<T>(element, name + X3DTOK.X3DTOKCOM_0O);
                        gg.PhysicsData.Config2 = new X3DPhysicsDataConfig<T>();
                        gg.PhysicsData.Config2.Transform = GetMatrix<T>(element, name + X3DTOK.X3DTOKCOM_1T);
                        gg.PhysicsData.Config2.LinearVelocity = GetVector3<T>(element, name + X3DTOK.X3DTOKCOM_1LV);
                        gg.PhysicsData.Config2.AngularVelocity = GetVector3<T>(element, name + X3DTOK.X3DTOKCOM_1AV);
                        gg.PhysicsData.Config2.Force = GetVector3<T>(element, name + X3DTOK.X3DTOKCOM_1F);
                        gg.PhysicsData.Config2.AppliedForce = GetVector3<T>(element, name + X3DTOK.X3DTOKCOM_1AF);
                        gg.PhysicsData.Config2.Torque = GetVector3<T>(element, name + X3DTOK.X3DTOKCOM_1TO);
                        gg.PhysicsData.Config2.AppliedForce = GetVector3<T>(element, name + X3DTOK.X3DTOKCOM_1AT);
                        gg.PhysicsData.Config2.Orientation = GetQuaternion<T>(element, name + X3DTOK.X3DTOKCOM_1O);
                    }
                    break;
                case X3DShapeGlueType.LodGlue:
                    glue = new X3DLodGlue<T>();
                    {
                        var gg = glue as X3DLodGlue<T>;
                    }
                    break;
                case X3DShapeGlueType.LayerGlue:
                    glue = new X3DLayerGlue<T>();
                    {
                        var gg = glue as X3DLayerGlue<T>;
                    }
                    break;
                default:
                    glue = new X3DShapeGlue<T>();
                    break;
            }
            glue.ShapeGlueType = shapeGlueType;
            glue.Matrix = GetMatrix<T>(element, name + X3DTOK.X3DTOKCOM_M);
            glue.OriginalMatrix = GetMatrix<T>(element, name + X3DTOK.X3DTOKCOM_ORIGINALM);
            glue.Visibility = GetEnum<X3DVisibility>(element, name + X3DTOK.X3DTOKCOM_V);
            glue.Name = Get<string>(element, name + X3DTOK.X3DTOKCOM_NAME);
            glue.Icon = Get<int>(element, name + X3DTOK.X3DTOKCOM_ICON);
            glue.IconSelected = Get<int>(element, name + X3DTOK.X3DTOKCOM_ICONSELECTED);
            glue.SplineType = GetEnum<X3DSplineType>(element, name + X3DTOK.X3DTOKCOM_SPLINETYPE);
            glue.Scale = Get<T>(element, name + X3DTOK.X3DTOKCOM_SCALE);
            glue.SplineOffset = GetVector3<T>(element, name + X3DTOK.X3DTOKCOM_SPLINEOFFSET);
            return glue;
        }

        /// <summary>
        /// Gets a material from the given element (possibly a multi-material)
        /// </summary>
        /// <param name="egMaterial">The material element</param>
        /// <param name="material">The recipient material</param>
        /// <param name="image">The image</param>
        /// <param name="token">The attribute token prefix</param>
        public void GetMaterial<TY>(
            XmlElement egMaterial,
            ref X3DMaterial<TY> material,
            ref object movie,
            string token)
        {
            material = new X3DMaterial<TY>() { Emissive = GetColourRGB<TY>(egMaterial, token + X3DTOK.X3DTOKCOM_EM), Diffuse = GetColourRGB<TY>(egMaterial, token + X3DTOK.X3DTOKCOM_DI), Specular = GetColourRGB<TY>(egMaterial, token + X3DTOK.X3DTOKCOM_SP), Power = Get<TY>(egMaterial, token + X3DTOK.X3DTOKCOM_PO), Opacity = Get<TY>(egMaterial, token + X3DTOK.X3DTOKCOM_OP), Shiny = Get<TY>(egMaterial, token + X3DTOK.X3DTOKCOM_SHINY), BumpMap = Get<int>(egMaterial, token + X3DTOK.X3DTOKCOM_BUMPMAP), Shadow = GetEnum<X3DShadowCasting>(egMaterial, token + X3DTOK.X3DTOKCOM_SHADOWCASTING), TexScale1 = Get<TY>(egMaterial, token + X3DTOK.X3DTOKCOM_TS1), TexScale2 = Get<TY>(egMaterial, token + X3DTOK.X3DTOKCOM_TS2), MaterialType = GetEnum<X3DMaterialType>(egMaterial, token + X3DTOK.X3DTOKCOM_TY) };
            switch (material.MaterialType)
            {
                case X3DMaterialType.TextureRenderer:
                case X3DMaterialType.LiveVideo:
                case X3DMaterialType.Bitmap:
                case X3DMaterialType.BGR:
                case X3DMaterialType.BGRA:
                    {
                        var egTexture = egMaterial.SelectSingleNode(
                        token + X3DTOK.X3DTOKCOM_IMAGE) as XmlElement;
                        if (null != egTexture)
                        {
                            material.Filename = Get<string>(egTexture, token + X3DTOK.X3DTOKCOM_FILENAME);
                            if (material.MaterialType == X3DMaterialType.TextureRenderer)
                                movie = GetRawFile(material.Filename);
                            material.Filter = GetEnum<X3DTextureFilter>(egTexture, token + X3DTOK.X3DTOKCOM_FILTER);
                            material.Mode = GetEnum<X3DTextureMode>(egTexture, token + X3DTOK.X3DTOKCOM_MODE);
                            material.RepeatX = GetEnum<X3DTextureRepeat>(egTexture, token + X3DTOK.X3DTOKCOM_REPX);
                            material.RepeatY = GetEnum<X3DTextureRepeat>(egTexture, token + X3DTOK.X3DTOKCOM_REPY);
                            if (Exists(egTexture, token + X3DTOK.X3DTOKCOM_TRANSFORM))
                            {
                                material.Transform = GetMatrix<TY>(egTexture, token + X3DTOK.X3DTOKCOM_TRANSFORM);
                                material.ApplyTransform = true;
                            }
                            else
                                material.ApplyTransform = false;
                            material.ColourScale = GetVector4F<TY>(egTexture, token + X3DTOK.X3DTOKCOM_SCALE);
                            material.ColourBias = GetVector4F<TY>(egTexture, token + X3DTOK.X3DTOKCOM_BIAS);
                            material.Purpose = GetEnum<X3DMaterialPurpose>(egTexture, token + X3DTOK.X3DTOKCOM_PURPOSE);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
