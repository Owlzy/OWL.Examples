using OWL.Graph;
using OWL.Texture;

namespace Examples.Loading.Scenes
{
    class MainScene : Scene
    {

        Sprite bg;
        AnimatedSprite crate;

        public MainScene()
        {
            bg = new Sprite(Game1.White);
            bg.Width = Game1.Device.Viewport.Width;
            bg.Height = Game1.Device.Viewport.Height;
            bg.Tint = Microsoft.Xna.Framework.Color.BlanchedAlmond;
            AddChild(bg);

            var frames = new TextureRegion2D[8];
            for (var i = 0; i < frames.Length; i++)
            {
                frames[i] = Game1.Assets.GetTextureRegion("ammo_crate_00" + i);
            }

            crate = new AnimatedSprite(frames);
            crate.SetAnchor(0.5f);
            crate.SetPosition(Game1.Device.Viewport.Width / 2f, Game1.Device.Viewport.Height / 2f);
            crate.Looping = true;
            crate.AnimationSpeed = 0.01f;
            AddChild(crate);

            crate.GotoAndPlay(0);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            crate.Update(deltaTime);
        }
    }
}