using UnityEngine;
using UnityEngine.SceneManagement;

namespace CJR.GameScene
{
    using Scene;
    public class LobbyScene : IScene
    {
        private GameScene.SceneDataState _state;
        public GameScene.SceneDataState State
        {
            private set
            {
                _state = value;
                _eventHandler?.Invoke(this, new GameScene.SceneDataArgs(_state));
            }
            get => _state;
        }

        private event GameScene.GameSceneDataHandler _eventHandler;
        public event GameScene.GameSceneDataHandler GameSceneDataHandler
        {
            add => _eventHandler += value;
            remove => _eventHandler -= value;
        }

        public void Dispose()
        {
            
        }

        public void Start()
        {

        }

        public void UILoad()
        {

        }

        public void Finish()
        {

        }

        public void Update(float fDelta)
        {

        }

        //public void Start()
        //{
        //    void SceneLoadEnd()
        //    {
        //        UILoad();
        //    }

        //    // do Start..
        //    {
        //        SceneLoader.Instance.LoadScene(sceneName: SceneNames.UIScene, loadType: LoadSceneMode.Additive, onComplete: SceneLoadEnd);
        //    }
        //    State = GameScene.SceneDataState.Start;
        //}

        //public void UILoad()
        //{
        //    // Do UILoad..
        //    {
        //        // 흐....음........ 
        //        var tempRoot = GameObject.Find("UISceneManager");
        //        // 이걸 여기서 이렇게 해야 할까?
        //    }
        //    State = GameScene.SceneDataState.UILoad;
        //}

        //public void Finish()
        //{
        //    // Do Finish..
        //    {

        //    }
        //    State = GameScene.SceneDataState.Finish;
        //}
    }
}