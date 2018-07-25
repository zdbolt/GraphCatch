using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class ShowScreens : MonoBehaviour {
	public bool ShowDebugScreens = true;

	public void Awake() {
		if (!ShowDebugScreens) {
			// Deactivate Screen Marker
			for (int i = 0; i < transform.childCount; i++) {
				transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
			}
		}
	}
}
