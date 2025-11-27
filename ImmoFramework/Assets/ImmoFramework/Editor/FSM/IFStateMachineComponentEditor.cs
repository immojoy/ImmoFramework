//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------


using UnityEditor;

using ImmoFramework.Runtime;

namespace ImmoFramework.Editor
{
    [CustomEditor(typeof(IFStateMachineComponent))]
    internal sealed class IFStateMachineComponentEditor : IFComponentEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!EditorApplication.isPlaying)
            {
                EditorGUILayout.HelpBox("Available during runtime only.", MessageType.Info);
                return;
            }

            IFStateMachineComponent t = (IFStateMachineComponent)target;

            if (IsPrefabInHierarchy(t.gameObject))
            {
                EditorGUILayout.LabelField("State Machine Count", t.Count.ToString());

                IFStateMachineBase[] stateMachines = t.GetAllStateMachines();
                foreach (IFStateMachineBase stateMachine in stateMachines)
                {
                    DrawStateMachine(stateMachine);
                }
            }

            Repaint();
        }

        private void OnEnable()
        {
        }

        private void DrawStateMachine(IFStateMachineBase stateMachine)
        {
            EditorGUILayout.LabelField(stateMachine.Name, stateMachine.IsRunning ? string.Format("{0}, {1:F1} s", stateMachine.CurrentStateName, stateMachine.CurrentStateTime) : (stateMachine.IsDestroyed ? "Destroyed" : "Not Running"));
        }
    }
}
