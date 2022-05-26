using System;

namespace CJR.GameScene
{
    /// <summary>
    /// Start -> UIload -> End ~~ ...
    /// </summary>

    public interface IScene : IDisposable
    {
        event GameScene.GameSceneDataHandler GameSceneDataHandler;
        GameScene.SceneDataState State { get; }
        void Start();
        void UILoad();
        void End();
        void Update();
    }
}