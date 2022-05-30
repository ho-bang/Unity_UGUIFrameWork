using System;
using UnityEngine;

namespace CJR.GameScene
{
    public class GameSceneManager : MonoBehaviour
    {
        // ��⿭ ó���� �ʿ��ұ�?
        private SceneBase _currentScene;

        public void StartScene(SceneBase scene)
        {
            if(scene == null)
            {
                return;
            }

            if (_currentScene != null)
            {
                // ���� ���� ���� �ϰ� �ִ� ���̶��?
                CleanUpScene();
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

            if (_currentScene.SceneState == GameScene.SceneDataState.FinishEnd)
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
                case GameScene.SceneDataState.StartEnd:
                    _currentScene.LoadUI();
                    break;
                case GameScene.SceneDataState.UILoad:
                    break;
                case GameScene.SceneDataState.UILoadFinish:
                    break;
                case GameScene.SceneDataState.Finish:
                    break;
                case GameScene.SceneDataState.FinishEnd:
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