using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalibrationScreen : MonoBehaviour {
	public enum ScreenIndex :int { FLOOR = 0, LEFT, FRONT, RIGHT }
	public ScreenIndex index;


	// Clockwise from Top Left (TL)
	public enum Direction : int {TL = 0, T, TR, R, BR, B, BL, L}
	private Direction dir = Direction.TL;
	private Image[] controlPoints = new Image[8];
	private DisplayCamera displayCam;
	private bool active = false;
	private CalibrationScreen next, prev;
	private CalibrationScreen toSwap;
	private bool midSwap = false;
	private bool buffer = false;
	private bool[] AxisInUse = new bool[3] {false, false, false};

// ---------- Setup Methods ----------

	public void setup(DisplayCamera dc, CalibrationScreen nextScreen, CalibrationScreen prevScreen) {
		displayCam = dc;
		next = nextScreen;
		prev = prevScreen;

		index = (ScreenIndex)dc.cameraData.display;

		//Debug.Log (prev.ToString () + " > " + index.ToString () + " > " + next.ToString ());

		Image[] temp = gameObject.GetComponentInChildren<GridLayoutGroup> ().GetComponentsInChildren<Image> ();
		foreach (Image img in temp) {
			switch (img.gameObject.name) {
			case "TopLeftMarker":
				controlPoints [0] = img;
				break;
			case "TopMidMarker":
				controlPoints [1] = img;
				break;
			case "TopRightMarker":
				controlPoints [2] = img;
				break;
			case "MidLeftMarker":
				controlPoints [7] = img;
				break;
			case "MidRightMarker":
				controlPoints [3] = img;
				break;
			case "BotLeftMarker":
				controlPoints [6] = img;
				break;
			case "BotMidMarker":
				controlPoints [5] = img;
				break;
			case "BotRightMarker":
				controlPoints [4] = img;
				break;
			}
		}
	}

	public void setActive(Direction d, CalibrationScreen toSwap, bool midSwap) {
		buffer = false;
		active = true;
		dir = d;
		this.midSwap = midSwap;
		this.toSwap = toSwap;
		if (midSwap && this == toSwap)
			updateUI (dir, Color.white, Color.yellow);
		else
			updateUI (dir, Color.white, Color.black);
		//Debug.Log (prev.ToString () + " > " + index.ToString () + " > " + next.ToString ());
	}
		
// ---------- Update Loop ----------

	// Update is called once per frame
	void Update () {
		if (displayCam.cameraData.display != (int)index)
			updateDisplay ();
		
		if (!buffer) {
			buffer = true;
		} else if (active) {
			// Test for and resolve inputs

			// Directional Axes - Up, Down, Left, Right, and LStick
			if (Input.GetAxis ("Horizontal") != 0f)
				updateCornerPosition (0.005f * Input.GetAxis ("Horizontal"), 0.0f);
			if (Input.GetAxis ("Vertical") != 0f)
				updateCornerPosition (0.0f, 0.005f * Input.GetAxis ("Vertical"));


			// Fine Axes - WASD and DPad
			if (Input.GetAxisRaw ("FineVertical") != 0f) {
				if (!AxisInUse [0]) {
					AxisInUse [0] = true;
					if (Input.GetAxis ("FineVertical") > 0f)
						updateCornerPosition (0.0f, 0.001f);
					else
						updateCornerPosition (0.0f, -0.001f);
				}
			}
			if (Input.GetAxisRaw ("FineVertical") == 0f) {
				AxisInUse [0] = false;
			}
			
			if (Input.GetAxisRaw ("FineHorizontal") != 0f) {
				if (!AxisInUse [1]) {
					AxisInUse [1] = true;
					if (Input.GetAxis ("FineHorizontal") > 0f)
						updateCornerPosition (0.001f, 0f);
					else
						updateCornerPosition (-0.001f, 0f);
				}
			}
			if (Input.GetAxisRaw ("FineHorizontal") == 0f) {
				AxisInUse [1] = false;
			}

			// Screen Toggles - QE and Left/Right Bumpers
			if (Input.GetAxisRaw ("ScreenToggle") != 0f) {
				if (!AxisInUse [2]) {
					AxisInUse [2] = true;
					if (Input.GetAxis ("ScreenToggle") > 0f)
						switchFocus (next);
					else
						switchFocus (prev);
				}
			}
			if (Input.GetAxisRaw ("ScreenToggle") == 0f) {
				AxisInUse [2] = false;
			}

			// Control Toggle - Shift and X Button
			if (Input.GetButtonUp ("ControlToggle")) {
				dir = (Direction)((((int)dir) + 1) % 8);
				if (midSwap && toSwap == this)
					updateUI (dir, Color.white, Color.yellow);
				else
					updateUI (dir, Color.white, Color.black);
			}

			// Swap Toggle - Space and Y Button
			if (Input.GetButtonUp ("SwapToggle"))
				swap ();
			
		}
	}

// ---------- Main Methods ----------

	private void updateCornerPosition (float dx, float dy) {
		CameraData.Viewport newView = displayCam.cameraData.viewport;

		switch (dir) {
		case Direction.T:
			newView.t = enforceBounds (newView.t, dx, 1, 0);
			break;
		case Direction.B:
			newView.b = enforceBounds (newView.b, dx, 1, 0);
			break;
		case Direction.L:
			newView.l = enforceBounds (newView.l, dy, 1, 0);
			break;
		case Direction.R:
			newView.r = enforceBounds (newView.r, dy, 1, 0);
			break;

		case Direction.TL:
			newView.tl = new Vector2(enforceBounds(newView.tl.x, dx), enforceBounds(newView.tl.y, dy));
			break;
		case Direction.TR:
			newView.tr = new Vector2(enforceBounds(newView.tr.x, dx), enforceBounds(newView.tr.y, dy));
			break;
		case Direction.BL:
			newView.bl = new Vector2(enforceBounds(newView.bl.x, dx), enforceBounds(newView.bl.y, dy));
			break;
		case Direction.BR:
			newView.br = new Vector2(enforceBounds(newView.br.x, dx), enforceBounds(newView.br.y, dy));
			break;
		}
		displayCam.updateViewport (newView);
	}

	private void swap () {
		if (midSwap) {
			if (this != toSwap) {
				// Perform Swap
				ScreenIndex temp = this.index;
				this.index = toSwap.index;
				toSwap.index = temp;
				toSwap.updateUI (dir, Color.black, Color.black);
			} else
				updateUI (dir, Color.white, Color.black);
			midSwap = false;
		} else {
			// Store Info for Swap
			toSwap = this;
			midSwap = true;
			updateUI (dir, Color.white, Color.yellow);
		}
	}

// ---------- Helper Methods ----------
	private void switchFocus (CalibrationScreen toFocus) {
		this.active = false;
		if (midSwap && this == toSwap)
			updateUI (dir, Color.yellow, Color.yellow);
		else 
			updateUI (dir, Color.black, Color.black);
		toFocus.setActive (dir, toSwap, midSwap);
		midSwap = false;
	}

	private float enforceBounds (float n, float dn, float max = 1.0f, float min = -1.0f) {
		if (dn > 0)
			return n + dn > max ? max : n + dn;
		else
			return n + dn < min ? min : n + dn;
	}

	public void updateDisplay() {
		//Debug.Log(this.ToString() + ": " + index.ToString());
		displayCam.updateDisplay ((int)index);
	}

	public void updateUI (Direction d, Color color, Color altColor) {
		for (int i = 0; i < controlPoints.Length; ++i) {
			if (i == (int)d)
				controlPoints [i].color = color;
			else
				controlPoints [i].color = altColor;
		}
	}
}
