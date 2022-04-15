using Microsoft.Xna.Framework;
using OWL.Graph;

namespace Examples.SceneStack
{
    public class Mote : Container
    {
        public Vector2 Velocity { get; private set; }
        public float Torque { get; private set; }

        public Mote(OWL.Texture.TextureRegion2D texture)
        {
            var view = new Sprite(texture);
            view.SetAnchor(0.5f);
            view.Tint = Color.BlueViolet;
            view.SetScale(4f);
            AddChild(view);

            Velocity = new Vector2(OWL.Mathf.Random(-0.1f, 0.1f), OWL.Mathf.Random(-0.1f, 0.1f));
            Torque = OWL.Mathf.Random(-0.001f, 0.001f);
        }
    }
}
