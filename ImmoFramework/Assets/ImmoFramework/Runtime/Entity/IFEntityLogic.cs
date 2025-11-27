
using System;

using UnityEngine;


namespace ImmoFramework.Runtime
{
    public abstract class IFEntityLogic : MonoBehaviour
    {
        private bool m_Visibility = false;
        private IFEntity m_Entity = null;
        private Transform m_OriginalTransform = null;
        private Transform m_CachedTransform = null;


        public IFEntity Entity => m_Entity;
        public Transform OriginalTransform => m_OriginalTransform;
        public Transform CachedTransform => m_CachedTransform;
        public bool Visibility
        {
            get => m_Visibility;
            set
            {
                if (m_Visibility != value)
                {
                    m_Visibility = value;
                    gameObject.SetActive(m_Visibility);
                }
            }
        }


        public virtual void Initialize(object data)
        {
            m_Entity = GetComponent<IFEntity>();
            m_OriginalTransform = transform;
            m_CachedTransform = transform;
        }


        public virtual void Destroy()
        {

        }


        public virtual void Show(object data)
        {
            Visibility = true;
        }


        public virtual void Hide(object data)
        {
            Visibility = false;
        }


        public virtual void AddChild(IFEntityLogic child, Transform parentTransform, object data)
        {

        }


        public virtual void RemoveChild(IFEntityLogic child, object data)
        {

        }


        public virtual void AttachToParent(IFEntityLogic parent, Transform parentTransform, object data)
        {

        }


        public virtual void DetachFromParent(IFEntityLogic parent, object data)
        {

        }
        

        public virtual void LogicUpdate(float elapsedTime, float realElapsedTime)
        {
            
        }
    }
}