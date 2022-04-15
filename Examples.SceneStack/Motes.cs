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
            }
        }
    }
}
