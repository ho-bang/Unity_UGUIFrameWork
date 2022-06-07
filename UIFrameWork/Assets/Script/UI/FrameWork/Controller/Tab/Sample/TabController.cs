using UnityEngine;

namespace CJR.UI
{
    public class TabsSample : MonoBehaviour
    {
        private TabSample _tabSample; 

        public void Awake()
        {
            _tabSample = GetComponent<TabSample>();
            if (_tabSample == null)
            {
                Debug.LogError("tab sample error");
                return;
            }

            _tabSample.RegisterOnPointClick(OnClickTab);
        }

        public void OnClickTab(TabSample tabManager)
        {

        }
    }
}