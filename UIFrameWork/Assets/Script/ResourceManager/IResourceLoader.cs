using System;
using UnityEngine;

namespace CJR.ResourceManager
{
    public interface IResourceLoader<T>
    {
        T Get(string name, Action onComplete);
        void GetAsync(string name, Action<AsyncOperation> onComplete);
    }
}