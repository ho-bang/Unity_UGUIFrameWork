using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Script.Scene
{
    public class SceneLoader : MonoBehaviour
    {
        //scene load는 기본적으로 비동기 로딩을 원칙으로 한다.
        List<string> _sceneName = new List<string>();

        public void LoadUIScene(string sceneName, Action<AsyncOperation> onComplete)
        {
            void OnComplete(AsyncOperation asyncOperation)
            {
                if (asyncOperation.isDone)
                {
                    Debug.Log($"Scene Load Success _ {sceneName}");
                    _sceneName.Add(sceneName);
                }
            }

            if (_sceneName.Contains(sceneName))
            {
                Debug.Log($"Already Exist Scene _ {sceneName}");
                return;
            }

            var async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            async.completed += onComplete;
            async.completed += OnComplete;
        }


        public void UnloadScene(string sceneName, Action<AsyncOperation> onComplete)
        {
            void OnComplete(AsyncOperation asyncOperation)
            {
                if (asyncOperation.isDone)
                {
                    Debug.Log($"Scene Unload Success _ {sceneName}");
                    _sceneName.Remove(sceneName);
                }
            }

            if (_sceneName.Contains(sceneName) == false)
            {
                Debug.Log($"Not Exist Scene _ {sceneName}");
                return;
            }

            var async = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
            async.completed += onComplete;
            async.completed += OnComplete;
        }


        public void Update()
        {
        }
    }
}