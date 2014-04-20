using System;
using System.Collections.Generic;
using WaveEngine.Framework.Services;
using System.Threading;
using Android.App;
using Android.Content.PM;
using Android.Views;

namespace LauncherAndroid
{
    [Activity(Label = "Launcher",
            Icon = "@drawable/icon",
            MainLauncher = true,
            LaunchMode = LaunchMode.SingleTask,
            ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class AndroidActivity : WaveEngine.Adapter.Application
    {
        private KMMobileApplication.Game game;

        public AndroidActivity()
        {
			this.FullScreen = true;

			this.DefaultOrientation = WaveEngine.Common.Input.DisplayOrientation.LandscapeLeft;
            this.SupportedOrientations = WaveEngine.Common.Input.DisplayOrientation.LandscapeLeft | WaveEngine.Common.Input.DisplayOrientation.LandscapeRight;
        }

        public override void Initialize()
        {
            game = new KMMobileApplication.Game();
            game.Initialize(this);

			this.Window.AddFlags(WindowManagerFlags.KeepScreenOn); 
        }

        public override void Update(TimeSpan elapsedTime)
        {
            game.UpdateFrame(elapsedTime);
        }

        public override void Draw(TimeSpan elapsedTime)
        {
            game.DrawFrame(elapsedTime);
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {
				WaveServices.Platform.Exit();
            }

            return base.OnKeyDown(keyCode, e);
        }
    }
}

