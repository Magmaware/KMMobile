using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace KMMobile.X3D
{
    /// <summary>
    /// Defines origami between panels
    /// </summary>
    /// <typeparam name="T">The numeric type</typeparam>
    public class X3DOrigami<T>
    {
        /// <summary>
        /// Structure defining parent/child relationships for origami
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class X3DOrigamiRelationship
        {
            /// <summary>
            /// The parent shape
            /// </summary>
            public X3DShape<T> Parent;
            /// <summary>
            /// The child shape
            /// </summary>
            public X3DShape<T> Child;
            /// <summary>
            /// The glue between them
            /// </summary>
            public X3DJointedShapeGlue<T> Glue;
            /// <summary>
            /// Whether to reverse angles
            /// </summary>
            public bool Reverse;
        }

        /// <summary>
        /// Parent/child relationships for origami
        /// </summary>
        public List<X3DOrigamiRelationship> Relationships;

        /// <summary>
        /// The origami root
        /// </summary>
        public X3DShape<T> Root;

        /// <summary>
        /// Creates an origami from the given xml
        /// </summary>
        /// <param name="element">The xml element containing origami</param>
        /// <param name="reader">The reader</param>
        public void Create(XmlElement element, X3DBaseReader reader)
        {
            var _reader = reader as X3DFormatReader<T>;
            X3DShape<T> _root;
            if (_reader.Shapes.TryGetValue(X3DBaseReader.Get<int>(element, X3DTOK.X3DTOK_ROOT),
                out _root))
            {
                Root = _root;
                Relationships = new List<X3DOrigami<T>.X3DOrigamiRelationship>();
                var relations = element.SelectNodes(X3DTOK.X3DTOK_ORIGAMIRELATIONSHIP);
                for (int i = 0; i < relations.Count; i++)
                {
                    var relation = relations[i] as XmlElement;
                    var call = new X3DOrigamiRelationship();
                    call.Parent = _reader.Shapes[X3DBaseReader.Get<int>(relation, X3DTOK.X3DTOK_PARENT)];
                    call.Child = _reader.Shapes[X3DBaseReader.Get<int>(relation, X3DTOK.X3DTOK_CHILD)];
                    call.Reverse = X3DBaseReader.Get<bool>(relation, X3DTOK.X3DTOK_REVERSE);
                    call.Glue = call.Parent.FindGlue(call.Child) as X3DJointedShapeGlue<T>;
                    Relationships.Add(call);
                }
            }
            else
            {
                Root = null;
                Relationships = new List<X3DOrigamiRelationship>();
            }
        }
    }
}
