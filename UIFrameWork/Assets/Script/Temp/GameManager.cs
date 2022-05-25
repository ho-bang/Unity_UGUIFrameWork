namespace CJR.GameManager
{
    using CJR.Scene;

    public class GameManager : MonobehaviourSingleton<GameManager>
    {
        public readonly SceneLoader SceneLoader = new SceneLoader();
        private readonly CJRSceneManager _sm = new ();

        void Awake()
        {
            AddLobbyScene();
        }

        public void AddLobbyScene()
        {
            _sm.AddScene(state: CJRSceneManager.SceneState.Lobby);
        }

        public void AddGameScene()
        {
            _sm. AddScene(state: CJRSceneManager.SceneState.GameScene);
        }

        public void ChangeToLobby()
        {
            _sm.ChanageScene(state: CJRSceneManager.SceneState.Lobby);
        }

        public void ChangeToMain()
        {
            _sm.ChanageScene(state: CJRSceneManager.SceneState.GameScene);
        }
    }
}