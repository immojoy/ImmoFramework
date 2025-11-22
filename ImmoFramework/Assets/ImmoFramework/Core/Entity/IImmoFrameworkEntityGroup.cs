using System.Collections.Generic;


namespace Immo.Framework.Core.Entity
{
    /// <summary>
    /// Interface for ImmoFramework entity groups.
    /// </summary>
    /// <remarks>
    /// An entity group is a collection of entities that share common characteristics.<br/>
    /// For example, all enemies in a game could belong to an "Enemy" entity group.
    /// </remarks>
    public interface IImmoFrameworkEntityGroup
    {

        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        string Name { get; }


        /// <summary>
        /// Gets the number of entities in the group.
        /// </summary>
        int EntityCount { get; }


        /// <summary>
        /// Gets or sets the auto release interval for the entity group.
        /// </summary>
        float AutoReleaseInterval { get; set; }


        /// <summary>
        /// Gets or sets the capacity of the object pool of the entity group.
        /// </summary>
        int Capacity { get; set; }


        /// <summary>
        /// Gets or sets the expire time for entities in the group.
        /// </summary>
        float ExpireTime { get; set; }


        /// <summary>
        /// Gets or sets the priority of the entity group.
        /// </summary>
        int Priority { get; set; }


        /// <summary>
        /// Checks if the entity group contains an entity with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the entity to check for.</param>
        /// <returns><b>True</b> if the entity group contains the entity; otherwise, <b>false</b>.</returns>
        bool HasEntity(int id);


        /// <summary>
        /// Gets the entity with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the entity to get.</param>
        /// <returns>The entity with the specified ID.</returns>
        IImmmoFrameworkEntity GetEntityById(int id);



        // Note: There two types of entity group, on is single-entity, the other is multi-entity.
        // For single-entity group, call GetEntityByAsset to get the only entity.
        // For multi-entity group, call GetEntitiesByAsset to get all entities associated with the asset.
        // Use EntityCount to determine how many entities are in the group.

        /// <summary>
        /// Gets the entity with the specified asset name.
        /// </summary>
        /// <param name="assetName">The asset name of the entity to get.</param>
        /// <returns>The entity with the specified asset name.</returns>
        IImmmoFrameworkEntity GetEntityByAsset(string assetName);


        /// <summary>
        /// Gets all entities associated with the specified asset.
        /// </summary>
        /// <param name="assetName">The asset name to filter entities by.</param>
        /// <returns>An array of entities associated with the specified asset.</returns>
        IImmmoFrameworkEntity[] GetEntitiesByAsset(string assetName);


        /// <summary>
        /// Gets all entities associated with the specified asset and adds them to the provided list.
        /// </summary>
        /// <param name="assetName">The asset name to filter entities by.</param>
        /// <param name="results">The list to which the entities will be added.</param>
        void GetEntitiesByAsset(string assetName, List<IImmmoFrameworkEntity> results);


        /// <summary>
        /// Gets all entities in the entity group.
        /// </summary>
        /// <returns>An array of all entities in the entity group.</returns>
        IImmmoFrameworkEntity[] GetAllEntities();


        /// <summary>
        /// Gets all entities in the entity group and adds them to the provided list.
        /// </summary>
        /// <param name="results">The list to which the entities will be added.</param
        void GetAllEntities(List<IImmmoFrameworkEntity> results);



        
    }
}