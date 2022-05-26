using CJR.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CJR.GameScene
{
    using Scene;
    public class LobbyScene : SceneBase
    {
        public Canvas UiCanvas;
        [field: SerializeField] public override UIDialog[] UIList { set; get; }
        [field: SerializeField] public override string[] UIResourcePath { set; get; }
        
        public GameScene.SceneType _sceneType;
        public override GameScene.SceneType SceneType => _sceneType;
        
        private GameScene.SceneDataState _state;
        public override GameScene.SceneDataState State => _state;


        public override void OnStart()
        {
            _state = GameScene.SceneDataState.Start;
            void SceneLoadEnd()
            {
                _state = GameScene.SceneDataState.StartEnd;
            }

            // do Start..
            SceneLoader.Instance.LoadScene(sceneName: SceneNames.UIScene, loadType: LoadSceneMode.Additive, onComplete: SceneLoadEnd);
        }

        public override void LoadUI()
        {
            // 캔버스 매니저 같은거라도 만들까.
        }

        public override void OnFinish()
        {            // Do Finish..
            _state = GameScene.SceneDataState.Finish;

            _state = GameScene.SceneDataState.FinishEnd;
        }

        public override void OnUpdate(float dt)
        {
        }
    }
}