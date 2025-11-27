
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;


namespace ImmoFramework.Runtime
{
    public interface IIFEntity
    {
        /// <summary>
        /// Entity ID
        /// </summary>
        public int ID { get; }


        /// <summary>
        /// The asset name of the entity
        /// </summary>
        public string EntityAssetName { get; }


        /// <summary>
        /// The instance of the entity in the scene
        /// </summary>
        public object EntityInstance { get; }


        /// <summary>
        /// The group this entity belongs to
        /// </summary>
        public IFEntityGroup EntityGroup { get; }


        /// <summary>
        /// Initialize the entity
        /// </summary>
        public void Initialize(object data);


        /// <summary>
        /// Destroy the entity
        /// </summary>
        public void Destroy();


        /// <summary>
        /// Show the entity
        /// </summary>
        public void Show(object data);


        /// <summary>
        /// Hide the entity
        /// </summary>
        public void Hide(object data);


        /// <summary>
        /// Add a child entity
        /// </summary>
        public void AddChild();


        /// <summary>
        /// Remove a child entity
        /// </summary>
        public void RemoveChild();


        /// <summary>
        /// Attach the entity to its parent
        /// </summary>
        public void AttachToParent();



        /// <summary>
        /// Detach the entity from its parent
        /// </summary>
        public void DetachFromParent();


        /// <summary>
        /// Update the entity
        /// </summary>
        public void EntityUpdate(float elapsedTime, float realElapsedTime);
        
    }
}