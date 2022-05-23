using System.Collections.Generic;
using CJR.Resource;
using UnityEngine;

namespace CJR.UI
{
    // WINDOW 개념을 대체 할 수 있는 게 없을까.
    public static class UIManager
    {
        private static readonly List<UIDialog> _openList = new();
        public static IReadOnlyList<UIDialog> OpenedList => _openList;
        public static UIDialog Open(GameObject parent, string name)
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

        public static void Close(UIDialog ui)
        {
            if (_openList.Contains(ui) == false)
            {
                return;
            }

            ui.SetParent(null);
            ui.Close();
            ui.Return();

            _openList.Remove(ui);
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

        public static void SendMessageToOpenList(IUIMessage message)
        {
            foreach (var openedUI in _openList)
            {
                openedUI.ReceiveMessage(message);
            }
        }

        public static void SendMessageToGobChild(IUIMessage message, GameObject game)
        {
            var children = game.GetComponentsInChildren<UIDialog>(includeInactive: true);
            foreach (var uiDialog in children)
            {
                uiDialog.ReceiveMessage(message);
            }
        }
    }
}