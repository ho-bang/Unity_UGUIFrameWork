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
            var element = UIResourceManager.Instance?.GetUIElementInstance(name, onComplete: null);
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

        public static bool Close(UIDialog ui)
        {
            if (_openList.Contains(ui) == false)
            {
                return false;
            }

            if (ui == null)
            {
                return false;
            }

            ui.Close();
            ui.Return();
            UIResourceManager.Instance?.ReturnInstance(ui);
            
            _openList.Remove(ui);
            return true;
        }

        public static void CloseFromAbove()
        {
            if (_openList.Count == 0)
            {
                return;
            }

            var closeTarget = _openList[^1];
            if (closeTarget == null)
            {
                _openList.RemoveAt(index: _openList.Count - 1);
                return;
            }
            
            closeTarget.Close();
            _openList.Remove(closeTarget);

            UIResourceManager.Instance?.ReturnInstance(closeTarget);
        }

        public static void Destroy(UIDialog uiDialog)
        {
            // fake null
            UIResourceManager.Instance?.RemoveInstance(uiDialog);

            if (_openList.Count == 0)
            {
                return;
            }
            _openList.Remove(uiDialog);
        }

        public static void SendMessageToOpenList(IUIMessage message)
        {
            foreach (var openedUI in _openList)
            {
                if (openedUI == null)
                {
                    // 여긴 ~~ 흠.. null 확률이 좀 있지
                    continue;
                }

                openedUI.ReceiveMessage(message);
            }
        }

        public static void SendMessageToGobChild(IUIMessage message, GameObject game)
        {
            var children = game.GetComponentsInChildren<UIDialog>(includeInactive: true);
            foreach (var uiDialog in children)
            {
                if (uiDialog == null)
                {
                    // 여기는 차라리 null날 확률이 적다
                    continue;
                }

                uiDialog.ReceiveMessage(message);
            }
        }
    }
}