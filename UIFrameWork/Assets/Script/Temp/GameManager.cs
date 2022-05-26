using CJR.GameScene;

namespace CJR.GameManager
{
    using Scene;

    public class GameManager : MonobehaviourSingleton<GameManager>
    {
        private readonly GameSceneManager _gameSceneManager = new();

        void Awake()
        {
            AddLobbyScene();
        }

        public void AddLobbyScene()
        {
            _gameSceneManager.StartScene(GameScene.GameScene.SceneType.Lobby);
            //SceneLoader.LoadScene(sceneName: SceneNames.UIScene, loadType: LoadSceneMode.Additive, onComplete: AddSceneComplete);
        }

        public void AddGameScene()
        {
            //SceneLoader.Instance.LoadScene(sceneName: SceneNames.GameScene, loadType: LoadSceneMode.Additive, onComplete: AddSceneComplete);
        }

        private void AddSceneComplete()
        {

        }
    }
}