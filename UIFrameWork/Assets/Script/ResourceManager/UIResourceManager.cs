using System;
using CJR.UI;
using UnityEngine;

namespace CJR.Resource
{
    public class UIResourceManager : MonobehaviourSingleton<UIResourceManager>
    {
        private readonly IResourceLoader<UIDialog> _uiElementResourceLoader = new ResourceLoader<UIDialog> (
            resourcePoolFactory : () => new ResourcePool<UIDialog>(),
            poolObjectFactory : (path, action) =>
            {
                var resource = Resources.Load(path);
                if (resource is null)
                {
                    Debug.LogWarning($"resource is null _ {path}");
                    return null;
                }

                if (resource is not GameObject gob)
                {
                    Debug.LogWarning($"not gameObject _ {path}");
                    return null;
                }

                var instance = Instantiate(gob);
                var element = instance.GetComponent<UIDialog>();
                if (element is not IPoolObject<UIDialog> poolObj)
                {
                    return null;
                }

                poolObj.OnReturn = action;
                poolObj.Key = path;
                return poolObj;
            });

        public UIDialog GetUIElementInstance(string name, Action onComplete)
        {
            return _uiElementResourceLoader.Get(name, onComplete) as UIDialog;
        }

        public void ReturnInstance(UIDialog dialog)
        {
            dialog.SetParent(Instance.gameObject);

            _uiElementResourceLoader.Return(dialog);
        }

        public void RemoveInstance(UIDialog dialog)
        {
            _uiElementResourceLoader.Remove(dialog);
        }
    }
}