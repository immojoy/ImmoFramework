
namespace Immo.Framework.Core.Entity
{
    /// <summary>
    /// Interface for ImmoFramework entities.
    /// </summary>
    /// <remarks>
    /// An entity represents a distinct dynamic object with certain logic, such as a player, NPC, or item.<br/>
    /// If a game object is static and does not require specific logic, it is typically not considered an entity.
    /// </remarks>
    public interface IImmmoFrameworkEntity
    {
        /// <summary>
        /// Gets the unique identifier of the entity.
        /// </summary>
        int Id { get; }


        /// <summary>
        /// Gets the asset name of the entity.
        /// </summary>
        string AssetName { get; }


        /// <summary>
        /// Gets the underlying handle of the entity.
        /// </summary>
        object Handle { get; }


        /// <summary>
        /// Gets the entity group to which the entity belongs.
        /// </summary>
        IImmoFrameworkEntityGroup EntityGroup { get; }


        /// <summary>
        /// Called when the entity is initialized.
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <param name="assetName">Asset name</param>
        /// <param name="entityGroup">Entity group to which the entity belongs</param>
        /// <param name="isNewInstance">If the entity is a new instance</param>
        /// <param name="userData">Custom user data</param>
        void Initialize(int id, string assetName, IImmoFrameworkEntityGroup entityGroup, bool isNewInstance, object userData);


        /// <summary>
        /// Called when the entity is recycled.
        /// </summary>
        /// <remarks>
        /// This is typically used to reset the entity's state and prepare it for reuse.
        /// </remarks>
        void Recycle();


        /// <summary>
        /// Called when the asset associated with the entity is shown.
        /// </summary>
        /// <param name="userData">Custom user data</param>
        void Show(object userData);


        /// <summary>
        /// Called when the asset associated with the entity is hidden.
        /// </summary>
        /// <param name="userData">Custom user data</param>
        /// <param name="isShutdown">If this is called during EntityModule shutdown</param>
        void Hide(object userData, bool isShutdown);


        /// <summary>
        /// Called when a child entity is attached to this entity.
        /// </summary>
        /// <param name="childEntity">Child entity</param>
        /// <param name="userData">Custom user data</param>
        void AttachChild(IImmmoFrameworkEntity childEntity, object userData);


        /// <summary>
        /// Called when a child entity is detached from this entity.
        /// </summary>
        /// <param name="childEntity">Child entity</param>
        /// <param name="userData">Custom user data</param>
        void DetachChild(IImmmoFrameworkEntity childEntity, object userData);


        /// <summary>
        /// Called when this entity is attached to a parent entity.
        /// </summary>
        /// <param name="parentEntity">Parent entity</param>
        /// <param name="userData">Custom user data</param>
        void AttachToParent(IImmmoFrameworkEntity parentEntity, object userData);


        /// <summary>
        /// Called when this entity is detached from a parent entity.
        /// </summary>
        /// <param name="parentEntity">Parent entity</param>
        /// <param name="userData">Custom user data</param>
        void DetachFromParent(IImmmoFrameworkEntity parentEntity, object userData);


        /// <summary>
        /// Called every frame to update the entity.
        /// </summary>
        void Update();
    }
}