namespace CJR.GameScene
{
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
            // do start..
            {

            }
            State = GameScene.SceneDataState.Start;
        }

        public void UILoad()
        {
            // do UILoad..
            {

            }
            State = GameScene.SceneDataState.Start;
        }

        public void End()
        {
            // do End..
            {

            }
            State = GameScene.SceneDataState.Start;
        }

        public void Update()
        {

        }
    }
}