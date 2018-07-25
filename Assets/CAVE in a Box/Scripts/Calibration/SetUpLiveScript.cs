using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUpLiveScript : MonoBehaviour
{
	public int FPS = 60;
	public Rect sourceRect;
	public WebCamTexture webCamTexture;


	private int camNumber = -1;
	private WebCamDevice webCamDevice;

/**********************************************************************************************************************/

	// This function will be called at the first frame of the application
	void Start()
	{
		WebCamDevice[] devices = WebCamTexture.devices;
		Debug.Log ("Number of cameras detected: " + devices.Length);

		for (int i = 0; i < devices.Length; i++)
		{
			print("Camera available: " + WebCamTexture.devices[i].name + " // Number " + i);

			if (WebCamTexture.devices [i].name == "RICOH THETA S")
			{
				camNumber = i;
			}	
		} 

		gameObject.GetComponent<ManagerScript> ().ManageFunction ();
			
//		if (camNumber != -1) // If the "RICOH THETA S" camera has been found then...
//		{
//			webCamDevice = WebCamTexture.devices[camNumber]; // Our new camera becomes the one with the ID "camNumber"
//			webCamTexture = new WebCamTexture(webCamDevice.name, 1920, 1080, FPS); // We want to display the live on the appropriate texture "webCamTexture" from the appropriate camera device "webCamDevice"
//			GetComponent<Renderer>().material.mainTexture = webCamTexture; // We apply the new texture onto the material of our "in game" screen
//			webCamTexture.Play(); // We display the live
//		}
//		else // If the "RICOH THETA S" camera has not been found then we print "No camera" on the console and we call the function 'NoCamera()' 
//		{
//			print("No 360 camera detected");
//		}
	}
}
	




	