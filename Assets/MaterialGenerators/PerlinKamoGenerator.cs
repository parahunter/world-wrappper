using UnityEngine;
using System.Collections;

public class PerlinKamoGenerator : MonoBehaviour
{
	public Material material;

	// Width and height of the texture in pixels.
	public int pixWidth;
	public int pixHeight;
	
	// The origin of the sampled area in the plane.
	public float xOrg;
	public float yOrg;
	
	// The number of cycles of the basic noise pattern that are repeated
	// over the width and height of the texture.
	public float baseScale = 1.0f;

	[System.Serializable]
	public class ThresholdColor
	{
		public float threshold;
		public Color color;
	}
	public ThresholdColor[] colors;

	private Texture2D noiseTex;
	private Color[] pix; 

	// Use this for initialization
	void Start()
	{
		// Set up the texture and a Color array to hold pixels during processing.
		noiseTex = new Texture2D(pixWidth, pixHeight);
		pix = new Color[noiseTex.width * noiseTex.height];
		CalcNoise(baseScale);
		material.mainTexture = noiseTex;
	}
	
	// Update is called once per frame
	void Update()
	{
		//CalcNoise(Time.time * baseScale);
		//material.mainTexture = noiseTex;
	}

	void CalcNoise(float scale)
	{
		// For each pixel in the texture...
		for (int y = 0; y < noiseTex.height; y++)
		{
			for (int x = 0; x < noiseTex.width; x++)
			{
				// Get a sample from the corresponding position in the noise plane
				// and create a greyscale pixel from it.
				float xCoord = xOrg + (float)x / noiseTex.width * scale;
				float yCoord = yOrg + (float)y / noiseTex.height * scale;
				float sample = Mathf.PerlinNoise(xCoord, yCoord);

				for(int i = 0; i < colors.Length; ++i)
				{
					if (sample < colors[i].threshold)
					{
						pix[y * noiseTex.width + x] = colors[i].color;
						break;
					}
				}
			}
		}
		
		// Copy the pixel data to the texture and load it into the GPU.
		noiseTex.SetPixels(pix);
		noiseTex.Apply();
	}
}
