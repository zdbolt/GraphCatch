using UnityEngine;	
using System;
using System.Collections;

public class EyeCamera : MonoBehaviour {	
	private GameObject screen;

	//private float eyeSeparation = 0.065F;
    private float eyeSeparation = 0.5F;
    private float eyeOffset;

	public float nearClipPlane = 0.3f;
	public float farClipPlane = 1000.0f;
	
	void Start () {
		GetComponent<Camera>().farClipPlane  = farClipPlane;
		GetComponent<Camera>().nearClipPlane = nearClipPlane;
	
		eyeOffset = 0.5f * eyeSeparation;
		if (transform.parent.name.Contains("Left")) {
			eyeOffset *= -1;
		}
		
		configureCamera();
	}
		
	void OnPreCull () {
		updateCameraTransform();
		updateCameraProjection();
	}
	
	private void configureCamera() {
		char[] delimiters = {' '};
		String[] nameSplit = gameObject.name.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries);
				
		CameraData cameraData = GameObject.Find(nameSplit[0] + " CameraData").GetComponent<CameraData>();
		screen = GameObject.Find("CAVE in a Box/Screens/" + cameraData.screen + " Screen");
		
		// Set the CAVE Camera Display
		GetComponent<Camera>().targetDisplay = cameraData.display;
		GetComponent<Camera>().depth = 0;
	}
	
	/*
		Math - Works
	*/
	private void updateCameraTransform() {
        // Vector3 offset = eyeOffset * Vector3.right; //screen.transform.right;

        //Vector3 offset = transform.parent.InverseTransformVector(eyeOffset * Vector3.right);
        Vector3 screenForward = transform.parent.InverseTransformVector(screen.transform.forward);
		Vector3 screenUp      = transform.parent.InverseTransformVector(screen.transform.up);

        transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.LookRotation(screenForward, screenUp);
	}

	private void updateCameraProjection() {
		Transform screenMarker = screen.transform.Find(screen.name + " Marker");
		Vector2 screenSize = new Vector2(screenMarker.localScale[0], screenMarker.localScale[1]);
		
		// find eye position relative to screen
        Vector3 eye = screen.transform.InverseTransformPoint(transform.TransformPoint(Vector3.zero));
        // frustum planes through edges of the screen
        float left   = (-0.5f * screenSize.x - eye.x) * GetComponent<Camera>().nearClipPlane / -eye.z;
        float right  = ( 0.5f * screenSize.x - eye.x) * GetComponent<Camera>().nearClipPlane / -eye.z;
        float top    = ( 0.5f * screenSize.y - eye.y) * GetComponent<Camera>().nearClipPlane / -eye.z;
        float bottom = (-0.5f * screenSize.y - eye.y) * GetComponent<Camera>().nearClipPlane / -eye.z;

        GetComponent<Camera>().projectionMatrix =
            makeFrustum(left, right, top, bottom, GetComponent<Camera>().nearClipPlane, GetComponent<Camera>().farClipPlane);
	}

	private Matrix4x4 makeFrustum (
            float left, float right, float top, float bottom, float near, float far)
    {
        float x = 2.0f * near / (right - left);
        float y = 2.0f * near / (top - bottom);
        float a = (right + left) / (right - left);
        float b = (top + bottom) / (top - bottom);
        float c = -(far + near) / (far - near);
        float d = -(2.0f * far * near) / (far - near);
        float e = -1.0f;

        Matrix4x4 m = new Matrix4x4();
        m[0, 0] = x;
        m[0, 1] = 0;
        m[0, 2] = a;
        m[0, 3] = 0;

        m[1, 0] = 0;
        m[1, 1] = y;
        m[1, 2] = b;
        m[1, 3] = 0;

        m[2, 0] = 0;
        m[2, 1] = 0;
        m[2, 2] = c;
        m[2, 3] = d;

        m[3, 0] = 0;
        m[3, 1] = 0;
        m[3, 2] = e;
        m[3, 3] = 0;

        return m;
    }
}
