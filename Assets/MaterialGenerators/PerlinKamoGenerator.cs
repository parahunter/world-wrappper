using UnityEngine;
using System.Collections;

public class PerlinKamoGenerator : MonoBehaviour
{
	public Material material;

	// Width and height of the texture in pixels.
	public int pixWidth;
	public int pixHeight;
	
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

	[System.Serializable]
	public class Layer
	{
		public float normalizedY;
		public float scale;
		public Color color;
		public float ratio;
		public float noise;
		public float xOff;
		public float yOff;

		[HideInInspector]
		public int[] start;
		[HideInInspector]
		public int[] end;
	}
	public Layer[] layers;

	public float layerNoiseStep = 0.01f;
	public float layerStableNoiseRatio = 0.3f;

	private Texture2D noiseTex;
	private Color[] pix; 

	// Use this for initialization
	void Start()
	{
		// Set up the texture and a Color array to hold pixels during processing.
		noiseTex = new Texture2D(pixWidth, pixHeight);
		pix = new Color[noiseTex.width * noiseTex.height];
		CalcLayers();
		CalcNoise();
		material.mainTexture = noiseTex;
	}
	
	// Update is called once per frame
	void Update()
	{
		//CalcNoise(Time.time * baseScale);
		//material.mainTexture = noiseTex;
	}

	void CalcLayers()
	{
		for (int layerIndex = 0; layerIndex < layers.Length; ++layerIndex)
		{
			layers[layerIndex].start = new int[noiseTex.width];
			layers[layerIndex].end = new int[noiseTex.width];
			float yOffset = layers[layerIndex].noise * 0.5f;
			for(int x = 0; x < noiseTex.width; ++x)
			{
				float rand = Random.value * layers[layerIndex].noise;
				if (Mathf.Abs(rand - yOffset) > layerStableNoiseRatio * layers[layerIndex].noise)
				{
					yOffset += Mathf.Sign(rand - yOffset) * layerNoiseStep;
				}
				if (layerIndex > 0)
					layers[layerIndex].start[x] = layers[layerIndex-1].end[x] + 1;
				else
					layers[layerIndex].start[x] = 0;
				layers[layerIndex].end[x] = Mathf.Clamp(Mathf.RoundToInt(noiseTex.height * (layers[layerIndex].normalizedY + yOffset)), 0, noiseTex.height);
			}
		}
	}

	void CalcNoise()
	{
		for (int layerIndex = 0; layerIndex < layers.Length; ++layerIndex)
		{
			CalcNoise(layers[layerIndex]);
		}
	}

	void CalcNoise(Layer layer)
	{
		// For each pixel in the texture...
		for (int x = 0; x < noiseTex.width; x++)
		{
			for (int y = layer.start[x]; y < layer.end[x]; y++)
			{
				// Get a sample from the corresponding position in the noise plane
				// and create a greyscale pixel from it.
				float xCoord = layer.xOff + Mathf.Abs((float)x / (noiseTex.width / 2) - 1.0f) * layer.scale;
				float yCoord = layer.yOff + (float)y / noiseTex.height * layer.scale;
				float sample = Mathf.PerlinNoise(xCoord, yCoord);

				for(int i = 0; i < colors.Length; ++i)
				{
					if (sample < colors[i].threshold)
					{
						//pix[y * noiseTex.width + x] = colors[i].color;
						pix[y * noiseTex.width + x] = Color.Lerp(colors[i].color, layer.color, layer.ratio);
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
