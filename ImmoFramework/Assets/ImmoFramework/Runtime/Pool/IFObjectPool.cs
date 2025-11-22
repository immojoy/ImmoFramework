
using System;
using System.Collections.Generic;

using UnityEngine;


namespace ImmoFramework.Runtime
{
    public class PoolConfig
    {
        public int InitialSize = 10;
        public int MaxSize = 100;
        public float AutoReleaseInterval = 60f;
        public bool CollectionCheck = true;
    }


    public class IFObjectPool<T> where T : class
    {
        private readonly Stack<IFPoolableObject<T>> m_Stack;
        private readonly Func<T> m_OnCreate;
        private readonly Action<T> m_OnAcquire;
        private readonly Action<T> m_OnRelease;
        private readonly Action<T> m_OnDestroy;

        private readonly int m_MaxSize;
        private readonly bool m_CollectionCheck;

        private readonly Dictionary<T, IFPoolableObject<T>> m_InPoolObjects;
        private readonly float m_AutoReleaseInterval;
        private readonly DateTime m_LastAutoReleaseTime;


        public int UsedCount => m_InPoolObjects.Count;
        public int FreeCount => m_Stack.Count;
        public int TotalCount => UsedCount + FreeCount;


        public IFObjectPool(
            Func<T> onCreate,
            Action<T> onAcquire = null,
            Action<T> onRelease = null,
            Action<T> onDestroy = null,
            PoolConfig config = null)
        {
            config ??= new PoolConfig();

            m_OnCreate = onCreate ?? throw new Exception("OnCreate action must be provided.");
            m_OnAcquire = onAcquire;
            m_OnRelease = onRelease;
            m_OnDestroy = onDestroy;

            m_MaxSize = config.MaxSize;
            m_AutoReleaseInterval = config.AutoReleaseInterval;
            m_CollectionCheck = config.CollectionCheck;

            m_Stack = new Stack<IFPoolableObject<T>>(config.InitialSize);
            m_InPoolObjects = new Dictionary<T, IFPoolableObject<T>>();

            m_LastAutoReleaseTime = default;
        }


        private void Prewarm(int count)
        {
            for (int i = 0; i < count; i++)
            {
                // T obj = Activator.CreateInstance<T>();
                // m_OnCreate?.Invoke(obj);
                // IFPoolableObject<T> poolableObject = new ConcretePoolableObject<T>(obj);
                // m_Stack.Push(poolableObject);
            }
        }


        private IFPoolableObject<T> CreatePoolableObject()
        {
            // T obj = Activator.CreateInstance<T>();
            // m_OnCreate?.Invoke(obj);
            // return new ConcretePoolableObject<T>(obj);
            return null;
        }
    }
}