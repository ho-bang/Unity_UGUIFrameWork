using UnityEngine.Windows.WebCam;

namespace CJR.GameScene
{
    using UI;

    public class MainScene : SceneBase
    {
        public GameScene.SceneType _sceneType = GameScene.SceneType.Main;
        public override GameScene.SceneType SceneType => _sceneType;

        private GameScene.SceneDataState _sceneState = GameScene.SceneDataState.None;
        public override GameScene.SceneDataState SceneState => _sceneState;

        public override void Init()
        {
            _sceneState = GameScene.SceneDataState.Start;

            LoadUI();
        }

        public override void LoadUI()
        {
            _sceneState = GameScene.SceneDataState.UILoad;

            foreach (var uiPath in UIPathArrToLoadOnStart)
            {
                UIManager.Open(_uiCanvasGameObject, uiPath);
            }
        }
    }
}
