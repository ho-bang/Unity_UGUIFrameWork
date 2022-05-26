using System;
using UnityEngine;

namespace CJR.GameScene
{
    public class GameSceneManager : MonoBehaviour
    {
        private SceneBase _currentScene;

        public void StartScene(SceneBase scene, GameScene.SceneType sceneType)
        {
            if (_currentScene != null)
            {
                // 씬이 아직 뭔가 하고 있는 중이라면?
                DisposeCurrentScene();
            }

            switch (scene.SceneType)
            {
                case GameScene.SceneType.Lobby:
                    _currentScene = scene;
                    break;
                case GameScene.SceneType.Game:
                    break;
            }
        }

        public void DisposeCurrentScene()
        {
            _currentScene = null;
        }

        private void UpdateCurrentScene()
        {
            if (_currentScene.State == GameScene.SceneDataState.FinishEnd)
            {
                return;
            }

            var dt = Time.deltaTime;
            _currentScene?.OnUpdate(dt);

            switch (_currentScene.State)
            {
                case GameScene.SceneDataState.None:
                    _currentScene.OnStart();
                    break;
                case GameScene.SceneDataState.Start:
                    break;
                case GameScene.SceneDataState.StartEnd:
                    _currentScene.OnFinish();
                    break;
                case GameScene.SceneDataState.Finish:
                    break;
                case GameScene.SceneDataState.FinishEnd:
                    DisposeCurrentScene();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void Update()
        {
            UpdateCurrentScene();
        }
    }
}