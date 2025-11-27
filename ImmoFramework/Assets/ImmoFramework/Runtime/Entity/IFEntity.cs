
using System;
using TreeEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;


namespace ImmoFramework.Runtime
{
    public sealed class IFEntity : MonoBehaviour, IIFEntity
    {
        private int m_Id;
        private string m_EntityAssetName;
        private IFEntityGroup m_EntityGroup;
        private IFEntityLogic m_EntityLogic;
        private object m_EntityInstance;


        public int ID
        {
            get
            {
                return m_Id;
            }
            set
            {
                m_Id = value;
            }
        }

        public string EntityAssetName
        {
            get
            {
                return m_EntityAssetName;
            }
            set
            {
                m_EntityAssetName = value;
            }
        }

        public object EntityInstance
        {
            get
            {
                return m_EntityInstance;
            }
            set
            {
                m_EntityInstance = value;
            }
        }

        public IFEntityGroup EntityGroup
        {
            get
            {
                return m_EntityGroup;
            }
            set
            {
                m_EntityGroup = value;
            }
        }

        public IFEntityLogic Logic
        {
            get
            {
                return m_EntityLogic;
            }
            set
            {
                m_EntityLogic = value;
            }
        }


        public IFEntity()
        {
            
        }


        public void Initialize(object data)
        {
            // TODO: Initialize entity logic by its type and initialize it
            // If there is already an existing logic, consider reusing or replacing it
            // If it's the same type as the old one, reuse it, otherwise replace it
            IFEntityModule.EntityDataForShow entityDataForShow = (IFEntityModule.EntityDataForShow)data;
            Type logicType = entityDataForShow.LogicType;
            if (logicType == null)
            {
                Debug.LogError("Logic type is null.");
                return;
            }

            if (m_EntityLogic != null)
            {
                if (m_EntityLogic.GetType() == logicType)
                {
                    m_EntityLogic.enabled = true;
                    return;
                }

                Destroy(m_EntityLogic);
                m_EntityLogic = null;
            }

            m_EntityLogic = (IFEntityLogic)gameObject.AddComponent(logicType);
            m_EntityLogic.Initialize(data);

            transform.SetParent(m_EntityGroup.RootTransform);
        }

        public void Destroy()
        {
            // Destruction logic here
        }

        public void Show(object data)
        {
            // TODO: Call entity logic's Show method if needed
            m_EntityLogic.Show(data);
        }

        public void Hide(object data)
        {
            m_EntityLogic.Hide(data);
        }

        public void AddChild()
        {
            // Add child logic here
        }

        public void RemoveChild()
        {
            // Remove child logic here
        }

        public void AttachToParent()
        {
            // Attach to parent logic here
        }

        public void DetachFromParent()
        {
            // Detach from parent logic here
        }

        public void EntityUpdate(float elapsedTime, float realElapsedTime)
        {
            // Update logic here
        }
    }
}