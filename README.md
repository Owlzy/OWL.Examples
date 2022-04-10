# OWL.Examples
Example usage for the MonoGame scene graph - OWL.

```csharp
using OWL.Graph;

namespace Examples.Basic.Scenes
{
    class BasicScene : Scene
    {

        Sprite sprite;

        public BasicScene()
        {
            sprite = new Sprite(Game1.White);
            sprite.Width = 40;
            sprite.Height = 40;
            sprite.SetPosition(400, 250);
            sprite.SetAnchor(0.5f);
            sprite.Tint = Microsoft.Xna.Framework.Color.Red;
            AddChild(sprite);
        }

        public override void Update(float deltaTime)
        {
            var speed = 3f;
            sprite.Rotation += speed * deltaTime;
        }
    }
}
```