using System;
using System.Collections.Generic;


namespace Immo.Framework.Utils.Pool
{
    public class ObjectPool<T> : IObjectPool<T> where T : class
    {
        private readonly Stack<T> m_Stack;
        private readonly Func<T> m_CreateFunc;
        private readonly Action<T> m_OnAcquire;
        private readonly Action<T> m_OnRelease;
        private readonly Action<T> m_OnDestroy;
        private readonly int m_MaxSize;
        private readonly bool m_CollectionCheck;


        private int m_CountAll;
        private readonly object m_Lock = new object();

        public int CountInactive
        {
            get
            {
                lock (m_Lock)
                {
                    return m_Stack.Count;
                }
            }
        }

        public int CountAll
        {
            get
            {
                lock (m_Lock)
                {
                    return m_CountAll;
                }
            }
        }

        public int CountActive
        {
            get
            {
                lock (m_Lock)
                {
                    return m_CountAll - m_Stack.Count;
                }
            }
        }


        public ObjectPool(
            Func<T> createFunc,
            Action<T> onAcquire = null,
            Action<T> onRelease = null,
            Action<T> onDestroy = null,
            bool collectionCheck = true,
            int defaultCapacity = 10,
            int maxSize = 10000)
        {
            m_Stack = new Stack<T>(defaultCapacity);
            m_CreateFunc = createFunc;
            m_OnAcquire = onAcquire;
            m_OnRelease = onRelease;
            m_OnDestroy = onDestroy;
            m_CollectionCheck = collectionCheck;
            m_MaxSize = maxSize;

            Prewarm(defaultCapacity);
        }

        public T Acquire()
        {
            lock (m_Lock)
            {
                T item;
                if (m_Stack.Count == 0)
                {
                    item = CreateNewItem();
                }
                else
                {
                    item = m_Stack.Pop();
                }

                m_OnAcquire?.Invoke(item);

                if (item is IPoolableObject poolable)
                {
                    poolable.OnAcquire();
                }

                return item;
            }
        }

        public void Release(T obj)
        {
            lock (m_Lock)
            {
                if (m_CollectionCheck && m_Stack.Contains(obj))
                {
                    throw new Exception("Trying to release an object that is already in the pool.");
                }

                m_OnRelease?.Invoke(obj);

                if (obj is IPoolableObject poolable)
                {
                    poolable.OnRelease();
                }

                if (m_Stack.Count < m_MaxSize)
                {
                    m_Stack.Push(obj);
                }
                else
                {
                    m_OnDestroy?.Invoke(obj);

                    if (obj is IPoolableObject poolableDestroy)
                    {
                        poolableDestroy.OnDestroy();
                    }

                    m_CountAll--;
                }

                m_Stack.Push(obj);
            }
        }

        public void Clear()
        {
            lock (m_Lock)
            {
                while (m_Stack.Count > 0)
                {
                    T item = m_Stack.Pop();
                    m_OnDestroy?.Invoke(item);

                    if (item is IPoolableObject poolable)
                    {
                        poolable.OnDestroy();
                    }

                }
                m_CountAll = 0;
            }
        }

        public void Dispose()
        {
            Clear();
        }
        


        private void Prewarm(int count)
        {
            for (int i = 0; i < count; i++)
            {
                T item = CreateNewItem();
                if (item != null)
                {
                    m_Stack.Push(item);
                }
            }
        }

        private T CreateNewItem()
        {
            if (m_CountAll >= m_MaxSize)
            {
                return null;
            }

            try
            {
                T item = m_CreateFunc();
                m_CountAll++;
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create new item for the pool.", ex);
            }
        }
    }   
}