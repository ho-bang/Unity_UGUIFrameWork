using System.Collections.Generic;
using Assets.Script.ResourceManager;
using UnityEngine;

namespace Assets.UI.FrameWork
{
    // 비동기를 지원한다면, 인터페이스로 세분화 시키는 게 좋겠다.
    public class UIElement_Data<T> where T : UIElement
    {
        public UIElement Element { get; private set; }

        public void Init(T element)
        {
            Element = element;
        }
    }

    public static class UIManager
    {
        //private static readonly Dictionary<string, UIElement_Data<UIElement>> _dic = new();
        private static readonly List<UIElement> _openList = new();

        public static UIElement Open(GameObject parent, string name)
        {
            var element = GetUIElementInstance(name);
            if (element == null)
            {
                return null;
            }

            element.SetParent(parent);
            element.Open();

            // 나중에 속성으로 보이는 위치를 조절해주긴 해야 한다.
            // 지금은 무조건 마지막에 보이도록 처리
            element.SetAsLastSibling();

            _openList.Add(element);
            return element;
        }


        public static void Close(string name)
        {
            if (_openList.Count == 0) return;

            var closeTarget = _openList[^1];
            closeTarget.Close();

            // 혹은, 다른 곳으로 옮겨야 한다.
            // 혹은 그냥 삭제하던가.
            closeTarget.SetParent(null);

            _openList.Remove(closeTarget);
        }

        private static UIElement GetUIElementInstance(string name)
        {
            //if (_dic.TryGetValue(name, out var elementData) == false)
            //{
            //    var element = UIResourceManager.GetUIElementInstance(name, onComplete: null);
            //    elementData = new UIElement_Data<UIElement>();
            //    elementData.Init(element);
            //    _dic.Add(name, elementData);
            //}

            var element = UIResourceManager.GetUIElementInstance(name, onComplete: null);
            return element;
        }
    }
}