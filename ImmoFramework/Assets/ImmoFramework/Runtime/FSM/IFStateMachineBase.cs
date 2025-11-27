using System;
using System.Collections.Generic;

using UnityEngine;



namespace ImmoFramework.Runtime
{
    public abstract class IFStateMachineBase
    {
        /// <summary>
        /// Name of the state machine
        /// </summary>
        public abstract string Name { get; }


        /// <summary>
        /// Gets a value indicating whether the state machine is currently running.
        /// </summary>
        public abstract bool IsRunning { get; }


        /// <summary>
        /// Gets the name of the current state.
        /// </summary>
        public abstract string CurrentStateName { get; }


        /// <summary>
        /// Gets the elapsed time since the current state was entered.
        /// </summary>
        public abstract float CurrentStateTime { get; }


        /// <summary>
        /// Gets a value indicating whether the state machine is destroyed.
        /// </summary>
        public abstract bool IsDestroyed { get; }


        public abstract void Update(float elapsedTime, float realElapsedTime);
    }
}