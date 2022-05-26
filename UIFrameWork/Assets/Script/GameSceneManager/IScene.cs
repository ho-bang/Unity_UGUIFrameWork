using CJR.UI;
using UnityEngine;

namespace CJR.GameScene
{
    /// <summary>
    /// Start -> Finish ~~ ...
    /// </summary>

    public abstract class SceneBase : MonoBehaviour
    {
        public abstract UIDialog[] UIList { set; get; }
        public abstract string[] UIResourcePath { set; get; }
        public abstract GameScene.SceneType SceneType { get; }
        public abstract GameScene.SceneDataState State { get; }
        public abstract void OnStart();
        public abstract void LoadUI();
        public abstract void OnFinish();
        public abstract void OnUpdate(float dt);
    }

    public interface IScene<T> where T : MonoBehaviour
    {
        //event GameScene.GameSceneDataHandler GameSceneDataHandler;
        GameScene.SceneDataState State { get; }
        void Start();
        void Finish();
        void Update(float dt);
    }
}