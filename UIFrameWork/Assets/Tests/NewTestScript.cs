using System.IO;
using System.Net;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace UniRxTest
{
    using UniRx;
    public class NewTestScript
    {
        // A Test behaves as an ordinary method
        [Test]
        public void IntReactiveProperty()
        {
            var reactive = new IntReactiveProperty(10);
            reactive.Where(v => v == 15).Subscribe(v => Debug.Log($"intReactive _ {v}"));
            reactive.Where(v => v == 13).Subscribe(v => Debug.Log($"intReactive _ {v}"));
        }

        [Test]
        public void ReactiveCollection()
        {
            var collection = new ReactiveCollection<string>();
            collection.ObserveAdd().Subscribe(x => {Debug.Log($"add _ {x}");});
            collection.ObserveRemove().Subscribe(x => { Debug.Log($"remove _ {x}"); });

            collection.Add("t1");
            collection.Add("t2");
            collection.Add("t3");
            collection.Add("t4");
            collection.Remove("t4");
        }

        [Test]
        public void ButtonOnClickASObservable()
        {
            var go = new GameObject();
            var button = go.AddComponent<Button>();
            button.onClick.AsObservable().Subscribe(_ => Debug.Log("Click Button")).AddTo(go);
            button.onClick.Invoke();

            Object.DestroyImmediate(go);
        }
        
        [Test]
        public void ObservableCreate()
        {
            // �̰Ŵ� ����� �ؾ��ϳ�.. Reactive�� ��ü�� ������ ����� �� �� �ִ�.
            Observable.Create<int>(ob =>
            {
                Debug.Log($"{ob}");

                for (var i = 0; i < 100; i++)
                {
                    ob.OnNext(i);
                }

                Debug.Log("Finished");

                return Disposable.Create(() =>
                {
                    Debug.Log("Dispose");
                });
            }).Subscribe(x => Debug.Log(x));
        }

        [Test]
        public void ObservableStart()
        {
            // �ٸ� �����忡�� ����
            // Start�� ���󰡺���, �����ٷ����� ThreadPool�� ����ϴ� �� �� �� �ִ�.
            Observable.Start(() =>
            {
                var req = (HttpWebRequest)WebRequest.Create("https://google.com");
                var res = (HttpWebResponse)req.GetResponse();
                using (var reader = new StreamReader(res.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }).ObserveOnMainThread()  // �̰� �ٸ� �����忡�� ������ �� �ʼ������� �ؾ� �Ѵٴµ�, �����ٷ��� ���� IObservable�� �����ٷ��� AsyncConversions�� �ٲٴµ� �̰� �ٽ� ���� ������� ��������
                .Subscribe(x => Debug.Log($"{x}")); // �� ��
        }
    }
}
