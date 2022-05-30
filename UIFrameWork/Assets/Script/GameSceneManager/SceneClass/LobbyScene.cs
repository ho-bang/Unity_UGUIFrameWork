using CJR.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CJR.GameScene
{
    using Scene;
    public class LobbyScene : SceneBase
    {
        private GameObject _canvasGob;
        private GameObject _canvasGameObject
        {
            get
            {
                if (_canvasGob == null)
                {
                    _canvasGob = SceneLoader.Instance.FindGobFromScene(SceneNames.UIScene, "Canvas");
                }

                return _canvasGob;
            }
        }

        public GameScene.SceneType _sceneType;
        public override GameScene.SceneType SceneType => _sceneType; 
        
        private GameScene.SceneDataState _sceneState = GameScene.SceneDataState.None;
        public override GameScene.SceneDataState SceneState => _sceneState;

        public override void Init()
        {
            _sceneState = GameScene.SceneDataState.Start;
            void SceneLoadEnd()
            {
                _sceneState = GameScene.SceneDataState.StartEnd;
            }

            SceneLoader.Instance.LoadScene(sceneName: SceneNames.UIScene, loadType: LoadSceneMode.Additive, onComplete: SceneLoadEnd);
        }

        public override void LoadUI()
        {
            if (_canvasGameObject == null)
            {
                Debug.LogError($"Not found ui canvas game object");
                return;
            }

            if (UIPathArrToLoadOnStart == null)
            {
                Debug.LogError($"UIPathArrToLoadOnStart is null");
                return;
            }

            foreach (var uiPath in UIPathArrToLoadOnStart)
            {
                if (string.IsNullOrEmpty(uiPath))
                {
                    continue;
                }

                UIManager.Open(_canvasGameObject, uiPath);
            }
        }

        public override void OnFinish()
        {           
            _sceneState = GameScene.SceneDataState.Finish;
            _sceneState = GameScene.SceneDataState.FinishEnd;
        }

        public override void CleanUp()
        {
            _sceneState = GameScene.SceneDataState.CleanUp;
        }

        public override void OnUpdate(float dt)
        {
        }
    }
}