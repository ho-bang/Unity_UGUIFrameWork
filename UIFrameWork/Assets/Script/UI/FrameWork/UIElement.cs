using UnityEngine;

namespace CJR.UI
{
    interface IPoolObject
    {
        void Return();
    }

    public class UIElement : MonoBehaviour, IPoolObject
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

        public void Return()
        {
            // ResourceManager.Return 에 넣어줘야 한다.
            // 추가로 ChildElement도 찾아서 처리해주는 게 필요하다.
        }

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