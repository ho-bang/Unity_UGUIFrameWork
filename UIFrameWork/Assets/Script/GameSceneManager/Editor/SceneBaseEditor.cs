using UnityEngine;

namespace CJR.GameScene.Editor
{
    using UnityEditor;

    [CustomEditor(typeof(SceneBase))]
    public class SceneBaseEditor : Editor
    {
        private SceneBase _sceneBase; 
        void OnEnable()
        {
            _sceneBase = (SceneBase)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_sceneBase == null)
            {
                return;
            }
            else
            {
                Debug.Log("test");
            }

            var sceneBase = (SceneBase)target;
            for (var index = 0; index < _sceneBase.UIList.Length; index++)
            {
                var uiPrefab = _sceneBase.UIList[index];
                if (uiPrefab == null)
                {
                    _sceneBase.UIResourcePath[index] = string.Empty;
                    continue;
                }

                var path = AssetDatabase.GetAssetPath(uiPrefab);
                _sceneBase.UIResourcePath[index] = path;
            }
        }
    }
}