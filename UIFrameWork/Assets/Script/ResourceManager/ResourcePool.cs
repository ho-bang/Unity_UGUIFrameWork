using System.Collections.Generic;
using UnityEngine;

namespace CJR.Resource
{
    public class InstancePool<T> : IInstancePool<T> where T : IPoolObject<T>
    {
        private readonly List<IPoolObject<T>> _instanceQueue = new();
        public IPoolObject<T> Get()
        {
            if (_instanceQueue.Count > 0)
            {
                var dequeue = _instanceQueue[0];
                _instanceQueue.Remove(dequeue);
                return dequeue;
            }

            return default;
        }

        public void Return(IPoolObject<T> instance)
        {
            if (_instanceQueue.Contains(instance))
            {
                Debug.Log($"contain instance");
                return;
            }

            _instanceQueue.Add(instance);
        }

        public void Remove(IPoolObject<T> instance)
        {
            if (_instanceQueue.Contains(instance) == false)
            {
                Debug.Log($"contain instance");
                return;
            }

            _instanceQueue.Remove(instance);
        }
    }
}
