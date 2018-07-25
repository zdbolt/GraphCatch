using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PictureAnalysisScript : MonoBehaviour
{
	public Texture2D[] testPictures;
	public bool test;

	private Texture2D [] pictures;
	private SetUpLiveScript liveScript;
	private WebCamTexture newWebCamTexture;

	void Start () {
		liveScript = gameObject.GetComponent<SetUpLiveScript> ();
		if (test)
			pictures = testPictures;
		
	}

//	private bool middleCrosshairsFlag = true;
//	private int i, j, k, u, d, nDisplayColorBis;
//	private int[,] corners = new int[2, 12]; // This array will contain the 12 'X' and 'Y'corners coordinates of the 3 screens of the the Cave in a Box
//	private Texture2D [] pictures; // This variable is a 2D texture which will contain screenshot that we want to analyse

//	private GameObject outputJsonObject;
//	private SetUpLiveScript liveScript;
//
//	public int leftRatio1 = 0, upRatio1 = 0, leftRatio2 = 0, upRatio2 = 0, leftRatio3 = 0, upRatio3 = 0;
//
//	void Start () {
//		liveScript = gameObject.GetComponent<SetUpLiveScript> ();
//	}
//
//*************************************************************************************************************************************************************************************************

	public void TakePicture (int i)
	{
		newWebCamTexture = liveScript.webCamTexture;
		Rect newSourceRect = liveScript.sourceRect;

		newWebCamTexture.Pause (); // Pause the live feed of camera

		// Set all our useful variables for this functions
		int x = Mathf.FloorToInt (newSourceRect.x); // This variable contains the 'X' coordonate 
		int y = Mathf.FloorToInt (newSourceRect.y); // This variable contains the 'Y' coordonate  
		int width = Mathf.FloorToInt (newSourceRect.width); // This variable 
		int height = Mathf.FloorToInt (newSourceRect.height);

		Color[] pixels = newWebCamTexture.GetPixels (x, y, width, height); // We save all the pixels from the paused live into an array calls "pixels"
		pictures[i] = new Texture2D (width, height); // We create a new 2D texture with the same dimensions of the live display
		pictures[i].SetPixels (pixels); // We apply all the pixels from the array "pixels" onto this 2D texture
		//GameObject.Find ("Auto-Calib-CIAB/Main Screen").GetComponent<Renderer>().material.mainTexture = redPicture; // We set on the main texture of the 'Main Screen' object the new 2D texture that the screenshot of the live  

		newWebCamTexture.Play (); 
	}

//*************************************************************************************************************************************************************************************************
//
//	public void TakeGreenPicture ()
//	{
//		SetUpLiveScript setUpLiveScript = GameObject.Find ("Auto-Calib-CIAB/Main Screen").GetComponent<SetUpLiveScript> (); // We retrieve the class "SetUpLiveScript" inside a variable 
//		Rect newSourceRect = setUpLiveScript.sourceRect;
//
//		newWebCamTexture.Pause (); // We pause the live of camera
//
//		// We set all our useful variables for this functions
//		int x = Mathf.FloorToInt (newSourceRect.x); // This variable contains the 'X' coordonate 
//		int y = Mathf.FloorToInt (newSourceRect.y); // This variable contains the 'Y' coordonate  
//		int width = Mathf.FloorToInt (newSourceRect.width); // This variable 
//		int height = Mathf.FloorToInt (newSourceRect.height);
//
//		Color[] greenPixels = newWebCamTexture.GetPixels (x, y, width, height); // We save all the pixels from the paused live into an array calls "pixels"
//		greenPicture = new Texture2D (width, height); // We create a new 2D texture with the same dimensions of the live display
//		greenPicture.SetPixels (greenPixels); // We apply all the pixels from the array "pixels" onto this 2D texture
//		//GameObject.Find ("Auto-Calib-CIAB/Main Screen").GetComponent<Renderer>().material.mainTexture = greenPicture; // We set on the main texture of the 'Main Screen' object the new 2D texture that the screenshot of the live  
//
//		newWebCamTexture.Play ();
//	}
//
////*************************************************************************************************************************************************************************************************
//
//	public void TakeBluePicture ()
//	{
//		SetUpLiveScript setUpLiveScript = GameObject.Find ("Auto-Calib-CIAB/Main Screen").GetComponent<SetUpLiveScript> (); // We retrieve the class "SetUpLiveScript" inside a variable 
//		Rect newSourceRect = setUpLiveScript.sourceRect;
//
//		newWebCamTexture.Pause (); // We pause the live of camera
//
//		// We set all our useful variables for this functions
//		int x = Mathf.FloorToInt (newSourceRect.x); // This variable contains the 'X' coordonate 
//		int y = Mathf.FloorToInt (newSourceRect.y); // This variable contains the 'Y' coordonate  
//		int width = Mathf.FloorToInt (newSourceRect.width); // This variable 
//		int height = Mathf.FloorToInt (newSourceRect.height);
//
//		Color[] bluePixels = newWebCamTexture.GetPixels (x, y, width, height); // We save all the pixels from the paused live into an array calls "pixels"
//		bluePicture = new Texture2D (width, height); // We create a new 2D texture with the same dimensions of the live display
//		bluePicture.SetPixels (bluePixels); // We apply all the pixels from the array "pixels" onto this 2D texture
//		//GameObject.Find ("Auto-Calib-CIAB/Main Screen").GetComponent<Renderer>().material.mainTexture = bluePicture; // We set on the main texture of the 'Main Screen' object the new 2D texture that the screenshot of the live  
//
//		newWebCamTexture.Play ();
//	}
//
////*************************************************************************************************************************************************************************************************
//
//	public void TakeWhitePicture ()
//	{
//		SetUpLiveScript setUpLiveScript = GameObject.Find ("Auto-Calib-CIAB/Main Screen").GetComponent<SetUpLiveScript> (); // We retrieve the class "SetUpLiveScript" inside a variable 
//		Rect newSourceRect = setUpLiveScript.sourceRect;
//
//		newWebCamTexture.Pause (); // We pause the live of camera
//
//		// We set all our useful variables for this functions
//		int x = Mathf.FloorToInt (newSourceRect.x); // This variable contains the 'X' coordonate 
//		int y = Mathf.FloorToInt (newSourceRect.y); // This variable contains the 'Y' coordonate  
//		int width = Mathf.FloorToInt (newSourceRect.width); // This variable 
//		int height = Mathf.FloorToInt (newSourceRect.height);
//
//		Color[] whitePixels = newWebCamTexture.GetPixels (x, y, width, height); // We save all the pixels from the paused live into an array calls "pixels"
//		whitePicture = new Texture2D (width, height); // We create a new 2D texture with the same dimensions of the live displa
//		whitePicture.SetPixels (whitePixels); // We apply all the pixels from the array "pixels" onto this 2D texture
//
//		GameObject.Find ("Auto-Calib-CIAB/Main Screen").GetComponent<Renderer>().material.mainTexture = whitePicture; // We set on the main texture of the 'Main Screen' object the new 2D texture that the screenshot of the live  
//
//		newWebCamTexture.Play ();
//	}
//
////*************************************************************************************************************************************************************************************************
//

	private Color ThresholdScaling(Color color, float threshold, float scaling) {
		return new Color (((color.r - threshold) * scaling), ((color.g - threshold) * scaling), ((color.b - threshold) * scaling), 1);
	}

	public void FindScreens() { // assumes pictures 0-3 are RGB W in that order
		if (pictures.Length <= 0) {
			Debug.Log ("No Pictures in Queue");
			return;
		}
		//preprocessing
		Color newColor, redPixel, greenPixel, bluePixel = new Color();
		for (int i = 0; i < pictures [0].width; i++) { 			// X axis
			for (int j = 0; j < pictures [0].height; j++) {		// Y axis
				//all multi-image preprocessing done here

				for (int k = 0; k < pictures.Length; k++) {
					// all individual img processing done here
					if (k < 2)
						pictures [k].SetPixel (i, j, ThresholdScaling (pictures [k].GetPixel (i, j), .5f, 2f));
					else if (k == 3) {
						newColor = ThresholdScaling (pictures [k].GetPixel (i, j), .5f, 5f);
						float gray = (0.299f * newColor.r + 0.587f * newColor.g + 0.114f * newColor.b); // some magic multiplication
						newColor = new Color (gray, gray, gray);
						if (newColor.grayscale != 0f)
							pictures [k].SetPixel (i, j, Color.red);
						else
							pictures [k].SetPixel (i, j, Color.black);
					}
				}

				// all multi-image postprocessing done here
				if (pictures [3].GetPixel(i,j) == Color.red) {
					redPixel   	= pictures [0].GetPixel (i, j);
					greenPixel 	= pictures [1].GetPixel (i, j);
					bluePixel  	= pictures [2].GetPixel (i, j);
					if (redPixel.r == 0 && (redPixel.g > 0f || redPixel.b > 0f)) {
						pictures [3].SetPixel (i, j, Color.black);
					}
					if (greenPixel.g == 0 && (greenPixel.b > 0f || greenPixel.r > 0f)) {
						pictures [3].SetPixel (i, j, Color.black);
					}
				    if (bluePixel.b == 0 && (bluePixel.r > 0f || bluePixel.g > 0f)){
						pictures [3].SetPixel (i, j, Color.black);
					}
				}

			}
		}
		//postprocessing

	}


	//public void FindScreens() // Initial images are RGB WB
	//{
//		for (i = 0; i < whitePicture.width; i++) { // x-axis
//			for (j = 0; j < whitePicture.height; j++) { // y-axis 
//				Color redPixel = redPicture.GetPixel (i, j);
//				Color newRedPixel = new Vector4 (((redPixel.r - 0.5f) * 2f), ((redPixel.g - 0.5f) * 2f), ((redPixel.b - 0.5f) * 2f), 1);
//				redPicture.SetPixel (i, j, newRedPixel);
//
//				Color greenPixel = greenPicture.GetPixel (i, j);
//				Color newGreenPixel = new Vector4 (((greenPixel.r - 0.5f) * 2f), ((greenPixel.g - 0.5f) * 2f), ((redPixel.b - 0.5f) * 2f), 1);
//				greenPicture.SetPixel (i, j, newGreenPixel);
//
//				Color bluePixel = bluePicture.GetPixel (i, j);
//				Color newBluePixel = new Vector4 (((bluePixel.r - 0.5f) * 2f), ((bluePixel.g - 0.5f) * 2f), ((bluePixel.b - 0.5f) * 2f), 1);
//				bluePicture.SetPixel (i, j, newBluePixel);
//
//				Color whitePixel = whitePicture.GetPixel (i, j);
//				Color newWhitePixel = new Vector4 (((whitePixel.r - 0.5f) * 5f), ((whitePixel.g - 0.5f) * 5f), ((whitePixel.b - 0.5f) * 5f), 1);
//				whitePicture.SetPixel (i, j, newWhitePixel);
//			}
//		}
	//}
//		for (i = 0; i < whitePicture.width; i++) // x-axis
//		{
//			for (j = 0; j < whitePicture.height; j++) // y-axis 
//			{
//				Color whitePixel = whitePicture.GetPixel (i, j);
//				float gray = (0.299f * whitePixel.r + 0.587f * whitePixel.g + 0.114f * whitePixel.b); some magic multiplication
//				Color grayColor = new Vector4 (gray, gray, gray, 1);
//				whitePicture.SetPixel (i, j, grayColor);
//
//				Color newWhitePixel = whitePicture.GetPixel (i, j);
//
//				if (newWhitePixel.grayscale != 0f)
//				{
//					whitePicture.SetPixel (i, j, Color.red);
//				}
//				else
//				{
//					whitePicture.SetPixel (i, j, Color.black);
//				}
//
//				Color newNewWhitePixel = whitePicture.GetPixel (i, j);
//				Color redPixel = redPicture.GetPixel (i, j);
//				Color greenPixel = greenPicture.GetPixel (i, j);
//				Color bluePixel = bluePicture.GetPixel (i, j);
//
//				if (newNewWhitePixel == Color.red)
//				{
//					if (redPixel.r == 0)
//					{
//						if (redPixel.g > 0f || redPixel.b > 0f)
//						{
//							whitePicture.SetPixel (i, j, Color.black);
//						}	
//					}
//
//					if (greenPixel.g == 0)
//					{
//						if (greenPixel.b > 0f || greenPixel.r > 0f)
//						{
//							whitePicture.SetPixel (i, j, Color.black);
//						}	
//					}
//
//					if (bluePixel.b == 0)
//					{
//						if (bluePixel.r > 0f || bluePixel.g > 0f)
//						{
//							whitePicture.SetPixel (i, j, Color.black);
//						}	
//					}
//				}
//			}
//		}
//
//		whitePicture.Apply ();
//	}
//
////*************************************************************************************************************************************************************************************************
//
//	public void RemovePollution()
//	{
//		for (i = 0; i < whitePicture.width; i++)
//		{
//			for (j = 0; j < whitePicture.height; j++)
//			{
//				// We set our useful local variables // These local variables are our useful pixels with which we want to check the red level 
//				Color pixel = whitePicture.GetPixel (i, j); // This is our current pixel (you know this one)
//				Color pixelLeft = whitePicture.GetPixel (i - 1, j); // This pixel is situated one column before the current pixel
//				Color pixelRight = whitePicture.GetPixel (i + 1, j); // This pixel is situated one column after the current pixel
//				Color pixelDown = whitePicture.GetPixel (i, j - 1); // This pixel is situated one line under the current pixel
//				Color pixelUp = whitePicture.GetPixel (i, j + 1); // This pixel is situated one line above the current pixel
//
//				// If the current pixel is red and the left one and the right one are not then we set the current pixel to black
//				if ((pixel.r == 1 && pixelLeft.r == 0 && pixelRight.r == 0) || (pixel.r == 1 && pixelDown.r == 0 && pixelUp.r == 0))
//				{
//					whitePicture.SetPixel (i, j, Color.black);
//				}
//
//				if ((pixel.r == 0 && pixelLeft.r == 1 && pixelRight.r == 1) || (pixel.r == 0 && pixelUp.r == 1 && pixelDown.r == 1))
//				{
//					whitePicture.SetPixel (i, j, Color.red);
//				}
//
//				if (i <= 50 || i >= whitePicture.width - 50)
//				{
//					whitePicture.SetPixel (i, j, Color.black);
//				}
//				else
//				{
//					if (j <= 50 || j >= whitePicture.height - 50)
//					{
//						whitePicture.SetPixel (i, j, Color.black);
//					}	
//				}	
//			}
//		}
//	}
//
////*************************************************************************************************************************************************************************************************
//
//	public void IsolateScreens()
//	{
//		bool firstRedPixelFlag = false, newRedColumnFlag = false;
//
//		int[,] pixelID = new int[whitePicture.width, whitePicture.height];
//		int[] nID = new int[whitePicture.width * whitePicture.height];
//		int id = 0, max = 0, maxID = 0;
//
//		for (i = 1; i < whitePicture.width - 1; i++) // x-axis
//		{
//			for (j = 1; j < whitePicture.height - 1; j++) // y-axis 
//			{
//				// We set our useful local variables // These local variables are our useful pixels with which we want to check the red level 
//				Color pixel = whitePicture.GetPixel (i, j); // This is our current pixel (you know this one)
//				Color pixelLeft = whitePicture.GetPixel (i - 1, j); // This pixel is situated one column before the current pixel
//				Color pixelRight = whitePicture.GetPixel (i + 1, j); // This pixel is situated one column after the current pixel
//				Color pixelDown = whitePicture.GetPixel (i, j - 1); // This pixel is situated one line under the current pixel
//				Color pixelUp = whitePicture.GetPixel (i, j + 1); // This pixel is situated one line above the current pixel
//				Color pixelUpRight = whitePicture.GetPixel (i + 1, j + 1);
//				Color pixelUpLeft = whitePicture.GetPixel (i - 1, j + 1);
//				Color pixelDownLeft = whitePicture.GetPixel (i - 1, j - 1);
//				Color pixelDownRight = whitePicture.GetPixel (i + 1, j - 1);
//
//				if (pixel.r != 0) // If the current pixel is red then...
//				{
//					if (pixelLeft.r == 0) // If the pixel under the current red pixel is black, it means that it is the first one of a mass of red pixels...
//					{
//						if (pixelDown.r == 0)
//						{
//							id++;
//							pixelID [i, j] = id;
//
//							if (pixelUp.r != 0) // If the pixel above the current red pixel is red too, then...
//							{
//								if (pixelLeft.r != 0)
//								{
//									pixelID [i, j] = pixelID [i - 1, j];	
//								}
//								else
//								{
//									if (pixelID [i, j + 1] == 0)
//									{
//										pixelID [i, j + 1] = pixelID [i, j];
//
//										if (pixelRight.r != 0 && pixelID [i + 1, j] != pixelID [i, j])
//										{
//											pixelID [i + 1, j] = pixelID [i, j];
//										}
//
//										if (pixelDownRight.r != 0 && pixelID [i + 1, j - 1] != pixelID [i, j])
//										{
//											pixelID [i + 1, j - 1] = pixelID [i, j];
//										}
//
//										if (pixelUpRight.r != 0 && pixelID [i + 1, j + 1] != pixelID [i, j])
//										{
//											pixelID [i + 1, j + 1] = pixelID [i, j];
//										}
//									}
//									else
//									{
//										if (pixelID [i, j + 1] != pixelID [i, j])
//										{
//											pixelID [i, j] = pixelID [i, j + 1];
//										}
//									}	
//								}	
//							}
//						}
//						else
//						{	
//							if (pixelID [i, j] != 0)
//							{
//								if (pixelUp.r != 0 && pixelID [i, j + 1] != pixelID [i, j]) // If the pixel above the current red pixel is red too, then...
//								{
//									pixelID [i, j + 1] = pixelID [i, j];
//								} 
//
//								if (pixelRight.r != 0 && pixelID [i + 1, j] != pixelID [i, j])
//								{
//									pixelID [i + 1, j] = pixelID [i, j];
//								}
//
//								if (pixelDownRight.r != 0 && pixelID [i + 1, j - 1] != pixelID [i, j])
//								{
//									pixelID [i + 1, j - 1] = pixelID [i, j];
//								}
//
//								if (pixelUpRight.r != 0 && pixelID [i + 1, j + 1] != pixelID [i, j])
//								{
//									pixelID [i + 1, j + 1] = pixelID [i, j];
//								}
//
//								if (pixelUpLeft.r != 0 && pixelID [i - 1, j + 1] != pixelID [i, j])
//								{
//									pixelID [i - 1, j + 1] = pixelID [i, j];
//								}
//							}
//						}	
//					}
//					else // If the pixel under the current red pixel is noy black, it means that it is not the first one of a mass of red pixels...
//					{
//						if (pixelDown.r != 0 && pixelID [i, j - 1] != pixelID [i, j]) // If the pixel above the current red pixel is red too, then...
//						{
//							pixelID [i, j - 1] = pixelID [i, j];
//						} 
//
//						if (pixelUp.r != 0 && pixelID [i, j + 1] != pixelID [i, j]) // If the pixel above the current red pixel is red too, then...
//						{
//							pixelID [i, j + 1] = pixelID [i, j];
//						} 
//
//						if (pixelLeft.r != 0 && pixelID [i - 1, j] != pixelID [i, j])
//						{
//							pixelID [i - 1, j] = pixelID [i, j];
//						}
//
//						if (pixelRight.r != 0 && pixelID [i + 1, j] != pixelID [i, j])
//						{
//							pixelID [i + 1, j] = pixelID [i, j];
//						}
//
//						if (pixelDownLeft.r != 0 && pixelID [i - 1, j - 1] != pixelID [i, j])
//						{
//							pixelID [i - 1, j - 1] = pixelID [i, j];
//						}
//
//						if (pixelDownRight.r != 0 && pixelID [i + 1, j - 1] != pixelID [i, j])
//						{
//							pixelID [i + 1, j - 1] = pixelID [i, j];
//						}
//
//						if (pixelUpLeft.r != 0 && pixelID [i - 1, j + 1] != pixelID [i, j])
//						{
//							pixelID [i - 1, j + 1] = pixelID [i, j];
//						}
//
//						if (pixelUpRight.r != 0 && pixelID [i + 1, j + 1] != pixelID [i, j])
//						{
//							pixelID [i + 1, j + 1] = pixelID [i, j];
//						}
//					}	
//				}
//			}
//		}
//
//		for (i = whitePicture.width - 50; i > 0; i--)
//		{
//			for (j = whitePicture.height - 50; j > 0; j--)
//			{
//				Color pixel = whitePicture.GetPixel (i, j); // This is our current pixel (you know this one)
//				Color pixelLeft = whitePicture.GetPixel (i - 1, j); // This pixel is situated one column before the current pixel
//				Color pixelRight = whitePicture.GetPixel (i + 1, j); // This pixel is situated one column after the current pixel
//				Color pixelDown = whitePicture.GetPixel (i, j - 1); // This pixel is situated one line under the current pixel
//				Color pixelUp = whitePicture.GetPixel (i, j + 1); // This pixel is situated one line above the current pixel
//				Color pixelUpRight = whitePicture.GetPixel (i + 1, j + 1);
//				Color pixelUpLeft = whitePicture.GetPixel (i - 1, j + 1);
//				Color pixelDownLeft = whitePicture.GetPixel (i - 1, j - 1);
//				Color pixelDownRight = whitePicture.GetPixel (i + 1, j - 1);
//
//				if (pixel == Color.red)
//				{
//					if (pixelDown.r != 0 && pixelID [i, j - 1] != pixelID [i, j]) // If the pixel above the current red pixel is red too, then...
//					{
//						pixelID [i, j - 1] = pixelID [i, j];
//					} 
//
//					if (pixelUp.r != 0 && pixelID [i, j + 1] != pixelID [i, j]) // If the pixel above the current red pixel is red too, then...
//					{
//						pixelID [i, j + 1] = pixelID [i, j];
//					} 
//
//					if (pixelLeft.r != 0 && pixelID [i - 1, j] != pixelID [i, j])
//					{
//						pixelID [i - 1, j] = pixelID [i, j];
//					}
//
//					if (pixelRight.r != 0 && pixelID [i + 1, j] != pixelID [i, j])
//					{
//						pixelID [i + 1, j] = pixelID [i, j];
//					}
//
//					if (pixelDownLeft.r != 0 && pixelID [i - 1, j - 1] != pixelID [i, j])
//					{
//						pixelID [i - 1, j - 1] = pixelID [i, j];
//					}
//
//					if (pixelDownRight.r != 0 && pixelID [i + 1, j - 1] != pixelID [i, j])
//					{
//						pixelID [i + 1, j - 1] = pixelID [i, j];
//					}
//
//					if (pixelUpLeft.r != 0 && pixelID [i - 1, j + 1] != pixelID [i, j])
//					{
//						pixelID [i - 1, j + 1] = pixelID [i, j];
//					}
//
//					if (pixelUpRight.r != 0 && pixelID [i + 1, j + 1] != pixelID [i, j])
//					{
//						pixelID [i + 1, j + 1] = pixelID [i, j];
//					}
//				}	
//			}
//		}
//
//		for (i = 0; i < whitePicture.width; i++) 
//		{
//			for (j = 0; j < whitePicture.height; j++) 
//			{
//				Color pixel = whitePicture.GetPixel (i, j);
//
//				if (pixel.r != 0) 
//				{
//					nID [pixelID [i, j]]++;
//				}	
//			}
//		}
//
//		// Now we look for the ID which is the most common among all red pixels
//		for (k = 1; k < nID.Length; k++) 
//		{
//			if (nID [k] > max) 
//			{
//				max = nID [k];
//				maxID = k;
//			}
//		}
//
//		//Debug.Log ("max : " + max);
//		//Debug.Log ("maxID : " + maxID);
//
//		// Finally we check if the current red pixel has the ID 'maxID' and if yes, it means that it is a red pixel from the screens
//		// And if not, we set this pixel to black because it's not part of the screens
//		for (i = 0; i < whitePicture.width; i++) 
//		{
//			for (j = 0; j < whitePicture.height; j++) 
//			{
//				if (pixelID [i, j] != maxID) 
//				{
//					whitePicture.SetPixel (i, j, Color.black);
//				} 
//			}
//		}
//	}
//
////*************************************************************************************************************************************************************************************************
//
//	public void FindCorners()
//	{
//		int[,] upMiddleCorners = new int[2, 100];
//		int[,] downMiddleCorners = new int[2, 100];
//		int nUpMiddleCorner = 0, nBorderRedPixel = 0, nDownMiddleCorner = 0;
//		int positionState = 0, downPositionState = 0, upPositionState = 0, previousState = 0, downPreviousState = 0, upPreviousState = 0;
//		int x1 = 0, x2 = 0, x3 = 0, x4 = 0, y1 = 0, y2 = 0, y3 = 0, y4 = 0, xx1 = 0, xx2 = 0, xx3 = 0, xx4 = 0, yy1 = 0, yy2 = 0, yy3 = 0, yy4 = 0;
//		float slope1, slope2, slope3, deltax1 = 0f, deltay1 = 0f, deltax2 = 0f, deltay2 = 0f, deltax3 = 0f, deltay3 = 0f;
//		float slope11, slope22, slope33, deltaxx1 = 0f, deltayy1 = 0f, deltaxx2 = 0f, deltayy2 = 0f, deltaxx3 = 0f, deltayy3 = 0f;
//		float middleCornerWidth, middleCornerHeight; 
//
//		for (j = 0; j < whitePicture.height; j++) 
//		{
//			nBorderRedPixel = 0;
//			positionState = previousState;
//			positionState++;
//
//			for (i = 0; i < whitePicture.width / 2; i++) 
//			{
//				Color pixel = whitePicture.GetPixel (i, j); 
//				Color pixelLeft = whitePicture.GetPixel (i - 1, j); 
//				Color pixelDown = whitePicture.GetPixel (i, j - 1); 
//				Color pixelUp = whitePicture.GetPixel (i, j + 1); 
//
//				if (pixel.r == 1 && pixelLeft.r == 0)
//				{
//					nBorderRedPixel++;
//				}	
//
//				if ((pixel.r == 1 && pixelLeft.r == 0 && pixelUp.r == 0) || (pixel.r == 1 && pixelLeft.r == 0 && pixelDown.r == 0))
//				{
//					switch (positionState)
//					{
//					case 1:
//
//						if (nBorderRedPixel == 1)
//						{	
//							positionState = 6;
//							previousState = 1;
//							x1 = i;
//							y1 = j;
//						}
//						break;
//
//					case 2:
//
//						if (nBorderRedPixel == 1)
//						{	
//							positionState = 6;
//							previousState = 2;
//							x2 = i;
//							y2 = j;
//						}
//						break;
//
//					case 3:
//
//						if (nBorderRedPixel == 1)
//						{	
//							positionState = 6;
//							previousState = 3;
//							x3 = i;
//							y3 = j;
//						}
//						break;
//
//					case 4:
//
//						if (nBorderRedPixel == 1)
//						{	
//							positionState = 7;
//							previousState = 6;
//							x4 = i;
//							y4 = j;
//						}
//						break;
//
//					case 5:
//
//						if (nBorderRedPixel == 1)
//						{	
//							positionState = 7;
//							previousState = 6;
//							x1 = x2;
//							y1 = y2;
//							x2 = x3;
//							y2 = y3;
//							x3 = x4;
//							y3 = y4;
//							x4 = i;
//							y4 = j;
//						}
//						break;
//
//					case 6:
//						break;
//					}
//				}
//
//				if (positionState == 7)
//				{
//					positionState = 6;
//					previousState = 4;
//					deltax1 = x2 - x1;
//					deltay1 = y2 - y1;
//					deltax2 = x3 - x2;
//					deltay2 = y3 - y2;
//					deltax3 = x4 - x3;
//					deltay3 = y4 - y3;
//
//					if (deltax1 == 0f)
//					{
//						deltax1 = 0.01f;
//					}	
//
//					if (deltax2 == 0f)
//					{
//						deltax2 = 0.01f;
//					}
//
//					if (deltax3 == 0f)
//					{
//						deltax3 = 0.01f;
//					}
//
//					slope1 = deltay1 / deltax1;
//					slope2 = deltay2 / deltax2;
//					slope3 = deltay3 / deltax3;
//
//					if ((slope1 < 0f && slope1 >= -2f && slope2 < 0f && slope2 >= -2f && slope3 > 100f) || (slope1 < 0f && slope1 >= -2f && slope2 < 0f && slope2 >= -2f && slope3 < -2f))
//					{
//						whitePicture.SetPixel (x3, y3, Color.green);
//						corners [0, 0] = x3;
//						corners [1, 0] = y3;
//					}
//
//					if ((slope1 > 100f && slope2 > 0f && slope2 <= 2f && slope3 > 0f && slope3 <= 2f) || (slope1 > 2f && slope2 > 0f && slope2 <= 2f && slope3 > 0f && slope3 <= 2f))
//					{
//						whitePicture.SetPixel (x2, y2, Color.green);
//						corners [0, 1] = x2;
//						corners [1, 1] = y2;
//						previousState = 0;
//						break;
//					}
//				}
//			}
//		}
//
//		for (j = whitePicture.height; j > 0; j--) 
//		{
//			nBorderRedPixel = 0;
//			positionState = previousState;
//			positionState++;
//
//			for (i = whitePicture.width; i > whitePicture.width / 2; i--) 
//			{
//				Color pixel = whitePicture.GetPixel (i, j); 
//				Color pixelRight = whitePicture.GetPixel (i + 1, j); 
//				Color pixelDown = whitePicture.GetPixel (i, j - 1); 
//				Color pixelUp = whitePicture.GetPixel (i, j + 1); 
//
//				if (pixel.r == 1 && pixelRight.r == 0)
//				{
//					nBorderRedPixel++;
//				}	
//
//				if ((pixel.r == 1 && pixelRight.r == 0 && pixelUp.r == 0) || (pixel.r == 1 && pixelRight.r == 0 && pixelDown.r == 0))
//				{
//					switch (positionState)
//					{
//					case 1:
//
//						if (nBorderRedPixel == 1)
//						{	
//							positionState = 6;
//							previousState = 1;
//							x1 = i;
//							y1 = j;
//						}
//						break;
//
//					case 2:
//
//						if (nBorderRedPixel == 1)
//						{	
//							positionState = 6;
//							previousState = 2;
//							x2 = i;
//							y2 = j;
//						}
//						break;
//
//					case 3:
//
//						if (nBorderRedPixel == 1)
//						{	
//							positionState = 6;
//							previousState = 3;
//							x3 = i;
//							y3 = j;
//						}
//						break;
//
//					case 4:
//
//						if (nBorderRedPixel == 1)
//						{	
//							positionState = 7;
//							previousState = 6;
//							x4 = i;
//							y4 = j;
//						}
//						break;
//
//					case 5:
//
//						if (nBorderRedPixel == 1)
//						{	
//							positionState = 7;
//							previousState = 6;
//							x1 = x2;
//							y1 = y2;
//							x2 = x3;
//							y2 = y3;
//							x3 = x4;
//							y3 = y4;
//							x4 = i;
//							y4 = j;
//						}
//						break;
//
//					case 6:
//						break;
//					}
//				}
//
//				if (positionState == 7)
//				{
//					positionState = 6;
//					previousState = 4;
//					deltax1 = x2 - x1;
//					deltay1 = y2 - y1;
//					deltax2 = x3 - x2;
//					deltay2 = y3 - y2;
//					deltax3 = x4 - x3;
//					deltay3 = y4 - y3;
//
//					if (deltax1 == 0f)
//					{
//						deltax1 = 0.01f;
//					}	
//
//					if (deltax2 == 0f)
//					{
//						deltax2 = 0.01f;
//					}
//
//					if (deltax3 == 0f)
//					{
//						deltax3 = 0.01f;
//					}
//
//					slope1 = deltay1 / deltax1;
//					slope2 = deltay2 / deltax2;
//					slope3 = deltay3 / deltax3;
//
//					if ((slope1 < 0f && slope1 >= -2f && slope2 < 0f && slope2 >= -2f && slope3 > 100f) || (slope1 < 0f && slope1 >= -2f && slope2 < 0f && slope2 >= -2f && slope3 < -2f))
//					{
//						whitePicture.SetPixel (x3, y3, Color.green);
//						corners [0, 6] = x3;
//						corners [1, 6] = y3;
//					}
//
//					if ((slope1 > 100f && slope2 > 0f && slope2 <= 2f && slope3 > 0f && slope3 <= 2f) || (slope1 > 2f && slope2 > 0f && slope2 <= 2f && slope3 > 0f && slope3 <= 2f))
//					{
//						whitePicture.SetPixel (x2, y2, Color.green);
//						corners [0, 7] = x2;
//						corners [1, 7] = y2;
//						previousState = 0;
//						break;
//					}
//				}
//			}
//		}
//
//		for (i = corners [0, 0] + 10; i < corners [0, 7] - 10; i++) 
//		{
//			nBorderRedPixel = 0;
//			downPositionState = downPreviousState;
//			downPositionState++;
//			upPositionState = upPreviousState;
//			upPositionState++;
//
//			for (j = 0; j < whitePicture.height; j++) 
//			{
//				Color pixel = whitePicture.GetPixel (i, j); 
//				Color pixelLeft = whitePicture.GetPixel (i - 1, j); 
//				Color pixelRight = whitePicture.GetPixel (i + 1, j); 
//				Color pixelDown = whitePicture.GetPixel (i, j - 1); 
//				Color pixelUp = whitePicture.GetPixel (i, j + 1); 
//
//				if ((pixel.r == 1 && pixelDown.r == 0) || (pixel.r == 1 && pixelUp.r == 0))
//				{
//					nBorderRedPixel++;
//				}	
//
//				if ((pixel.r == 1 && pixelLeft.r == 0 && pixelDown.r == 0) || (pixel.r == 1 && pixelRight.r == 0 && pixelDown.r == 0))
//				{
//					switch (downPositionState)
//					{
//					case 1:
//
//						if (nBorderRedPixel == 1)
//						{	
//							downPositionState = 6;
//							downPreviousState = 1;
//							x1 = i;
//							y1 = j;
//							//whitePicture.SetPixel (i, j, Color.blue);
//						}
//						break;
//
//					case 2:
//
//						if (nBorderRedPixel == 1)
//						{	
//							downPositionState = 6;
//							downPreviousState = 2;
//							x2 = i;
//							y2 = j;
//							//whitePicture.SetPixel (i, j, Color.blue);
//						}		
//						break;
//
//					case 3:
//
//						if (nBorderRedPixel == 1)
//						{	
//							downPositionState = 6;
//							downPreviousState = 3;
//							x3 = i;
//							y3 = j;
//							//whitePicture.SetPixel (i, j, Color.blue);
//						}
//						break;
//
//					case 4:
//
//						if (nBorderRedPixel == 1)
//						{
//							downPositionState = 7;
//							downPreviousState = 6;
//							x4 = i;
//							y4 = j;
//							//whitePicture.SetPixel (i, j, Color.blue);
//						}		
//						break;
//
//					case 5:
//
//						if (nBorderRedPixel == 1)
//						{	
//							downPositionState = 7;
//							downPreviousState = 6;
//							x1 = x2;
//							y1 = y2;
//							x2 = x3;
//							y2 = y3;
//							x3 = x4;
//							y3 = y4;
//							x4 = i;
//							y4 = j;
//							//whitePicture.SetPixel (i, j, Color.magenta);
//						}
//						break;
//
//					case 6:
//						break;
//					}
//				}
//
//				if (downPositionState == 7)
//				{
//					downPositionState = 6;
//					downPreviousState = 4;
//					deltax1 = x2 - x1;
//					deltay1 = y2 - y1;
//					deltax2 = x3 - x2;
//					deltay2 = y3 - y2;
//					deltax3 = x4 - x3;
//					deltay3 = y4 - y3;
//
//					if (deltax1 == 0f)
//					{
//						deltax1 = 0.01f;
//					}	
//
//					if (deltax2 == 0f)
//					{
//						deltax2 = 0.01f;
//					}
//
//					if (deltax3 == 0f)
//					{
//						deltax3 = 0.01f;
//					}
//
//					slope1 = deltay1 / deltax1;
//					slope2 = deltay2 / deltax2;
//					slope3 = deltay3 / deltax3;
//
//					if (slope1 > 0f && slope2 == 0f && slope3 < 0f)
//					{
//						downMiddleCorners [0, nDownMiddleCorner] = x2 + ((x3 - x2) / 2);
//						downMiddleCorners [1, nDownMiddleCorner] = y2 + 1;
//						whitePicture.SetPixel (x2 + ((x3 - x2) / 2), y2 + 1, Color.white);
//						whitePicture.SetPixel (x2 + ((x3 - x2) / 2) + 1, y2 + 1, Color.white);
//						nDownMiddleCorner++;
//					}
//					else
//					{	
//						if (slope1 > 0f && slope2 < 0f && slope3 < 0f)
//						{	
//							downMiddleCorners [1, nDownMiddleCorner] = y2 + 1;
//
//							if (x3 - x2 >= x2 - x1)
//							{
//								downMiddleCorners [0, nDownMiddleCorner] = x2 + ((x3 - x2) / 2);
//								whitePicture.SetPixel (x2 + ((x3 - x2) / 2), y2 + 1, Color.white);
//								whitePicture.SetPixel (x2 + ((x3 - x2) / 2) + 1, y2 + 1, Color.white);
//							}
//							else
//							{
//								downMiddleCorners [0, nDownMiddleCorner] = x1 + ((x2 - x1) / 2);
//								whitePicture.SetPixel (x1 + ((x2 - x1) / 2), y2 + 1, Color.white);
//								whitePicture.SetPixel (x1 + ((x2 - x1) / 2) + 1, y2 + 1, Color.white);
//							}
//
//							nDownMiddleCorner++;
//						}
//					}
//				}
//
//				if ((pixel.r == 1 && pixelLeft.r == 0 && pixelUp.r == 0) || (pixel.r == 1 && pixelRight.r == 0 && pixelUp.r == 0))
//				{
//					switch (upPositionState)
//					{
//					case 1:
//
//						if (nBorderRedPixel == 2)
//						{	
//							upPositionState = 6;
//							upPreviousState = 1;
//							xx1 = i;
//							yy1 = j;
//							//whitePicture.SetPixel (i, j, Color.blue);
//						}
//						break;
//
//					case 2:
//
//						if (nBorderRedPixel == 2)
//						{	
//							upPositionState = 6;
//							upPreviousState = 2;
//							xx2 = i;
//							yy2 = j;
//							//whitePicture.SetPixel (i, j, Color.blue);
//						}		
//						break;
//
//					case 3:
//
//						if (nBorderRedPixel == 2)
//						{	
//							upPositionState = 6;
//							upPreviousState = 3;
//							xx3 = i;
//							yy3 = j;
//							//whitePicture.SetPixel (i, j, Color.blue);
//						}
//						break;
//
//					case 4:
//
//						if (nBorderRedPixel == 2)
//						{
//							upPositionState = 7;
//							upPreviousState = 6;
//							xx4 = i;
//							yy4 = j;
//							//whitePicture.SetPixel (i, j, Color.blue);
//						}		
//						break;
//
//					case 5:
//
//						if (nBorderRedPixel == 2)
//						{	
//							upPositionState = 7;
//							upPreviousState = 6;
//							xx1 = xx2;
//							yy1 = yy2;
//							xx2 = xx3;
//							yy2 = yy3;
//							xx3 = xx4;
//							yy3 = yy4;
//							xx4 = i;
//							yy4 = j;
//							//whitePicture.SetPixel (i, j, Color.yellow);
//						}
//						break;
//
//					case 6:
//						break;
//					}
//				}
//
//				if(upPositionState == 7)
//				{	
//					upPositionState = 6;
//					upPreviousState = 4;
//					deltaxx1 = xx2 - xx1;
//					deltayy1 = yy2 - yy1;
//					deltaxx2 = xx3 - xx2;
//					deltayy2 = yy3 - yy2;
//					deltaxx3 = xx4 - xx3;
//					deltayy3 = yy4 - yy3;
//
//					if (deltaxx1 == 0f)
//					{
//						deltaxx1 = 0.01f;
//					}	
//
//					if (deltaxx2 == 0f)
//					{
//						deltaxx2 = 0.01f;
//					}
//
//					if (deltaxx3 == 0f)
//					{
//						deltaxx3 = 0.01f;
//					}
//
//					slope11 = deltayy1 / deltaxx1;
//					slope22 = deltayy2 / deltaxx2;
//					slope33 = deltayy3 / deltaxx3;
//
//					if (slope11 < 0f && slope22 == 0f && slope33 > 0f)
//					{
//						upMiddleCorners [0, nUpMiddleCorner] = xx2 + ((xx3 - xx2) / 2);
//						upMiddleCorners [1, nUpMiddleCorner] = yy2 - 1;
//						whitePicture.SetPixel (xx2 + ((xx3 - xx2) / 2), yy2 - 1, Color.white);
//						whitePicture.SetPixel (xx2 + ((xx3 - xx2) / 2) + 1, yy2 - 1, Color.white);
//						nUpMiddleCorner++;
//					}
//					else
//					{	
//						if(slope11 < 0f && slope22 > 0f && slope33 > 0f)
//						{	
//							upMiddleCorners [1, nUpMiddleCorner] = yy2 - 1;
//
//							if (xx3 - xx2 >= xx2 - xx1)
//							{
//								upMiddleCorners [0, nUpMiddleCorner] = xx2 + ((xx3 - xx2) / 2);
//								whitePicture.SetPixel (xx2 + ((xx3 - xx2) / 2), yy2 - 1, Color.white);
//								whitePicture.SetPixel (xx2 + ((xx3 - xx2) / 2) + 1, yy2 - 1, Color.white);
//							} 
//							else
//							{
//								upMiddleCorners [0, nUpMiddleCorner] = xx1 + ((xx2 - xx1) / 2);
//								whitePicture.SetPixel (xx1 + ((xx2 - xx1) / 2), yy2 - 1, Color.white);
//								whitePicture.SetPixel (xx1 + ((xx2 - xx1) / 2) + 1, yy2 - 1, Color.white);
//							}
//
//							nUpMiddleCorner++;
//						}
//					}
//				}
//			}
//		}
//
//		float[,] middleCornerHyp = new float[nUpMiddleCorner + 1, nDownMiddleCorner + 1];
//		float[,] compareCornerHyp = new float[nUpMiddleCorner + 1, nDownMiddleCorner + 1];
//		float minMiddleCornerHyp1 = float.MaxValue, minMiddleCornerHyp2 = float.MaxValue, hypReference, hypReferenceWidth, hypReferenceHeight;
//		int minMiddleCornerU1 = 0, minMiddleCornerU2 = 0, minMiddleCornerD1 = 0, minMiddleCornerD2 = 0;
//
//		hypReferenceHeight = corners [1, 1] - corners [1, 0];
//		hypReferenceWidth = corners [0, 1] - corners [0, 0];
//
//		if (hypReferenceWidth == 0f)
//		{
//			hypReferenceWidth = 0.01f;
//		}	
//
//		hypReference = Mathf.Sqrt (Mathf.Pow (hypReferenceWidth, 2) + Mathf.Pow (hypReferenceHeight, 2));
//
//		for (u = 0; u < nUpMiddleCorner + 1; u++)
//		{
//			for (d = 0; d < nDownMiddleCorner + 1; d++)
//			{
//				if (upMiddleCorners [1, u] != 0 && downMiddleCorners [1, d] != 0 && upMiddleCorners [0, u] != 0 && downMiddleCorners [0, d] != 0)
//				{	
//					middleCornerHeight = Mathf.Abs (upMiddleCorners [1, u] - downMiddleCorners [1, d]);
//					middleCornerWidth = Mathf.Abs (upMiddleCorners [0, u] - downMiddleCorners [0, d]);
//
//					if (middleCornerWidth == 0f)
//					{
//						middleCornerWidth = 0.01f;
//					}	
//
//					middleCornerHyp [u, d] = Mathf.Sqrt (Mathf.Pow (middleCornerWidth, 2) + Mathf.Pow (middleCornerHeight, 2));
//					compareCornerHyp [u, d] = Mathf.Abs (middleCornerHyp [u, d] - hypReference);
//				}
//			}	
//		}
//
//		for (u = 0; u < nUpMiddleCorner + 1; u++)
//		{
//			for (d = 0; d < nDownMiddleCorner + 1; d++)
//			{
//				if (compareCornerHyp [u, d] < minMiddleCornerHyp1 && compareCornerHyp [u, d] != 0)
//				{
//					minMiddleCornerHyp2 = minMiddleCornerHyp1;
//					minMiddleCornerU2 = minMiddleCornerU1;
//					minMiddleCornerD2 = minMiddleCornerD1;
//
//					minMiddleCornerHyp1 = compareCornerHyp [u, d];
//					minMiddleCornerU1 = u;
//					minMiddleCornerD1 = d;
//				}
//				else
//				{
//					if (compareCornerHyp [u, d] < minMiddleCornerHyp2 && compareCornerHyp [u, d] != 0)
//					{
//						minMiddleCornerHyp2 = compareCornerHyp [u, d];
//						minMiddleCornerU2 = u;
//						minMiddleCornerD2 = d;
//					}	
//				}	
//			}	
//		}
//
//		if (upMiddleCorners [0, minMiddleCornerU1] > upMiddleCorners [0, minMiddleCornerU2])
//		{
//			corners [0, 2] = upMiddleCorners [0, minMiddleCornerU2];
//			corners [0, 3] = upMiddleCorners [0, minMiddleCornerU2] + 1;
//			corners [1, 2] = upMiddleCorners [1, minMiddleCornerU2];
//			corners [1, 3] = upMiddleCorners [1, minMiddleCornerU2];
//
//			corners [0, 4] = upMiddleCorners [0, minMiddleCornerU1];
//			corners [0, 5] = upMiddleCorners [0, minMiddleCornerU1] + 1;
//			corners [1, 4] = upMiddleCorners [1, minMiddleCornerU1];
//			corners [1, 5] = upMiddleCorners [1, minMiddleCornerU1];
//		}
//		else
//		{
//			corners [0, 2] = upMiddleCorners [0, minMiddleCornerU1];
//			corners [0, 3] = upMiddleCorners [0, minMiddleCornerU1] + 1;
//			corners [1, 2] = upMiddleCorners [1, minMiddleCornerU1];
//			corners [1, 3] = upMiddleCorners [1, minMiddleCornerU1];
//
//			corners [0, 4] = upMiddleCorners [0, minMiddleCornerU2];
//			corners [0, 5] = upMiddleCorners [0, minMiddleCornerU2] + 1;
//			corners [1, 4] = upMiddleCorners [1, minMiddleCornerU2];
//			corners [1, 5] = upMiddleCorners [1, minMiddleCornerU2];
//		}
//
//		if (upMiddleCorners [0, minMiddleCornerD1] > upMiddleCorners [0, minMiddleCornerD2])
//		{
//			corners [0, 9] = downMiddleCorners [0, minMiddleCornerD1];
//			corners [0, 8] = downMiddleCorners [0, minMiddleCornerD1] + 1;
//			corners [1, 9] = downMiddleCorners [1, minMiddleCornerD1];
//			corners [1, 8] = downMiddleCorners [1, minMiddleCornerD1];
//
//			corners [0, 11] = downMiddleCorners [0, minMiddleCornerD2];
//			corners [0, 10] = downMiddleCorners [0, minMiddleCornerD2] + 1;
//			corners [1, 11] = downMiddleCorners [1, minMiddleCornerD2];
//			corners [1, 10] = downMiddleCorners [1, minMiddleCornerD2];
//		}
//		else
//		{
//			corners [0, 9] = downMiddleCorners [0, minMiddleCornerD2];
//			corners [0, 8] = downMiddleCorners [0, minMiddleCornerD2] + 1;
//			corners [1, 9] = downMiddleCorners [1, minMiddleCornerD2];
//			corners [1, 8] = downMiddleCorners [1, minMiddleCornerD2];
//
//			corners [0, 11] = downMiddleCorners [0, minMiddleCornerD1];
//			corners [0, 10] = downMiddleCorners [0, minMiddleCornerD1] + 1;
//			corners [1, 11] = downMiddleCorners [1, minMiddleCornerD1];
//			corners [1, 10] = downMiddleCorners [1, minMiddleCornerD1];
//		}
//	}
//
////*************************************************************************************************************************************************************************************************
//
//	public void TakeCrosshairsPicture()
//	{
//		SetUpLiveScript setUpLiveScript = GameObject.Find ("Auto-Calib-CIAB/Main Screen").GetComponent<SetUpLiveScript> (); // We retrieve the class "SetUpLiveScript" inside a variable 
//		Rect newSourceRect = setUpLiveScript.sourceRect;
//
//		newWebCamTexture.Pause (); // We pause the live of camera
//
//		// We set all our useful variables for this functions
//		int x = Mathf.FloorToInt (newSourceRect.x); // This variable contains the 'X' coordonate 
//		int y = Mathf.FloorToInt (newSourceRect.y); // This variable contains the 'Y' coordonate  
//		int width = Mathf.FloorToInt (newSourceRect.width); // This variable 
//		int height = Mathf.FloorToInt (newSourceRect.height);
//
//		Color[] crosshairsPixels = newWebCamTexture.GetPixels (x, y, width, height); // We save all the pixels from the paused live into an array calls "pixels"
//		crosshairsPicture = new Texture2D (width, height); // We create a new 2D texture with the same dimensions of the live display
//		crosshairsPicture.SetPixels (crosshairsPixels); // We apply all the pixels from the array "pixels" onto this 2D texture
//		//GameObject.Find ("Auto-Calib-CIAB-Object/Main Screen").GetComponent<Renderer>().material.mainTexture = picture; // We set on the main texture of the 'Main Screen' object the new 2D texture that the screenshot of the live  
//
//		newWebCamTexture.Play ();
//	}
//
////*************************************************************************************************************************************************************************************************
//
//	public void FindCrosshairs ()
//	{
//		for (i = corners [0, 0]; i < corners [0, 7]; i++) // x-axis
//		{
//			for (j = corners [1, 0]; j < corners [1, 1]; j++) // y-axis
//			{
//				Color crosshairsPixel = crosshairsPicture.GetPixel (i, j);
//				Color newcrosshairsPixel = new Vector4 (((crosshairsPixel.r - 0.5f) * 10f), ((crosshairsPixel.g - 0.5f) * 10f), ((crosshairsPixel.b - 0.5f) * 10f) , 1);
//				redPicture.SetPixel (i, j, newcrosshairsPixel);
//			}
//		}
//
//		for (i = corners [0, 0]; i < corners [0, 7]; i++) // x-axis
//		{
//			for (j = corners [1, 0]; j < corners [1, 1]; j++) // y-axis
//			{
//				Color crosshairsPixel = crosshairsPicture.GetPixel (i, j);
//				float gray = (0.299f * crosshairsPixel.r + 0.587f * crosshairsPixel.g + 0.114f * crosshairsPixel.b);
//				Color grayColor = new Vector4 (gray, gray, gray, 1);
//				crosshairsPicture.SetPixel (i, j, grayColor);
//
//				Color newCrosshairsPixel = crosshairsPicture.GetPixel (i, j);
//
//				if (newCrosshairsPixel.grayscale != 0f)
//				{
//					crosshairsPicture.SetPixel (i, j, Color.red);
//				}
//				else
//				{
//					crosshairsPicture.SetPixel (i, j, Color.black);
//				}
//			}
//		}
//	}
//
//	public void CalculateOffset ()
//	{
//		bool crossPartDetected = false;
//		int[,] crosshair = new int[2, 6];
//		int xCrossMin = 0, xCrossMax = 0, yCrossMin = 0, crosshairID = 0, firstCrossPixelHeight = 0, secondCrossPixelHeight = 0; 
//		int xMiddleScreen1 = 0, yMiddleScreen1 = 0, xMiddleScreen2 = 0, yMiddleScreen2 = 0, xMiddleScreen3 = 0, yMiddleScreen3 = 0; 
//		int xMiddleOffset1 = 0, yMiddleOffset1 = 0, xMiddleOffset2 = 0, yMiddleOffset2 = 0, xMiddleOffset3 = 0, yMiddleOffset3 = 0;
//	
//		for (i = corners [0, 0]; i < corners [0, 7]; i++) // x-axis
//		{
//			for (j = corners [1, 0]; j < corners [1, 1]; j++) // y-axis
//			{
//				// We set our useful pixels 
//				Color crosshairsPixel = crosshairsPicture.GetPixel (i, j); // This is our current pixel (you know this one)
//				Color crosshairsPixelDown = crosshairsPicture.GetPixel (i, j - 1); // This pixel is situated one line under the current pixel
//
//				if (crosshairsPixel.r == 1 && crosshairsPixelDown.r == 0)
//				{
//					firstCrossPixelHeight = secondCrossPixelHeight;
//					secondCrossPixelHeight = j;
//
//					if (secondCrossPixelHeight - firstCrossPixelHeight < -4 && firstCrossPixelHeight != 0)
//					{
//						crossPartDetected = true;
//						xCrossMin = i;
//						yCrossMin = firstCrossPixelHeight;
//					}
//
//					if (secondCrossPixelHeight - firstCrossPixelHeight > 4 && firstCrossPixelHeight != 0 && crossPartDetected == true)
//					{
//						crossPartDetected = false;
//
//						xCrossMax = i;
//
//						crosshair [0, crosshairID] = xCrossMin + ((xCrossMax - xCrossMin) / 2);
//						crosshair [1, crosshairID] = yCrossMin + ((xCrossMax - xCrossMin) / 2);
//
//						crosshairsPicture.SetPixel (crosshair [0, crosshairID], crosshair [1, crosshairID], Color.white);
//
//						crosshairID++;
//					}	
//				}
//			}
//		}
//
//		xMiddleScreen1 = corners [0, 0] + ((corners [0, 11] - corners [0, 0]) / 2);
//		yMiddleScreen1 = corners [1, 0] + ((corners [1, 1] - corners [1, 0]) / 2);
//
//		xMiddleScreen2 = corners [0, 10] + ((corners [0, 9] - corners [0, 10]) / 2);
//		yMiddleScreen2 = corners [1, 10] + ((corners [1, 3] - corners [1, 10]) / 2);
//
//		xMiddleScreen3 = corners [0, 8] + ((corners [0, 7] - corners [0, 8]) / 2);
//		yMiddleScreen3 = corners [1, 8] + ((corners [1, 5] - corners [1, 8]) / 2);
//
//		xMiddleOffset1 = crosshair [0, 0] - xMiddleScreen1;
//		yMiddleOffset1 = crosshair [1, 0] - yMiddleScreen1;
//
//		xMiddleOffset2 = crosshair [0, 1] - xMiddleScreen2;
//		yMiddleOffset2 = crosshair [1, 1] - yMiddleScreen2;
//
//		xMiddleOffset3 = crosshair [0, 2] - xMiddleScreen3;
//		yMiddleOffset3 = crosshair [1, 2] - yMiddleScreen3;
//
//		crosshair [0, 0] -= xMiddleOffset1;
//		crosshair [1, 0] -= yMiddleOffset1;
//	
//		crosshair [0, 1] -= xMiddleOffset2;
//		crosshair [1, 1] -= yMiddleOffset2;
//
//		crosshair [0, 2] -= xMiddleOffset3;
//		crosshair [1, 2] -= yMiddleOffset3;
//	}
//
////*************************************************************************************************************************************************************************************************
//
//	public void CalculateRatio()
//	{
//		bool crossPartDetected = false;
//		int[,] crosshair = new int[2, 6];
//		int xCrossMin = 0, xCrossMax = 0, yCrossMin = 0, crosshairID = 0, firstCrossPixelHeight = 0, secondCrossPixelHeight = 0; 
//		int xMiddleScreen1 = 0, yMiddleScreen1 = 0, xMiddleScreen2 = 0, yMiddleScreen2 = 0, xMiddleScreen3 = 0, yMiddleScreen3 = 0; 
//		int xMiddleOffset1 = 0, yMiddleOffset1 = 0, xMiddleOffset2 = 0, yMiddleOffset2 = 0, xMiddleOffset3 = 0, yMiddleOffset3 = 0;
//		int leftScaleRef1 = 0, upScaleRef1 = 0, leftScaleRef2 = 0, upScaleRef2 = 0, leftScaleRef3 = 0, upScaleRef3 = 0;
//
//		for (i = corners [0, 0]; i < corners [0, 7]; i++) // x-axis
//		{
//			for (j = corners [1, 0]; j < corners [1, 1]; j++) // y-axis
//			{
//				// We set our useful pixels 
//				Color crosshairsPixel = crosshairsPicture.GetPixel (i, j); // This is our current pixel (you know this one)
//				Color crosshairsPixelDown = crosshairsPicture.GetPixel (i, j - 1); // This pixel is situated one line under the current pixel
//
//				if (crosshairsPixel.r == 1 && crosshairsPixelDown.r == 0)
//				{
//					firstCrossPixelHeight = secondCrossPixelHeight;
//					secondCrossPixelHeight = j;
//
//					if (secondCrossPixelHeight - firstCrossPixelHeight < -4 && firstCrossPixelHeight != 0)
//					{
//						crossPartDetected = true;
//						xCrossMin = i;
//						yCrossMin = firstCrossPixelHeight;
//					}
//
//					if (secondCrossPixelHeight - firstCrossPixelHeight > 4 && firstCrossPixelHeight != 0 && crossPartDetected == true)
//					{
//						crossPartDetected = false;
//
//						xCrossMax = i;
//
//						crosshair [0, crosshairID] = xCrossMin + ((xCrossMax - xCrossMin) / 2);
//						crosshair [1, crosshairID] = yCrossMin + ((xCrossMax - xCrossMin) / 2);
//
//						crosshairsPicture.SetPixel (crosshair [0, crosshairID], crosshair [1, crosshairID], Color.white);
//
//						crosshairID++;
//					}	
//				}
//			}
//		}
//
//		leftScaleRef1 = (xMiddleScreen1 - corners [0, 0]) / 2;
//		upScaleRef1 = (yMiddleScreen1 - corners [1, 0]) / 2;
//
//		leftScaleRef2 = (xMiddleScreen2 - corners [0, 10]) / 2;
//		upScaleRef2 = (yMiddleScreen2 - corners [1, 10]) / 2;
//
//		leftScaleRef3 = (xMiddleScreen3 - corners [0, 8]) / 2;
//		upScaleRef3 = (yMiddleScreen3 - corners [1, 8]) / 2;
//
//		crosshair [0, 0] -= xMiddleOffset1;
//		crosshair [1, 0] -= yMiddleOffset1;
//
//		crosshair [0, 1] -= xMiddleOffset1;
//		crosshair [1, 1] -= yMiddleOffset1;
//
//		crosshair [0, 2] -= xMiddleOffset2;
//		crosshair [1, 2] -= yMiddleOffset2;
//
//		crosshair [0, 3] -= xMiddleOffset2;
//		crosshair [1, 3] -= yMiddleOffset2;
//
//		crosshair [0, 4] -= xMiddleOffset3;
//		crosshair [1, 4] -= yMiddleOffset3;
//
//		crosshair [0, 5] -= xMiddleOffset3;
//		crosshair [1, 5] -= yMiddleOffset3;
//
//		leftRatio1 = leftScaleRef1 / crosshair [0, 0];
//		upRatio1 =  upScaleRef1 / crosshair [1, 1];
//
//		leftRatio2 = leftScaleRef2 / crosshair [0, 2];
//		upRatio2 = upScaleRef2 / crosshair [1, 3];
//
//		leftRatio3 = leftScaleRef3 / crosshair [0, 4];
//		upRatio3 = leftScaleRef3 / crosshair [1, 5];
//	}
}
