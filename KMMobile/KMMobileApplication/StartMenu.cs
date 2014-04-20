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
        }

        public Button pushButton1, pushButton2, pushButton3;

        public int Width { get; private set; }
        public int Height { get; private set; }

        protected override void CreateScene()
        {
            RenderManager.BackgroundColor = Color.MintCream;
            pushButton1 = new Button() {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Text = string.Empty,
                BorderColor = Color.Transparent,
                BackgroundImage = @"Content/Folder.wpk",
                PressedBackgroundImage = @"Content/FolderPressed.wpk",
                Margin = new Thickness(20, 20, 20, 20),
                Width = this.Width / 4.0f,
                Height = this.Width / 4.0f };
            pushButton2 = new Button() {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Text = string.Empty,
                BorderColor = Color.Transparent,
                BackgroundImage = @"Content/Run.wpk",
                PressedBackgroundImage = @"Content/RunPressed.wpk",
                Margin = new Thickness(20, 20, 20, 20),
                Width = this.Width / 4.0f,
                Height = this.Width / 4.0f };
            pushButton3 = new Button() {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Text = string.Empty,
                BorderColor = Color.Transparent,
                BackgroundImage = @"Content/Option.wpk",
                PressedBackgroundImage = @"Content/OptionPressed.wpk",
                Margin = new Thickness(20, 20, 20, 20),
                Width = this.Width / 4.0f,
                Height = this.Width / 4.0f };
            EntityManager.Add(pushButton1.Entity);
            EntityManager.Add(pushButton2.Entity);
            EntityManager.Add(pushButton3.Entity);
        }
    }
}
