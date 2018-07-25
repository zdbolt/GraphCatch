using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ImageKernel {
	public static double[,] Gauss = new double[,] {
		{ 0.003765, 0.015019, 0.023792, 0.015019, 0.003765},
		{ 0.015019, 0.059912, 0.094907, 0.059912, 0.015019},
		{ 0.023792, 0.094907, 0.150342, 0.094907, 0.023792},
		{ 0.015019, 0.059912, 0.094907, 0.059912, 0.015019},
		{ 0.003765, 0.015019, 0.023792, 0.015019, 0.003765}};
	public static double[,] SobelX = new double[,] {
		{ -1.0, 0.0, 1.0 },
		{ -2.0, 0.0, 2.0 }, 
		{ -1.0, 0.0, 1.0 }};
	public static double[,] SobelY = new double[,]{
		{  1.0,  2.0,  1.0 },
		{  0.0,  0.0,  0.0 }, 
		{ -1.0, -2.0, -1.0 }};

	public static Texture2D ConvolutionFilter (Texture2D src, double[,] kernel, double factor = 1, int bias = 0, bool grayscale = false) {
		Color32[] sourceData = src.GetPixels32 ();
		Color32[] resultBuffer = src.GetPixels32 ();

		if (grayscale) {
			float gray = 0f;
			for (int i = 0; i < sourceData.Length; ++i) {
				gray =  sourceData[i].r * .3f;
				gray += sourceData[i].g * .59f;
				gray += sourceData[i].b * .11f;

				sourceData[i] = new Color32 ((byte)gray, (byte)gray, (byte)gray, 255);
			}
		}

		double red, green, blue;
		red = green = blue = 0.0;

		int kernelWidth = kernel.GetLength (1);

		int kernelOffset = (kernelWidth - 1) / 2;
		int arrayOffset = 0;
		int calcOffset = 0;

		for (int y = 0; y < src.height; ++y) {
			for (int x = 0; x < src.width; ++x) {
				blue = red = green = 0.0;
				arrayOffset = (y * src.width) + x;

				for (int ky = -kernelOffset; ky <= kernelOffset; ++ky) {
					for (int kx = -kernelOffset; kx <= kernelOffset; ++kx) {

						calcOffset = arrayOffset + kx + (ky * src.width);

						if (ky + y >= 0 && ky + y < src.height) {
							if (kx + x >= 0 && kx + x < src.width) {
								red     += (double)(sourceData [calcOffset].r) * kernel [ky + kernelOffset, kx + kernelOffset];
								green   += (double)(sourceData [calcOffset].g) * kernel [ky + kernelOffset, kx + kernelOffset];
								blue    += (double)(sourceData [calcOffset].b) * kernel [ky + kernelOffset, kx + kernelOffset];
							}
						}
					}
				}
				red = factor * red + bias;
				green = factor * green + bias;
				blue = factor * blue + bias;

				if (red > 255)
					red = 255;
				else if (red < 0)
					red = 0;

				if (green > 255)
					green = 255;
				else if (green < 0)
					green = 0;

				if (blue > 255)
					blue = 255;
				else if (blue < 0)
					blue = 0;

				resultBuffer [arrayOffset].r = (byte) red;
				resultBuffer [arrayOffset].g = (byte) green;
				resultBuffer [arrayOffset].b = (byte) blue;
				resultBuffer [arrayOffset].a = 255;
			}
		}

		Texture2D resultImage = new Texture2D (src.width, src.height);
		resultImage.SetPixels32 (resultBuffer);
		return resultImage;
	}

	public static Texture2D ConvolutionFilter (Texture2D src, double[,] xKernel, double[,] yKernel, double factor = 1, int bias = 0, bool grayscale = false) {
		Color32[] sourceData = src.GetPixels32 ();
		Color32[] resultBuffer = src.GetPixels32 ();

		if (grayscale) {
			float gray = 0f;
			for (int i = 0; i < sourceData.Length; ++i) {
				gray =  sourceData[i].r * .299f;
				gray += sourceData[i].g * .587f;
				gray += sourceData[i].b * .114f;

				sourceData[i] = new Color32 ((byte)gray, (byte)gray, (byte)gray, 255);
			}
		}

		double redX, redY, redTotal, greenX, greenY, greenTotal, blueX, blueY, blueTotal;
		redX = redY = redTotal = greenX = greenY = greenTotal = blueX = blueY = blueTotal = 0.0;;

		// KERNELS ARE IDENTICAL IN SIZE
		int kernelWidth = xKernel.GetLength (1);

		int kernelOffset = (kernelWidth - 1) / 2;
		int arrayOffset = 0;
		int calcOffset = 0;

		for (int y = 0; y < src.height; ++y) {
			for (int x = 0; x < src.width; ++x) {
				blueX = redX = greenX = 0.0;
				blueY = redY = greenY = 0.0;
				blueTotal = redTotal = greenTotal = 0.0;

				arrayOffset = (y * src.width) + x;

				for (int ky = -kernelOffset; ky <= kernelOffset; ++ky) {
					for (int kx = -kernelOffset; kx <= kernelOffset; ++kx) {

						calcOffset = arrayOffset + kx + (ky * src.width);

						if (ky + y >= 0 && ky + y < src.height) {
							if (kx + x >= 0 && kx + x < src.width) {
								redX     += (double)(sourceData [calcOffset].r) * xKernel [ky + kernelOffset, kx + kernelOffset];
								greenX   += (double)(sourceData [calcOffset].g) * xKernel [ky + kernelOffset, kx + kernelOffset];
								blueX    += (double)(sourceData [calcOffset].b) * xKernel [ky + kernelOffset, kx + kernelOffset];

								redY     += (double)(sourceData [calcOffset].r) * yKernel [ky + kernelOffset, kx + kernelOffset];
								greenY   += (double)(sourceData [calcOffset].g) * yKernel [ky + kernelOffset, kx + kernelOffset];
								blueY    += (double)(sourceData [calcOffset].b) * yKernel [ky + kernelOffset, kx + kernelOffset];
							}
						}
					}
				}
				redTotal 	= (double)Mathf.Sqrt ((float)((redX   * redX)   + (redY   * redY)));
				greenTotal 	= (double)Mathf.Sqrt ((float)((greenX * greenX) + (greenY * greenY)));
				blueTotal 	= (double)Mathf.Sqrt ((float)((blueX  * blueX)  + (blueY  * blueY)));

				redTotal	= factor * redTotal   + bias;
				greenTotal 	= factor * greenTotal + bias;
				blueTotal 	= factor * blueTotal  + bias;

				if (redTotal > 255)
					redTotal = 255;
				else if (redTotal < 0)
					redTotal = 0;

				if (greenTotal > 255)
					greenTotal = 255;
				else if (greenTotal < 0)
					greenTotal = 0;

				if (blueTotal > 255)
					blueTotal = 255;
				else if (blueTotal < 0)
					blueTotal = 0;

				resultBuffer [arrayOffset].r = (byte) redTotal;
				resultBuffer [arrayOffset].g = (byte) greenTotal;
				resultBuffer [arrayOffset].b = (byte) blueTotal;
				resultBuffer [arrayOffset].a = 255;
			}
		}

		Texture2D resultImage = new Texture2D (src.width, src.height);
		resultImage.SetPixels32 (resultBuffer);
		return resultImage;
	}
}

