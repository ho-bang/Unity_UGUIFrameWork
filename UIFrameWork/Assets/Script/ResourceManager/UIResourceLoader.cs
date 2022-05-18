using System;
using Assets.UI.FrameWork;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Script.ResourceManager
{
    public class UIResourceLoader<T> : IResourceLoader<T> where T : UIElement
    {
        public T Get(string name, Action onComplete)
        {
            var resource = Resources.Load(name);
            if (resource == null)
            {
                Debug.Log($"resource is null _ {name}");
                return null;
            }

            var gob = resource as GameObject;
            if (gob == null)
            {
                Debug.Log($"not gameobject _ {name}");
                return null;
            }

            var instance = Object.Instantiate(gob);
            var element = instance.GetComponent<UIElement>();
            if (element is T e) return e;

            Debug.Log($"invalid type resource _ {resource} _ type {typeof(T)}");
            return null;
        }

        public void GetAsync(string name, Action<AsyncOperation> onComplete)
        {
            var resource = Resources.LoadAsync(name);
            resource.completed += onComplete;
        }
    }
}