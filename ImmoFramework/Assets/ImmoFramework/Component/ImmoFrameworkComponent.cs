using UnityEngine;


namespace Immo.Framework.Component
{
    public abstract class ImmoFrameworkComponent : MonoBehaviour
    {
        protected virtual void Awake()
        {
            ImmoFrameworkGameEntry.RegisterComponent(this);
        }
    }
}