using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (SetUpLiveScript))]
public class ManagerScript : MonoBehaviour 
{
	//private PictureAnalysisScript pictureAnalysisScript;
	//private OutputJsonScript outputJsonScript;
	private WebCamTexture newWebCamTexture;
	private SetUpLiveScript liveScript;

	private Vector3 mainScreenPosition;
	private GameObject[] screens = new GameObject[4];
	private Texture2D [] pictures; // This variable is a 2D texture which will contain screenshot that we want to analyse

	public float cameraDelay = 2.0f;
	public Material UnlitBase;
	public Material[] calibrationTextures;

	public void Start ()
	{
		liveScript = gameObject.GetComponent<SetUpLiveScript> ();
		//pictureAnalysisScript = gameObject.GetComponent<PictureAnalysisScript> ();
		//outputJsonScript = gameObject.GetComponent<OutputJsonScript> ();
	}

	public void ManageFunction()
	{
		screens [0] = GameObject.Find ("Planes/Plane 1");
		screens [1] = GameObject.Find ("Planes/Plane 2");
		screens [2] = GameObject.Find ("Planes/Plane 3");
		screens [3] = GameObject.Find ("Planes/Plane 4");

		int limit = 5 + (calibrationTextures.Length * 4);
		pictures = new Texture2D[limit];

		StartCoroutine (ManageScripts());
	}

	void UpdateOneScreen (int index, Material newMat) {
		for (int i = 0; i < 4; ++i) {
			if (index == i)
				screens [i].GetComponent<Renderer> ().material = newMat;
			else
				screens [i].GetComponent<Renderer> ().material = calibrationTextures [0];
		}
	}

	void UpdateAllScreens (Material newMat) {
		for (int i = 0; i < 4; ++i) {
				screens [i].GetComponent<Renderer> ().material = newMat;
		}
	}

	public void TakePicture (int i)
	{
		newWebCamTexture = liveScript.webCamTexture;
		Rect newSourceRect = liveScript.sourceRect;

		newWebCamTexture.Pause (); // Pause the live feed of camera

		// Set all our useful variables for this functions
		int x = Mathf.FloorToInt (newSourceRect.x);
		int y = Mathf.FloorToInt (newSourceRect.y);
		int width = Mathf.FloorToInt (newSourceRect.width);
		int height = Mathf.FloorToInt (newSourceRect.height);

		Color[] pixels = newWebCamTexture.GetPixels (x, y, width, height);
		pictures[i] = new Texture2D (width, height);
		pictures[i].SetPixels (pixels);

		newWebCamTexture.Play (); 
	}

	IEnumerator ManageScripts()
	{
		//Take RGB WB Pictures
		UpdateAllScreens (UnlitBase);

		UnlitBase.color = Color.red;
		yield return new WaitForSeconds (cameraDelay);
		//TakePicture (0);

		UnlitBase.color = Color.green;
		yield return new WaitForSeconds (cameraDelay);
		//TakePicture (1);

		UnlitBase.color = Color.blue;
		yield return new WaitForSeconds (cameraDelay);
		//TakePicture (2);

		UnlitBase.color = Color.white;
		yield return new WaitForSeconds (cameraDelay);
		//TakePicture (3);

		UnlitBase.color = Color.black;
		yield return new WaitForSeconds (cameraDelay);
		//TakePicture (4);

		// Get calibration images for each screen individually, using remaining materials in queue
		for (int i = 0; i < calibrationTextures.Length - 1; ++i) {
			for (int j = 0; j < screens.Length; ++j) {
				UpdateOneScreen (j, calibrationTextures [i + 1]);
				yield return new WaitForSeconds (cameraDelay);
				//TakePicture (5 + (4 * i) + j);
			}
		}

		UpdateAllScreens (calibrationTextures [0]);

		//pictureAnalysisScript.FindScreens ();
//
//		GameObject.Find ("CAVE in a Box/Screens/Left Screen/Left Screen Marker").GetComponent<Renderer> ().material.color = Color.black;
//		GameObject.Find ("CAVE in a Box/Screens/Right Screen/Right Screen Marker").GetComponent<Renderer> ().material.color = Color.black;
//		GameObject.Find ("CAVE in a Box/Screens/Front Screen/Front Screen Marker").GetComponent<Renderer> ().material.color = Color.black;
//		GameObject.Find ("CAVE in a Box/Screens/Floor Screen/Floor Screen Marker").GetComponent<Renderer> ().material.color = Color.black;
//
//		mainScreenPosition.x += 3;
//		GameObject.Find ("Auto-Calib-CIAB/Main Screen").GetComponent<Transform> ().position = mainScreenPosition;
//
		/*pictureAnalysisScript.RemovePollution ();

		pictureAnalysisScript.IsolateScreens ();

		pictureAnalysisScript.FindCorners ();

		GameObject.Find ("CAVE in a Box/Screens/Left Screen/Left Screen Marker").GetComponent<Renderer> ().material.color = Color.black;
		GameObject.Find ("CAVE in a Box/Screens/Right Screen/Right Screen Marker").GetComponent<Renderer> ().material.color = Color.black;
		GameObject.Find ("CAVE in a Box/Screens/Front Screen/Front Screen Marker").GetComponent<Renderer> ().material.color = Color.black;
		GameObject.Find ("CAVE in a Box/Screens/Floor Screen/Floor Screen Marker").GetComponent<Renderer> ().material.color = Color.black;

		GameObject.Find ("CAVE in a Box/Screens/Front Screen/Front Crosshair").SetActive (true);
		GameObject.Find ("CAVE in a Box/Screens/Left Screen/Left Crosshair").SetActive (true);
		GameObject.Find ("CAVE in a Box/Screens/Right Screen/Right Crosshair").SetActive (true);

		yield return new WaitForSeconds (2.0f);
		
		pictureAnalysisScript.TakeCrosshairsPicture ();

		pictureAnalysisScript.FindCrosshairs ();

		pictureAnalysisScript.CalculateOffset ();

		GameObject.Find ("CAVE in a Box/Screens/Front Screen/Front Crosshair").SetActive (false);
		GameObject.Find ("CAVE in a Box/Screens/Left Screen/Left Crosshair").SetActive (false);
		GameObject.Find ("CAVE in a Box/Screens/Right Screen/Right Crosshair").SetActive (false);

		GameObject.Find ("CAVE in a Box/Screens/Front Screen/Front X Crosshair").SetActive (true);
		GameObject.Find ("CAVE in a Box/Screens/Front Screen/Front Y Crosshair").SetActive (true);

		GameObject.Find ("CAVE in a Box/Screens/Left Screen/Left X Crosshair").SetActive (true);
		GameObject.Find ("CAVE in a Box/Screens/Left Screen/Left Y Crosshair").SetActive (true);

		GameObject.Find ("CAVE in a Box/Screens/Right Screen/Right X Crosshair").SetActive (true);
		GameObject.Find ("CAVE in a Box/Screens/Right Screen/Right Y Crosshair").SetActive (true);

		yield return new WaitForSeconds (2.0f);

		pictureAnalysisScript.TakeCrosshairsPicture ();

		pictureAnalysisScript.FindCrosshairs ();

		pictureAnalysisScript.CalculateRatio ();

		outputJsonScript.WriteNewJSON ();

		SceneManager.LoadScene (1);*/
	}
}
