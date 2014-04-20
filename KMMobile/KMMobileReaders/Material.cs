using KMMobile.X3D;
using System;
using WaveEngine.Common.Math;

namespace KMMobile
{
    /// <summary>
    /// A material
    /// </summary>
    public class Material
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Material()
        {
            Guid = Guid.NewGuid();
        }
        /// <summary>
        /// Whether to not use the guid
        /// </summary>
        public bool DoNotUseGUID;
        /// <summary>
        /// The name of the material
        /// </summary>
        public string Name;
        /// <summary>
        /// The unique material name
        /// </summary>
        public Guid Guid;
        /// <summary>
        /// The tag (contains user-defined items)
        /// </summary>
        public object Tag;

        //MESH_BASE_STANDARD
        /// <summary>
        /// The diffuse texture
        /// </summary>
        public string DiffuseTexture;
        /// <summary>
        /// The diffuse texture size
        /// </summary>
        public int DiffuseSizeX;
        public int DiffuseSizeY;
        /// <summary>
        /// The normal map
        /// </summary>
        public string NormalMap;
        /// <summary>
        /// The specular map
        /// </summary>
        public string SpecularTexture;
        /// <summary>
        /// The ambient map
        /// </summary>
        public string AmbientMap;
        /// <summary>
        /// The base transform
        /// </summary>
        public Vector4? BaseTransform;
        /// <summary>
        /// The factor to scale how much the environment and ambient texture
        /// illuminate the surface
        /// </summary>
        public float? EnvironmentScale;
        /// <summary>
        /// Multiplier for diffuse colours
        /// </summary>
        public float? DiffuseScale;
        /// <summary>
        /// Factor to scale the specular component
        /// </summary>
        public float? SpecularScale;
        /// <summary>
        /// Coefficient that influences the size of the highlight (phong)
        /// </summary>
        public float? SpecularPower;
        /// <summary>
        /// Width of an additionally lightened rim of an object
        /// </summary>
        public float? PhongRimWidth;
        /// <summary>
        /// Factor to scale brightness of the additionally lightened rim
        /// </summary>
        public float? PhongRimScale;
        /// <summary>
        /// Main angle that determines direction of the anisotropy
        /// </summary>
        public float? AnisotropyAngle;
        /// <summary>
        /// Factor that controls the anisotropy strength
        /// </summary>
        public float? AnisotropyScale;
        /// <summary>
        /// Coefficient that allows to compensate for fresnel effect
        /// </summary>
        public float? FresnelBias;
        /// <summary>
        /// Factor to scale fresnel effect
        /// </summary>
        public float? FresnelPower;
        /// <summary>
        /// Colour multiplier for all colour components (Multiplier)
        /// </summary>
        public Colour? ColourScale;

        //AUXILIARY
        /// <summary>
        /// Colour to render a surface with material applied as a solid contour
        /// </summary>
        public Colour? AuxiliaryColour;

        //REFRACTION
        /// <summary>
        /// Refraction coefficient
        /// </summary>
        public float? RefractionScale;

        //LIGHTMAP
        /// <summary>
        /// The lightmap texture
        /// </summary>
        public string LightMap;
        /// <summary>
        /// The intensity of illumination by the light map
        /// </summary>
        public float? LightMapScale;

        //DETAIL
        /// <summary>
        /// The detail transform
        /// </summary>
        public Vector4? DetailTransform;
        /// <summary>
        /// The detail diffuse texture
        /// </summary>
        public string DetailDiffuseTexture;
        /// <summary>
        /// The detail normal map
        /// </summary>
        public string DetailNormalMap;
        /// <summary>
        /// The detail specular map
        /// </summary>
        public string DetailSpecularMap;
        /// <summary>
        /// Visibility of the detail diffuse texture
        /// </summary>
        public float? DetailDiffuse;
        /// <summary>
        /// Visibility of the detail normal map
        /// </summary>
        public float? DetailNormal;
        /// <summary>
        /// Visibility of the detail specular map
        /// </summary>
        public float? DetailSpecular;

        //PARALAX
        /// <summary>
        /// The parallax map
        /// </summary>
        public string ParallaxMap;
        /// <summary>
        /// Distance to displace pixels by parallax mapping
        /// </summary>
        public float? ParallaxScale;

        //EMISSION
        /// <summary>
        /// Factor to scale the glow component which creates a halo
        /// around an object surface
        /// </summary>
        public float? GlowScale;
        /// <summary>
        /// Scale of visibility for the emission texture
        /// </summary>
        public float? EmissionScale;
        /// <summary>
        /// The emission map
        /// </summary>
        public string EmissionMap;

        /// <summary>
        /// flag marking if back face is culled or drawn
        /// </summary>
        public bool TwoSided;

        /// <summary>
        /// The opacity
        /// </summary>
        public float? Opacity;
        /// <summary>
        /// The shininess
        /// </summary>
        public float? Shininess;
        /// <summary>
        /// The shininess strength
        /// </summary>
        public float? ShininessStrength;

        //UNUSED
        /// <summary>
        /// The diffuse colour
        /// </summary>
        public Colour? DiffuseColour;
        /// <summary>
        /// The emissive colour
        /// </summary>
        public Colour? EmissiveColour;
        /// <summary>
        /// The ambient colour
        /// </summary>
        public Colour? AmbientColour;
        /// <summary>
        /// The specular colour
        /// </summary>
        public Colour? SpecularColour;

        //REFLECTIVE MATERIAL ONLY
        /// <summary>
        /// Transparent
        /// </summary>
        public int? Transparent;
        /// <summary>
        /// Reflective
        /// </summary>
        public int? Reflective;
        /// <summary>
        /// The reflectivity
        /// </summary>
        public float? Reflectivity;
        /// <summary>
        /// The refraction
        /// </summary>
        public float? Refraction;
    }
}
