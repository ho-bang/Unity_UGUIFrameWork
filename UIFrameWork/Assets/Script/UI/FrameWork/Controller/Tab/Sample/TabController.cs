using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CJR.UI
{
    public class TabsSample : MonoBehaviour
    {
        // grid 처리가 알아서 필요하긴 하겠지?
        private List<TabSample> _tabs;

        public GridLayoutGroup _gridLayoutGroup;


        public void Awake()
        {
            _tabs = new List<TabSample>(GetComponents<TabSample>());
            
            foreach (var tabSample in _tabs)
            {
                tabSample.RegisterOnPointClick(OnClickTab);
            }

            // 요렇게 하지 말고, 그냥 메세지 방식으로 하면 또 문제 없을 것 같은데
        }

        public void OnClickTab(TabSample tabManager)
        {

        }
    }
}