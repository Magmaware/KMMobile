using WaveEngine.Common;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;

namespace KMMobile
{
    public class KMApplication : Game
    {
        public override void Initialize(IApplication application)
        {
            base.Initialize(application);
            ScreenContext screenContext = new ScreenContext(new StartMenu(application.Width, application.Height));
            WaveServices.ScreenContextManager.To(screenContext);
        }
    }
}
