using System;
using System.Collections.Generic;
using CJR.ResourceManager;
using UnityEngine;

namespace CJR.UI
{
    public class UIElement : MonoBehaviour, IPoolObject<UIElement>
    {
        private GameObject Parent;
        private readonly List<UIElement> _childElement = new ();
        private RectTransform _myRectTransform;
        private bool _active;

        protected bool Active
        {
            get => _active;
            set
            {
                _active = value;
                gameObject.gameObject.SetActive(_active);
            }
        }

        public void SetParent(GameObject parent)
        {
            if (parent == null)
            {
                Parent = null;
                _myRectTransform.SetParent(null, worldPositionStays: false);
                return;
            }

            Parent = parent;
            var parentUI = Parent.GetComponent<UIElement>();
            if (parentUI != null)
            {
                parentUI.AddChild(this);
            }

            _myRectTransform.SetParent(parent.transform, worldPositionStays: false);
        }

        public void AddChild(UIElement element)
        {
            if (element == null)
            {
                Debug.Log($"ui is null");
                return;
            }

            if (_childElement.Contains(element))
            {
                Debug.Log($"already contain ui {element.name}");
                return;
            }

            _childElement.Add(element);
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

        #region IPoolObj
        public string Key { set; get; }
        public Action<UIElement> OnReturn { set; get; }

        public void Return()
        {
            Close();
            // object pool에 반환한다.
            OnReturn?.Invoke(this);
            // 부모를 ResouceLoader쪽으로 바꾸던가 해야 함
        }
        #endregion
    }
}