using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PictureAnalysisScriptV2 : MonoBehaviour {
    /* TODO 
     * X - Find Edges
     *   - Find Corners
     *   - Find Screens from Corners
     *   - Unwarp to roughly 2D space
     *   - Align Corners
     *   - ...?
     *   - Profit
     */

    // Modified and Original are always the same size.
    public Texture2D original;
    private Texture2D modified;

    public float threshold, scaling;
    public bool stackChanges = false;


        
    // Use this for initialization
    void Start () {
        modified = new Texture2D (original.width, original.height);
        GetComponent<Renderer> ().material.mainTexture = modified;
        reset ();
    }
    
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp (KeyCode.I))
            ApplyFilter (new double[,]{ { -1.0 } }, 1.0, 255);
        if (Input.GetKeyUp (KeyCode.G))
            ApplyFilter (ImageKernel.Gauss);
        if (Input.GetKeyUp (KeyCode.Space))
            ApplyFilters (ImageKernel.SobelX, ImageKernel.SobelY, 1.0, 0, true);
        if (Input.GetKeyUp (KeyCode.C)) 
            ApplyFilter (new double[,] { { 1.0 } }, (double)scaling, (int)Mathf.Floor (-threshold * 255));
        if (Input.GetKeyUp (KeyCode.R))
            reset();
    }

        
/**** HELPER METHODS ******************************************************************************************************************************************/

    private void ApplyFilter (double[,] kernel, double factor = 1.0, int bias = 0, bool grayscale = false) {
        modified.SetPixels32 (ImageKernel.ConvolutionFilter ((stackChanges ? modified : original), kernel, factor, bias, grayscale).GetPixels32 ());
        modified.Apply ();
    }

    private void ApplyFilters (double[,] kernelX, double[,] kernelY, double factor = 1.0, int bias = 0, bool grayscale = false) {
        modified.SetPixels32 (ImageKernel.ConvolutionFilter ((stackChanges ? modified : original), kernelX, kernelY, factor, bias, grayscale).GetPixels32 ());
        modified.Apply ();
    }

    private Color ThresholdScaling(Color color, float threshold, float scaling) {
        return new Color (((color.r - threshold) * scaling), ((color.g - threshold) * scaling), ((color.b - threshold) * scaling), 1);
    }

    void reset() {
        modified.SetPixels32 (original.GetPixels32 ());
        modified.Apply ();
    }
}
