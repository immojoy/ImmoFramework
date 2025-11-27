
using System;
using System.Collections.Generic;
using UnityEngine;


namespace ImmoFramework.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("ImmoFramework/Component/ImmoFramework Entity")]
    public sealed partial class IFEntityComponent : IFComponent
    {
        private IFEntityModule m_EntityModule;
        private readonly Dictionary<string, GameObject> m_EntityGroupInstance = new();

        [SerializeField]
        private Transform m_RootTransform = null;

        [SerializeField]
        private IFEntityGroupData[] m_EntityGroups = null;


        #region Unity Callbacks
        protected override void Awake()
        {
            base.Awake();

            m_EntityModule = IFModuleEntry.GetModule<IFEntityModule>();
            if (m_EntityModule == null)
            {
                Debug.LogError("Invalid entity module.");
                return;
            }
        }

        private void Start()
        {
            if (m_RootTransform == null)
            {
                m_RootTransform = new GameObject("Entity Instances").transform;
                m_RootTransform.SetParent(gameObject.transform);
                m_RootTransform.localScale = Vector3.one;
            }

            m_EntityModule.SetResourceManager(IFModuleEntry.GetModule<IFResourceModule>());

            for (int i = 0; i < m_EntityGroups.Length; i++)
            {
                if (!AddEntityGroup(m_EntityGroups[i].Name))
                {
                    Debug.LogWarning($"Entity group '{m_EntityGroups[i].Name}' already exists.");
                }
            }
        }
        #endregion


        /// <summary>
        /// Show an entity in the scene
        /// </summary>
        /// <param name="entityId"> Entity ID </param>
        /// <param name="logicType"> Logic type of the entity </param>
        /// <param name="entityAssetName"> Name of the entity asset </param>
        /// <param name="entityGroupName"> Name of the entity group the entity belongs to </param>
        /// <param name="entityData"> Additional data for showing the entity </param>
        public void ShowEntity(int entityId, Type logicType, string entityAssetName, string entityGroupName, object entityData)
        {
            m_EntityModule.ShowEntity(entityId, logicType, entityAssetName, entityGroupName, entityData);
        }


        /// <summary>
        /// Hide an entity
        /// </summary>
        /// <param name="entity"> The entity to hide </param>
        /// <param name="entityData"> Additional data for hiding the entity </param>
        public void HideEntity(IFEntity entity, object entityData)
        {
            m_EntityModule.HideEntity(entity, entityData);
        }


        /// <summary>
        /// Create a new entity group and add it to the module
        /// </summary>
        /// <param name="groupName">Name of the entity group to add</param>
        public bool AddEntityGroup(string groupName)
        {
            if (HasEntityGroup(groupName))
            {
                return false;
            }

            bool result =  m_EntityModule.AddEntityGroup(groupName);
            IFEntityGroup entityGroup = m_EntityModule.GetEntityGroup(groupName);
            if (entityGroup == null)
            {
                Debug.LogError($"Failed to get entity group '{groupName}' after adding it.");
                return false;
            }

            GameObject entityGroupInstance = new GameObject(string.Format("Entity Group - {0}", groupName));
            entityGroupInstance.transform.SetParent(m_RootTransform);
            entityGroupInstance.transform.localScale = Vector3.one;

            entityGroup.RootTransform = entityGroupInstance.transform;
            m_EntityGroupInstance.Add(groupName, entityGroupInstance);

            return result;
        }


        /// <summary>
        /// Check if an entity group exists in the module
        /// </summary>
        /// <param name="groupName">Name of the entity group to check</param>
        /// <returns><b>true</b> if the entity group exists, otherwise <b>false</b></returns>
        public bool HasEntityGroup(string groupName)
        {
            return m_EntityModule.HasEntityGroup(groupName);
        }


        /// <summary>
        /// Get an entity group by its name
        /// </summary>
        /// <param name="groupName">Name of the entity group to get</param>
        /// <returns>The <b>IFEntityGroup</b> with the specified name, or <b>null</b> if not found</returns>
        public IFEntityGroup GetEntityGroup(string groupName)
        {
            return m_EntityModule.GetEntityGroup(groupName);
        }
    }
}