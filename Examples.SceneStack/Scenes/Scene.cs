using Microsoft.Xna.Framework;
using MonoGame.Extended.Tweening;
using OWL.Graph;
using System;

namespace Examples.SceneStack.Scenes
{
    public abstract class Scene : Container
    {
        public Tweener Tweens { get { return _tweens; } }
        protected readonly Tweener _tweens = new Tweener();
        public bool BackgroundUpdate { get; set; } = false;
        public Action OnPause { get; set; }
        public Action OnClose { get; set; }

        public virtual void Update(float deltaTime)
        {
            _tweens.Update(deltaTime);
        }

        public static void Delay(Scene scene, float delay, Action callback)
        {
            scene.Tweens.TweenTo(new Container(), a => a.Position, new Vector2(), delay)
                .OnEnd((Tween tween) => callback?.Invoke());
        }
    }
}