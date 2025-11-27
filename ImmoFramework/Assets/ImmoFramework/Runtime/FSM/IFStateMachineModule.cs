
using System;
using System.Collections.Generic;


namespace ImmoFramework.Runtime
{
    public sealed class IFStateMachineModule : IFModule
    {
        private readonly Dictionary<string, IFStateMachineBase> m_StateMachines;


        /// <summary>
        /// Gets the number of state machines.
        /// </summary>
        public int Count => m_StateMachines.Count;


        public IFStateMachineModule()
        {
            m_StateMachines = new Dictionary<string, IFStateMachineBase>();
        }


        public override void Update(float elapsedSeconds, float realElapsedSeconds)
        {
            foreach (var sm in m_StateMachines.Values)
            {
                if (sm is IFStateMachineBase stateMachine)
                {
                    stateMachine.Update(elapsedSeconds, realElapsedSeconds);
                }
            }
        }


        public override void Shutdown()
        {

        }


        public IFStateMachine<T> CreateStateMachine<T>(string name, T owner, params IFState<T>[] states) where T : class
        {
            if (m_StateMachines.ContainsKey(name))
            {
                throw new InvalidOperationException($"State machine for type {typeof(T).FullName} already exists.");
            }

            IFStateMachine<T> stateMachine = new IFStateMachine<T>(name, owner, states);
            m_StateMachines[name] = stateMachine;
            return stateMachine;
        }


        public IFStateMachine<T> GetStateMachine<T>(string name) where T : class
        {
            if (m_StateMachines.TryGetValue(name, out var stateMachine))
            {
                return stateMachine as IFStateMachine<T>;
            }

            throw new KeyNotFoundException($"State machine for type {typeof(T).FullName} does not exist.");
        }
        

        public IFStateMachineBase[] GetAllStateMachines()
        {
            IFStateMachineBase[] stateMachines = new IFStateMachineBase[m_StateMachines.Count];
            m_StateMachines.Values.CopyTo(stateMachines, 0);
            return stateMachines;
        }


        public bool HasStateMachine(string name)
        {
            return m_StateMachines.ContainsKey(name);
        }


        public bool DestroyStateMachine(string name)
        {
            if (m_StateMachines.TryGetValue(name, out var sm))
            {
                if (sm is IFStateMachine<object> stateMachine)
                {
                    stateMachine.Stop();
                }
                return m_StateMachines.Remove(name);
            }
            return false;
        }
    }
}