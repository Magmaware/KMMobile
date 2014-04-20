using System;
using System.ComponentModel;
using WaveEngine.Common.Graphics;

namespace KMMobile
{
    /// <summary>
    /// A colour
    /// </summary>
    public struct Colour : IFormattable, IConvertible
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="r">The red component</param>
        /// <param name="g">The green component</param>
        /// <param name="b">The blue component</param>
        /// <param name="a">The alpha component</param>
        public Colour(float r, float g, float b, float a)
        {
            R = Math.Max(Math.Min(r, 1), 0);
            G = Math.Max(Math.Min(g, 1), 0);
            B = Math.Max(Math.Min(b, 1), 0);
            A = Math.Max(Math.Min(a, 1), 0);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="v">String value</param>
        public Colour(string v)
        {
            var c = Colour.Parse(v);
            R = Math.Max(Math.Min(c.R, 1), 0);
            G = Math.Max(Math.Min(c.G, 1), 0);
            B = Math.Max(Math.Min(c.B, 1), 0);
            A = Math.Max(Math.Min(c.A, 1), 0);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="c">The colour</param>
		public Colour(WaveEngine.Common.Graphics.Color c)
        {
            R = c.R / 255.0f;
            G = c.G / 255.0f;
            B = c.B / 255.0f;
            A = c.A / 255.0f;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="color"></param>
		public Colour(int color) : this(new WaveEngine.Common.Graphics.Color(color))
        {
        }

        /// <summary>
        /// Red
        /// </summary>
        public float R;

        /// <summary>
        /// Green
        /// </summary>
        public float G;

        /// <summary>
        /// Blue
        /// </summary>
        public float B;

        /// <summary>
        /// Alpha
        /// </summary>
        public float A;

        /// <summary>
        /// Red
        /// </summary>
        public byte RValue { get { return (byte)(R * 255.0f); } }

        /// <summary>
        /// Green
        /// </summary>
        public byte GValue { get { return (byte)(G * 255.0f); } }

        /// <summary>
        /// Blue
        /// </summary>
        public byte BValue { get { return (byte)(B * 255.0f); } }

        /// <summary>
        /// Alpha
        /// </summary>
        public byte AValue { get { return (byte)(A * 255.0f); } }

        /// <summary>
        /// Formats this object to a string
        /// </summary>
        /// <param name="format">The format</param>
        /// <param name="formatProvider">The format provider</param>
        /// <returns>The object as a string</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("{0} {1} {2} {3}", R, G, B, A);
        }

        /// <summary>
        /// The basic to-string operator
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(null, null);
        }

        public uint ToInt()
        {
			return new WaveEngine.Common.Graphics.Color((int)(R * 255.0), (int)(G * 255.0), (int)(B * 255)).ToUnsignedInt();
        }

        /// <summary>
        /// Parses the colour
        /// </summary>
        /// <param name="s">The string value for the colour</param>
        /// <returns>The colour</returns>
        public static Colour Parse(string s)
        {
            float r, g, b, a;
            var splits = s.Split(' ');
            if (splits.Length < 3)
            {
                splits = s.Split(',');
                if (splits.Length < 3)
                    throw new Exception("Incorrect colour string format");
            }
            r = Convert.ToSingle(splits[0]);
            g = Convert.ToSingle(splits[1]);
            b = Convert.ToSingle(splits[2]);
            if (splits.Length > 3)
                a = Convert.ToSingle(splits[3]);
            else
                a = 1.0f;
            return new Colour(r, g, b, a);
        }

        public Colour Mul(float value)
        {
            return new Colour(R * value, G * value, B * value, A * value);
        }

        /// <summary>
        /// This means that Colour can be converted to Color implicitly
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
		public static implicit operator WaveEngine.Common.Graphics.Color(Colour c)
        {
			return new WaveEngine.Common.Graphics.Color(
                (byte)Clamp((c.A * 255), 255, 0),
                (byte)Clamp((c.R * 255), 255, 0),
                (byte)Clamp((c.G * 255), 255, 0),
                (byte)Clamp((c.B * 255), 255, 0));
        }

        /// <summary>
        /// This means that Color can be converted to Colour implicitly
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
		public static implicit operator Colour(WaveEngine.Common.Graphics.Color c)
        {
            return new Colour(c);
        }

        /// <summary>
        /// Generic clamp function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        public static T Clamp<T>(T value, T max, T min) where T : System.IComparable<T>
        {
            T result = value;
            if (value.CompareTo(max) > 0)
                result = max;
            if (value.CompareTo(min) < 0)
                result = min;
            return result;
        }

        /// <summary>
        /// This object can be converted to a string
        /// </summary>
        /// <returns>The string</returns>
        public TypeCode GetTypeCode()
        {
            return TypeCode.String;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            return this.ToString();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            if (conversionType == typeof(Colour))
                return this;
            else if (conversionType == typeof(string))
                return ToString();
            else
                throw new InvalidCastException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Knows how to convert a colour from a type
    /// </summary>
    public class ColourTypeConverter : TypeConverter
    {
        /// <summary>
        /// Whether the type converter for colour is registered
        /// </summary>
        private static bool RegisteredTypeConverter = false;

        /// <summary>
        /// Static constructor
        /// </summary>
        public static void RegisterTypeConverter()
        {
            if (!RegisteredTypeConverter)
            {
                TypeDescriptor.AddAttributes(
                    typeof(Colour),
                    new TypeConverterAttribute(typeof(ColourTypeConverter)));
                RegisteredTypeConverter = true;
            }
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            else
                return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
                return Colour.Parse(value as string);
            else
                return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
                return ((Colour)value).ToString();
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
