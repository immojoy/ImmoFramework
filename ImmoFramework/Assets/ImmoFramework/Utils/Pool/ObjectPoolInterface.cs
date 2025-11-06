



namespace Immo.Framework.Utils.Pool
{
    /// <summary>
    /// Interface for a generic object pool.
    /// </summary>
    public interface IObjectPool<T> where T : class
    {
        /// <summary>
        /// Acquires an object from the pool.
        /// </summary>
        /// <returns>An object of type T.</returns>
        T Acquire();

        /// <summary>
        /// Releases an object back to the pool.
        /// </summary>
        /// <param name="obj">The object to release.</param>
        void Release(T obj);

        /// <summary>
        /// Clears all objects from the pool.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets the number of inactive objects in the pool.
        /// </summary>
        int CountInactive
        {
            get;
        }

        /// <summary>
        /// Gets the total number of objects managed by the pool.
        /// </summary>
        int CountAll
        {
            get;
        }

        /// <summary>
        /// Gets the number of active objects in the pool.
        /// </summary>
        int CountActive
        {
            get;
        }
    }
}