using OWL.Graph;

namespace Examples.SceneStack.Scenes
{
    class MainScene : Scene
    {

        Sprite bg;
        Motes motes;

        public MainScene()
        {
            bg = new Sprite(Game1.White);
            bg.Width = Game1.Device.Viewport.Width;
            bg.Height = Game1.Device.Viewport.Height;
            bg.Tint = Microsoft.Xna.Framework.Color.BlanchedAlmond;
            AddChild(bg);

            motes = new Motes(20);
            motes.SetPosition(100f, 100f);
            AddChild(motes);

            Delay(this, 2000f, () =>
            {
                OnPause?.Invoke();
            });
        }

        public override void Update(float deltaTime)
        { 
            base.Update(deltaTime);
            motes.Update(deltaTime);
        }
    }
}
