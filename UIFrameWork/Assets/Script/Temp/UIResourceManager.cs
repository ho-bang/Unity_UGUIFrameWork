using System;
using UnityEngine;

namespace CJR.Resource
{
    using UI;
    
    public class UIResourceManager : MonobehaviourSingleton<UIResourceManager>
    {
        private readonly IObjectLoader<UIDialog> _uiDialogObjectLoader = new ObjectLoader<UIDialog>(
            resourcePoolFactory: () => new ObjectPool<UIDialog>(),
            poolObjectFactory: (key, action) =>
            {
                var resource = Resources.Load(key);
                if (resource is null)
                {
                    Debug.LogWarning($"resource is null _ {key}");
                    return null;
                }

                if (resource is not GameObject gob)
                {
                    Debug.LogWarning($"not gameObject _ {key}");
                    return null;
                }

                var instance = Instantiate(gob);
                var element = instance.GetComponent<UIDialog>();

                element.OnReturn = action;
                return element;
            });

        public UIDialog GetDialogInstance(string name, Action onComplete)
        {
            return _uiDialogObjectLoader.Get(name, onComplete);
        }

        public void ReturnInstance(UIDialog dialog)
        {
            dialog.SetParent(Instance.gameObject);

            _uiDialogObjectLoader.Return(dialog.Key, dialog);
        }

        public void RemoveInstance(UIDialog dialog)
        {
            _uiDialogObjectLoader.Remove(dialog.Key, dialog);
        }
    }
}