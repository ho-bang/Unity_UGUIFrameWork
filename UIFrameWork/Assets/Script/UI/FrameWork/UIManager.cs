using System.Collections.Generic;
using CJR.ResourceManager;
using UnityEngine;

namespace CJR.UI
{
    public static class UIManager
    {
        private static readonly List<UIElement> _openList = new();
        public static IReadOnlyList<UIElement> OpenedList => _openList;
        public static UIElement Open(GameObject parent, string name)
        {
            var element = UIResourceManager.Instance.GetUIElementInstance(name, onComplete: null);
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

        public static void CloseFromAbove()
        {
            if (_openList.Count == 0)
            {
                return;
            }

            var closeTarget = _openList[^1];
            closeTarget.Close();
            _openList.Remove(closeTarget);

            UIResourceManager.Instance.ReturnInstance(closeTarget);
        }
    }
}