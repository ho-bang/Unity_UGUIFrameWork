﻿using UnityEngine;

namespace CJR.UI
{
    public class UIElement : MonoBehaviour
    {
        private GameObject Parent;
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
            Parent = parent;
            _myRectTransform.SetParent(parent.transform, worldPositionStays: false);
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
    }
}