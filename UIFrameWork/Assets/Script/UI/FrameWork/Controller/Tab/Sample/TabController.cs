using UnityEngine;

namespace CJR.UI
{
    public class TabsSample : MonoBehaviour
    {

        // 메세지로 했을 경우 좋은 방식이 뭐냐? 
            // 종속성을 없앨 수 잇다.
            // 비교적, 여러 UI들이 수신할 수 있다.
            // 단점
                // 미친놈 처럼 주화입마에 빠져서 복잡해 질 수 있다.
        
        // 요런 컴포넌트 방식으로 하면 좋은 점이 뭐냐?
            // 코드가 간단하다.
            // 복붙 복붙..
            // 요런 걸 극복하려면, 컴포넌트화 시켜주면 된다.


        public void Awake()
        {
            var tabSamples = GetComponents<TabSample>();
            if (tabSamples == null)
            {
                Debug.LogError("tab sample error");
                return;
            }

            foreach (var tabSample in tabSamples)
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