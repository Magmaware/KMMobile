using System;
using System.Collections.Generic;
using WaveEngine.Framework.Services;
using System.Threading;
using Android.App;
using Android.Content.PM;
using Android.Views;

namespace LauncherOUYA
{
	public class AndroidActivity : WaveEngine.Adapter.Application
    {
        private KMMobileApplication.Game game;

        public AndroidActivity()
        {
			this.FullScreen = true;
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
    }
}

