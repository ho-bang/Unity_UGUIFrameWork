using System;
using CJR.UI;
using UnityEngine;

namespace CJR.ResourceManager
{
    public class UIResourceManager : MonobehaviourSingleton<UIResourceManager>
    {
        private readonly IResourceLoader<UIElement> _uiElementResourceLoader = new ResourceLoader<UIElement>(
            resourcePoolFactory: () => new ResourcePool<UIElement>(),
            poolObjectFactory : (path, action) =>
            {
                var resource = Resources.Load(path);
                if (resource == null)
                {
                    Debug.LogWarning($"resource is null _ {path}");
                    return default;
                }

                var gob = resource as GameObject;
                if (gob == null)
                {
                    Debug.LogWarning($"not gameObject _ {path}");
                    return default;
                }

                var instance = UnityEngine.Object.Instantiate(gob);
                var element = instance.GetComponent<UIElement>();
                if (element is IPoolObject<UIElement> poolObj)
                {
                    poolObj.OnReturn = action;
                    poolObj.Key = path;
                    return poolObj;
                }

                return null;
            });

        public UIElement GetUIElementInstance(string name, Action onComplete)
        {
            return _uiElementResourceLoader.Get(name, onComplete) as UIElement;
        }
    }
}