# OWL.Examples
Example usage for the MonoGame scene graph - see [OWL](https://github.com/Owlzy/OWL).

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

## Output
![alt text](https://cdn.discordapp.com/attachments/483046185997697037/962794518002933851/RedSpin.gif)

