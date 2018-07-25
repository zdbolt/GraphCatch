using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraData : MonoBehaviour {		
	// Viewport Corners in Pixels
	[System.Serializable]
	public struct Viewport {
		public Vector2 tl;
		public Vector2 tr;
		public Vector2 bl;
		public Vector2 br;

		public float t;
		public float b;
		public float l;
		public float r;
	};

	public int display;
	public string screen;
	public Viewport viewport;
}