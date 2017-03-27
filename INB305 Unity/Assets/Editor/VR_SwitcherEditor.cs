using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VR_Switcher))]
public class VR_SwitcherEditor : Editor {
	private VR_Switcher switcher;



	public void Awake(){
		switcher = (VR_Switcher)target;
	}

	public override void OnInspectorGUI (){
		base.OnInspectorGUI ();
		serializedObject.Update();


		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Current Setting: " + switcher.currentSetting.ToString());
		if (GUILayout.Button("Switch to SteamVR")){
			UnityEditorInternal.VR.VREditor.SetVREnabledDevicesOnTargetGroup(BuildTargetGroup.Standalone, new string[]{"OpenVR", "None"});
            switcher.SteamVRSwitch();
			Selection.activeObject = switcher.vrtk.gameObject;
        }
		if (GUILayout.Button("Switch to Simulator")){
			UnityEditorInternal.VR.VREditor.SetVREnabledDevicesOnTargetGroup(BuildTargetGroup.Standalone, new string[]{"None", "OpenVR"});
            switcher.SimulatorSwitch();
			Selection.activeObject = switcher.vrtk.gameObject;
        }
		EditorGUILayout.EndVertical();
	}

}
