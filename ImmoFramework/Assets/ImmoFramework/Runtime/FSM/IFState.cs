using System;
using System.Collections.Generic;



namespace ImmoFramework.Runtime
{
    public abstract class IFState<T> where T : class
    {
        /// <summary>
        /// Called when the state is initialized.
        /// </summary>
        /// <param name="stateMachine">The state machine that owns this state.</param>
        public virtual void OnInitialize(IFStateMachine<T> stateMachine)
        {
        }


        /// <summary>
        /// Called when the state is entered.
        /// </summary>
        /// <param name="stateMachine">The state machine that owns this state.</param>
        public virtual void OnEnter(IFStateMachine<T> stateMachine)
        {
        }


        /// <summary>
        /// Called when the state is updated.
        /// </summary>
        /// <param name="stateMachine">The state machine that owns this state.</param>
        /// <param name="elapsedTime">The elapsed time since the last update.</param>
        /// <param name="realElapsedTime">The real elapsed time since the last update.</param>
        public virtual void OnUpdate(IFStateMachine<T> stateMachine, float elapsedTime, float realElapsedTime)
        {
        }


        /// <summary>
        /// Called when the state is exited.
        /// </summary>
        /// <param name="stateMachine">The state machine that owns this state.</param>
        public virtual void OnExit(IFStateMachine<T> stateMachine)
        {
        }


        /// <summary>
        /// Called when the state is destroyed.
        /// </summary>
        /// <param name="stateMachine">The state machine that owns this state.</param>
        public virtual void OnDestroy(IFStateMachine<T> stateMachine)
        {
        }


        /// <summary>
        /// Changes the current state to the specified state.
        /// </summary>
        /// <typeparam name="TState">The type of the state to change to.</typeparam>
        /// <param name="stateMachine">The state machine that owns this state.</param>
        public void ChangeState<TState>(IFStateMachine<T> stateMachine) where TState : IFState<T>  
        {  
            stateMachine.ChangeState<TState>();  
        }
    }
}