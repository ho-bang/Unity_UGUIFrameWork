using System.Collections.Generic;
using CJR.Resource;
using UnityEngine;

namespace CJR.UI
{
    using GameScene;
    
    public static class UIManager
    {
        private static readonly List<UIDialog> _openList = new();
        public static IReadOnlyList<UIDialog> OpenedList => _openList;
        public static UIDialog Open(GameObject parent, GameScene.SceneType sceneType, string name)
        {
            var ui = UIInstanceManager.Instance?.GetDialogInstance(name, onComplete: null);
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

        public static void OnDestroy(UIDialog uiDialog)
        {
            // fake null
            UIInstanceManager.Instance?.RemoveInstance(uiDialog);

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