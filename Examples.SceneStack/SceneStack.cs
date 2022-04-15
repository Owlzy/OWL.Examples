using Examples.SceneStack.Scenes;
using OWL.Graph;
using System.Collections.Generic;

namespace Examples.SceneStack
{
    class SceneStack
    {
        private List<Scene> _scenes = new List<Scene>();
        private Container _stage;

        public SceneStack(Container stage)
        {
            _stage = stage;
        }

        public void Push(Scene scene)
        {
            _scenes.Add(scene);
            _stage.AddChild(scene);
        }

        public void Pop()
        {
            _scenes[_scenes.Count - 1].Destroy();
            _scenes.RemoveAt(_scenes.Count - 1);
        }

        public void Add(Scene scene, int index)
        {
            _scenes.Insert(index, scene);
            _stage.AddChildAt(scene, index);
        }

        public void Remove(Scene scene)
        {
            scene.Destroy();
            _scenes.Remove(scene);
        }

        public void Update(float deltaTime)
        {
            // Always update top most scene
            if (_scenes.Count > 0)
            {
                _scenes[_scenes.Count - 1].Update(deltaTime);

                // Cycle array backwards to ovoid issues with splicing
                for (var i = _scenes.Count - 2; i >= 0; i--)
                {
                    if (_scenes[i].BackgroundUpdate)
                        _scenes[i].Update(deltaTime);
                }
            }
            else
            {
                //no scenes in scene stack
            }
        }
    }
}
