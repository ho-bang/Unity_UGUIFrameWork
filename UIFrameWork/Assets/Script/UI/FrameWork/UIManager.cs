using System.Collections.Generic;
using CJR.ResourceManager;
using UnityEngine;

namespace CJR.UI
{
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
            element.SetAsLastSibling();

            _openList.Add(element);
            return element;
        }


        // UI를 꺼줄 때, Active될 때,고민이다.. 
        public static void Close(string name)
        {
            if (_openList.Count == 0) return;

            var closeTarget = _openList[^1];
            closeTarget.Close();
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