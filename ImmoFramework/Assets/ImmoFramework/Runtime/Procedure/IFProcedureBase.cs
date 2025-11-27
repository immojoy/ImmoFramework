
using System;


namespace ImmoFramework.Runtime
{
    public abstract class IFProcedureBase : IFState<IFProcedureModule>
    {
        /// <summary>
        /// Called when the procedure is initialized.
        /// </summary>
        /// <param name="stateMachine"></param>
        public override void OnInitialize(IFStateMachine<IFProcedureModule> stateMachine)
        {
            base.OnInitialize(stateMachine);
        }


        /// <summary>
        /// Called when the procedure is entered.
        /// </summary>
        /// <param name="stateMachine"></param>
        public override void OnEnter(IFStateMachine<IFProcedureModule> stateMachine)
        {
            base.OnEnter(stateMachine);
        }


        /// <summary>
        /// Called when the procedure is updated.
        /// </summary>
        /// <param name="stateMachine"></param>
        /// <param name="elapsedTime"></param>
        /// <param name="realElapsedTime"></param>
        public override void OnUpdate(IFStateMachine<IFProcedureModule> stateMachine, float elapsedTime, float realElapsedTime)
        {
            base.OnUpdate(stateMachine, elapsedTime, realElapsedTime);
        }


        /// <summary>
        /// Called when the procedure is exited.
        /// </summary>
        /// <param name="stateMachine"></param>
        public override void OnExit(IFStateMachine<IFProcedureModule> stateMachine)
        {
            base.OnExit(stateMachine);
        }


        /// <summary>
        /// Called when the procedure is destroyed.
        /// </summary>
        /// <param name="stateMachine"></param>
        public override void OnDestroy(IFStateMachine<IFProcedureModule> stateMachine)
        {
            base.OnDestroy(stateMachine);
        }
    }
}