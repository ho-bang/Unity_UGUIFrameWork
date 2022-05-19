using System;
using CJR.UI;

namespace CJR.ResourceManager
{
    public static class UIResourceManager
    {
        private static readonly IResourceLoader<UIElement> UIResourceLoader = new UIResourceLoader<UIElement>();
        // 여기서 풀링 처리를 하던가.. LifeTimeManager를 만드는 게 좋을 것 같다.

        public static UIElement GetUIElementInstance(string name, Action onComplete)
        {
            return UIResourceLoader.Get(name, onComplete);
        }

        public static void ReturnUI(UIElement element)
        {
            UIResourceLoader.Return(element);
        }
    }
}