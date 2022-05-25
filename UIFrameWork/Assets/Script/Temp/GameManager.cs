using UnityEngine.SceneManagement;

namespace CJR.GameManager
{
    using Scene;

    public class GameManager : MonobehaviourSingleton<GameManager>
    {
        public SceneLoader SceneLoader { get; } = new SceneLoader();

        void Awake()
        {
            AddLobbyScene();
        }

        public void AddLobbyScene()
        {
            SceneLoader.LoadScene(sceneName: SceneNames.UIScene, loadType: LoadSceneMode.Additive, onComplete: AddSceneComplete);
        }

        public void AddGameScene()
        {
            SceneLoader.LoadScene(sceneName: SceneNames.GameScene, loadType: LoadSceneMode.Additive, onComplete: AddSceneComplete);
        }

        private void AddSceneComplete()
        {

        }
    }
}