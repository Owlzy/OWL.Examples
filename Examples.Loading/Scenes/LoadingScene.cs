using OWL.Graph;
using System;

namespace Examples.Loading.Scenes
{
    public class LoadingScene : Scene
    {
        public Action<float> OnLoaderProgress;

        private const float barWidth = 240f;

        private float loaded;
        private Sprite loadingBar;

        public LoadingScene()
        {
            var width = Game1.Device.Viewport.Width;
            var height = Game1.Device.Viewport.Height;

            var bg = new Sprite(Game1.White);
            bg.Width = width;
            bg.Height = height;
            bg.Tint = Microsoft.Xna.Framework.Color.Black;
            AddChild(bg);

            var logo = new Sprite(Game1.Assets.GetTextureRegion("logo_white_small"));
            logo.SetAnchor(0.5f);
            logo.SetPosition(width * 0.5f, height * 0.5f - 20);
            logo.SetScale(0.55f);
            AddChild(logo);

            loadingBar = new Sprite(Game1.White);
            loadingBar.SetPosition(width * 0.5f - barWidth * 0.5f, height * 0.5f + 20);
            loadingBar.SetAnchor(0f, 0.5f);
            AddChild(loadingBar);
        }
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            var w = loaded * barWidth;
            loadingBar.Width += (w - loadingBar.Width) * 0.12f;
        }

        public void Load(float progress)
        {
            loaded = progress;
        }
    }
}
