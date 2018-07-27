using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCamera : MonoBehaviour {
	public bool useDebugEyes = false;
	public bool monoMode = false;
	
	public int vertexResolution = 1;
	
	public Material leftEyeMaterial;
	public Material rightEyeMaterial;

	private Mesh screenMesh;
	[HideInInspector]
	public CameraData cameraData;

	void Start () {
		GetComponent<Camera>().projectionMatrix = Matrix4x4.identity;
		GetComponent<Camera>().worldToCameraMatrix = Matrix4x4.identity;

		configureCamera();
		BuildMesh();
	}

	public void updateViewport(CameraData.Viewport viewport) {
		cameraData.viewport = viewport;
		BuildMesh();
	}

	public void updateDisplay(int display) {
		cameraData.display = display;
		GetComponent<Camera>().targetDisplay = display;
		Debug.Log ("Camera " + this.name + " targeting display " + cameraData.display.ToString());
	}

	private void configureCamera() {
		char[] delimiters = {' '};
		string[] nameSplit = gameObject.name.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries);
		
		cameraData = GameObject.Find(nameSplit[0] + " CameraData").GetComponent<CameraData>();
		 
		if (useDebugEyes) {
			leftEyeMaterial = (Material) Resources.Load("Materials/Debug/DebugLeftEye", typeof(Material));
			rightEyeMaterial = (Material) Resources.Load("Materials/Debug/DebugRightEye", typeof(Material));
		}
		 
		// Set the CAVE Camera Display
		GetComponent<Camera>().targetDisplay = cameraData.display;
		GetComponent<Camera>().depth = 1;
	}

	void BuildMesh() {
		// Find Control Points of edges
		Vector2 l, r, t, b, m;
		l = Vector2.Lerp(cameraData.viewport.bl,cameraData.viewport.tl,cameraData.viewport.l);
		r = Vector2.Lerp(cameraData.viewport.br,cameraData.viewport.tr,cameraData.viewport.r);
		t = Vector2.Lerp(cameraData.viewport.tl,cameraData.viewport.tr,cameraData.viewport.t);
		b = Vector2.Lerp(cameraData.viewport.bl,cameraData.viewport.br,cameraData.viewport.b);

		// Midpoint - Cramer's Rule
		float a1 = r.y-l.y;
		float b1 = l.x - r.x;
		float c1 = a1 * l.x + b1 * l.y;
		float a2 = t.y-b.y;
		float b2 = b.x - t.x;
		float c2 = a2 * b.x + b2 * b.y;

		float d = a1 * b2 - a2 * b1;

		if (d != 0)
			m = new Vector2 (((b2 * c1) - (b1 * c2)) / d, ((a1 * c2) - (a2 * c1)) / d);
		else
			m = Vector2.Lerp (Vector2.Lerp (t, b, 0.5f), Vector2.Lerp (l, r, 0.5f), 0.5f);

		// Generate Vertex List
		Vector2 yp0, yp1, yp2, xp0;
		float xr, yr;
		List<Vector3> vertices = new List<Vector3> ();
		for (int y = 0; y <= (2 * vertexResolution); y++) {
			if (y <= vertexResolution) {
				yr = (float) y  / vertexResolution;
				yp0 = Vector2.Lerp (cameraData.viewport.bl, l, yr);
				yp1 = Vector2.Lerp (b, m, yr);
				yp2 = Vector2.Lerp (cameraData.viewport.br, r, yr);
			} else {
				yr = (float)(y - vertexResolution)  / vertexResolution;
				yp0 = Vector2.Lerp (l, cameraData.viewport.tl, yr);
				yp1 = Vector2.Lerp (m, t, yr);
				yp2 = Vector2.Lerp (r, cameraData.viewport.tr, yr);
			}
			for (int x = 0; x <= (2 * vertexResolution); x++) {
				if (x <= vertexResolution) {
					xr = (float)x / vertexResolution;
					xp0 = Vector2.Lerp (yp0, yp1, xr);
				} else {
					xr = (float)(x - vertexResolution) / vertexResolution;
					xp0 = Vector2.Lerp (yp1, yp2, xr);
				}
				vertices.Add (new Vector2(xp0.x, xp0.y));
			}
		}

		// Generate UV List
		float uvMin = 0.0f;
		float uvMax = 1.0f;
		float yRelUv, xRelUv;

		List<Vector2> uvs = new List<Vector2>();
		for (int y = 0; y <= (2 * vertexResolution); y++) {
			for (int x = 0; x <= (2 * vertexResolution); x++) {
				yRelUv = Mathf.Lerp(uvMin, uvMax, (float) y / (2 * vertexResolution));
				xRelUv = Mathf.Lerp(uvMin, uvMax, (float) x / (2 * vertexResolution));

				Vector2 uv = new Vector2(xRelUv, yRelUv);
				uvs.Add(uv);				
			}
		}

		// Generate triangle list
		List<int> triangles = new List<int>();	
		int gridSize = (2 * vertexResolution) + 1;
		for (int y = 0; y < (2 * vertexResolution); y++) {	
			for (int x = 0; x < (2 * vertexResolution); x++) {
				triangles.Add(x + y * gridSize);
				triangles.Add(x + (y + 1) * gridSize);
				triangles.Add((x + y * gridSize) + 1);

				triangles.Add(x + (y + 1) * gridSize);
				triangles.Add((x + (y + 1) * gridSize) + 1);
				triangles.Add((x + y * gridSize) + 1);
			}
		}

		screenMesh = new Mesh();
		screenMesh.vertices = vertices.ToArray();
		screenMesh.uv = uvs.ToArray();
		screenMesh.triangles = triangles.ToArray();
		screenMesh.RecalculateNormals();
	}

	void OnPostRender() {
		if (monoMode == false) {			
			// Left Eye is on the left
			// Left Eye Mesh
			leftEyeMaterial.SetPass(0);
			Graphics.DrawMeshNow(
				screenMesh, 
				Matrix4x4.TRS(
					new Vector3(-0.5f, 0.0f, 0.0f), 
					Quaternion.identity, 
					new Vector3(0.5f, 1.0f, 1.0f)
				)
			);
			
			// Right Eye Mesh
			rightEyeMaterial.SetPass(0);
			Graphics.DrawMeshNow(
				screenMesh, 
				Matrix4x4.TRS(
					new Vector3(0.5f, 0.0f, 0.0f), 
					Quaternion.identity, 
					new Vector3(0.5f, 1.0f, 1.0f)
				)
			);
		} else {
			leftEyeMaterial.SetPass(0);
			Graphics.DrawMeshNow(
				screenMesh, 
				Matrix4x4.TRS(
					Vector3.zero, 
					Quaternion.identity, 
					Vector3.one
				)
			);			
		}
	}
}
