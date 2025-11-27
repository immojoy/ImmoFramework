using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ImmoFramework.Runtime;



public class TestProcedureA : IFProcedureBase
{
    private ProcedureEventA2BHandler m_ProcedureEventA2BHandler;

    public override void OnEnter(IFStateMachine<IFProcedureModule> stateMachine)
    {
        base.OnEnter(stateMachine);

        m_ProcedureEventA2BHandler = new ProcedureEventA2BHandler(this, stateMachine);
        IFGameEntry.EventComponent.RegisterHandler(m_ProcedureEventA2BHandler);

        Debug.Log("Entered TestProcedureA");
    }


    public override void OnExit(IFStateMachine<IFProcedureModule> stateMachine)
    {
        base.OnExit(stateMachine);

        IFGameEntry.EventComponent.UnregisterHandler(m_ProcedureEventA2BHandler);

        Debug.Log("Exited TestProcedureA");
    }
}
