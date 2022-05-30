using System.Collections.Generic;
using CJR.UI;
using UnityEngine;

namespace CJR.GameScene
{
    public class SceneBase : MonoBehaviour
    {
        [field: SerializeField] public List<UIDialog> UIListToLoadOnStart { set; get; } = new();
        [field: SerializeField, HideInInspector] public string[] UIPathArrToLoadOnStart { set; get; }

        public virtual GameScene.SceneType SceneType { get; }
        public virtual GameScene.SceneDataState SceneState { get; }
        public virtual void Init() { }
        public virtual void LoadUI() { }
        public virtual void OnFinish() { }
        public virtual void CleanUp() { }
        public virtual void OnUpdate(float dt) { }
    }
}