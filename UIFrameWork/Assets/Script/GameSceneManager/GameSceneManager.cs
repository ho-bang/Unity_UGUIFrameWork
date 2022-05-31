using System;
using UnityEngine;

namespace CJR.GameScene
{
    public class GameSceneManager : MonoBehaviour
    {
        // 대기열 처리가 필요할까? 혹은 여러 개의 씬이 들어올 수 있겠지?
        private SceneBase _currentScene;

        public void StartScene(SceneBase scene)
        {
            if(scene == null)
            {
                return;
            }

            if (_currentScene != null)
            {
                // 씬이 아직 뭔가 하고 있는 중이라면?
                CleanUpScene();
            }

            switch (scene.SceneType)
            {
                case GameScene.SceneType.Lobby:
                    _currentScene = scene;
                    break;
                case GameScene.SceneType.Main:
                    break;
            }
        }

        public void CleanUpScene()
        {
            _currentScene.CleanUp();
            _currentScene = null;
        }

        private void UpdateCurrentScene()
        {
            if (_currentScene == null)
            {
                return;
            }

            if (_currentScene.SceneState is GameScene.SceneDataState.Finish or GameScene.SceneDataState.CleanUp)
            {
                return;
            }

            var dt = Time.deltaTime;
            _currentScene.OnUpdate(dt);

            switch (_currentScene.SceneState)
            {
                case GameScene.SceneDataState.None:
                    _currentScene.Init();
                    break;
                case GameScene.SceneDataState.Start:
                    break;
                case GameScene.SceneDataState.UILoad:
                    break;
                case GameScene.SceneDataState.Finish:
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