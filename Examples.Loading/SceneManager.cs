using Examples.Loading.Scenes;

namespace Examples.Loading
{
    class SceneManager
    {
        private readonly SceneStack _scenes;

        public SceneManager(SceneStack stack)
        {
            _scenes = stack;
        }

        public Scene ShowMainScene()
        {
            var scene = new MainScene();

            _scenes.Push(scene);

            return scene;
        }
    }
}