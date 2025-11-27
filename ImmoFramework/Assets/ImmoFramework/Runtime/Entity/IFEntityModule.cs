
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


namespace ImmoFramework.Runtime
{
    public sealed partial class IFEntityModule : IFModule
    {
        private readonly Dictionary<string, IFEntityGroup> m_EntityGroups;
        private IFResourceModule m_ResourceModule;
        private int m_SerialId;


        public IFEntityModule()
        {
            m_EntityGroups = new Dictionary<string, IFEntityGroup>();
            m_ResourceModule = null;
            m_SerialId = 0;
        }


        public override void Update(float elapsedSeconds, float realElapsedSeconds)
        {

        }


        public override void Shutdown()
        {
            
        }


        /// <summary>
        /// Set the resource manager for the entity module, it's used during loading and unloading of entity assets
        /// </summary>
        /// <param name="resourceModule"> The resource manager to set </param>
        public void SetResourceManager(IFResourceModule resourceModule)
        {
            m_ResourceModule = resourceModule;
        }


        /// <summary>
        /// Show an entity
        /// </summary>
        /// <param name="entityId"> Entity ID </param>
        /// <param name="logicType"> Logic type of the entity </param>
        /// <param name="entityAssetName"> Name of the entity asset </param>
        /// <param name="entityGroupName"> Name of the entity group the entity belongs to </param>
        /// <param name="entityData"> Additional data for showing the entity </param>
        /// <remarks>
        /// This method handles showing an entity by either acquiring an existing instance or loading a new one if necessary.
        /// </remarks>
        public void ShowEntity(int entityId, Type logicType, string entityAssetName, string entityGroupName, object entityData)
        {
            IFEntityGroup entityGroup = m_EntityGroups[entityGroupName];

            // # The instance pool doesn't create and initialize a new instance on acquisition
            // # For a new instance, load the relevant asset (mostly a prefab) on demand
            IFEntityInstance instance = entityGroup.AcquireEntityInstance(entityAssetName);

            // # Data associated with entity and entity instance is not used by resource manager directly
            // # The resource manager passes onto the callback function which later on uses the data to initialize the entity
            EntityDataForShow entityDataForShow = null;

            if (instance.IsInstantiated == false)
            {
                entityDataForShow = EntityDataForShow.Create(++m_SerialId, entityId, logicType, entityGroup, entityData);
                m_ResourceModule.LoadAssetAsyncWithCallbacks<GameObject>(entityAssetName, LoadAssetSuccessCallback, EntityDataForLoad.Create(entityDataForShow, instance));
                return;
            }

            entityDataForShow = EntityDataForShow.Create(m_SerialId, entityId, logicType, entityGroup, entityData);
            InternalShowEntity(entityAssetName, false, EntityDataForLoad.Create(entityDataForShow, instance));
        }


        /// <summary>
        /// Hide an entity
        /// </summary>
        /// <param name="entity"> The entity to hide </param>
        /// <param name="entityData"> Additional data for hiding the entity </param>
        public void HideEntity(IFEntity entity, object entityData)
        {
            IFEntityGroup entityGroup = entity.EntityGroup;
            entityGroup.ReleaseEntityInstance(entity);
            entityGroup.RemoveEntity(entity);
            entity.Hide(entityData);
        }


        /// <summary>
        /// Create a new entity group and add it to the module
        /// </summary>
        /// <param name="groupName">Name of the entity group to add</param>
        public bool AddEntityGroup(string groupName)
        {
            m_EntityGroups.Add(groupName, new IFEntityGroup(groupName, m_ResourceModule));
            return true;
        }


        /// <summary>
        /// Check if an entity group exists in the module
        /// </summary>
        /// <param name="groupName">Name of the entity group to check</param>
        /// <returns><b>true</b> if the entity group exists, otherwise <b>false</b></returns>
        public bool HasEntityGroup(string groupName)
        {
            return m_EntityGroups.ContainsKey(groupName);
        }


        /// <summary>
        /// Get an entity group by its name
        /// </summary>
        /// <param name="groupName">Name of the entity group to get</param>
        /// <returns>The <b>IFEntityGroup</b> with the specified name, or <b>null</b> if not found</returns>
        public IFEntityGroup GetEntityGroup(string groupName)
        {
            IFEntityGroup entityGroup;
            if (m_EntityGroups.TryGetValue(groupName, out entityGroup))
            {
                return entityGroup;
            }
            return null;
        }


        /// <summary>
        /// Internal method for showing an entity in the scene
        /// </summary>
        /// <param name="entityAssetName">Name of the entity asset, usually a prefab name</param>
        /// <param name="loadData"> Entity data for load and show, see <see cref="EntityDataForLoad"/> </param>
        private void InternalShowEntity(string entityAssetName, bool isNewInstance, object loadData)
        {
            IFEntityInstance instance = (loadData as EntityDataForLoad).EntityInstance;
            EntityDataForShow entityDataForShow = (loadData as EntityDataForLoad).EntityDataForShow;

            IFEntity entity = instance.Instance.GetComponent<IFEntity>();
            if (entity == null)
            {
                entity = instance.Instance.AddComponent<IFEntity>();
            }

            entity.EntityAssetName = entityAssetName;
            entity.EntityGroup = entityDataForShow.EntityGroup;
            entity.EntityInstance = instance;
            if (isNewInstance)
            {
                instance.Instance.name = string.Format("{0} - {1}", entityAssetName, entityDataForShow.SerialId);
            }

            entity.Initialize(entityDataForShow.EntityData);
            entity.Show(entityDataForShow.EntityData);
        }


        /// <summary>
        /// Load asset success callback
        /// </summary>
        /// <param name="entityAssetName">Name of the asset to load</param>
        /// <param name="entityAsset">Instantiated instance of the asset</param>
        /// <param name="loadData">
        ///     <para>Entity data for load and show, see <see cref="EntityDataForLoad"/></para>
        ///     <para>
        ///         <i>
        ///             <b>EntityDataForShow</b> is not used here, this method only handles initialization, 
        ///             but it's included because it needs to be passed into <b>InternalShowEntity()</b> for showing the entity
        ///         </i>
        ///     </para> 
        /// </param>
        private void LoadAssetSuccessCallback(string entityAssetName, object entityAsset, object loadData)
        {
            EntityDataForLoad entityDataForLoad = (EntityDataForLoad)loadData;

            GameObject go = GameObject.Instantiate((GameObject)entityAsset);
            entityDataForLoad.EntityInstance.AssetName = entityAssetName;
            entityDataForLoad.EntityInstance.Instance = go;
            entityDataForLoad.EntityInstance.IsInstantiated = true;

            InternalShowEntity(entityAssetName, true, loadData);
        }
    }
}