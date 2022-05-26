using System;

namespace CJR.GameScene
{
    /// <summary>
    /// Start -> UIload -> Finish ~~ ...
    /// </summary>

    public interface IScene : IDisposable
    {
        event GameScene.GameSceneDataHandler GameSceneDataHandler;
        GameScene.SceneDataState State { get; }
        void Start();
        void UILoad();
        void Finish();
        void Update();
    }
}