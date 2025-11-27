

using UnityEngine;

using ImmoFramework.Runtime;


public class ProcedureEventA2B : IFEvent
{
    public ProcedureEventA2B(object source)
        : base(source)
    {
    }
}

public class ProcedureEventA2BHandler : IFEventHandler<ProcedureEventA2B>
{
    private IFProcedureBase m_Procedure;
    private IFStateMachine<IFProcedureModule> m_StateMachine;

    public ProcedureEventA2BHandler(IFProcedureBase procedure, IFStateMachine<IFProcedureModule> stateMachine)
    {
        this.m_Procedure = procedure;
        this.m_StateMachine = stateMachine; 
    }

    public override void HandleEvent(ProcedureEventA2B e)
    {
        m_Procedure.ChangeState<TestProcedureB>(m_StateMachine);
        Debug.Log($"Change procedure from A to B");
    }
}

public class ProcedureEventB2A : IFEvent
{
    public ProcedureEventB2A(object source)
        : base(source)
    {
    }
}

public class ProcedureEventB2AHandler : IFEventHandler<ProcedureEventB2A>
{
    private IFProcedureBase m_Procedure;
    private IFStateMachine<IFProcedureModule> m_StateMachine;

    public ProcedureEventB2AHandler(IFProcedureBase procedure, IFStateMachine<IFProcedureModule> stateMachine)
    {
        this.m_Procedure = procedure;
        this.m_StateMachine = stateMachine; 
    }

    public override void HandleEvent(ProcedureEventB2A e)
    {
        m_Procedure.ChangeState<TestProcedureA>(m_StateMachine);
        Debug.Log($"Change procedure from B to A");
    }
}


public class ProcedureSwitch : MonoBehaviour
{
    private void Start()
    {
    }

    private void OnGUI()
    {

        if (GUILayout.Button("A to B"))
        {
            IFGameEntry.EventComponent.TriggerEvent(new ProcedureEventA2B(this));
        }

        if (GUILayout.Button("B to A"))
        {
            IFGameEntry.EventComponent.TriggerEvent(new ProcedureEventB2A(this));
        }
    }
}