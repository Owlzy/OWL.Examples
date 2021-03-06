# OWL.Examples

A collection of samples showing potential ways to implement common game patterns using a scene graph in MonoGame.

See [OWL](https://github.com/Owlzy/OWL)

<br/>

## Basic Usage

A basic scene with a white sprite tinted red. We offset the sprite relative to the container to create an orbit like effect.

```csharp
using OWL.Graph;

namespace Examples.Basic.Scenes
{
    class BasicScene : Scene
    {

        Sprite sprite;
        Container container;

        public BasicScene()
        {
            container = new Container();
            container.SetPosition(400, 250);
            AddChild(container);

            sprite = new Sprite(Game1.White);
            sprite.Width = 40;
            sprite.Height = 40;
            sprite.SetPosition(100, 0);
            sprite.SetAnchor(0.5f);
            sprite.Tint = Microsoft.Xna.Framework.Color.Red;
            container.AddChild(sprite);
        }

        public override void Update(float deltaTime)
        {
            var speed = 3f;
            container.Rotation += speed * deltaTime;
        }
    }
}

```

<br/>

<img src="https://cdn.discordapp.com/attachments/483046185997697037/962803376100347914/RedOrbit.gif" alt="drawing" width="600"/>

<br/>

## Scene Stack

Trigger a pause scene using a scene stack. Scenes in the background pause because they are not at the top of the stack.

```csharp
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
            AddChild(motes);

            Delay(this, 2000f, () => OnPause?.Invoke());
        }

        public override void Update(float deltaTime)
        { 
            base.Update(deltaTime);
            motes.Update(deltaTime);
        }
    }
}
```
<br/>

<img src="https://cdn.discordapp.com/attachments/483046185997697037/964337178350071808/scenestack.gif" alt="drawing" width="600"/>

<br/>

## Loading Scene

Load some frames for an animated sprite and display the loading progress.

```csharp
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
```

<br/>

<img src="https://cdn.discordapp.com/attachments/483046185997697037/965742301836083302/loading.gif" alt="drawing" width="600"/>
