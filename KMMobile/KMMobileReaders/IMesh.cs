using System.Collections.Generic;

namespace KMMobile
{
    /// <summary>
    /// Base interface for all kinds of meshes
    /// </summary>
    public interface IMesh
    {
        /// <summary>
        /// The mesh type
        /// </summary>
        MeshType MeshType { get; }

        /// <summary>
        /// The name of this mesh
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The surfaces contained in this mesh
        /// </summary>
        List<Surface> Surfaces { get; }

        /// <summary>
        /// Evaluate the bounds of this mesh
        /// </summary>
        void EvaluateBounds();
        /// <summary>
        /// Bounding sphere radius
        /// </summary>
        float SphereRadius { get; set; }
    }
}
