using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMultiDisplay : MonoBehaviour {
	public bool enableDesktop = false;
	
	// Supports a maximum of 5 displays (4 cave walls + 1 desktop interface)
	// Display Order (0 floor), (1, left), (2, front), (3, right), (4, desktop)
	void Awake () {
		// Display.displays[0] is the primary, default display and is always ON.

		if (Display.displays.Length > 1)
			Display.displays[1].Activate();
		if (Display.displays.Length > 2)
			Display.displays[2].Activate();
		if (Display.displays.Length > 3)
			Display.displays[3].Activate();		
		
        // If Desktop is enabled and present, Activate it
		if (enableDesktop && Display.displays.Length > 4) 
			Display.displays[4].Activate();
	}
}
