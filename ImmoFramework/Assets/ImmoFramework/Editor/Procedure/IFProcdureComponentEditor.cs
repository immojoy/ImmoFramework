

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

using ImmoFramework.Runtime;
using System;
using System.Reflection;


namespace ImmoFramework.Editor
{
    [CustomEditor(typeof(IFProcedureComponent))]
    public sealed class IFProcedureComponentEditor : IFComponentEditor
    {
        private SerializedProperty m_AvailableProcedureTypeNames = null;
        private SerializedProperty m_StartingProcedureTypeName = null;


        private string[] m_ProcedureTypeNames = null;
        private List<string> m_CurrentProcedureTypeNames = null;
        private int m_StartingProcedureTypeIndex = 0;


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            IFProcedureComponent t = (IFProcedureComponent)target;

            if (string.IsNullOrEmpty(m_StartingProcedureTypeName.stringValue) && m_CurrentProcedureTypeNames.Count > 0)
            {
                EditorGUILayout.HelpBox("Starting procedure is not set.", MessageType.Error);
            }
            else if (EditorApplication.isPlaying)
            {
                EditorGUILayout.LabelField("Current Procedure", t.CurrentProcedure == null ? "None" : t.CurrentProcedure.GetType().ToString());
            }

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                GUILayout.Label("Available Procedures", EditorStyles.boldLabel);
                if (m_ProcedureTypeNames.Length > 0)
                {
                    EditorGUILayout.BeginVertical("box");
                    {
                        foreach (string procedureTypeName in m_ProcedureTypeNames)
                        {
                            bool selected = m_CurrentProcedureTypeNames.Contains(procedureTypeName);
                            if (selected != EditorGUILayout.ToggleLeft(procedureTypeName, selected))
                            {
                                if (!selected)
                                {
                                    m_CurrentProcedureTypeNames.Add(procedureTypeName);
                                    WriteAvailableProcedureTypeNames();
                                }
                                else
                                {
                                    m_CurrentProcedureTypeNames.Remove(procedureTypeName);
                                    WriteAvailableProcedureTypeNames();
                                }
                            }
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
                else
                {
                    EditorGUILayout.HelpBox("No available procedures.", MessageType.Info);
                }

                if (m_CurrentProcedureTypeNames.Count > 0)
                {
                    EditorGUILayout.Separator();

                    int selectedIndex = EditorGUILayout.Popup("Starting Procedure", m_StartingProcedureTypeIndex, m_CurrentProcedureTypeNames.ToArray());
                    if (selectedIndex != m_StartingProcedureTypeIndex)
                    {
                        m_StartingProcedureTypeIndex = selectedIndex;
                        m_StartingProcedureTypeName.stringValue = m_CurrentProcedureTypeNames[selectedIndex];
                    }
                }
                // else
                // {
                //     EditorGUILayout.HelpBox("Select an available procedure first.", MessageType.Info);
                // }
            }
            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();

            Repaint();
        }


        protected override void OnCompileComplete()
        {
            base.OnCompileComplete();

            RefreshTypeNames();
        }


        private void OnEnable()
        {
            m_AvailableProcedureTypeNames = serializedObject.FindProperty("m_AvailableProcedureTypeNames");
            m_StartingProcedureTypeName = serializedObject.FindProperty("m_StartingProcedureTypeName");

            RefreshTypeNames();
        }


        private void RefreshTypeNames()
        {
            List<string> typeNames = new();

            Assembly assembly = null;
            assembly = Assembly.Load("Assembly-CSharp");

            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (type.IsClass && !type.IsAbstract && typeof(IFProcedureBase).IsAssignableFrom(type))
                {
                    typeNames.Add(type.FullName);
                }
            }
            m_ProcedureTypeNames = typeNames.ToArray();
            m_ProcedureTypeNames ??= Array.Empty<string>();

            ReadAvailableProcedureTypeNames();

            int oldCount = m_CurrentProcedureTypeNames.Count;
            m_CurrentProcedureTypeNames = m_CurrentProcedureTypeNames.Where(x => m_ProcedureTypeNames.Contains(x)).ToList();
            if (m_CurrentProcedureTypeNames.Count != oldCount)
            {
                WriteAvailableProcedureTypeNames();
            }
            else if (!string.IsNullOrEmpty(m_StartingProcedureTypeName.stringValue))
            {
                m_StartingProcedureTypeIndex = m_CurrentProcedureTypeNames.IndexOf(m_StartingProcedureTypeName.stringValue);
                if (m_StartingProcedureTypeIndex < 0)
                {
                    m_StartingProcedureTypeName.stringValue = null;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
        

        private void ReadAvailableProcedureTypeNames()
        {
            m_CurrentProcedureTypeNames = new();
            int count = m_AvailableProcedureTypeNames.arraySize;
            for (int i = 0; i < count; i++)
            {
                m_CurrentProcedureTypeNames.Add(m_AvailableProcedureTypeNames.GetArrayElementAtIndex(i).stringValue);
            }
        }
        

        private void WriteAvailableProcedureTypeNames()
        {
            m_AvailableProcedureTypeNames.ClearArray();
            if (m_CurrentProcedureTypeNames == null)
            {
                return;
            }

            m_CurrentProcedureTypeNames.Sort();
            int count = m_CurrentProcedureTypeNames.Count;
            for (int i = 0; i < count; i++)
            {
                m_AvailableProcedureTypeNames.InsertArrayElementAtIndex(i);
                m_AvailableProcedureTypeNames.GetArrayElementAtIndex(i).stringValue = m_CurrentProcedureTypeNames[i];
            }

            if (!string.IsNullOrEmpty(m_StartingProcedureTypeName.stringValue))
            {
                m_StartingProcedureTypeIndex = m_CurrentProcedureTypeNames.IndexOf(m_StartingProcedureTypeName.stringValue);
                if (m_StartingProcedureTypeIndex < 0)
                {
                    m_StartingProcedureTypeName.stringValue = null;
                }
            }
        }
    }
}