namespace CJR.Scene
{
    public class CJRSceneManager 
    {
        public enum SceneState
        {
            Lobby,
            GameScene
        }

        public void ChanageScene(SceneState state)
        {
            switch (state)
            {
                case SceneState.Lobby:
                    break;
                case SceneState.GameScene:
                    break;
            }
        }
    }
}