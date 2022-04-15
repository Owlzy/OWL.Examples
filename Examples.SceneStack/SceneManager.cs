using Examples.SceneStack.Scenes;

namespace Examples.SceneStack
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
            scene.OnPause += () => ShowPauseScene();

            _scenes.Push(scene);

            return scene;
        }

        public Scene ShowPauseScene()
        {
            var scene = new PauseScene();
            scene.OnClose += () => _scenes.Pop();

            _scenes.Push(scene);

            return scene;
        }
    }
}