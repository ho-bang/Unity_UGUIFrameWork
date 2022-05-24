using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CJR.Scene
{
    public static class SceneNames
    {
        private const string _folderPath = "Scene";
        private const string _lobby = "Lobby";
        private const string _main = "Main";

        public static string Lobby => $"{_folderPath}/{_lobby}";
        public static string main  => $"{_folderPath}/{_main}";
    }

    //scene load는 기본적으로 비동기 로딩을 원칙으로 한다.
    public class SceneLoader : MonobehaviourSingleton<SceneLoader>
    {
        private readonly List<string> _openedScenes = new();
        public IReadOnlyList<string> ReadOnlyOpenedScenes => _openedScenes;

        private readonly List<string> _loadingScene = new();
        private readonly List<string> _unloadingScene = new();

        public void LoadScene(string sceneName, LoadSceneMode loadType, Action onComplete)
        {
            if (_openedScenes.Contains(sceneName))
            {
                Debug.LogWarning($"already exist scene _ {sceneName}");
                return;
            }

            if (_loadingScene.Contains(sceneName))
            {
                Debug.LogWarning($"already loading _ {sceneName}");
                return;
            }

            // 언로딩 중인 씬은.. 로딩할 수 없도록 처리한다. 혹은 Queue처리가 되어야 한다.
            if (_unloadingScene.Contains(sceneName))
            {
                Debug.LogWarning($"Already Loading _ {sceneName}");
                return;
            }

            void LoadComplete(AsyncOperation asyncOperation)
            {
                _loadingScene.Remove(sceneName);
                _openedScenes.Add(sceneName);
                onComplete?.Invoke();
                Debug.Log($"load complete _ {sceneName}");
            }

            _loadingScene.Add(sceneName);
            var async = SceneManager.LoadSceneAsync(sceneName, loadType);
            async.completed += LoadComplete;
        }

        public void UnloadScene(string sceneName, Action onComplete)
        {
            if (_openedScenes.Contains(sceneName) == false)
            {
                Debug.LogWarning($"not Exist Scene _ {sceneName}");
                return;
            }

            if (_unloadingScene.Contains(sceneName))
            {
                Debug.LogWarning($"already unloading _ {sceneName}");
                return;
            }

            void UnloadComplete(AsyncOperation asyncOperation)
            {
                _unloadingScene.Remove(sceneName);
                _openedScenes.Remove(sceneName);
                onComplete?.Invoke();
                Debug.Log($"unload complete _ {sceneName}");
            }

            _unloadingScene.Add(sceneName);
            var async = SceneManager.UnloadSceneAsync(sceneName);
            async.completed += UnloadComplete;
        }

        public void UnloadAllOpenedScene(Action onComplete)
        {
            void UnloadAllComplete()
            {
                if (_unloadingScene.Count == 0)
                {
                    onComplete?.Invoke();
                    Debug.Log("unload all complete");
                }
            }

            foreach (var openedScene in _openedScenes)
            {
                UnloadScene(openedScene, UnloadAllComplete);
            }
        }
    }
}