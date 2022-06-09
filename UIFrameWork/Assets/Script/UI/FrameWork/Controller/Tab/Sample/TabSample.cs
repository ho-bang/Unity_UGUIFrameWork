using UnityEngine;
using UnityEngine.EventSystems;

namespace CJR.UI
{
    public class TabSample : CJRUIBase, ITabController<TabSample>
    {
        public ITabController<TabSample>.OnPointerClickHandler PointerClickHandler { get; set; }

        public void RegisterOnPointClick(ITabController<TabSample>.OnPointerClickHandler handler)
        {
            PointerClickHandler -= handler;
            PointerClickHandler += handler;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("OnPointClick ..");
            PointerClickHandler?.Invoke(this);
        }
    }
}
