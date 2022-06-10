using System;
using UnityEngine;

namespace CJR.Resource
{
    using UI;
    
    public class UIInstanceManager : MonobehaviourSingleton<UIInstanceManager>
    {
        // 나중에 이런 식으로도 사용이 가능하다.
        private readonly IObjectLoader<Texture> _uiTextureManager = new ObjectLoader<Texture>(null, null); 

        private readonly IObjectLoader<CJRUIBase> _uiInstanceLoader = new ObjectLoader<CJRUIBase>(
            resourcePoolFactory: () => new ObjectPool<CJRUIBase>(),
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
                var element = instance.GetComponent<CJRUIBase>();

                element.OnReturn = action;
                return element;
            });

        public CJRUIBase GetInstance(string name, Action onComplete)
        {
            return _uiInstanceLoader.Get(name, onComplete);
        }

        public void ReturnInstance(CJRUIBase dialog)
        {
            dialog.SetParent(Instance.gameObject);

            _uiInstanceLoader.Return(dialog.Key, dialog);
        }

        public void RemoveInstance(CJRUIBase dialog)
        {
            _uiInstanceLoader.Remove(dialog.Key, dialog);
        }
    }
}