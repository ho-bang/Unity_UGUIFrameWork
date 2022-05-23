using System;
using System.Collections.Generic;
using CJR.Resource;
using UnityEngine;

namespace CJR.UI
{
    public class UIDialog : MonoBehaviour, IPoolObject<UIDialog>
    {
        private GameObject Parent;
        // 굳이 이걸 두는 게 좋은 짓일까.. 
        private readonly List<UIDialog> _childElement = new ();
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
                var parentDialog = Parent.GetComponent<UIDialog>();
                if (parentDialog != null)
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
                var parentUI = Parent.GetComponent<UIDialog>();
                if (parentUI is null) return;
                parentUI.AddChild(this);

                _myRectTransform.SetParent(parent.transform, worldPositionStays: false);
            }
        }

        public void AddChild(UIDialog dialog)
        {
            if (dialog is null)
            {
                Debug.Log($"ui is null");
                return;
            }

            if (_childElement.Contains(dialog))
            {
                Debug.Log($"already contain ui {dialog.name}");
                return;
            }

            _childElement.Add(dialog);
        }

        public void RemoveChild(UIDialog dialog)
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
            UIManager.Destory(this);
        }

        #region IPoolObj
        public string Key { set; get; }
        public Action<UIDialog> OnReturn { set; get; }

        public void Return()
        {
            // object pool에 반환한다.
            OnReturn?.Invoke(this);
        }
        #endregion
    }
}