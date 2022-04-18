using Examples.Loading.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OWL.Graph;
using OWL.Rendering;
using OWL.Texture;
using System;
using System.Collections.Generic;

namespace Examples.Loading
{
    public class Game1 : Game
    {
        public static GraphicsDevice Device;
        public static TextureRegion2D White;
        public static Assets Assets;

        public Action OnLoadingComplete;

        private readonly Container _stage = new Container();

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Renderer _renderer;
        private SceneStack _stack;
        private SceneManager _sceneManager;
        private LoadingScene _loadingScene;

        private bool _loading = false;
        private int _loadTotal = 0;

        private List<ManifestItem> _manifest;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Device = _graphics.GraphicsDevice;
            White = new TextureRegion2D(new Texture2D(_graphics.GraphicsDevice, 4, 4)
                .FillTexture(_graphics.GraphicsDevice, 4, 4, pixel => Color.White));

            // TODO: Add your initialization logic here
            _stack = new SceneStack(_stage);
            _sceneManager = new SceneManager(_stack);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _renderer = new Renderer(_graphics.GraphicsDevice, _spriteBatch);

            // TODO: use this.Content to load your game content here
            Assets = new Assets(Content, _graphics.GraphicsDevice);

            // Load loading scene assets
            Assets.Load("textures/logo_white_small", out Texture2D _);

            // Create loading scene and add to stack
            _loadingScene = new LoadingScene();
            _stack.Push(_loadingScene);

            // Loader progress callback
            _loadingScene.OnLoaderProgress += (float progress) =>
            {
                _loadingScene.Load(progress);
            };

            // Complete callback
            OnLoadingComplete += () =>
            {
                // Show main scene after short delay
                Scene.Delay(_loadingScene, 1000f, () =>
                {
                    _stack.Pop(); // Remove preloader scene from stack
                    var mainScene = _sceneManager.ShowMainScene(); // Show main scene
                });
            };

            // Create asset list
            _manifest = new List<ManifestItem>();
            for (var i = 0; i <= 7; i++)
            {
                _manifest.Add(new ManifestItem
                {
                    uri = @"textures/ammo_crate_00" + i,
                    name = @"ammo_crate_00" + i
                });
            }
            _loadTotal = _manifest.Count;

            // Why not give a small delay before loading too, just to make sure everything is ready
            Scene.Delay(_loadingScene, 500f, () =>
            {
                _loading = true;
            });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float deltaTime = gameTime.ElapsedGameTime.Milliseconds;
            _stack.Update(deltaTime);

            // Load assets
            if (_loading)
            {
                // Pop asset
                var asset = _manifest[_manifest.Count - 1];
                _manifest.RemoveAt(_manifest.Count - 1);

                // Load file
                Assets.Load(asset.uri, out Texture2D _);

                // Calc progress
                var progress = (_loadTotal - _manifest.Count) / (float)_loadTotal;
                _loadingScene.OnLoaderProgress?.Invoke(progress);

                // Finish loading
                if (_manifest.Count == 0)
                {
                    _loading = false;
                    OnLoadingComplete?.Invoke();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _renderer.Begin();
            _renderer.Render(_stage);
            _renderer.End();

            base.Draw(gameTime);
        }
    }

    public struct ManifestItem
    {
        public string uri;
        public string name;

        public ManifestItem(string uri)
        {
            name = "";
            this.uri = uri;
        }

        public ManifestItem(string name, string uri)
        {
            this.name = name;
            this.uri = uri;
        }
    }
}
