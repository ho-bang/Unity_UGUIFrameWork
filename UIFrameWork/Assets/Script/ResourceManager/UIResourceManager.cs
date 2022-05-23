using System;
using CJR.UI;
using UnityEngine;

namespace CJR.Resource
{
    public class UIResourceManager : MonobehaviourSingleton<UIResourceManager>
    {
        private readonly IResourceLoader<UIDialog> _uiElementResourceLoader = new ResourceLoader<UIDialog>(
            resourcePoolFactory : () => new ResourcePool<UIDialog>(),
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

                var instance = Instantiate(gob);
                var element = instance.GetComponent<UIDialog>();
                if (element is IPoolObject<UIDialog> poolObj)
                {
                    poolObj.OnReturn = action;
                    poolObj.Key = path;
                    return poolObj;
                }

                return null;
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
    }
}