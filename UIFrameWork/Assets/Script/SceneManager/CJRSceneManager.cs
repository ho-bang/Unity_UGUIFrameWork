using UnityEngine.SceneManagement;

namespace CJR.Scene
{
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
                    SceneLoader.Instance.LoadScene(sceneName: SceneNames.Lobby, loadType: LoadSceneMode.Additive, onComplete: null);
                    break;
                case SceneState.GameScene:
                    SceneLoader.Instance.LoadScene(sceneName: SceneNames.GameScene, loadType: LoadSceneMode.Additive, onComplete: null);
                    break;
            }
        }

        public void ChanageScene(SceneState state)
        {
            switch (state)
            {
                case SceneState.Lobby:
                    SceneLoader.Instance.UnloadAllOpenedScene(onComplete: () =>
                    {
                        SceneLoader.Instance.LoadScene(sceneName: SceneNames.Lobby, loadType: LoadSceneMode.Additive, onComplete: null);
                    });

                    break;
                case SceneState.GameScene:
                    SceneLoader.Instance.UnloadAllOpenedScene(onComplete: () =>
                    {
                        SceneLoader.Instance.LoadScene(sceneName: SceneNames.GameScene, loadType: LoadSceneMode.Additive, onComplete: null);
                    });

                    break;
            }
        }
    }
}