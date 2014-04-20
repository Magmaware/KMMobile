using System.Collections.Generic;
using System.Xml;

namespace KMMobile.X3D
{
    public class X3DAnimation<TY>
    {
        public virtual void Create(XmlElement element, X3DBaseReader reader)
        {
            var _reader = reader as X3DFormatReader<TY>;
            if (null != _reader)
            {
                if (_reader.Animations == null)
                    _reader.Animations = new List<X3DAnimation<TY>>();
                _reader.Animations.Add(this);
            }
        }
    }
}