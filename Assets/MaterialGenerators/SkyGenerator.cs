using UnityEngine;
using System.Collections;

public class SkyGenerator : MonoBehaviour {

	public Material material;
	
	// Width and height of the texture in pixels.
	public int pixWidth;
	public int pixHeight;
	
	// The origin of the sampled area in the plane.
	public float xOrg;
	public float yOrg;
	
	public Color color1;
	public Color color2;
	
	private Texture2D noiseTex;
	private Color[] pix; 

	// Use this for initialization
	void Awake () {
		// Set up the texture and a Color array to hold pixels during processing.
		noiseTex = new Texture2D(pixWidth, pixHeight);
		pix = new Color[noiseTex.width * noiseTex.height];
		material.mainTexture = noiseTex;
		CalcTexture();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CalcTexture()
	{
		Color color = new Color ();

		// For each pixel in the texture...
		for (int y = 0; y < noiseTex.height; y++)
		{
			float gradient = (float)y / noiseTex.height;
			color.r = Mathf.Lerp(color1.r, color2.r, gradient);
			color.g = Mathf.Lerp(color1.g, color2.g, gradient);
			color.b = Mathf.Lerp(color1.b, color2.b, gradient);
			for (int x = 0; x < noiseTex.width; x++)
			{
				// Get a sample from the corresponding position in the noise plane
				// and create a greyscale pixel from it.
				float xCoord = xOrg + (float)x / noiseTex.width;
				float yCoord = yOrg + (float)y / noiseTex.height;
				pix[y * noiseTex.width + x] = color;
			}
		}
		
		// Copy the pixel data to the texture and load it into the GPU.
		noiseTex.SetPixels(pix);
		noiseTex.Apply();
	}
}
