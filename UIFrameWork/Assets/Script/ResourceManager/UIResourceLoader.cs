using System;
using System.Collections.Generic;
using CJR.UI;
using UnityEngine;

namespace CJR.Resource
{
    public class ResourceLoader<T> : IResourceLoader<T> where T : IPoolObject<T>
    {
        private readonly Dictionary<string, IInstancePool<T>> _elementObjectPool = new();
        private readonly Func<IInstancePool<T>> _resourcePoolResourcePoolFactory;
        private readonly Func<string, Action<T>, IPoolObject<T>> _poolObjectFactory;

        public ResourceLoader(Func<IInstancePool<T>> resourcePoolFactory, Func<string, Action<T>, IPoolObject<T>> poolObjectFactory)
        {
            _resourcePoolResourcePoolFactory = resourcePoolFactory;
            _poolObjectFactory = poolObjectFactory;
        }

        public IPoolObject<T> Get(string path, Action onComplete)
        {
            var poolElement = GetFromPool(path);
            if (poolElement is not null)
            {
                return poolElement;
            }

            var iPoolObject = _poolObjectFactory?.Invoke(path, Return);
            if (iPoolObject is null)
            {
                Debug.LogWarning($"invalid Type instance _ {path} _ Type {typeof(UIDialog)}");
                return null;
            }

            return iPoolObject;
        }

        public void GetAsync(string name, Action<AsyncOperation> onComplete)
        {
            // 미구현
            var resource = Resources.LoadAsync(name);
            resource.completed += onComplete;
        }

        public void Return(T instance)
        {
            if (instance is IPoolObject<T> poolElement)
            {
                var key = poolElement.Key;
                if (string.IsNullOrEmpty(key))
                {
                    Debug.LogWarning($"not found key _ {key}");
                    return;
                }

                if (_elementObjectPool.TryGetValue(key, out var poolObj))
                {
                    poolObj?.Return(instance);
                }
                else
                {
                    var resourcePool = GetNewResourcePool();
                    if (resourcePool is null)
                    {
                        Debug.LogWarning($"return fail _ instance pool is null");
                        return;
                    }
                    resourcePool.Return(instance);

                    _elementObjectPool.Add(key, resourcePool);
                }
            }
        }

        // Fake Null을 대비해서 리소스 풀에서 제거하도록 처리한다. 실수를 방지하기 위함
        public void Remove(T instance)
        {
            if (instance is IPoolObject<T> poolElement)
            {
                var key = poolElement.Key;
                if (string.IsNullOrEmpty(key))
                {
                    Debug.LogWarning($"not found key _ {key}");
                    return;
                }

                if (_elementObjectPool.TryGetValue(key, out var poolObj))
                {
                    poolObj?.Remove(instance);
                }
            }
        }

        private IInstancePool<T> GetNewResourcePool()
        {
            return _resourcePoolResourcePoolFactory?.Invoke();
        }

        private T GetFromPool(string path)
        {
            if (!_elementObjectPool.TryGetValue(path, out var pool))
            {
                return default;
            }

            var poolObj = pool.Get();
            return poolObj ?? default;
        }
    }
}