using System;
using System.Collections.Generic;
using System.Linq;
using WaveEngine.Common.Math;

namespace KMMobile
{
    public static class BadouiClarksonApproximator
    {
        public const int SphereApproximationIterations = 1000;

        /// <summary>
        /// Works out the approximate centre and radius for a given set of points
        /// </summary>
        /// <param name="vertices">The number of vertices</param>
        /// <param name="centre">The returned centre</param>
        /// <param name="radius">The returned sphere radius</param>
        /// <param name="numberOfIterations">The number of iterations (for accuracy)</param>
        public static void ApproximateSphereCentreAndRadius(
        IEnumerable<Vector2> vertices,
        out Vector2 centre,
        out float radius,
        int numberOfIterations = SphereApproximationIterations)
        {
            centre = vertices.First();
            radius = 0.0f;
            for (var i = 0; i < numberOfIterations; i++)
            {
                var winner = new Vector2();
                var distmax = (centre - vertices.First()).LengthSquared();
                foreach (var vertex in vertices.Skip(1))
                {
                    var dist = (centre - vertex).LengthSquared();
                    if (dist > distmax)
                    {
                        winner = vertex;
                        distmax = dist;
                    }
                }
                radius = distmax;
                centre = new Vector2(
                    centre.X + (1.0f / (i + 1.0f)) * (winner.X - centre.X),
                    centre.Y + (1.0f / (i + 1.0f)) * (winner.Y - centre.Y));
            }
            radius = (float)Math.Sqrt(radius);
        }
        /// <summary>
        /// Works out the approximate centre and radius for a given set of points
        /// </summary>
        /// <param name="vertices">The number of vertices</param>
        /// <param name="centre">The returned centre</param>
        /// <param name="radius">The returned sphere radius</param>
        /// <param name="numberOfIterations">The number of iterations (for accuracy)</param>
        public static void ApproximateSphereCentreAndRadius(
        IEnumerable<Vector3> vertices,
        out Vector3 centre,
        out float radius,
        int numberOfIterations = SphereApproximationIterations)
        {
            radius = 0.0f;
            if (vertices.Count() <= 0)
            {
                centre = new Vector3();
                return;
            }

            centre = vertices.First();
            
            for (var i = 0; i < numberOfIterations; i++)
            {
                var winner = new Vector3();
                var distmax = (centre - vertices.First()).LengthSquared();
                foreach (var vertex in vertices.Skip(1))
                {
                    var dist = (centre - vertex).LengthSquared();
                    if (dist > distmax)
                    {
                        winner = vertex;
                        distmax = dist;
                    }
                }
                radius = distmax;
                centre = new Vector3(
                    centre.X + (1.0f / (i + 1.0f)) * (winner.X - centre.X),
                    centre.Y + (1.0f / (i + 1.0f)) * (winner.Y - centre.Y),
                    centre.Z + (1.0f / (i + 1.0f)) * (winner.Z - centre.Z));
            }
            radius = (float)Math.Sqrt(radius);
        }
    }
}
