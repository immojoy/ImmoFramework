using UnityEngine;


namespace Immo.Framework.Component
{
    public abstract class ImmoFrameworkComponent : MonoBehaviour
    {
        protected virtual void Awake()
        {
            ImmoFrameworkComponentEntry.RegisterComponent(this);
        }
    }
}