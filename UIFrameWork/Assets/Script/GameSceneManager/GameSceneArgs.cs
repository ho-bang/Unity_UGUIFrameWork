namespace CJR.GameScene
{
    public class GameScene
    {
        public enum SceneType
        {
            Lobby,
            Game
        }

        public enum SceneDataState
        {
            None,
            Start,
            StartEnd,
            UILoad,
            UILoadFinish,
            Finish,
            FinishEnd,
            CleanUp
        }

        public class SceneDataArgs
        {
            public SceneDataArgs(SceneDataState state)
            {
                State = state;
            }

            public SceneDataState State { private set; get; }
        }

        public delegate void GameSceneDataHandler(object sceneData, SceneDataArgs args);
    }
}