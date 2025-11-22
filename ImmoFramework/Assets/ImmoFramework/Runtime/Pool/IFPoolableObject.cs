
using System;


namespace ImmoFramework.Runtime
{
    public abstract class IFPoolableObject<T> where T : class
    {
        public T Target { get; set; }
        

        /// <summary>
        /// Gets or sets the time when the object was created.
        /// </summary>
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// Gets or sets the time when the object was last acquired from the pool.
        /// </summary>
        public DateTime LastAcquireTime { get; set; }


        public IFPoolableObject()
        {
            Target = null;
            CreateTime = default;
            LastAcquireTime = default;
        }


        public IFPoolableObject(T target)
        {
            Target = target;
            CreateTime = default;
            LastAcquireTime = default;
        }


        /// <summary>
        /// Called when the object is acquired from the pool.
        /// </summary>
        public abstract void OnAcquire();


        /// <summary>
        /// Called when the object is released back to the pool.
        /// </summary>
        public abstract void OnRelease();

        
        /// <summary>
        /// Called when the object is destroyed.
        /// </summary>
        public abstract void OnDestroy();
    }
}