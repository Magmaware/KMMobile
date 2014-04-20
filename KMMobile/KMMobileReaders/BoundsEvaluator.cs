using System.Collections.Generic;
using WaveEngine.Common.Math;

namespace KMMobile
{
    /// <summary>
    /// Helpers for the types of base object that exist
    /// </summary>
    public static class BoundsEvaluator
    {
        /// <summary>
        /// Works out the bounds of this base object
        /// </summary>
        /// <param name="baseObject">The base object</param>
        public static void EvaluateBounds(this BaseObjectWithVertices<Vector2> baseObject)
        {
            var vecMin = new Vector2(float.MaxValue, float.MaxValue);
            var vecMax = new Vector2(float.MinValue, float.MinValue);
            foreach (var point in baseObject.Vertices)
            {
                vecMin.X = System.Math.Min(vecMin.X, point.X);
                vecMin.Y = System.Math.Min(vecMin.Y, point.Y);
                vecMax.X = System.Math.Max(vecMax.X, point.X);
                vecMax.Y = System.Math.Max(vecMax.Y, point.Y);
            }
            baseObject.BoundingBoxMinimum = vecMin;
            baseObject.BoundingBoxMaximum = vecMax;
            Vector2 centre;
            float radius;
            BadouiClarksonApproximator.ApproximateSphereCentreAndRadius(
            baseObject.Vertices, out centre, out radius);
            baseObject.SphereRadius = radius;
            baseObject.SphereCentre = centre;
        }
        /// <summary>
        /// Works out the bounds of this base object
        /// </summary>
        /// <param name="baseObject">The base object</param>
        public static void EvaluateBounds(this BaseObjectWithVertices<Vector3> baseObject)
        {
            var vecMin = new Vector3(
                float.MaxValue,
                float.MaxValue,
                float.MaxValue);
            var vecMax = new Vector3(
                float.MinValue,
                float.MinValue,
                float.MinValue);
            foreach (var point in baseObject.Vertices)
            {
                vecMin.X = System.Math.Min(vecMin.X, point.X);
                vecMin.Y = System.Math.Min(vecMin.Y, point.Y);
                vecMin.Z = System.Math.Min(vecMin.Z, point.Z);
                vecMax.X = System.Math.Max(vecMax.X, point.X);
                vecMax.Y = System.Math.Max(vecMax.Y, point.Y);
                vecMax.Z = System.Math.Max(vecMax.Z, point.Z);
            }
            baseObject.BoundingBoxMinimum = vecMin;
            baseObject.BoundingBoxMaximum = vecMax;
            Vector3 centre;
            float radius;

            BadouiClarksonApproximator.ApproximateSphereCentreAndRadius(
            baseObject.Vertices, out centre, out radius);
            baseObject.SphereRadius = radius;
            baseObject.SphereCentre = centre;
        }
        /// <summary>
        /// Works out the bounds of this mesh
        /// </summary>
        /// <param name="mesh">The mesh to evaluate the bounds for</param>
        public static void EvaluateBounds(this Mesh mesh)
        {
            foreach (var surface in mesh.Surfaces)
                surface.EvaluateBounds();
            var vecMin = new Vector3(
                float.MaxValue,
                float.MaxValue,
                float.MaxValue);
            var vecMax = new Vector3(
                float.MinValue,
                float.MinValue,
                float.MinValue);
            foreach (var surface in mesh.Surfaces)
            {
                vecMin.X = System.Math.Min(vecMin.X, surface.BoundingBoxMinimum.X);
                vecMin.Y = System.Math.Min(vecMin.Y, surface.BoundingBoxMinimum.Y);
                vecMin.Z = System.Math.Min(vecMin.Z, surface.BoundingBoxMinimum.Z);
                vecMax.X = System.Math.Max(vecMax.X, surface.BoundingBoxMaximum.X);
                vecMax.Y = System.Math.Max(vecMax.Y, surface.BoundingBoxMaximum.Y);
                vecMax.Z = System.Math.Max(vecMax.Z, surface.BoundingBoxMaximum.Z);
            }
            mesh.BoundingBoxMinimum = vecMin;
            mesh.BoundingBoxMaximum = vecMax;
            Vector3 centre;
            float radius;
            BadouiClarksonApproximator.ApproximateSphereCentreAndRadius(
            mesh.AllVertices(), out centre, out radius);
            mesh.SphereRadius = radius;
            mesh.SphereCentre = centre;
        }
        /// <summary>
        /// Works out the bounds of this skinned mesh
        /// </summary>
        /// <param name="mesh">The mesh to evaluate the bounds for</param>
        public static void EvaluateBounds(this SkinnedMesh mesh)
        {
            foreach (var surface in mesh.Surfaces)
                surface.EvaluateBounds();
            var vecMin = new Vector3(
                float.MaxValue,
                float.MaxValue,
                float.MaxValue);
            var vecMax = new Vector3(
                float.MinValue,
                float.MinValue,
                float.MinValue);
            foreach (var surface in mesh.Surfaces)
            {
                vecMin.X = System.Math.Min(vecMin.X, surface.BoundingBoxMinimum.X);
                vecMin.Y = System.Math.Min(vecMin.Y, surface.BoundingBoxMinimum.Y);
                vecMin.Z = System.Math.Min(vecMin.Z, surface.BoundingBoxMinimum.Z);
                vecMax.X = System.Math.Max(vecMax.X, surface.BoundingBoxMaximum.X);
                vecMax.Y = System.Math.Max(vecMax.Y, surface.BoundingBoxMaximum.Y);
                vecMax.Z = System.Math.Max(vecMax.Z, surface.BoundingBoxMaximum.Z);
            }
            mesh.BoundingBoxMinimum = vecMin;
            mesh.BoundingBoxMaximum = vecMax;
            Vector3 centre;
            float radius;
            BadouiClarksonApproximator.ApproximateSphereCentreAndRadius(
            mesh.AllVertices(), out centre, out radius);
            mesh.SphereRadius = radius;
            mesh.SphereCentre = centre;
        }
        /// <summary>
        /// Yields all the vertices in the mesh
        /// </summary>
        /// <param name="mesh">The mesh</param>
        /// <returns>All the vertices</returns>
        public static IEnumerable<Vector3> AllVertices(this Mesh mesh)
        {
            foreach (var surface in mesh.Surfaces)
                foreach (var vertex in surface.Vertices)
                    yield return vertex;
        }
        /// <summary>
        /// Yields all the vertices in the skinned mesh
        /// </summary>
        /// <param name="mesh">The mesh</param>
        /// <returns>All the vertices</returns>
        public static IEnumerable<Vector3> AllVertices(this SkinnedMesh mesh)
        {
            foreach (var surface in mesh.Surfaces)
                foreach (var vertex in surface.Vertices)
                    yield return vertex;
        }
    }
}
