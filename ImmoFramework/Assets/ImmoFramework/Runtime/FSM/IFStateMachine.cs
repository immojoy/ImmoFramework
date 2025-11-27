using System;
using System.Collections.Generic;

using UnityEngine;



namespace ImmoFramework.Runtime
{
    public class IFStateMachine<T> : IFStateMachineBase where T : class
    {
        /// <summary>
        /// Owner of the state machine
        /// </summary>
        public T Owner { get; set; }


        private readonly Dictionary<Type, IFState<T>> m_States;
        private IFState<T> m_CurrentState;
        private float m_CurrentStateTime;


        /// <summary>
        /// Gets the name of the state machine.
        /// </summary>
        public override string Name { get; }


        /// <summary>
        /// Gets the current state of the state machine.
        /// </summary>
        public IFState<T> CurrentState => m_CurrentState;

        /// <summary>
        /// Gets the elapsed time since the current state was entered.
        /// </summary>
        public override float CurrentStateTime => m_CurrentStateTime;

        /// <summary>
        /// Gets a value indicating whether the state machine is currently running.
        /// </summary>
        public override bool IsRunning => m_CurrentState != null;


        /// <summary>
        /// Gets the name of the current state.
        /// </summary>
        public override string CurrentStateName => m_CurrentState?.GetType().Name ?? string.Empty;


        /// <summary>
        /// Gets a value indicating whether the state machine is destroyed.
        /// </summary>
        public override bool IsDestroyed => m_CurrentState == null;


        public IFStateMachine(string name, T owner, params IFState<T>[] states)
        {
            if (states == null)
            {
                Debug.LogError("States array is null.");
            }

            if (states == null || states.Length == 0)
            {
                Debug.LogError("At least one state must be provided.");
            }

            Name = name;
            Owner = owner;
            m_States = new Dictionary<Type, IFState<T>>();

            foreach (var state in states)
            {
                Type stateType = state.GetType();
                if (m_States.ContainsKey(stateType))
                {
                    Debug.LogWarning($"State of type {stateType.FullName} is already added.");
                    continue;
                }

                m_States[stateType] = state;
                state.OnInitialize(this);
            }
        }


        /// <summary>
        /// Starts the state machine with the specified initial state.
        /// </summary>
        /// <param name="stateType"></param>
        public void Start(Type stateType)
        {
            if (IsRunning)
            {
                Debug.LogWarning("State machine is already running.");
                return;
            }

            if (!m_States.TryGetValue(stateType, out var state))
            {
                Debug.LogError($"State of type {stateType.FullName} is not found.");
                return;
            }

            m_CurrentState = state;
            m_CurrentStateTime = 0f;
            m_CurrentState.OnEnter(this);
        }


        /// <summary>
        /// Starts the state machine with the specified initial state.
        /// </summary>
        public void Start<TState>() where TState : IFState<T>
        {
            Type stateType = typeof(TState);
            if (stateType == null)
            {
                Debug.LogError("Invalid state type.");
                return;
            }

            Start(stateType);
        }


        /// <summary>
        /// Updates the current state of the state machine.
        /// </summary>
        public override void Update(float elapsedTime, float realElapsedTime)
        {
            if (m_CurrentState == null)
            {
                return;
            }

            m_CurrentStateTime += elapsedTime;
            m_CurrentState.OnUpdate(this, elapsedTime, realElapsedTime);
        }


        /// <summary>
        /// Changes the current state of the state machine to the specified state type.
        /// </summary>
        /// <param name="stateType"></param>
        public void ChangeState(Type stateType)
        {
            if (m_CurrentState == null)
            {
                Debug.LogError("State machine is not running.");
                return;
            }

            if (!m_States.TryGetValue(stateType, out var state))
            {
                Debug.LogError($"State of type {stateType.FullName} is not found.");
                return;
            }

            m_CurrentState.OnExit(this);
            m_CurrentState = state;
            m_CurrentStateTime = 0f;
            m_CurrentState.OnEnter(this);
        }


        /// <summary>
        /// Changes the current state of the state machine to the specified state type.
        /// </summary>
        /// <typeparam name="TState">IFState</typeparam>
        public void ChangeState<TState>() where TState : IFState<T>
        {
            Type stateType = typeof(TState);
            if (stateType == null)
            {
                Debug.LogError("Invalid state type.");
                return;
            }

            ChangeState(stateType);
        }


        /// <summary>
        /// Stops the state machine and exits the current state.
        /// </summary>
        public void Stop()
        {
            if (m_CurrentState != null)
            {
                m_CurrentState.OnExit(this);
                m_CurrentState = null;
                m_CurrentStateTime = 0f;
            }
        }


        /// <summary>
        /// Checks if the state machine contains a state of the specified type.
        /// </summary>
        public bool HasState(Type stateType)
        {
            if (stateType == null)
            {
                Debug.LogError("Invalid state type.");
                return false;
            }

            return m_States.ContainsKey(stateType);
        }


        /// <summary>
        /// Checks if the state machine contains a state of the specified type.
        /// </summary>
        /// <returns><b>true</b> if the state machine contains the specified state; otherwise, <b>false</b>.</returns>
        public bool HasState<TState>() where TState : IFState<T>
        {
            return m_States.ContainsKey(typeof(TState));
        }


        /// <summary>
        /// Gets the state of the specified type.
        /// </summary>
        public IFState<T> GetState(Type stateType)
        {
            if (stateType == null)
            {
                Debug.LogError("Invalid state type.");
                return null;
            }

            if (m_States.TryGetValue(stateType, out var state))
            {
                return state;
            }

            return null;
        }


        /// <summary>
        /// Gets the state of the specified type.
        /// </summary>
        /// <typeparam name="TState">IFState</typeparam>
        /// <returns>Gets the state of the specified type.</returns>
        public IFState<T> GetState<TState>() where TState : IFState<T>
        {
            if (m_States.TryGetValue(typeof(TState), out var state))
            {
                return state;
            }

            return null;
        }
    }
}