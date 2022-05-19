using System;
using System.Collections;
using CJR.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CJR.ResourceManager
{
    public interface IResourcePool<T>
    {
        Queue ResourceQueue { get; }
        T Get();
        void Return(T resource);
    }

    public class UIResourcePool
    {
        public void Return()
        {

        }
    }

    public class UIResourceLoader<T> : IResourceLoader<T> where T : UIElement
    {
        public T Get(string name, Action onComplete)
        {
            var resource = Resources.Load(name);
            if (resource == null)
            {
                Debug.LogWarning($"resource is null _ {name}");
                return null;
            }

            var gob = resource as GameObject;
            if (gob == null)
            {
                Debug.LogWarning($"not gameobject _ {name}");
                return null;
            }

            var instance = Object.Instantiate(gob);
            var element = instance.GetComponent<UIElement>();
            if (element is T e) return e;

            Debug.LogWarning($"invalid type resource _ {resource} _ type {typeof(T)}");
            return null;
        }

        public void GetAsync(string name, Action<AsyncOperation> onComplete)
        {
            var resource = Resources.LoadAsync(name);
            resource.completed += onComplete;
        }
    }
}