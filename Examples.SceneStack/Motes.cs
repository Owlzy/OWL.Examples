using OWL.Graph;
using System.Collections.Generic;

namespace Examples.SceneStack
{
    class Motes : Container
    {

        List<Mote> moteList = new List<Mote>();

        public Motes(int total)
        {
            for (var i = 0; i < total; i++)
            {
                var mote = new Mote(Game1.White);

                mote.SetPosition(
                    OWL.Mathf.Random(0f, Game1.Device.Viewport.Width),
                    OWL.Mathf.Random(0f, Game1.Device.Viewport.Height));

                AddChild(mote);
                moteList.Add(mote);
            }
        }

        public void Update(float deltaTime)
        {
            for (var i = 0; i < moteList.Count; i++)
            {
                var mote = moteList[i];
                mote.Position += mote.Velocity * deltaTime;
                mote.Rotation += mote.Torque * deltaTime;

                if (mote.X <= -mote.Width / 2)
                {
                    mote.X = Game1.Device.Viewport.Width;
                }
                if (mote.X >= Game1.Device.Viewport.Width + mote.Width / 2)
                {
                    mote.X = 0f;
                }
                if (mote.Y <= -mote.Height / 2)
                {
                    mote.Y = Game1.Device.Viewport.Height;
                }
                if (mote.Y >= Game1.Device.Viewport.Height + mote.Height / 2)
                {
                    mote.Y = 0f;
                }
            }
        }
    }
}
