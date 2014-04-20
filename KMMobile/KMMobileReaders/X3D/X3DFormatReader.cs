using KMMobile.ZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Xml;

namespace KMMobile.X3D
{
    public class X3DFormatReader<TY> : X3DBaseReader
    {
        /// <summary>
        /// The version of X3D that was used to save the X3D file
        /// </summary>
        public string X3DVersion = string.Empty;

        /// <summary>
        /// The kind of contents this file has
        /// </summary>
        public string What = string.Empty;

        /// <summary>
        /// The program that created this file
        /// </summary>
        public string CreatingProgram = string.Empty;

        /// <summary>
        /// The version of KASEMAKE that was used to save the X3D file
        /// </summary>
        public string CreatingProgramVersion = string.Empty;

        /// <summary>
        /// The copyright notice for this version of KASEMAKE / X3D
        /// </summary>
        public string CopyrightNotice = string.Empty;

        /// <summary>
        /// The time the file was saved
        /// </summary>
        public string CreationTime = string.Empty;

        /// <summary>
        /// The date the file was saved
        /// </summary>
        public string CreationDate = string.Empty;

        /// <summary>
        /// The user that saved the file
        /// </summary>
        public string CreatingUser = string.Empty;

        /// <summary>
        /// The computer that the file was saved on
        /// </summary>
        public string CreatingComputer = string.Empty;

        /// <summary>
        /// The operating system of the computer that the file was saved on
        /// </summary>
        public string CreatingOperatingSystem = string.Empty;

        /// <summary>
        /// The variables associated with this file
        /// </summary>
        public Dictionary<string, string> Vars;

        /// <summary>
        /// The scene scale
        /// </summary>
        public TY SceneScale = default(TY);

        /// <summary>
        /// The list of viewpoints
        /// </summary>
        public List<X3DView<TY>> Viewpoints;

        /// <summary>
        /// The list of cameras
        /// </summary>
        public List<X3DViewpoint<TY>> Cameras;

        /// <summary>
        /// The camera switching animation (time -> camera)
        /// </summary>
        public Dictionary<int, int> CameraSwitching;

        /// <summary>
        /// The list of lights
        /// </summary>
        public List<X3DLight<TY>> Lights;

        /// <summary>
        /// The dictionary of materials
        /// </summary>
        public Dictionary<int, X3DMaterialDef<TY>> Materials;

        /// <summary>
        /// The dictionary of primitives
        /// </summary>
        public Dictionary<int, X3DPrimitive<TY>> Primitives;

        /// <summary>
        /// The dictionary of artwork
        /// </summary>
        public Dictionary<int, X3DShape<TY>> Artwork;

        /// <summary>
        /// The dictionary of shapes
        /// </summary>
        public Dictionary<int, X3DShape<TY>> Shapes;

        /// <summary>
        /// The sound animation (if applicable)
        /// </summary>
        public X3DSoundAnimation SoundAnimation;

        /// <summary>
        /// Any panorama
        /// </summary>
        public X3DPanorama<TY> Panorama;

        /// <summary>
        /// Any environment map
        /// </summary>
        public X3DEnvironmentMap<TY> EnvironmentMap;

        /// <summary>
        /// The background colour
        /// </summary>
        public X3DColourRGB<TY>? BackgroundColour;

        /// <summary>
        /// The ambient colour
        /// </summary>
        public X3DColourRGB<TY>? AmbientColour;

        /// <summary>
        /// The background
        /// </summary>
        public X3DBackground<TY> Background;

        /// <summary>
        /// The gradient background
        /// </summary>
        public X3DGradientBackground<TY> GradientBackground;

        /// <summary>
        /// The watermark
        /// </summary>
        public X3DWatermark<TY> Watermark;

        /// <summary>
        /// The fog
        /// </summary>
        public X3DFog<TY> Fog;

        /// <summary>
        /// The ground
        /// </summary>
        public X3DGround<TY> Ground;

