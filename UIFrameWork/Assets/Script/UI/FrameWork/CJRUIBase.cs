using System;
using System.Collections.Generic;
using UnityEngine;

namespace CJR.UI
{
    using GameScene;

    public class CJRUIBase : MonoBehaviour
    {
        public GameScene.SceneType DialogSceneType { private set; get; }

        private GameObject Parent;
        // 굳이 이걸 두는 게 좋은 짓일까.. 
        private readonly List<CJRUIBase> _childElement = new ();
        private RectTransform _myRectTransform;
        private bool _active;

        protected bool Active
        {
            get => _active;
            private set
            {
                _active = value;
                gameObject.SetActive(_active);
            }
        }

        public void SetParent(GameObject parent)
        {
            if (Parent == parent)
            {
                return;
            }

            if (Parent is not null)
            {
                var parentDialog = Parent.GetComponent<CJRUIBase>();
                if (parentDialog is not null)
                {
                    parentDialog.RemoveChild(this);
                }
            }

            if (parent is null)
            {
                Parent = null;
                _myRectTransform.SetParent(null, worldPositionStays: false);
            }
            else
            {
                Parent = parent;
                var parentUI = Parent.GetComponent<CJRUIBase>();
                if (parentUI is not null)
                {
                    parentUI.AddChild(this);
                }
                
                _myRectTransform.SetParent(parent.transform, worldPositionStays: false);
            }
        }

        public void SetSceneType(GameScene.SceneType sceneType)
        {
            DialogSceneType = sceneType;
        }

        public void AddChild(CJRUIBase dialog)
        {
            if (dialog is null)
            {
                Debug.Log("ui is null");
                return;
            }

            if (_childElement.Contains(dialog))
            {
                Debug.Log($"already contain ui {dialog.name}");
                return;
            }

            _childElement.Add(dialog);
        }

        public void RemoveChild(CJRUIBase dialog)
        {
            if (dialog is null)
            {
                Debug.Log($"ui is null");
                return;
            }

            if (_childElement.Contains(dialog) == false)
            {
                return;
            }

            _childElement.Remove(dialog);
        }

        public void Open()
        {
            Active = true;
        }

        public void Close()
        {
            Active = false;
        }

        public void SetAsLastSibling()
        {
            transform.SetAsLastSibling();
        }

        public virtual void ReceiveMessage(IUIMessage message) { }
        protected virtual void OnAwake() { }
        protected virtual void Enable() { }

        void Awake()
        {
            OnAwake();
        }

        void OnEnable()
        {
            _myRectTransform = GetComponent<RectTransform>();
            Enable();
        }

        void OnDestroy()
        {
            UIManager.OnDestroy(this);
        }

        public string Key;
        public Action<string, CJRUIBase> OnReturn { set; get; }
        public void Return()
        {
            // object pool에 반환한다.
            OnReturn?.Invoke(Key, this);
        }
    }
}