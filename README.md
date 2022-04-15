# OWL.Examples
Example scene graph usage - see [OWL](https://github.com/Owlzy/OWL).

## Basic Usage

Basic scene with a white sprite tinted red. We offset the sprite relative to the container to create an orbit like effect.

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

Output
![alt text](https://cdn.discordapp.com/attachments/483046185997697037/962794518002933851/RedSpin.gif)

## Scene Stack

Trigger a pause scene using a scene stack. Scenes in background pause because they are not at the top of the stack.

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
```

Output
![alt text](https://cdn.discordapp.com/attachments/483046185997697037/964337178350071808/scenestack.gif)
