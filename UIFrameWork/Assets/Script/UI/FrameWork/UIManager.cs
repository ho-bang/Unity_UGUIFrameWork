using System.Collections.Generic;
using CJR.Resource;
using UnityEngine;

namespace CJR.UI
{
    using GameScene;
    
    public static class UIManager
    {
        private static readonly List<CJRUIBase> _openList = new();
        public static IReadOnlyList<CJRUIBase> OpenedList => _openList;
        public static CJRUIBase Open(GameObject parent, GameScene.SceneType sceneType, string name)
        {
            var ui = UIInstanceManager.Instance?.GetInstance(name, onComplete: null);
            if (ui == null)
            {
                return null;
            }

            ui.SetParent(parent);
            ui.SetSceneType(sceneType);
            ui.Open();
            ui.SetAsLastSibling();

            _openList.Add(ui);
            return ui;
        }

        public static bool Close(CJRUIBase ui)
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
            UIInstanceManager.Instance?.ReturnInstance(ui);
            
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

            UIInstanceManager.Instance?.ReturnInstance(closeTarget);
        }

        public static void OnDestroy(CJRUIBase ui)
        {
            // fake null
            UIInstanceManager.Instance?.RemoveInstance(ui);

            if (_openList.Count == 0)
            {
                return;
            }
            _openList.Remove(ui);
        }

        public static void SendMessageToOpenList(IUIMessage message)
        {
            foreach (var openedUI in _openList)
            {
                if (openedUI == null)
                {
                    continue;
                }

                openedUI.ReceiveMessage(message);
            }
        }

        public static void SendMessageToChild(IUIMessage message, GameObject go)
        {
            var children = go.GetComponentsInChildren<CJRUIBase>(includeInactive: true);
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

        public static void SendMessageToParents(IUIMessage message, GameObject go)
        {
            var children = go.GetComponentsInParent<CJRUIBase>(includeInactive: true);
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