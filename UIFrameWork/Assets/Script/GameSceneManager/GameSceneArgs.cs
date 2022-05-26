\namespace CJR.GameScene
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
            Start,
            UILoad,
            Finish
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