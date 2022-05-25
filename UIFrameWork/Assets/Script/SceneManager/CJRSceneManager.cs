using UnityEngine.SceneManagement;

namespace CJR.Scene
{
    using GameManager;

    public class CJRSceneManager 
    {
        public enum SceneState
        {
            Lobby,
            GameScene
        }

        public void AddScene(SceneState state)
        {
            switch (state)
            {
                case SceneState.Lobby:
                    GameManager.Instance.SceneLoader.LoadScene(sceneName: SceneNames.UIScene, loadType: LoadSceneMode.Additive, onComplete: null);
                    break;
                case SceneState.GameScene:
                    GameManager.Instance.SceneLoader.LoadScene(sceneName: SceneNames.GameScene, loadType: LoadSceneMode.Additive, onComplete: null);
                    break;
            }
        }

        public void ChanageScene(SceneState state)
        {
            switch (state)
            {
                case SceneState.Lobby:
                    GameManager.Instance.SceneLoader.UnloadAllOpenedScene(onComplete: () =>
                    {
                        GameManager.Instance.SceneLoader.LoadScene(sceneName: SceneNames.UIScene, loadType: LoadSceneMode.Additive, onComplete: null);
                    });

                    break;
                case SceneState.GameScene:
                    GameManager.Instance.SceneLoader.UnloadAllOpenedScene(onComplete: () =>
                    {
                        GameManager.Instance.SceneLoader.LoadScene(sceneName: SceneNames.GameScene, loadType: LoadSceneMode.Additive, onComplete: null);
                    });
                    break;
            }
        }
    }
}