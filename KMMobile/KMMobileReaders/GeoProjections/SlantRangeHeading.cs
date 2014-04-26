using System;

namespace KMMobile.GeoProjections
{
    /// <summary>
    /// Class representing an albers equal area projection.
    /// </summary>
    public class CSlantRangeHeading : CLambertAzimuthalEqualArea
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CSlantRangeHeading()
        {
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~CSlantRangeHeading()
        {
        }

        /// <summary>
        /// Project the given lat long to x, y using the input parameters to store the 
        /// result.
        /// </summary>
        public override void Project(double dLatY, double dLongX)
        {
            base.Project(dLatY, dLongX);

            dLatY *= Constants.conEARTH_RADIUS_METRES;
            dLongX *= Constants.conEARTH_RADIUS_METRES;
        }



        /// <summary>
        /// Project the given x y to lat long using the input parameters to store /// the result.	
        /// </summary>
        public override void InverseProject(double dLatY, double dLongX) 
        {
	        dLatY /= Constants.conEARTH_RADIUS_METRES;
	        dLongX /= Constants.conEARTH_RADIUS_METRES;

	        base.InverseProject(dLatY, dLongX);
        }
    }
}


