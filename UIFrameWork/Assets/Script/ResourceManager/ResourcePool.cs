using System.Collections.Generic;
using UnityEngine;

namespace CJR.Resource
{
    public class ResourcePool<T> : IResourcePool<T> where T : IPoolObject<T>
    {
        private readonly List<T> _resourceQueue = new();
        public T Get()
        {
            if (_resourceQueue.Count > 0)
            {
                var dequeue = _resourceQueue[0];
                _resourceQueue.Remove(dequeue);
                return dequeue;
            }

            return default;
        }

        public void Return(T instance)
        {
            if (_resourceQueue.Contains(instance))
            {
                Debug.Log($"contain instance");
                return;
            }

            _resourceQueue.Add(instance);
        }

        public void Remove(T instance)
        {
            if (_resourceQueue.Contains(instance) == false)
            {
                Debug.Log($"contain instance");
                return;
            }

            _resourceQueue.Remove(instance);
        }
    }
}
