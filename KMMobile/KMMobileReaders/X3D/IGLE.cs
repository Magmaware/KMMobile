namespace KMMobile.X3D
{
    public interface IGLE<T>
    {
        /// <summary>
        /// Converts a poly cylinder to a mesh
        /// </summary>
        /// <param name="surface">The surface</param>
        /// <param name="polyCylinder">The poly cylinder</param>
		void PolyCylinder(Surface surface, X3DPolyCylinderData<T> polyCylinder);
        /// <summary>
        /// Converts a poly cone to a mesh
        /// </summary>
        /// <param name="surface">The surface</param>
        /// <param name="polyCone">The poly cone</param>
		void PolyCone(Surface surface, X3DPolyConeData<T> polyCone);
        /// <summary>
        /// Converts a path extrusion to a mesh
        /// </summary>
        /// <param name="surface">The surface</param>
        /// <param name="pathExtrusion">The path extrusion</param>
        void PathExtrusion(Surface surface, X3DPathExtrusionData<T> pathExtrusion);
        /// <summary>
        /// Converts a twist extrusion to a mesh
        /// </summary>
        /// <param name="surface">The surface</param>
        /// <param name="twistExtrusion">The twist extrusion</param>
        void TwistExtrusion(Surface surface, X3DTwistExtrusionData<T> twistExtrusion);
        /// <summary>
        /// Converts a super extrusion to a mesh
        /// </summary>
        /// <param name="surface">The surface</param>
        /// <param name="superExtrusion">The super extrusion</param>
        void SuperExtrusion(Surface surface, X3DSuperExtrusionData<T> superExtrusion);
        /// <summary>
        /// Converts a spiral extrusion to a mesh
        /// </summary>
        /// <param name="surface">The surface</param>
        /// <param name="spiralExtrusion">The spiral extrusion</param>
        void SpiralExtrusion(Surface surface, X3DSpiralExtrusionData<T> spiralExtrusion);
        /// <summary>
        /// Converts a lathe to a mesh
        /// </summary>
        /// <param name="surface">The surface</param>
        /// <param name="lathe">The lathe</param>
        void Lathe(Surface surface, X3DLatheData<T> lathe);
        /// <summary>
        /// Converts a helicoid to a mesh
        /// </summary>
        /// <param name="surface">The surface</param>
        /// <param name="helicoid">The helicoid</param>
        void Helicoid(Surface surface, X3DHelicoidData<T> helicoid);
        /// <summary>
        /// Converts a toroid to a mesh
        /// </summary>
        /// <param name="toroid">The toroid</param>
        void Toroid(Surface surface, X3DToroidData<T> toroid);
        /// <summary>
        /// Converts a screw extrusion to a mesh
        /// </summary>
        /// <param name="surface">The surface</param>
        /// <param name="screwExtrusion">The screw extrusion</param>
        void ScrewExtrusion(Surface surface, X3DScrewExtrusionData<T> screwExtrusion);
    }
}
