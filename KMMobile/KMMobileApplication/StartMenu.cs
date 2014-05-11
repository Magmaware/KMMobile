using WaveEngine.Common.Graphics;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.UI;

namespace KMMobile
{
    public class StartMenu : Scene
    {
        public StartMenu(int width, int height)
        {
            Width = width;
            Height = height;
            ButtonSize = Width / 32;
            if (ButtonSize < 20)
                ButtonSize = 20;
            if (ButtonSize > 64)
                ButtonSize = 64;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public int ButtonSize { get; private set; }

        protected override void CreateScene()
        {
            RenderManager.BackgroundColor = Color.White;
        }
    }
}
