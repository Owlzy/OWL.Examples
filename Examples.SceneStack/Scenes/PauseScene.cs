using Microsoft.Xna.Framework;
using MonoGame.Extended.Tweening;
using OWL.Graph;

namespace Examples.SceneStack.Scenes
{
    class PauseScene : Scene
    {
        readonly Container holder;
        readonly Sprite panel;

        public PauseScene()
        {
            holder = new Container();
            holder.SetPosition(Game1.Device.Viewport.Width / 2f, Game1.Device.Viewport.Height / 2f);
            AddChild(holder);

            panel = new Sprite(Game1.White);
            panel.SetAnchor(0.5f);
            panel.Width = 300f;
            panel.Height = 200f;
            panel.Tint = Color.Brown;
            holder.AddChild(panel);

            holder.Visible = false;
            holder.SetScale(0.75f);

            _tweens.TweenTo(holder, o => o.Scale, new Vector2(1f), 320f)
                .Easing(EasingFunctions.BackOut)
                .OnBegin((Tween tween) => holder.Visible = true);

            Delay(this, 2000f, () =>
            {
                _tweens.TweenTo(holder, o => o.Scale, new Vector2(0.75f), 320f)
                    .Easing(EasingFunctions.BackIn)
                    .OnEnd((Tween tween) => OnClose?.Invoke());
            });
        }
    }
}
