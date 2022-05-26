using System;
using UnityEngine;

namespace CJR.GameScene
{
    public class GameSceneManager : MonoBehaviour
    {
        private IScene _currentScene;

        public void StartScene(GameScene.SceneType scene)
        {
            if (_currentScene != null)
            {
                // 씬이 아직 뭔가 하고 있는 중이라면?
                DisposeCurrentScene();
            }

            switch (scene)
            {
                case GameScene.SceneType.Lobby:
                    _currentScene = new LobbyScene();
                    _currentScene.GameSceneDataHandler += OnChangedCurrentSceneData;
                    _currentScene.Start();
                    break;
                case GameScene.SceneType.Game:
                    break;
            }

            _currentScene?.Start();
        }

        public void OnChangedCurrentSceneData(object sender, GameScene.SceneDataArgs args)
        {
            switch (args.State)
            {
                case GameScene.SceneDataState.Start:
                    _currentScene.UILoad();
                    break;
                case GameScene.SceneDataState.Finish:
                    DisposeCurrentScene();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void DisposeCurrentScene()
        {
            _currentScene.Dispose();
            _currentScene = null;
        }

        void Update()
        {
            _currentScene?.Update();
        }
    }
}