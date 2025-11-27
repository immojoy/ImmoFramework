
using System;


namespace ImmoFramework.Runtime
{
    public sealed class IFProcedureModule : IFModule
    {
        private IFStateMachineModule m_StateMachineModule;
        private IFStateMachine<IFProcedureModule> m_StateMachine;

        public override int Priority => -2;


        /// <summary>
        /// Gets the current procedure.
        /// </summary>
        public IFProcedureBase CurrentProcedure
        {
            get
            {
                if (m_StateMachine == null)
                {
                    return null;
                }

                return m_StateMachine.CurrentState as IFProcedureBase;
            }
        }


        /// <summary>
        /// Gets the time elapsed since the current procedure started.
        /// </summary>
        public float CurrentProcedureTime
        {
            get
            {
                if (m_StateMachine == null)
                {
                    return 0f;
                }

                return m_StateMachine.CurrentStateTime;
            }
        }


        /// <summary>
        /// Initializes the procedure module with the specified state machine module and procedures.
        /// </summary>
        /// <param name="stateMachineModule">The state machine module to use.</param>
        /// <param name="procedures">The procedures to initialize.</param>
        public void Initialize(IFStateMachineModule stateMachineModule, params IFProcedureBase[] procedures)
        {
            if (procedures == null || procedures.Length == 0)
            {
                throw new ArgumentException("Procedures cannot be null or empty.", nameof(procedures));
            }

            m_StateMachineModule = stateMachineModule;
            m_StateMachine = m_StateMachineModule.CreateStateMachine<IFProcedureModule>("ProcedureSM", this, procedures);
        }


        /// <summary>
        /// Starts the procedure with the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the procedure to start. </typeparam>
        public void StartProcedure<T>() where T : IFProcedureBase
        {
            m_StateMachine.Start<T>();
        }


        /// <summary>
        /// Starts the procedure with the specified type.
        /// </summary>
        public void StartProcedure(Type procedureType)
        {
            m_StateMachine.Start(procedureType);
        }


        /// <summary>
        /// Checks if the procedure with the specified type exists.
        /// </summary>
        /// <returns><b>true</b> if the procedure exists; otherwise, <b>false</b>.</returns>
        public bool HasProcedure<T>() where T : IFProcedureBase
        {
            return m_StateMachine.HasState<T>();
        }


        /// <summary>
        /// Checks if the procedure with the specified type exists.
        /// </summary>
        public bool HasProcedure(Type procedureType)
        {
            return m_StateMachine.HasState(procedureType);
        }


        /// <summary>
        /// Gets the procedure with the specified type.
        /// </summary>
        public IFProcedureBase GetProcedure(Type procedureType)
        {
            return m_StateMachine.GetState(procedureType) as IFProcedureBase;
        }


        /// <summary>
        /// Gets the procedure with the specified type.
        /// </summary>
        /// <typeparam name="T">IFProcedureBase</typeparam>
        /// <returns>The procedure with the specified type.</returns>
        public IFProcedureBase GetProcedure<T>() where T : IFProcedureBase
        {
            if (m_StateMachine == null)
            {
                throw new InvalidOperationException("Procedure module is not initialized.");
            }

            return m_StateMachine.GetState<T>() as IFProcedureBase;
        }


        public override void Update(float elapsedSeconds, float realElapsedSeconds)
        {
            
        }


        public override void Shutdown()
        {
            if (m_StateMachineModule != null)
            {
                if (m_StateMachine != null)
                {
                    m_StateMachineModule.DestroyStateMachine("ProcedureSM");
                    m_StateMachine = null;
                }

                m_StateMachineModule = null;
            }
        }
    }
}