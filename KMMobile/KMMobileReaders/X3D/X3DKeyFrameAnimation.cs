using System.Collections.Generic;

namespace KMMobile.X3D
{
    public class X3DKeyFrameAnimation<TY, KT> : X3DAnimation<TY>
    {
        public Dictionary<int, KT> KeyFrames = new Dictionary<int, KT>();
    }
}
