using System.Collections.Generic;
using CJR.Scene;
using CJR.UI;
using UnityEngine;

namespace CJR.GameScene
{
    public class SceneBase : MonoBehaviour
    {
        private GameObject _uiCanvasGob;
        protected GameObject _uiCanvasGameObject
        {
            get
            {
                if (_uiCanvasGob == null)
                {
                    _uiCanvasGob = SceneLoader.Instance.FindGobFromScene(SceneNames.UIScene, "Canvas");
                }

                return _uiCanvasGob;
            }
        }

        [field: SerializeField] public List<CJRUIBase> UIListToLoadOnStart { set; get; } = new ();
        [field: SerializeField, HideInInspector] public string[] UIPathArrToLoadOnStart { set; get; }
        [field: SerializeField] public GameScene.SceneType SceneType { protected set; get; }
        public GameScene.SceneDataState SceneState { protected set; get; }

        public virtual void Init() { }
        public virtual void LoadUI() { }
        public virtual void Finish() { }
        public virtual void CleanUp() { }
        public virtual void OnUpdate(float dt) { }
    }
}