        /// <summary>
        /// The walkthrough
        /// </summary>
        public X3DWalkthrough<TY> Walkthrough;

        /// <summary>
        /// The notes
        /// </summary>
        public XmlDocument Notes;

        /// <summary>
        /// The measurements
        /// </summary>
        public List<X3DMeasurement<TY>> Measurements;

        /// <summary>
        /// The animations
        /// </summary>
        public List<X3DAnimation<TY>> Animations;

        /// <summary>
        /// Opens an X3D-type file (e.g. KSN and KOB files)
        /// </summary>
        /// <param name="filename">The filename to open</param>
        /// <param name="createImageFromBinary">The function to create an image from binary data</param>
        /// <returns>The reader</returns>
        public static X3DFormatReader<TY> Open(string filename, Func<byte[], bool, bool, X3DMobileImage> createImageFromBinary)
        {
            return new X3DFormatReader<TY>(new ZipFile(filename), createImageFromBinary);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="file">The file we are reading</param>
        /// <param name="createImageFromBinary">The function to create an image from binary data</param>
        /// <param name="progress">The progress bar</param>
        public X3DFormatReader(ZipFile file, Func<byte[], bool, bool, X3DMobileImage> createImageFromBinary)
            : base(file, createImageFromBinary)
        {
            var rawXml = RawXml;
            var egInfo = rawXml.SelectSingleNode("//" + X3DTOK.X3DTOK_EGINFO) as XmlElement;
            if (null != egInfo)
            {
                X3DVersion = egInfo.GetAttribute(X3DTOK.X3DTOK_EGX3D);
                What = egInfo.GetAttribute(X3DTOK.X3DTOK_EGWHAT);
                CreatingProgram = egInfo.GetAttribute(X3DTOK.X3DTOK_EGPROGRAM);
                CreatingProgramVersion = egInfo.GetAttribute(X3DTOK.X3DTOK_EGVERSION);
                CopyrightNotice = egInfo.GetAttribute(X3DTOK.X3DTOK_EGCOPYRIGHT);
                CreationTime = egInfo.GetAttribute(X3DTOK.X3DTOK_EGTIME);
                CreationDate = egInfo.GetAttribute(X3DTOK.X3DTOK_EGDATE);
                CreatingUser = egInfo.GetAttribute(X3DTOK.X3DTOK_EGUSER);
                CreatingComputer = egInfo.GetAttribute(X3DTOK.X3DTOK_EGCOMPUTER);
                CreatingOperatingSystem = egInfo.GetAttribute(X3DTOK.X3DTOK_EGOPERATINGSYSTEM);
                var egVars = rawXml.SelectSingleNode("//" + X3DTOK.X3DTOK_EGVARS) as XmlElement;
                if (null != egVars)
                {
                    Vars = new Dictionary<string, string>();
                    SceneScale = Get<TY>(egVars, X3DTOK.X3DTOK_SCENESCALE);
                    foreach (XmlElement egVar in
                        egVars.SelectNodes(X3DTOK.X3DTOK_PAIR))
                    {
                        Vars[egVar.GetAttribute(X3DTOK.X3DTOK_NAME)] = egVar.GetAttribute(X3DTOK.X3DTOK_VALUE);
                    }
                }
                var egViews =
                rawXml.SelectSingleNode("//" + X3DTOK.X3DTOK_EGVIEWS) as XmlElement;
                if (null != egViews)
                {
                    Viewpoints = new List<X3DView<TY>>();
                    foreach (XmlElement egView in
                        egViews.SelectNodes(X3DTOK.X3DTOK_EGVIEW))
                    {
                        var vp = new X3DView<TY>();
                        var viewpoint =
                        egView.SelectSingleNode(X3DTOK.X3DTOK_VIEWPOINT) as XmlElement;
                        vp.Viewpoint.ViewNavigation = GetEnum<X3DViewNavigation>(viewpoint, X3DTOK.X3DTOK_VPMODE);
                        vp.Viewpoint.StaticMatrix = GetMatrix<TY>(viewpoint, X3DTOK.X3DTOK_VPSTATIC);
                        vp.Viewpoint.ExamineMatrix = GetMatrix<TY>(viewpoint, X3DTOK.X3DTOK_VPEXAMINE);
                        vp.Viewpoint.FlyMatrix = GetMatrix<TY>(viewpoint, X3DTOK.X3DTOK_VPFLY);
                        vp.Viewpoint.WalkMatrix = GetMatrix<TY>(viewpoint, X3DTOK.X3DTOK_VPWALK);

                        var projection =
                        viewpoint.SelectSingleNode(X3DTOK.X3DTOK_PROJECTION) as XmlElement;
                        vp.Projection.ProjectionType = GetEnum<X3DProjectionType>(projection, X3DTOK.X3DTOK_PRJTYPE);
                        vp.Projection.P1 = Get<TY>(projection, X3DTOK.X3DTOK_PRJP1);
                        vp.Projection.P2 = Get<TY>(projection, X3DTOK.X3DTOK_PRJP2);
                        vp.Projection.P3 = Get<TY>(projection, X3DTOK.X3DTOK_PRJP3);
                        vp.Projection.P4 = Get<TY>(projection, X3DTOK.X3DTOK_PRJP4);
                        vp.Projection.P5 = Get<TY>(projection, X3DTOK.X3DTOK_PRJP5);
                        vp.Projection.P6 = Get<TY>(projection, X3DTOK.X3DTOK_PRJP6);

                        var avatar =
                        viewpoint.SelectSingleNode(X3DTOK.X3DTOK_AVATAR) as XmlElement;
                        vp.Avatar.Width = Get<TY>(avatar, X3DTOK.X3DTOK_AVWIDTH);
                        vp.Avatar.Height = Get<TY>(avatar, X3DTOK.X3DTOK_AVHEIGHT);
                        vp.Avatar.StepHeight = Get<TY>(avatar, X3DTOK.X3DTOK_AVSTEP);
                        vp.Avatar.RelativeSpeed = Get<TY>(avatar, X3DTOK.X3DTOK_AVSPEED);
                        vp.Avatar.ShowHeadlight = Get<bool>(avatar, X3DTOK.X3DTOK_AVHEADLIGHT);

                        vp.ShowNotes = Get<bool>(viewpoint, X3DTOK.X3DTOK_VIEWSN);
                        vp.ShowWatermark = Get<bool>(viewpoint, X3DTOK.X3DTOK_VIEWSW);

                        Viewpoints.Add(vp);
                    }
                }
                var egCameras =
                rawXml.SelectSingleNode("//" + X3DTOK.X3DTOK_EGCAMERAS) as XmlElement;
                if (null != egCameras)
                {
                    Cameras = new List<X3DViewpoint<TY>>();
                    foreach (XmlElement egCamera in
                        egCameras.SelectNodes(X3DTOK.X3DTOK_CAMERA))
                    {
                        var camera = new X3DViewpoint<TY>() { FlyMatrix = GetMatrix<TY>(egCamera, X3DTOK.X3DTOK_VPCAMERA), SplineType = GetEnum<X3DSplineType>(egCamera, X3DTOK.X3DTOK_SPLINETYPE) };
                        var egCameraAnimation = egCamera.SelectSingleNode(
                        X3DTOK.X3DTOK_ANIMATION) as XmlElement;
                        if (null != egCameraAnimation)
                        {
                            camera.CameraAnimation = new X3DCameraAnimation<TY>();
                        }
                        else
                        {
                            //  The camera is not animated
                        }
                        Cameras.Add(camera);
                    }
                }
                var egCameraSwitching =
                rawXml.SelectSingleNode("//" + X3DTOK.X3DTOK_EGCAMERASWITCHING) as XmlElement;
                if (null != egCameraSwitching)
                {
                    CameraSwitching = new Dictionary<int, int>();
                    foreach (XmlElement egAnimationFrame in
                        egCameraSwitching.SelectNodes(X3DTOK.X3DTOK_FRAME))
                    {
                        int key =
                        Get<int>(egAnimationFrame, X3DTOK.X3DTOK_KEY);
                        int camera =
                        Get<int>(egAnimationFrame, X3DTOK.X3DTOK_CAMERASWITCH);
                        CameraSwitching[key] = camera;
                    }
                }
                var egLights =
                rawXml.SelectSingleNode("//" + X3DTOK.X3DTOK_EGLIGHTS) as XmlElement;
                if (null != egLights)
                {
                    Lights = new List<X3DLight<TY>>();
                    foreach (XmlElement egLight in
                        egLights.SelectNodes(X3DTOK.X3DTOK_EGLIGHT))
                    {
                        var light = new X3DLight<TY>() { Id = Get<int>(egLight, X3DTOK.X3DTOK_ID), LightType = GetEnum<X3DLightType>(egLight, X3DTOK.X3DTOK_T), AmbientColour = GetColourRGBA<TY>(egLight, X3DTOK.X3DTOK_A), DiffuseColour = GetColourRGBA<TY>(egLight, X3DTOK.X3DTOK_D), SpecularColour = GetColourRGBA<TY>(egLight, X3DTOK.X3DTOK_S), Exponent = Get<TY>(egLight, X3DTOK.X3DTOK_E), Cutoff = Get<TY>(egLight, X3DTOK.X3DTOK_C), Attenuation = GetVector3<TY>(egLight, X3DTOK.X3DTOK_AT), PresentationScale = Get<TY>(egLight, X3DTOK.X3DTOK_SC), On = Get<bool>(egLight, X3DTOK.X3DTOK_ON), Shown = Get<bool>(egLight, X3DTOK.X3DTOK_SH) };
                        var egLightGlue =
                        egLight.SelectSingleNode(X3DTOK.X3DTOK_EGGLUE) as XmlElement;
                        var glue = new X3DLightGlue<TY>() { Matrix = GetMatrix<TY>(egLightGlue, X3DTOK.X3DTOK_EGGLUE + X3DTOK.X3DTOKCOM_M), OriginalMatrix = GetMatrix<TY>(egLightGlue, X3DTOK.X3DTOK_EGGLUE + X3DTOK.X3DTOKCOM_ORIGINALM), H = Get<int>(egLight, X3DTOK.X3DTOK_EGGLUE + X3DTOK.X3DTOKCOM_LEVEL), Visibility = GetEnum<X3DVisibility>(egLightGlue, X3DTOK.X3DTOK_EGGLUE + X3DTOK.X3DTOKCOM_V), Name = Get<string>(egLightGlue, X3DTOK.X3DTOK_EGGLUE + X3DTOK.X3DTOKCOM_NAME), Icon = Get<int>(egLightGlue, X3DTOK.X3DTOK_EGGLUE + X3DTOK.X3DTOKCOM_ICON), IconSelected = Get<int>(egLightGlue, X3DTOK.X3DTOK_EGGLUE + X3DTOK.X3DTOKCOM_ICONSELECTED), SplineType = GetEnum<X3DSplineType>(egLightGlue, X3DTOK.X3DTOK_EGGLUE + X3DTOK.X3DTOKCOM_SPLINETYPE), SplineOffset = GetVector3<TY>(egLightGlue, X3DTOK.X3DTOK_EGGLUE + X3DTOK.X3DTOKCOM_SPLINEOFFSET) };
                        light.Glue = glue;
                        Lights.Add(light);
                    }
                }
                var egMaterials =
                rawXml.SelectSingleNode("//" + X3DTOK.X3DTOK_EGMATERIALS) as XmlElement;
                if (null != egMaterials)
                {
                    Materials = new Dictionary<int, X3DMaterialDef<TY>>();
                    foreach (XmlElement egMaterial in
                        egMaterials.SelectNodes(X3DTOK.X3DTOK_EGMATERIAL))
                    {
                        var matid = Get<int>(egMaterial, X3DTOK.X3DTOK_MREF);
                        var num = Get<int>(egMaterial, X3DTOK.X3DTOK_NUM);
                        var material = new X3DMaterialDef<TY>() { MaterialCount = num, Materials = new X3DMaterial<TY>[num], Movies = new object[num], ArtworkOnly = Get<bool>(egMaterial, X3DTOK.X3DTOK_ARTWORKONLY) };
                        for (int i = 0; i < num; i++)
                        {
                            GetMaterial(
                            egMaterial,
                            ref material.Materials[i],
                            ref material.Movies[i],
                            (i == 0) ? X3DTOK.X3DTOK_M0 : (i == 1) ? X3DTOK.X3DTOK_M1 : X3DTOK.X3DTOK_M2);
                        }
                        Materials[matid] = material;
                    }
                }
                var egPrimitives =
                rawXml.SelectSingleNode("//" + X3DTOK.X3DTOK_EGPRIMITIVES) as XmlElement;
                if (null != egPrimitives)
                {
                    Primitives = new Dictionary<int, X3DPrimitive<TY>>();
                    foreach (XmlElement egPrimitive in
                        egPrimitives.SelectNodes(X3DTOK.X3DTOK_EGPRIMITIVE))
                    {
                        var prim = X3DPrimitive<TY>.Construct(egPrimitive, this);
                        Primitives[prim.ID] = prim;
                    }
                }
                var egArtwork =
                rawXml.SelectSingleNode("//" + X3DTOK.X3DTOK_EGARTWORK) as XmlElement;
                if (null != egArtwork)
                {
                    Artwork = new Dictionary<int, X3DShape<TY>>();
                    foreach (XmlElement egArt in
                        egArtwork.SelectNodes(X3DTOK.X3DTOK_EGSHAPE))
                    {
                        X3DShape<TY> shape = X3DShape<TY>.Construct(egArt, this);
                        Artwork[shape.ID] = shape;
                    }
                }
                var egShapes =
                rawXml.SelectSingleNode("//" + X3DTOK.X3DTOK_EGSHAPES) as XmlElement;
                if (null != egShapes)
                {
                    Shapes = new Dictionary<int, X3DShape<TY>>();
                    foreach (XmlElement egShape in
                        egShapes.SelectNodes(X3DTOK.X3DTOK_EGSHAPE))
                    {
                        var shape = X3DShape<TY>.Construct(egShape, this);
                        Shapes[shape.ID] = shape;
                        shape.CreateOrigami(egShape, this);
                    }
                }
                var egSounds =
                rawXml.SelectSingleNode("//" + X3DTOK.X3DTOK_EGSOUNDS) as XmlElement;
                if (null != egSounds)
                {
                    SoundAnimation = new X3DSoundAnimation();
                    SoundAnimation.Create(egSounds, this);
                }
                var egEnvironment =
                rawXml.SelectSingleNode("//" + X3DTOK.X3DTOK_EGENVIRONMENT) as XmlElement;
                if (null != egEnvironment)
                {
                    var panorama = egEnvironment.SelectSingleNode(X3DTOK.X3DTOK_EGPANORAMA) as XmlElement;
                    if (null != panorama)
                    {
                        Panorama = new X3DPanorama<TY>();
                        Panorama.Create(panorama, this);
                    }
                    var envmap = egEnvironment.SelectSingleNode(X3DTOK.X3DTOK_EGENVIRONMENTMAP) as XmlElement;
                    if (null != envmap)
                    {
                        EnvironmentMap = new X3DEnvironmentMap<TY>();
                        EnvironmentMap.Create(envmap, this);
                    }
                    if (Exists(egEnvironment, X3DTOK.X3DTOK_BACKGROUNDCOLOUR))
                        BackgroundColour = GetColourRGB<TY>(egEnvironment, X3DTOK.X3DTOK_BACKGROUNDCOLOUR);
                    if (Exists(egEnvironment, X3DTOK.X3DTOK_AMBIENTCOLOUR))
                        AmbientColour = GetColourRGB<TY>(egEnvironment, X3DTOK.X3DTOK_AMBIENTCOLOUR);
                    var background = egEnvironment.SelectSingleNode(X3DTOK.X3DTOK_EGBACKGROUND) as XmlElement;
                    if (null != background)
                    {
                        Background = new X3DBackground<TY>();
                        Background.Create(background, this);
                    }
                    var gradient = egEnvironment.SelectSingleNode(X3DTOK.X3DTOK_EGGRADIENTBACKGROUND) as XmlElement;
                    if (null != gradient)
                    {
                        GradientBackground = new X3DGradientBackground<TY>();
                        GradientBackground.Create(gradient, this);
                    }
                    var watermark = egEnvironment.SelectSingleNode(X3DTOK.X3DTOK_EGWATERMARK) as XmlElement;
                    if (null != watermark)
                    {
                        Watermark = new X3DWatermark<TY>();
                        Watermark.Create(watermark, this);
                    }
                    var fog = egEnvironment.SelectSingleNode(X3DTOK.X3DTOK_EGFOG) as XmlElement;
                    if (null != fog)
                    {
                        Fog = new X3DFog<TY>();
                        Fog.Create(fog, this);
                    }
                    var ground = egEnvironment.SelectSingleNode(X3DTOK.X3DTOK_EGGROUND) as XmlElement;
                    if (null != ground)
                    {
                        Ground = new X3DGround<TY>();
                        Ground.Create(ground, this);
                    }
                    var walkthrough = egEnvironment.SelectSingleNode(X3DTOK.X3DTOK_WALKTHROUGH) as XmlElement;
                    if (null != walkthrough)
                    {
                        Walkthrough = new X3DWalkthrough<TY>();
                        Walkthrough.Create(walkthrough, this);
                    }
                }
                var egNotes =
                rawXml.SelectSingleNode("//" + X3DTOK.X3DTOK_NOTES) as XmlElement;
                if (null != egNotes)
                {
                    Notes = new XmlDocument();
                    Notes.LoadXml(egNotes.InnerXml);
                }
                var egMeasurements =
                rawXml.SelectSingleNode("//" + X3DTOK.X3DTOK_EGMEASUREMENTS) as XmlElement;
                if (null != egMeasurements)
                {
                    Measurements = new List<X3DMeasurement<TY>>();
                    foreach (XmlElement boundMeasure in
                        egMeasurements.SelectNodes(X3DTOK.X3DTOK_BOUNDINGMEASUREMENT))
                    {
                        var bound = new X3DBoundingMeasurement<TY>();
                        bound.Create(boundMeasure, this);
                        Measurements.Add(bound);
                    }
                    foreach (XmlElement cogMeasure in
                        egMeasurements.SelectNodes(X3DTOK.X3DTOK_CENTREOFGRAVITY))
                    {
                        var bound = new X3DCentreOfGravityMeasurement<TY>();
                        bound.Create(cogMeasure, this);
                        Measurements.Add(bound);
                    }
                    foreach (XmlElement p2pMeasure in
                        egMeasurements.SelectNodes(X3DTOK.X3DTOK_POINTTOPOINT))
                    {
                        var bound = new X3DPointToPointMeasurement<TY>();
                        bound.Create(p2pMeasure, this);
                        Measurements.Add(bound);
                    }
                }
            }
        }
    }
}
