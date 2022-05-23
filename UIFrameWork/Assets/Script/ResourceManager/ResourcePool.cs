using System.Collections.Generic;
using UnityEngine;

namespace CJR.Resource
{
    public class ResourcePool<T> : IResourcePool<T> where T : IPoolObject<T>
    {
        private readonly Queue<T> _resourceQueue = new();
        public T Get()
        {
            if (_resourceQueue.Count > 0)
                return _resourceQueue.Dequeue();

            return default;
        }

        public void Return(T resource)
        {
            if (_resourceQueue.Contains(resource))
            {
                Debug.Log($"contain resource");
                return;
            }

            _resourceQueue.Enqueue(resource);
        }
    }
}
