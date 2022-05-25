
using System;
using UnityEngine;

namespace CJR.GameScene
{
    public class GameScene
    {
        public enum SceneType
        {
            Lobby,
            Game
        }


        public enum SceneDataState
        {
            Start,
            UILoad,
            Finish
        }

        public class SceneDataArgs
        {
            public SceneDataArgs(SceneDataState state)
            {
                State = state;
            }

            public SceneDataState State { private set; get; }
        }

        public delegate void GameSceneDataHandler(object sceneData, SceneDataArgs args);
    }

    public interface IScene
    {
        event GameScene.GameSceneDataHandler GameSceneDataHandler;
        GameScene.SceneDataState State { get; }
        void Start();
        void UILoading();
        void End();
        void Update();
    }

    public class SceneData : IScene
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

        public void Start()
        {
            // do start..
            {

            }
            State = GameScene.SceneDataState.Start;
        }

        public void UILoading()
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

    public class GameSceneManager : MonoBehaviour
    {
        
        private IScene _currentScene;

        public void StartScene(GameScene.SceneType scene)
        {
            switch (scene)
            {
                case GameScene.SceneType.Lobby:
                    _currentScene = new SceneData();
                    _currentScene.GameSceneDataHandler += OnChangedCurrentSceneData;
                    break;
                case GameScene.SceneType.Game:
                    break;
            }
        }

        public void OnChangedCurrentSceneData(object sender, GameScene.SceneDataArgs args)
        {
            switch (args.State)
            {
                case GameScene.SceneDataState.Start:
                    break;
                case GameScene.SceneDataState.Finish:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void Update()
        {
            _currentScene?.Update();
        }
    }
}