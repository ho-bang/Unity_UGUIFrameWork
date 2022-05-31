using CJR.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CJR.GameScene
{
    using Scene;

    public class LobbyScene : SceneBase
    {
        public override void Init()
        {
            SceneState = GameScene.SceneDataState.Start;

            void SceneLoadEnd()
            {
                LoadUI();
            }

            SceneLoader.Instance.LoadScene(sceneName: SceneNames.UIScene, loadType: LoadSceneMode.Additive, onComplete: SceneLoadEnd);
        }

        public override void LoadUI()
        {
            SceneState = GameScene.SceneDataState.UILoad;

            if (_uiCanvasGameObject == null)
            {
                Debug.LogError($"Not found ui canvas game object");
                return;
            }

            if (UIPathArrToLoadOnStart == null)
            {
                Debug.LogError($"{nameof(UIPathArrToLoadOnStart)} is null");
                return;
            }

            foreach (var uiPath in UIPathArrToLoadOnStart)
            {
                if (string.IsNullOrEmpty(uiPath))
                {
                    continue;
                }

                UIManager.Open(_uiCanvasGameObject, SceneType, uiPath);
            }

            Finish();
        }

        public override void Finish()
        {
            SceneState = GameScene.SceneDataState.Finish;
        }

        public override void CleanUp()
        {
            SceneState = GameScene.SceneDataState.CleanUp;
        }

        public override void OnUpdate(float dt)
        {
        }
    }
}