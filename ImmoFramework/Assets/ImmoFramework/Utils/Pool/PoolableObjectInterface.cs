



namespace Immo.Framework.Utils.Pool
{
    /// <summary>
    /// Interface for a poolable object.
    /// </summary>
    public interface IPoolableObject
    {
        /// <summary>
        /// Called when the object is acquired from the pool.
        /// </summary>
        void OnAcquire();

        /// <summary>
        /// Called when the object is released back to the pool.
        /// </summary>
        void OnRelease();

        /// <summary>
        /// Called when the object is destroyed.
        /// </summary>
        void OnDestroy();
    }
}