using UnityEditor.SceneManagement;

namespace CJR.GameManager
{
    using GameScene;

    public class GameManager : MonobehaviourSingleton<GameManager>
    {
        private  GameSceneManager _gameSceneManager;

        public SceneBase Lobby;
        public SceneBase Main;

        void Awake()
        {
            _gameSceneManager = gameObject.AddComponent<GameSceneManager>();
            AddLobbyScene();
        }

        public void AddLobbyScene()
        {
            if (_gameSceneManager != null)
            {
                _gameSceneManager.StartScene(Lobby, GameScene.SceneType.Lobby);
            }
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