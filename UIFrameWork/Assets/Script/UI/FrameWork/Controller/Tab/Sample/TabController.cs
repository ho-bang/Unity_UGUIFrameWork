using UnityEngine;

namespace CJR.UI
{
    public class TabsSample : MonoBehaviour
    {

        // �޼����� ���� ��� ���� ����� ����? 
            // ���Ӽ��� ���� �� �մ�.
            // ����, ���� UI���� ������ �� �ִ�.
            // ����
                // ��ģ�� ó�� ��ȭ�Ը��� ������ ������ �� �� �ִ�.
        
        // �䷱ ������Ʈ ������� �ϸ� ���� ���� ����?
            // �ڵ尡 �����ϴ�.
            // ���� ����..
            // �䷱ �� �غ��Ϸ���, ������Ʈȭ �����ָ� �ȴ�.


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

            // �䷸�� ���� ����, �׳� �޼��� ������� �ϸ� �� ���� ���� �� ������
        }

        public void OnClickTab(TabSample tabManager)
        {

        }
    }
}