using System;
using UnityEngine;

namespace CJR.Resource
{
    public interface IResourceLoader<T> where T : IPoolObject<T>
    {
        IPoolObject<T> Get(string path, Action onComplete);
        void GetAsync(string name, Action<AsyncOperation> onComplete);
        void Return(T instance);
        void Remove(T instance);
    }

    public interface IInstancePool<T> where T : IPoolObject<T>
    {
        T Get();
        void Return(T instance);
        void Remove(T instance);
    }

    public interface IPoolObject<T>
    {
        string Key { set; get; }
        Action<T> OnReturn { set; get; }
        void Return();
    }
}