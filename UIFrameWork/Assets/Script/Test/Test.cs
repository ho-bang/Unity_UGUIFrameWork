using System;
using UnityEngine;
using UniRx;

public class UniRxTest
{
    private Subject<int> test = new Subject<int>();
    public IObservable<int> OnTimeChanged => test;


    void Start()
    {
        test.OnNext(1);
    }
}

public class Test : MonoBehaviour
{
    UniRxTest text = new UniRxTest();

    void Start()
    {
        text.OnTimeChanged.Where(t => t == 2).Subscribe(t => Debug.Log(t));
        text.OnTimeChanged.Subscribe(t => Debug.Log(t));
        text.OnTimeChanged.Subscribe(t => Debug.Log(t));

    }

    void Update()
    {
        
    }
}
