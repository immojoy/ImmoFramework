
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace ImmoFramework.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("ImmoFramework/Component/ImmoFramework Procedure")]
    public sealed class IFProcedureComponent : IFComponent
    {
        private IFProcedureModule m_ProcedureModule;
        private IFProcedureBase m_StartingProcedure;


        [SerializeField]
        private string[] m_AvailableProcedureTypeNames = null;

        [SerializeField]
        private string m_StartingProcedureTypeName = null;



        public IFProcedureBase CurrentProcedure => m_ProcedureModule.CurrentProcedure;
        public float CurrentProcedureTime => m_ProcedureModule.CurrentProcedureTime;


        #region Unity Callbacks
        protected override void Awake()
        {
            base.Awake();

            m_ProcedureModule = IFModuleEntry.GetModule<IFProcedureModule>();
            if (m_ProcedureModule == null)
            {
                Debug.LogError("Invalid procedure module.");
                return;
            }
        }

        private IEnumerator Start()
        {
            IFProcedureBase[] procedures = new IFProcedureBase[m_AvailableProcedureTypeNames.Length];
            for (int i = 0; i < m_AvailableProcedureTypeNames.Length; i++)
            {
                Type procedureType = Type.GetType(m_AvailableProcedureTypeNames[i]);
                if (procedureType == null)
                {
                    Debug.LogError($"Can not find procedure type: {m_AvailableProcedureTypeNames[i]}");
                    yield break;
                }

                procedures[i] = (IFProcedureBase)Activator.CreateInstance(procedureType);
                if (procedures[i] == null)
                {
                    Debug.LogError($"Can not create procedure instance: {m_AvailableProcedureTypeNames[i]}");
                    yield break;
                }

                if (m_StartingProcedureTypeName == m_AvailableProcedureTypeNames[i])
                {
                    m_StartingProcedure = procedures[i];
                }
            }

            if (m_StartingProcedure == null)
            {
                Debug.LogError("Starting procedure is not set.");
                yield break;
            }

            m_ProcedureModule.Initialize(IFModuleEntry.GetModule<IFStateMachineModule>(), procedures);

            yield return new WaitForEndOfFrame();

            Type startingProcedureType = m_StartingProcedure.GetType();
            m_ProcedureModule.StartProcedure(startingProcedureType);
        }
        #endregion


        /// <summary>
        /// Checks if the procedure with the specified type exists.
        /// </summary>
        public bool HasProcedure<T>() where T : IFProcedureBase
        {
            return m_ProcedureModule.HasProcedure<T>();
        }


        /// <summary>
        /// Checks if the procedure with the specified type exists.
        /// </summary>
        public bool HasProcedure(Type procedureType)
        {
            return m_ProcedureModule.HasProcedure(procedureType);
        }


        /// <summary>
        /// Gets the procedure with the specified type.
        /// </summary>
        public IFProcedureBase GetProcedure<T>() where T : IFProcedureBase
        {
            return m_ProcedureModule.GetProcedure<T>();
        }


        /// <summary>
        /// Gets the procedure with the specified type.
        /// </summary>
        public IFProcedureBase GetProcedure(Type procedureType)
        {
            return m_ProcedureModule.GetProcedure(procedureType);
        }
    }
}