using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Switcher : MonoBehaviour {
	[System.Serializable]
	public class VRObjects{
		public GameObject Rig, HeadSet, LeftController, RightController, LeftControllerModel, RightControllerModel;
	}

	public enum VrSetting{NotSet, Simulator, SteamVR};
	[HideInInspector]
	public VrSetting currentSetting;


	public VRTK.VRTK_SDKManager vrtk;
	public TMPro.TextMeshProUGUI settingText;

	public VRObjects SteamVR_Objects, Simulator_Objects;

	public void SteamVRSwitch(){
		currentSetting = VrSetting.SteamVR;
		vrtk.actualBoundaries = SteamVR_Objects.Rig;
		vrtk.actualHeadset = SteamVR_Objects.HeadSet;
		vrtk.actualLeftController = SteamVR_Objects.LeftController;
		vrtk.actualRightController = SteamVR_Objects.RightController;
		vrtk.modelAliasLeftController = SteamVR_Objects.LeftControllerModel;
		vrtk.modelAliasRightController = SteamVR_Objects.RightControllerModel;
		vrtk.boundariesSDK = VRTK.VRTK_SDKManager.SupportedSDKs.SteamVR;
		vrtk.controllerSDK = VRTK.VRTK_SDKManager.SupportedSDKs.SteamVR;
		vrtk.headsetSDK = VRTK.VRTK_SDKManager.SupportedSDKs.SteamVR;
		vrtk.systemSDK = VRTK.VRTK_SDKManager.SupportedSDKs.SteamVR;
		SteamVR_Objects.Rig.SetActive(true);
		Simulator_Objects.Rig.SetActive(false);
		settingText.text = "SteamVR Mode";
	}

	public void SimulatorSwitch(){
		currentSetting = VrSetting.Simulator;
		vrtk.actualBoundaries = Simulator_Objects.Rig;
		vrtk.actualHeadset = Simulator_Objects.HeadSet;
		vrtk.actualLeftController = Simulator_Objects.LeftController;
		vrtk.actualRightController = Simulator_Objects.RightController;
		vrtk.modelAliasLeftController = Simulator_Objects.LeftControllerModel;
		vrtk.modelAliasRightController = Simulator_Objects.RightControllerModel;
		vrtk.boundariesSDK = VRTK.VRTK_SDKManager.SupportedSDKs.Simulator;
		vrtk.controllerSDK = VRTK.VRTK_SDKManager.SupportedSDKs.Simulator;
		vrtk.headsetSDK = VRTK.VRTK_SDKManager.SupportedSDKs.Simulator;
		vrtk.systemSDK = VRTK.VRTK_SDKManager.SupportedSDKs.Simulator;
		SteamVR_Objects.Rig.SetActive(false);
		Simulator_Objects.Rig.SetActive(true);
		settingText.text = "Simulator Mode";
	}
}
