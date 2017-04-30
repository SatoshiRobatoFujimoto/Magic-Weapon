﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticPulse : MonoBehaviour {

	private SteamVR_TrackedObject trackedObject;
	private SteamVR_Controller.DeviceType device;

	// Use this for initialization
	void Start () {
		trackedObject = GetComponent<SteamVR_TrackedObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		device = SteamVR_Controller.Input ((int)trackedObject.index);
		if (device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("Trigger Press");
			device.TriggerHapticPulse (700);
		}
	}
}
