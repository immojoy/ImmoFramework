
using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Pool;


namespace ImmoFramework.Runtime
{
    public class IFEntityInstance
    {
        public string AssetName { get; set; }
        public GameObject Instance { get; set; }
        public bool IsInstantiated { get; set; }


        public IFEntityInstance()
        {
            AssetName = string.Empty;
            Instance = null;
            IsInstantiated = false;
        }
    }


    public class IFEntityGroup
    {
        /// <summary>
        /// The pool of the instances.</br>
        /// An entity might have different variance of entities, so use different pools for each variance.</br>
        /// For example, a goblin entity group might contain different types of goblins like "GoblinWarrior", "GoblinArcher", etc.
        /// </summary>
        private readonly Dictionary<string, ObjectPool<IFEntityInstance>> m_InstancePool;


        /// <summary>
        /// The list of entities in this group.
        /// </summary>
        private readonly List<IIFEntity> m_Entities;


        /// <summary>
        /// Resource module used for releasing on entity instance destruction.
        /// </summary>
        private IFResourceModule m_ResourceModule;


        /// <summary>
        /// The name of this entity group.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Gets the number of entities in this group.
        /// </summary>
        public int EntityCount => m_Entities.Count;


        /// <summary>
        /// The root transform of this entity group.
        /// </summary>
        public Transform RootTransform { get; set; }


        public IFEntityGroup(string name)
        {
            Name = name;

            m_InstancePool = new Dictionary<string, ObjectPool<IFEntityInstance>>();
            m_Entities = new List<IIFEntity>();
            m_ResourceModule = null;
        }


        public IFEntityGroup(string name, IFResourceModule resourceModule)
        {
            Name = name;

            m_InstancePool = new Dictionary<string, ObjectPool<IFEntityInstance>>();
            m_Entities = new List<IIFEntity>();
            m_ResourceModule = resourceModule;
        }


        /// <summary>
        /// Updates all entities in this group.
        /// </summary>
        /// <param name="elapsedTime">Logic elapsed time since last update.</param>
        /// <param name="realElapsedTime">Real elapsed time since last update.</param>
        public void Update(float elapsedTime, float realElapsedTime)
        {

        }


        public IFEntityInstance AcquireEntityInstance(string name)
        {
            if (!m_InstancePool.TryGetValue(name, out ObjectPool<IFEntityInstance> pool))
            {
                // TODO: Add createFun, and other callbacks for the pool
                pool = new ObjectPool<IFEntityInstance>(
                    createFunc: () => { return new IFEntityInstance(); },
                    actionOnGet: OnEntityInstanceAcquire,
                    actionOnRelease: OnEntityInstanceRelease,
                    actionOnDestroy: OnEntityInstanceDestroy
                );
                m_InstancePool.Add(name, pool);
            }
            return pool.Get();
        }


        public void ReleaseEntityInstance(IFEntity entity)
        {
            if (!m_InstancePool.TryGetValue(entity.EntityAssetName, out ObjectPool<IFEntityInstance> pool))
            {
                throw new Exception($"Entity asset name '{entity.EntityAssetName}' does not exist in the instance pool.");
            }
            pool.Release(entity.EntityInstance as IFEntityInstance);
        }


        public void AddEntity(IFEntity entity)
        {
            m_Entities.Add(entity);
        }


        public void RemoveEntity(IFEntity entity)
        {
            m_Entities.Remove(entity);
        }


        private void OnEntityInstanceAcquire(IFEntityInstance instance)
        {

        }

        private void OnEntityInstanceRelease(IFEntityInstance instance)
        {
            
        }
        

        private void OnEntityInstanceDestroy(IFEntityInstance instance)
        {
            GameObject.Destroy(instance.Instance);

            // This doesn't release the asset immediately, it decreases the reference count and releases when count reaches zero.
            m_ResourceModule.ReleaseAsset(instance.AssetName);
        }
    }
}