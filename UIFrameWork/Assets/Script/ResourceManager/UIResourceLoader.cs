using System;
using System.Collections.Generic;
using CJR.UI;
using UnityEngine;

namespace CJR.Resource
{
    public class ResourceLoader<T> : IResourceLoader<T> where T : IPoolObject<T>
    {
        private readonly Dictionary<string, IResourcePool<T>> _elementObjectPool = new();
        private readonly Func<IResourcePool<T>> _resourcePoolResourcePoolFactory;
        private readonly Func<string, Action<T>, IPoolObject<T>> _poolObjectFactory;

        public ResourceLoader(Func<IResourcePool<T>> resourcePoolFactory, Func<string, Action<T>, IPoolObject<T>> poolObjectFactory)
        {
            _resourcePoolResourcePoolFactory = resourcePoolFactory;
            _poolObjectFactory = poolObjectFactory;
        }

        public IPoolObject<T> Get(string path, Action onComplete)
        {
            var poolElement = GetFromPool(path);
            if (poolElement != null)
            {
                return poolElement;
            }

            var iPoolObject = _poolObjectFactory?.Invoke(path, Return);
            if (iPoolObject == null)
            {
                Debug.LogWarning($"invalid Type resource _ {path} _ Type {typeof(UIDialog)}");
                return null;
            }

            return iPoolObject;
        }

        public void GetAsync(string name, Action<AsyncOperation> onComplete)
        {
            // ¹Ì±¸Çö
            var resource = Resources.LoadAsync(name);
            resource.completed += onComplete;
        }

        public void Return(T resource)
        {
            if (resource is IPoolObject<T> poolElement)
            {
                var key = poolElement.Key;
                if (string.IsNullOrEmpty(key))
                {
                    Debug.LogWarning($"not found key _ {key}");
                    return;
                }

                if (_elementObjectPool.TryGetValue(key, out var poolObj))
                {
                    poolObj?.Return(resource);
                }
                else
                {
                    var resourcePool = GetNewInstance();
                    if (resourcePool == null)
                    {
                        Debug.LogWarning($"return fail _ resource pool is null");
                        return;
                    }
                    resourcePool.Return(resource);

                    _elementObjectPool.Add(key, resourcePool);
                }
            }
        }

        private IResourcePool<T> GetNewInstance()
        {
            return _resourcePoolResourcePoolFactory?.Invoke();
        }

        private T GetFromPool(string path)
        {
            if (_elementObjectPool.TryGetValue(path, out var pool))
            {
                var poolObj = pool.Get();
                if (poolObj != null)
                {
                    return poolObj;
                }
            }

            return default;
        }
    }
}