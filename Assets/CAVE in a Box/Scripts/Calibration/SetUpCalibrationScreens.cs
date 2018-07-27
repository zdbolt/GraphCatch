using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetUpCalibrationScreens : MonoBehaviour {
	public string postCalibrationScene;
	public DisplayCamera frontCam, leftCam, rightCam, floorCam;

	private CalibrationScreen[] screens;
	private bool AxisInUse = false;


	// Use this for initialization
	void Start () {
		postCalibrationScene = "Assets/CAVE in a Box/Scenes/" + postCalibrationScene;
		screens = GetComponentsInChildren<CalibrationScreen> ();
		int tempInt;
		CalibrationScreen tempScreen;
		// Gather and sort screens
		//Debug.Log ("Screen Order: " + screens[0].ToString() + screens[1].ToString() + screens[2].ToString() + screens[3].ToString());
		for (int i = 0; i < 4; i ++) {
			tempInt = (int) screens [i].index;
			if (tempInt != i) {
				//Debug.Log ("Swapped Screen " + i.ToString () + " with Screen " + tempInt.ToString ());
				tempScreen = screens [tempInt];
				screens [tempInt] = screens [i];
				screens [i] = tempScreen;
			}
		}
		//Debug.Log ("Screen Order: " + screens[0].ToString() + screens[1].ToString() + screens[2].ToString() + screens[3].ToString());

		screens [0].setup (floorCam, screens [1], screens [3]);
		screens [1].setup (leftCam,  screens [2], screens [0]);
		screens [2].setup (frontCam, screens [3], screens [1]);
		screens [3].setup (rightCam, screens [0], screens [2]);

		screens [0].setActive (CalibrationScreen.Direction.TL, null, false);
	}

	void Update() {
		// Start - Update Config File, Transition Scene
		if (Input.GetAxisRaw ("Submit") != 0f) {
			if (!AxisInUse) {
				AxisInUse = true;
				ConfigParser parser = GameObject.FindObjectOfType<ConfigParser> ();
				parser.newConfig (new CameraData[4] {
					floorCam.cameraData,
					leftCam.cameraData,
					frontCam.cameraData,
					rightCam.cameraData
				});
			}
			Debug.Log (postCalibrationScene);
			if (SceneManager.GetSceneByName(postCalibrationScene).IsValid()) {
				SceneManager.LoadScene(postCalibrationScene);
				SceneManager.SetActiveScene (SceneManager.GetSceneByName (postCalibrationScene));
				SceneManager.UnloadSceneAsync ("Assets/CAVE in a Box/Scenes/sdk/sdk-calibrate.unity");
			} else 
				Application.Quit();			
		}
	}
}
