using System;
using System.Collections.Generic;
using CJR.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CJR.Resource
{
    public class ObjectLoader<T> : IObjectLoader<T> where T : Object
    {
        private readonly Dictionary<string, IObjectPool<T>> _objectPoolDic = new();
        private readonly Func<IObjectPool<T>> _resourcePoolResourcePoolFactory;
        private readonly Func<string, Action<string, T>, T> _poolObjectFactory;
        private IObjectLoader<T> _objectLoaderImplementation;

        public ObjectLoader(Func<IObjectPool<T>> resourcePoolFactory, Func<string, Action<string, T>, T> poolObjectFactory)
        {
            _resourcePoolResourcePoolFactory = resourcePoolFactory;
            _poolObjectFactory = poolObjectFactory;
        }
       
        public T Get(string key, Action onComplete)
        {
            var poolObject = GetFromPool(key);
            if (poolObject is not null)
            {
                return poolObject;
            }

            var iPoolObject = _poolObjectFactory?.Invoke(key, Return);
            if (iPoolObject is null)
            {
                Debug.LogWarning($"invalid Type instance _ {key} _ Type {typeof(UIDialog)}");
                return null;
            }

            return iPoolObject;
        }

        public void GetAsync(string key, Action<AsyncOperation> onComplete)
        {
            // ¹Ì±¸Çö
        }

        public void Return(string key, T obj)
        {
            if (_objectPoolDic.TryGetValue(key, out var instancePool))
            {
                instancePool?.Return(obj);
            }
            else
            {
                instancePool = _resourcePoolResourcePoolFactory?.Invoke();
                if (instancePool is null)
                {
                    Debug.LogWarning($"return fail _ instance pool is null");
                    return;
                }

                instancePool.Return(obj);
                _objectPoolDic.Add(key, instancePool);
            }
        }

        public void Remove(string key, T obj)
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogWarning($"not found key _ {key}");
                return;
            }

            if (_objectPoolDic.TryGetValue(key, out var poolObj))
            {
                poolObj?.Remove(obj);
            }
        }

        private T GetFromPool(string key)
        {
            if (!_objectPoolDic.TryGetValue(key, out var pool))
            {
                return default;
            }

            var poolObj = pool.Get();
            if (poolObj == null)
                return default;

            return poolObj;
        }
    }
}