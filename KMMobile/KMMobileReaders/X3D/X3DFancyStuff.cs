using System.Xml;

namespace KMMobile.X3D
{
    public class X3DBackground<TY>
    {
        public X3DBackgroundData<TY> Data = new X3DBackgroundData<TY>();

        public void Create(XmlElement element, X3DBaseReader reader)
        {
            reader.GetMaterial<TY>(element, ref Data.Material, ref Data.Movie, X3DTOK.X3DTOK_BKIM);
            Data.Mode = X3DBaseReader.GetEnum<X3DBackgroundMode>(element, X3DTOK.X3DTOK_BKMO);
        }
    }

    public class X3DGradientBackground<TY>
    {
        public X3DGradientBackgroundData<TY> Data = new X3DGradientBackgroundData<TY>();

        public void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.Colour1 = reader.GetColourRGB<TY>(element, X3DTOK.X3DTOK_GRC0);
            Data.Colour2 = reader.GetColourRGB<TY>(element, X3DTOK.X3DTOK_GRC1);
            Data.Colour3 = reader.GetColourRGB<TY>(element, X3DTOK.X3DTOK_GRC2);
            Data.Colour4 = reader.GetColourRGB<TY>(element, X3DTOK.X3DTOK_GRC3);
        }
    }

    public class X3DWatermark<TY>
    {
        public X3DWatermarkData<TY> Data = new X3DWatermarkData<TY>();

        public void Create(XmlElement element, X3DBaseReader reader)
        {
            //  This was not serialized in X3D. I don't remember why...
            /*
                                                                        reader.GetMaterial<TY>(element, ref Data.Material, ref Data.Texture, ref Data.Movie, X3DTOK.X3DTOK_WMM);
                                                                        Data.WatermarkPosition = reader.GetEnum<X3DWatermarkPosition>(element, X3DTOK.X3DTOK_WMP);
                                                                        Data.X = reader.Get<int>(element, X3DTOK.X3DTOK_WMX);
                                                                        Data.Y = reader.Get<int>(element, X3DTOK.X3DTOK_WMY);
                                                                        */
        }
    }

    public class X3DFog<TY>
    {
        public X3DFogData<TY> Data = new X3DFogData<TY>();

        public void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.Enabled = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_FEN);
            Data.FogType = X3DBaseReader.GetEnum<X3DFogType>(element, X3DTOK.X3DTOK_FT);
            Data.FogColour = reader.GetColourRGBA<TY>(element, X3DTOK.X3DTOK_FCOL);
            Data.FogStart = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_FS);
            Data.FogEnd = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_FE);
            Data.FogDensity = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_FD);
            Data.FarPlane = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_FF);
        }
    }

    public class X3DGround<TY>
    {
        public X3DGroundData<TY> Data = new X3DGroundData<TY>();

        public void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.Enabled = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_GREN);
            Data.Tiling = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_GRTI);
            Data.GroundType = X3DBaseReader.GetEnum<X3DGroundType>(element, X3DTOK.X3DTOK_GRGT);
            Data.Size = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_GRSZ);
            reader.GetMaterial<TY>(element, ref Data.Material, ref Data.Movie, X3DTOK.X3DTOK_GRMA);
            Data.Offset = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_GROF);
            Data.Reflective = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_GRRE);
            Data.Opacity = X3DBaseReader.Get<TY>(element, X3DTOK.X3DTOK_GROP);
            Data.Lit = X3DBaseReader.Get<bool>(element, X3DTOK.X3DTOK_GRLI);
            if ((Data.GroundType == X3DGroundType.FacetHeightField) ||
            (Data.GroundType == X3DGroundType.SmoothHeightField))
            {
                Data.XS = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_GRXS);
                Data.YS = X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_GRYS);
                Data.HeightMap = reader.GetParts<TY>(element, X3DTOK.X3DTOK_GRHT);
            }
            if (X3DBaseReader.Exists(element, X3DTOK.X3DTOK_GRCOL))
                Data.Colour = reader.GetColourRGBA<TY>(element, X3DTOK.X3DTOK_GRCOL);
        }
    }

    public class X3DWalkthrough<TY>
    {
        public void Create(XmlElement element, X3DBaseReader reader)
        {
        }
    }

    public class X3DPanorama<TY>
    {
        public X3DPanoramaData<TY> Data = new X3DPanoramaData<TY>();

        public void Create(XmlElement element, X3DBaseReader reader)
        {
            Data.PanoType = X3DBaseReader.GetEnum<X3DPanoramaType>(element, X3DTOK.X3DTOK_PAPT);
            reader.GetMaterial<TY>(element, ref Data.Spherical, ref Data.SphericalMovie, X3DTOK.X3DTOK_PASP);
            reader.GetMaterial<TY>(element, ref Data.Cylindrical, ref Data.CylindricalMovie, X3DTOK.X3DTOK_PACY);
            reader.GetMaterial<TY>(element, ref Data.Front, ref Data.FrontMovie, X3DTOK.X3DTOK_PAFR);
            reader.GetMaterial<TY>(element, ref Data.Back, ref Data.BackMovie, X3DTOK.X3DTOK_PABA);
            reader.GetMaterial<TY>(element, ref Data.Left, ref Data.LeftMovie, X3DTOK.X3DTOK_PALE);
            reader.GetMaterial<TY>(element, ref Data.Right, ref Data.RightMovie, X3DTOK.X3DTOK_PARI);
            reader.GetMaterial<TY>(element, ref Data.Top, ref Data.TopMovie, X3DTOK.X3DTOK_PATO);
            reader.GetMaterial<TY>(element, ref Data.Bottom, ref Data.BottomMovie, X3DTOK.X3DTOK_PABO);
        }
    }

    public class X3DEnvironmentMap<TY> : X3DPanorama<TY>
    {
    }
}
