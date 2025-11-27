using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ImmoFramework.Runtime;

public class TestProcedureB : IFProcedureBase
{
    private ProcedureEventB2AHandler m_ProcedureEventB2AHandler;

    public override void OnEnter(IFStateMachine<IFProcedureModule> stateMachine)
    {
        base.OnEnter(stateMachine);

        m_ProcedureEventB2AHandler = new ProcedureEventB2AHandler(this, stateMachine);
        IFGameEntry.EventComponent.RegisterHandler(m_ProcedureEventB2AHandler);

        Debug.Log("Entered TestProcedureB");
    }


    public override void OnExit(IFStateMachine<IFProcedureModule> stateMachine)
    {
        base.OnExit(stateMachine);

        IFGameEntry.EventComponent.UnregisterHandler(m_ProcedureEventB2AHandler);

        Debug.Log("Exited TestProcedureB");
    }
}
