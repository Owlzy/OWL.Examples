using Examples.Basic.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OWL.Graph;
using OWL.Rendering;
using OWL.Texture;

namespace Examples.Basic
{
    public class Game1 : Game
    {
        public static TextureRegion2D White;

        private readonly Container _stage = new Container();

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Renderer _renderer;
        private Scene _scene;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            White = new TextureRegion2D(new Texture2D(_graphics.GraphicsDevice, 4, 4)
                .FillTexture(_graphics.GraphicsDevice, 4, 4, pixel => Color.White));

            // TODO: Add your initialization logic here
            _scene = new BasicScene();
            _stage.AddChild(_scene);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _renderer = new Renderer(_graphics.GraphicsDevice, _spriteBatch);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float deltaTime = gameTime.ElapsedGameTime.Milliseconds * 0.001f;
            _scene.Update(deltaTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _renderer.BeginRender();
            _renderer.Render(_stage);
            _renderer.EndRender();

            base.Draw(gameTime);
        }
    }
}
