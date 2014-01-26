using UnityEngine;
using System.Collections;

public class GroundLayersFilter : MonoBehaviour
{
	public Material material;
	
	[System.Serializable]
	public class Layer
	{
		public float normalizedY;
		public Color color;
		public float ratio;
		public float noise;
		public int[] edge;
	}
	public Layer[] layers;

	public float noiseStep = 0.01f;
	public float stableNoiseRatio = 0.3f;

	private Color[] pix; 
	
	// Use this for initialization
	void Start()
	{
		pix = new Color[material.mainTexture.width * material.mainTexture.height];

		// Set up the texture and a Color array to hold pixels during processing.
		CalcLayers();
	}
	
	// Update is called once per frame
	void Update()
	{
		//CalcNoise(Time.time * baseScale);
		//material.mainTexture = noiseTex;
	}
	
	void CalcLayers()
	{
		Texture2D texture = (Texture2D)material.mainTexture;

		for (int layerIndex = 0; layerIndex < layers.Length; ++layerIndex)
		{
			layers[layerIndex].edge = new int[texture.width];
			float yOffset = 0.0f;
			for(int x = 0; x < texture.width; ++x)
			{
				float rand = Random.value * layers[layerIndex].noise;
				if (Mathf.Abs(rand - yOffset) > stableNoiseRatio * layers[layerIndex].noise)
				{
					yOffset += Mathf.Sign(rand - yOffset) * noiseStep;
				}
				layers[layerIndex].edge[x] = Mathf.RoundToInt(texture.height * (layers[layerIndex].normalizedY + yOffset));
			}
		}

		// For each pixel in the texture...
		for (int y = 0; y < texture.height; y++)
		{
			for (int x = 0; x < texture.width; x++)
			{
				pix[y * texture.width + x] = texture.GetPixel(x,y);

				for (int layerIndex = 0; layerIndex < layers.Length; ++layerIndex)
				{
					if (y <= layers[layerIndex].edge[x])
					{
						pix[y * texture.width + x] = Color.Lerp(pix[y * texture.width + x], layers[layerIndex].color, layers[layerIndex].ratio);
						break;
					}
				}
			}
		}
		
		// Copy the pixel data to the texture and load it into the GPU.
		texture.SetPixels(pix);
		texture.Apply();
	}
}